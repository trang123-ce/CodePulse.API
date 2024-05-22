using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using CodePulse.API.Services.Interface;

namespace CodePulse.API.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostService(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public Task<BlogPost> DeleteBlogPost(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost> UpdateBlogPost(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
