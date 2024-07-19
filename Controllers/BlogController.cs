using BlogOnline.Models.DTOs.Requests;
using BlogOnline.Models.Entities;
using BlogOnline.Services;
using BlogOnline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogOnline.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ILocationService _locationService;
        private readonly ICategoryService _categoryService;

        public BlogController(IBlogService blogService, ILocationService locationService, ICategoryService categoryService)
        {
            _blogService = blogService;
            _locationService = locationService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var blogList = await _blogService.GetAllBlogsAsync();
            return View(blogList);
        }

        [HttpGet]
        public async Task<IActionResult> CreateBlog()
        {
            var locationList = await _locationService.GetAllLocationAsync();
            var categoryList = await _categoryService.GetAllCategoryAsync();
            ViewBag.LocationList = locationList;
            ViewBag.CategoryList = categoryList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBlog(BlogDto form)
        {
            var locationList = await _locationService.GetAllLocationAsync();
            var categoryList = await _categoryService.GetAllCategoryAsync();
            ViewBag.LocationList = locationList;
            ViewBag.CategoryList = categoryList;
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Failed to add new blog";
                return View(form);
            }
            var result = await _blogService.AddBlogAsync(form);
            ViewBag.Message = "Add new blog successfully";
            return View();
        }

        public async Task<IActionResult> RemoveBlog(Guid id)
        {
            var result = await _blogService.RemoveBlogAsync(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> UpdateBlog(Guid id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);
            if (blog == null) return RedirectToAction(nameof(Index));
            var locationList = await _locationService.GetAllLocationAsync();
            var categoryList = await _categoryService.GetAllCategoryAsync();
            ViewBag.LocationList = locationList;
            ViewBag.CategoryList = categoryList;
            ViewBag.Blog = blog;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateBlog(BlogDto form)
        {

            var blog = await _blogService.GetBlogByIdAsync(form.Id.Value);
            var locationList = await _locationService.GetAllLocationAsync();
            var categoryList = await _categoryService.GetAllCategoryAsync();
            ViewBag.LocationList = locationList;
            ViewBag.CategoryList = categoryList;
            ViewBag.Blog = blog;
            if (ModelState.IsValid )
            {
                var updateResult = await _blogService.UpdateBlogAsync(form);
                if (updateResult)
                {
                    ViewBag.Message = "Update blog successfully";
                    return View();
                }
            }
            ViewBag.Message = "Failed to update blog";
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> SearchBlog()
        {
            
            return View(new List<Blog>());
        }

        [HttpPost]
        public async Task<IActionResult> SearchBlog(string? searchString)
        {
            if (searchString == null) return View(new List<Blog>());
            var searchResult = await _blogService.SearchBlogAsync(searchString);
            ViewBag.SearchString = searchString;
            return View(searchResult);
        }
    }
}
