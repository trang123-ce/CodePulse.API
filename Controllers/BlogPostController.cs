using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Models.DTO.Request;
using CodePulse.API.Repositories.Interface;
using CodePulse.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public BlogPostController(IBlogPostService blogPostService, IBlogPostRepository blogPostRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _blogPostService = blogPostService;
            _blogPostRepository = blogPostRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        // Post: https://localhost:7292/api/BlogPost/BlogPost
        [HttpPost]
        public async Task<IActionResult> BlogPost([FromBody] CreateBlogPostRequestDto requestDto)
        {
            // convert from DTO to domain
            var blogPost = new BlogPost()
            {
                Author = requestDto.Author,
                Content = requestDto.Content,
                FeaturedImageUrl = requestDto.FeaturedImageUrl,
                IsVisible = requestDto.IsVisible,
                PublishedDate = requestDto.PublishedDate,
                ShortDescription = requestDto.ShortDescription,
                UrlHandle = requestDto.UrlHandle,
                Title = requestDto.Title,
                Categories = new List<Category>()
            };

            foreach (var item in requestDto.Categories)
            {
                var existingCategories = await _categoryRepository.GetById(item);
                if (existingCategories is not null)
                {
                    blogPost.Categories.Add(existingCategories);
                }
            }
            blogPost = await _blogPostRepository.CreateAsync(blogPost);

            await _blogPostRepository.SaveAsync();

            // convert from model to DTO
            var responce = new BlogPostDto()
            {
                Id = blogPost.Id,
                Content = blogPost.Content,
                Author = blogPost.Author,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                UrlHandle = blogPost.UrlHandle,
                Title = blogPost.Title,
                Categories = blogPost.Categories.Select(x => new CategoryDto { 
                    Id = x.Id, 
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(responce);
        }

        // Get: https://localhost:7292/api/BlogPost/BlogPost
        [HttpGet]
        public async Task<IActionResult> BlogPost()
        {
            var result = await _blogPostRepository.GetAll();

            // map model to the DTO
            var response = _mapper.Map<IEnumerable<BlogPost>, IEnumerable<BlogPostDto>>(result);

            return Ok(response);
        }

        // Get: https://localhost:7292/api/BlogPost/BlogPost/id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> BlogPost(Guid id)
        {
            var result = await _blogPostRepository.GetById(id);

            // map model to the DTO

            if (result == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<BlogPost, BlogPostDto>(result);

            return Ok(response);
        }

        // Put: https://localhost:7292/api/BlogPost/BlogPost/id


        // Delete: https://localhost:7292/api/BlogPost/BlogPost/id

    }
}
