using BELibrary.Core.Entity;
using BELibrary.DbContext;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace CinemaOnline.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index(int? page)
        {
            using (var workScope = new UnitOfWork(new CinemaOnlineDbContext()))
            {
                var listData = workScope.News.GetAll().ToList();

                var pageNumber = (page ?? 1);
                const int pageSize = 9;
                return View(listData.ToPagedList(pageNumber, pageSize));
            }
        }

        public ActionResult Detail(int id)
        {
            using (var workScope = new UnitOfWork(new CinemaOnlineDbContext()))
            {
                var news = workScope.News.Get(id);

                const int pageSize = 9;
                return View(news);
            }
        }
    }
}