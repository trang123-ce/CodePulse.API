using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _context.BlogPosts.AddAsync(blogPost);
            return blogPost;
        }
        public async Task<IEnumerable<BlogPost>> GetAll()
        {
            return await _context.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost?> GetById(Guid id)
        {
            return await _context.BlogPosts.AsNoTracking().FirstOrDefaultAsync(_ => _.Id ==  id);
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
         
        public void Update(BlogPost post)
        {
             _context.Set<BlogPost>().Update(post);
        }

        public void Delete(BlogPost post)
        {
             _context.BlogPosts.Remove(post);
        }
    }
}
