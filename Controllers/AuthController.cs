using AutoMapper;
using Azure;
using CodePulse.API.Models.DTO;
using CodePulse.API.Models.DTO.Request;
using CodePulse.API.Models.DTO.Response;
using CodePulse.API.Repositories.Interface;
using CodePulse.API.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Net.WebSockets;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository, IMapper mapper, IEmailSender emailSender)
        {
            this.userManager = userManager;
            _tokenRepository = tokenRepository;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        // POST: {apibaseurl}/api/auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            if (request == null || !ModelState.IsValid)
            {
                return ReturnResponse(null!, "Please fill your email and password!", null, HttpStatusCode.BadRequest);
            }

            // Create Identity User object
            var user = _mapper.Map<IdentityUser>(request);

            var identityResult = await userManager.CreateAsync(user, request.Password!);

            var data = new BaseResponse();
            if (identityResult.Succeeded)
            {
                // Add Role to user (Reader)
                identityResult = await userManager.AddToRoleAsync(user, "Reader");
                if (identityResult.Succeeded)
                {
                    data = new BaseResponse
                    {
                        Data = request.Email,
                        Errors = null,
                        Message = "Registration Successfully!",
                        Status = HttpStatusCode.OK
                    };
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        List<IdentityError> errorList = identityResult.Errors.ToList();
                        var errors = string.Join(", ", identityResult.Errors.ToList().Select(e => e.Description));

                        data = new BaseResponse
                        {
                            Data = request.Email,
                            Errors = errors,
                            Message = "Registration Failed!",
                            Status = HttpStatusCode.OK
                        };
                    };
                }
            }
            else
            {
                List<IdentityError> errorList = identityResult.Errors.ToList();
                var errors = string.Join(", ", identityResult.Errors.ToList().Select(e => e.Description));


                if (identityResult.Errors.Any())
                {
                    data = new BaseResponse
                    {
                        Data = request.Email,
                        Errors = errors,
                        Message = "Registration Failed!",
                        Status = HttpStatusCode.BadRequest
                    };
                }
            }
            return ReturnResponse(data.Data!, data.Message!, data.Errors, data.Status);
        }

        // POST: {apibaseurl}/api/auth/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            // Check Email
            var identityUser = await userManager.FindByEmailAsync(request.Email);

            if (identityUser is not null)
            {
                // Check Password
                var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser, request.Password);

                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(identityUser);
                    // Create a Token and Response

                    var token = _tokenRepository.CreateJwtToken(identityUser, roles);
                    var response = new LoginResponseDto()
                    {
                        Email = request.Email,
                        Roles = roles.ToList(),
                        Token = token
                    };

                    await userManager.ResetAccessFailedCountAsync(identityUser);

                    return Ok(new BaseResponse
                    {
                        Data = response,
                        Errors = "",
                        Message = "Login Successfully!",
                        Status = HttpStatusCode.OK
                    });
                }
                else
                {
                    await userManager.AccessFailedAsync(identityUser);

                    if (await userManager.IsLockedOutAsync(identityUser))
                    {
                        var content = $"Your account is locked out. To reset the password click this link: {request.ClientURI}";
                        var message = new Message(new string[] { request.Email },
                            "Locked out account information", content, null);

                        // await _emailSender.SendEmailAsync(message);

                        return Unauthorized(new BaseResponse
                        {
                            Data = "",
                            Errors = "",
                            Message = "The account is locked out",
                            Status = HttpStatusCode.OK
                        });
                    }


                }
            }
            ModelState.AddModelError("", "Email or Password Incorrect");

            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (forgotPasswordDto == null)
            {
                return BadRequest(ModelState);
            }
            var user = await userManager.FindByEmailAsync(forgotPasswordDto.Email);

            if (user is null)
            {
                return BadRequest("Invalid Email");
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var param = new Dictionary<string, string?>
            {
                {"token", token },
                {"email", forgotPasswordDto.Email }
            };

            var callback = QueryHelpers.AddQueryString(forgotPasswordDto.ClientURI, param);
            var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);
            await _emailSender.SendEmailAsync(message);

            return Ok();
        }
    }
}
