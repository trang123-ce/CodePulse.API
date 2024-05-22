using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayment()
        {
            var result = await _paymentRepository.GetAllPayment();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Payment(CreatePayment request)
        {
            var model = _mapper.Map<Payment>(request);

            var result = await _paymentRepository.AddPayment(model);
            return Ok(result);
        }
    }
}
