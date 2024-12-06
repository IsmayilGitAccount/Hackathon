using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.ViewModels.VacationRequests;

namespace WebApplication1.Controllers
{
    public class VacationRequestsController : Controller
    {
        private readonly AppDbContext _context;

        public VacationRequestsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vacation = await _context.VacationRequests.ToListAsync();
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVacationRequestsVM vacationRequestsVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //VacationRequests vacationRequests = new()
            //{
            //    Id = vacationRequestsVM.Id,
            //    EmployeeId = vacationRequestsVM.EmployeeId,
            //    StartDate = vacationRequestsVM.StartDate,
            //    EndDate = vacationRequestsVM.EndDate,
            //    Status = vacationRequestsVM.Status,
            //    Reason = vacationRequestsVM.Reason
            //};

            VacationRequests vacationRequests = new()
            {
                Id = 1,
                EmployeeId = "2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Status = "Status",
                Reason = "Reason"
            };
            await _context.VacationRequests.AddAsync(vacationRequests);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.VacationRequests.FindAsync(id.Value);
            if (data is null) return NotFound();

            _context.VacationRequests.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
