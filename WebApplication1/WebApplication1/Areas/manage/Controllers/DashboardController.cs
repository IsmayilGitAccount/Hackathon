using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Areas.manage.Controllers
{
    [Area("manage")]
    public class DashboardController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Employee> _userManager;

        public DashboardController(RoleManager<IdentityRole> roleManager,
                                   UserManager<Employee> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role1 = new IdentityRole("SuperAdmin");
        //    IdentityRole role2 = new IdentityRole("Employee");

        //    await _roleManager.CreateAsync(role1);
        //    var result = await _roleManager.CreateAsync(role2);

        //    return Ok(result);
        //}

        public async Task<IActionResult> CreateAdmin()
        {
            Employee newEmployee = new()
            {
                FullName = "Enver Zohrabov",
                Email = "enver@gmail.com",
                UserName = "enver",
            };

            await _userManager.CreateAsync(newEmployee, "Salam123!");
            var result = await _userManager.AddToRoleAsync(newEmployee, "Employee");

            return Ok(result);
        }
    }
}
