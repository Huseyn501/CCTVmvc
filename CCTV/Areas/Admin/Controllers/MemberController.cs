using CCTV.DAL;
using CCTV.Helpers.Extentions;
using CCTV.Models;
using CCTV.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CCTV.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MemberController : Controller
    {
        AppDbContext _context { get; set; }

        public IWebHostEnvironment Environment { get; }

        public MemberController(AppDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            Environment = environment;
        }
        public IActionResult Index()
        {
            var members = _context.Members.ToList();
            return View(members);
        }
        public IActionResult Create()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MemberVm memberVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Members member = new Members()
            {
                Name = memberVm.Name,
                Designation = memberVm.Designation,
                ImageUrl = memberVm.file.CreatingFile(Environment.WebRootPath, "Upload")
            };
            await _context.Members.AddAsync(member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _context.Members.FirstOrDefaultAsync(x => x.Id == id);
            if(member == null)
            {
                return NotFound();
            }
            if (member.ImageUrl != null)
            {
                member.ImageUrl.DeleteFile(Environment.WebRootPath, "Upload");
            }
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var member = await _context.Members.FirstOrDefaultAsync(x => x.Id == id);
            if(member==null)
            {
                return NotFound();
            }
            MemberVm memberVm = new MemberVm()
            {
                Name = member.Name,
                Designation = member.Designation,
            };
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,MemberVm memberVm)
        {
            var member = await _context.Members.FirstOrDefaultAsync(x=>x.Id == id);
            if (member == null)
            {
                return NotFound();
            }
            if(!ModelState.IsValid)
            {
                return View();
            }
            if(memberVm == null)
            {
                return NotFound();
            }
            if(memberVm.file != null)
            {
                if(member.ImageUrl != null)
                {
                    member.ImageUrl.DeleteFile(Environment.WebRootPath, "Upload");
                }
            }
            member.Name = memberVm.Name;
            member.Designation = memberVm.Designation;
            member.ImageUrl = memberVm.file.CreatingFile(Environment.WebRootPath, "Upload");
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
