using CCTV.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CCTV.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class DashboardController : Controller
    {
        AppDbContext _context;

        public DashboardController(AppDbContext  context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var members = _context.Members.ToList();
            return View(members);
        }
    }
}
