using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class AttendanceController : Controller
{
    private readonly AppDbContext _appDbContext;
    private readonly UserManager<Employee> _userManager;

    public AttendanceController(AppDbContext appDbContext,
                      UserManager<Employee> userManager)
    {
        _appDbContext = appDbContext;
        _userManager = userManager;
    }
    public IActionResult Index()
    {
        return View();
    }
    [Authorize]
    public async Task<IActionResult> StartAttendance()
    {
        var existUser = await _userManager.FindByNameAsync(User.Identity.Name);
        var attendances = await _appDbContext.Attendances.ToListAsync();
        if (attendances.Any(x => x.EmployeeId == existUser.Id && x.Date == DateTime.UtcNow.AddHours(4).Date))
        {
            ModelState.AddModelError("", "Gunde 1 defe start ola biler");
            return BadRequest();
        }

        var newAttendance = new Models.Attendance()
        {
            EmployeeId = existUser.Id,
            StartTime = DateTime.UtcNow.AddHours(4),
        };

        return Ok();
    }

    [Authorize]
    public async Task<IActionResult> EndAttendance()
    {
        var existUser = await _userManager.FindByNameAsync(User.Identity.Name);
        var attendances = await _appDbContext.Attendances.ToListAsync();
        if (!_appDbContext.Attendances.Any(x => x.EmployeeId == existUser.Id))
            return BadRequest();

        var attendance = await _appDbContext.Attendances.Where(x => x.EmployeeId == existUser.Id).FirstOrDefaultAsync();
        attendance.EndTime = DateTime.UtcNow.AddHours(4);

        var timeSpan = (attendance.EndTime - attendance.StartTime);
        attendance.TotalHours = (int)Math.Ceiling(timeSpan.Value.Minutes / 60.0);

        var userContract = await _appDbContext.Contracts.Where(x => x.EmployeeId == existUser.Id).FirstOrDefaultAsync();

        if (attendance.TotalHours > (userContract.MonthlyMaxHours / 30))
        {
            attendance.OvertimeHours = attendance.TotalHours - (userContract.MonthlyMaxHours / 30);
        }

        await _appDbContext.SaveChangesAsync();
        return Ok();
    }
}
