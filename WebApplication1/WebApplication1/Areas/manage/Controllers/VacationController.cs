using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;

namespace WebApplication1.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize("SuperAdmin")]
    public class VacationController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public VacationController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var vacations = await _appDbContext.VacationRequests.Include("Employee").ToListAsync();

            return View(vacations);
        }
    }
}
