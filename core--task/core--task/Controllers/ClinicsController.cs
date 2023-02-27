using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using core__task.Models;

namespace core__task.Controllers
{
    public class ClinicsController : Controller
    {
        private readonly CoreTaskContext _context;

        public ClinicsController(CoreTaskContext context)
        {
            _context = context;
        }

        // GET: Clinics
        public async Task<IActionResult> Index()
        {
              return View(await _context.Clinics.ToListAsync());
        }

        // GET: Clinics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clinics == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinics
                .FirstOrDefaultAsync(m => m.ClinicId == id);
            if (clinic == null)
            {
                return NotFound();
            }

            return View(clinic);
        }

        // GET: Clinics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clinics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClinicId,ClinicName,ClinicImg,ClinicDis")] Clinic clinic, IFormFile ClinicImg)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(ClinicImg.FileName);
                clinic.ClinicImg = ClinicImg.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ClinicImg.CopyToAsync(fileStream);
                }
                _context.Add(clinic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clinic);
        }

        // GET: Clinics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clinics == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }
            return View(clinic);
        }

        // POST: Clinics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClinicId,ClinicName,ClinicImg,ClinicDis")] Clinic clinic , IFormFile ClinicImg)
        {
            if (id != clinic.ClinicId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ClinicImg.FileName != null && ClinicImg.Length > 0)
                    {
                        var fileName = Path.GetFileName(ClinicImg.FileName);
                        clinic.ClinicImg = ClinicImg.FileName;
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await ClinicImg.CopyToAsync(fileStream);
                        }
                    }
                    else
                    {
                        var existingModel = _context.Clinics.AsNoTracking().FirstOrDefault(x => x.ClinicId == id);
                        clinic.ClinicImg = existingModel.ClinicImg;

                    }
                    _context.Update(clinic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClinicExists(clinic.ClinicId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clinic);
        }

        // GET: Clinics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clinics == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinics
                .FirstOrDefaultAsync(m => m.ClinicId == id);
            if (clinic == null)
            {
                return NotFound();
            }

            return View(clinic);
        }

        // POST: Clinics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clinics == null)
            {
                return Problem("Entity set 'CoreTaskContext.Clinics'  is null.");
            }
            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic != null)
            {
                _context.Clinics.Remove(clinic);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClinicExists(int id)
        {
          return _context.Clinics.Any(e => e.ClinicId == id);
        }
    }
}
