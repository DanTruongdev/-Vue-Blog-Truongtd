using BlogOnline.Models.Entities;

namespace BlogOnline.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetAllCategoryAsync();
    }
}
