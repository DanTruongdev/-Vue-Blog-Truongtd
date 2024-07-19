using BlogOnline.Base;
using BlogOnline.Extentions;
using BlogOnline.Models.DTOs.Requests;
using BlogOnline.Models.Entities;
using System.Linq.Expressions;

namespace BlogOnline.Services.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBaseRepository<Blog> _blogRepository;

        public BlogService(IBaseRepository<Blog> blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<Blog> AddBlogAsync(BlogDto form)
        {
            var newBlog = new Blog()
            {
                Title = form.Title,
                ShortDescription = form.ShortDescription,
                Content = form.Content,
                Image = FileHelper.UploadFile(form.Image),
                LocationId = form.LocationId,
                IsPublic = form.IsPublic,
                CategoryId = form.CategoryId,
                PublictDate = form.PublictDate.Value,
            };
            try
            {
                var addResult = await _blogRepository.AddAsync(newBlog);
                return addResult;
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Blog>> GetAllBlogsAsync()
        {
            var blogs = await _blogRepository.GetAllAsync();
            if (!blogs.Any()) return new List<Blog>();
            return blogs;
        }

        public async Task<Blog> GetBlogByIdAsync(Guid id)
        {
            Blog blog = await _blogRepository.GetByIdAsync(id);
            return blog;
        }

        public async Task<bool> RemoveBlogAsync(Guid id)
        {

            try
            {
                var deleteBlog = await _blogRepository.GetByIdAsync(id);
                if (deleteBlog == null) return false;
                var deleteResult = await _blogRepository.RemoveAsync(deleteBlog);
                FileHelper.RemoveFile(deleteBlog.Image);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Blog>> SearchBlogAsync(string searchString)
        {
            var searchResult = await _blogRepository.SearchAsync(b => b.Title.Contains(searchString));
            if (!searchResult.Any()) return new List<Blog>();
            return searchResult;
        }

        public async Task<bool> UpdateBlogAsync(BlogDto form)
        {
            
            try
            {
                var blogExisting = await _blogRepository.GetByIdAsync(form.Id.Value);
                if (blogExisting == null) return false;
                if (form.Image != null) FileHelper.RemoveFile(blogExisting.Image);
                blogExisting.Title = form.Title;
                blogExisting.ShortDescription = form.ShortDescription;
                blogExisting.Content = form.Content;
                blogExisting.Image = FileHelper.UploadFile(form.Image);
                blogExisting.LocationId = form.LocationId;
                blogExisting.IsPublic = form.IsPublic;
                blogExisting.CategoryId = form.CategoryId;
                blogExisting.PublictDate = form.PublictDate.HasValue ? form.PublictDate.Value : DateTime.Now;
                var updateResult = await _blogRepository.UpdateAsync(blogExisting);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
