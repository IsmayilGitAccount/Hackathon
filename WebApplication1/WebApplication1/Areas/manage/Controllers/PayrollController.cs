using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Areas.manage.Controllers
{
    [Area("manage")]

    [Authorize("SuperAdmin")]
    public class PayrollController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<Employee> _userManager;

        public PayrollController(AppDbContext appDbContext,
                                 UserManager<Employee> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.Include("Attendances");
            return View(users);
        }
        public async Task<IActionResult> GetUserAttendance(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return NotFound();
            }

            var totalHours = await _appDbContext.Attendances.Where(a => a.Date.HasValue && a.Date.Value.Month == DateTime.UtcNow.AddHours(4).Month && a.Date.Value.Year == DateTime.UtcNow.AddHours(4).Year).Select(x => x.TotalHours).SumAsync();

            var contract = _appDbContext.Contracts.Where(x => x.EmployeeId == userId).FirstOrDefault();

            if (totalHours > contract.MonthlyMaxHours)
            {
                var bonusSalary = (totalHours - contract.MonthlyMaxHours) * contract.HourlyRate * 2;
            }

            Payroll newPayroll = new()
            {
                EmployeeId = user.Id,
            };


            return Ok();
        }
    }
}
