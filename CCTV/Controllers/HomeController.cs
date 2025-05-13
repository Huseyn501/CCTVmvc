using CCTV.DAL;
using Microsoft.AspNetCore.Mvc;

namespace CCTV.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _dbcontext { get; set; }
        public HomeController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public IActionResult Index()
        {
            var members = _dbcontext.Members.ToList();
            return View(members);
        }
    }
}
