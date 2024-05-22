using CodePulse.API.Models.Domain;

namespace CodePulse.API.Services.Interface
{
    public interface IBlogPostService
    {
        Task<BlogPost> UpdateBlogPost(BlogPost blogPost);
        Task<BlogPost> DeleteBlogPost(BlogPost blogPost);
    }
}
