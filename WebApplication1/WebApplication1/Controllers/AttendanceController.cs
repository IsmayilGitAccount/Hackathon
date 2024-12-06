using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AttendanceController(AppDbContext _context):Controller
    {

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Start(int Id)
        {
            var employee =await _context.Employees.FindAsync(Id);
            if (employee == null)
            {
                BadRequest("This employee is not exist");
            }
            var Today = DateTime.Today;
            
            return View();
        }

    }
}
