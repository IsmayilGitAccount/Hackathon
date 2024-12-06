using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.ViewModels.Vacation;

namespace WebApplication1.Controllers
{
    public class VacationRequestConroller(AppDbContext _sql) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _sql.VacationRequests.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VacCreateVm createVm)
        {
            VacationRequests vm = new()
            {
                StartDate = createVm.StartDate.Value,
                EndDate = createVm.EndDate,
                Reason = createVm.Reason,
                Status = createVm.Status,
            };
            await _sql.VacationRequests.AddAsync(vm);
            await _sql.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _sql.VacationRequests.FindAsync(id.Value);
            if (data is null) return NotFound();

            _sql.VacationRequests.Remove(data);
            await _sql.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var VM = await _sql.VacationRequests.Where(x => x.Id == id.Value).Select(x => new VacationUpdateVM
            {
                EndDate = x.EndDate,
                Reason = x.Reason,
                Status = x.Status,
                StartDate = x.StartDate,
            }).FirstOrDefaultAsync();
            if (VM is null) return BadRequest();
            return View(VM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, VacationUpdateVM Vm)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _sql.VacationRequests.Where(x => x.Id == id.Value).FirstOrDefaultAsync();
            if (data is null) return BadRequest();

            data.StartDate = Vm.StartDate;
            data.EndDate = Vm.EndDate;
            data.Reason = Vm.Reason;
            data.Status = Vm.Status;
            await _sql.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }


}

