using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplication1.Helper;
using WebApplication1.Models;
using WebApplication1.Services.Abstrations;
using WebApplication1.ViewModels.Login;

namespace WebApplication1.Areas.manage.Controllers
{
    [Area("manage")]
    public class EmployeeController(UserManager<Employee> _emp, IEmailService _service) : Controller
    {
        //[HttpGet]
        //public async Task<IActionResult> Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        public async Task<IActionResult> Create(/*EmployeeCreateVM um*/)
        {
            EmployeeCreateVM vmodel = new()
            {
                UserName = "stniyaz",
                FullName = "asd asd",
                EmailAddress = "soltanovniyaz@gmail.com",
                Password ="asdasdhjbhjvjh2vA"
            };
            Employee emp = new()
            {
                UserName = vmodel.UserName,
                Email = vmodel.EmailAddress,
                FullName = vmodel.FullName,
            };
            var result = await _emp.CreateAsync(emp, vmodel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
                return Ok(result);
            }
            string token = await _emp.GenerateEmailConfirmationTokenAsync(emp);
            _service.SendEmailConfirmation(emp.Email, emp.UserName, token);
            return Ok("Email send!");

        }
    }
}
