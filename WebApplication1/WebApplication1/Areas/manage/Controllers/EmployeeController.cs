using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.ViewModels.Login;

namespace WebApplication1.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize("SuperAdmin")]
    public class EmployeeController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly EmailService _emailService;
        private readonly AppDbContext _dbContext;

        public EmployeeController(UserManager<Employee> userManager, EmailService emailService, AppDbContext dbContext)
        {
            _userManager = userManager;
            _emailService = emailService;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateVM createVM)
        {
            try
            {
                // create user
                Employee newEmployee = new()
                {
                    UserName = createVM.UserName,
                    FullName = createVM.FullName,
                    Email = createVM.EmailAddress
                };

                var result = await _userManager.CreateAsync(newEmployee, "Salam123!");
                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
                }
                await _userManager.AddToRoleAsync(newEmployee, "Employee");

                // contract
                Contract newContarct = new Contract()
                {
                    EmployeeId = newEmployee.Id,
                    HourlyRate = createVM.HourlyRate,
                    BonusPercentage = createVM.BonusPercentage,
                    MonthlyMaxHours = createVM.MonthlyMaxHours,
                    StartDate = createVM.StarDate,
                    EndDate = createVM.EndDate,
                };

                // email and send
                await _dbContext.Contracts.AddAsync(newContarct);
                await _dbContext.SaveChangesAsync();

                _emailService.SendEmail(newEmployee.Email, newEmployee.UserName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

            return RedirectToAction("index", "dashboard");
        }


    }
}
