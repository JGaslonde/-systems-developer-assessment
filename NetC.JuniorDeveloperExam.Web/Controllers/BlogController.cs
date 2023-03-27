using NetC.JuniorDeveloperExam.Web.Interfaces;
using NetC.JuniorDeveloperExam.Web.Models;
using NetC.JuniorDeveloperExam.Web.ViewModels;
using System.Web.Mvc;

namespace NetC.JuniorDeveloperExam.Web.Controllers
{
    public class BlogController : Controller
    {
        private ICacheService _cacheService;
        private IBlogService _blogService;
        

        public BlogController(ICacheService cacheService, IBlogService blogService)
        {
            _cacheService = cacheService;
            _blogService = blogService;
        }
        [HttpGet]
        public ActionResult Blog(int id)
        {
            var blog = _cacheService.GetOrSet<Blog>($"Blog{id}", () => _blogService.Get(id));

            if (blog == null)
                return View("Error");

            return View(new BlogViewModel { Blog = blog, NewComment = new Comment()});
        }

        [HttpPost]
        public ActionResult Comment(BlogViewModel model)
        {
            var blog = _blogService.SaveComment(model.NewComment, model.Blog.Id, model.ParentId);
            if (blog != null)
            {
                _cacheService.Update<Blog>($"Blog{model.Blog.Id}", blog);
                return RedirectToAction("Blog", new { id = model.Blog.Id });
            }
                
            return View("Error");
        }
    }
}