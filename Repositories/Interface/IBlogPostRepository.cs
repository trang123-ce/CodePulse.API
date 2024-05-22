using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAll();
        Task<BlogPost?> GetById(Guid id);
        void Update(BlogPost post);
        void Delete(BlogPost post);
        Task SaveAsync();
    }
}
