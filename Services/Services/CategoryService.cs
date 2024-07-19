using BlogOnline.Base;
using BlogOnline.Models.Entities;
using BlogOnline.Services.Interfaces;

namespace BlogOnline.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category> _categoryRepository;

        public CategoryService(IBaseRepository<Category> blogRepository)
        {
            _categoryRepository = blogRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            var categoryList = await _categoryRepository.GetAllAsync();
            if (!categoryList.Any()) return new List<Category>();
            return categoryList;
        }
    }
}
