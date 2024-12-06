using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Data;
using System.Text;
using WebApplication1.DAL;
using WebApplication1.Helper;
using WebApplication1.Models;
using WebApplication1.Services.Abstrations;
using WebApplication1.ViewModels.Login;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication1.Controllers
{
    public class AccountController(AppDbContext _sql, UserManager<Employee> _emp, SignInManager<Employee> _sign, IOptions<SmtpOption> _opt, IEmailService _service) : Controller
    {
        SmtpOption smtp = _opt.Value;



        //[HttpGet]
        //public async Task<IActionResult> Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Create(EmployeeCreateVM um)
        //{
        //    Employee emp = new()
        //    {
        //        FullName = um.FullName,
        //    };
        //    var result = await _emp.CreateAsync(emp, um.Password);
        //    if (!result.Succeeded)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);

        //        }
        //        return View();
        //    }
        //    string token = await _emp.GenerateEmailConfirmationTokenAsync(emp);
        //    _service.SendEmailConfirmation(emp.Email, emp.UserName, token);
        //    return Content("Email send!");

        //}
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM lm, string? ReturnUrl = null)
        {
            Employee? employee = null;
            if (!ModelState.IsValid) return View();
            if (lm.UsernameOrEmail.Contains('@'))
            {
                employee = await _emp.FindByEmailAsync(lm.UsernameOrEmail);
            }
            else
            {
                employee = await _emp.FindByNameAsync(lm.UsernameOrEmail);
            }
            if (employee is null)
            {
                ModelState.AddModelError("", "Username or Password is wrong");
                return View();
            }

            var result = await _sign.PasswordSignInAsync(employee, lm.Password, lm.RememberMe, true);
            if (result.Succeeded)
            {
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "You must confirm your account");
                }
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Wait until" + employee.LockoutEnd!.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    return View();

                }
            }
            if (string.IsNullOrEmpty(ReturnUrl))
            {
                if (await _emp.IsInRoleAsync(employee, "Admin"))
                {
                    return RedirectToAction("Index", new { Controller = "Auth", Area = "Admin" });
                }
                return RedirectToAction("Index", "Home");

            }
            return LocalRedirect(ReturnUrl);
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _sign.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> VerifyEmail(string token, string user)
        {
            var entity = await _emp.FindByNameAsync(user);
            if (entity is null) return BadRequest();
            var result = await _emp.ConfirmEmailAsync(entity, token.Replace(' ', '+'));
            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in result.Errors)
                {
                    sb.Append(item.Description);
                }
                return Content(sb.ToString());
            }

            await _sign.SignInAsync(entity, true);
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            var employee = await _emp.FindByNameAsync(User.Identity?.Name);
            if (employee == null)
            {
                return Unauthorized();
            }

            var model = new EmployeeUpdateVM
            {
                FullName = employee.FullName,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var employee = await _emp.FindByNameAsync(User.Identity?.Name);
            if (employee == null)
            {
                return Unauthorized();
            }
            employee.FullName = model.FullName;


            if (!string.IsNullOrEmpty(model.Password))
            {
                var passwordChangeResult = await _emp.ChangePasswordAsync(
                    employee,
                    model.Password,
                    model.NewPassword
                );

                if (!passwordChangeResult.Succeeded)
                {
                    foreach (var error in passwordChangeResult.Errors)
                    {
                        ModelState.AddModelError(" ", error.Description);
                    }
                    return View(model);
                }
            }

            var updateResult = await _emp.UpdateAsync(employee);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }



     

    }
}
