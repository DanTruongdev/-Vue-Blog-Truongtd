using BlogOnline.Models.DTOs.Requests;
using BlogOnline.Models.Entities;
using System.Linq.Expressions;

namespace BlogOnline.Services
{
    public interface IBlogService
    {
        public Task<Blog> GetBlogByIdAsync(Guid id);
     
        public Task<IEnumerable<Blog>> GetAllBlogsAsync();
        public Task<Blog> AddBlogAsync(BlogDto form);
        public Task<bool> UpdateBlogAsync(BlogDto form);
        public Task<bool> RemoveBlogAsync(Guid id);
        public Task<IEnumerable<Blog>> SearchBlogAsync(string searchString);
    }
}
