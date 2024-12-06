using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Areas.manage.Controllers
{
    public class PayrollController(AppDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Payroll payroll)
        {


            return View();
        }


    }
}
