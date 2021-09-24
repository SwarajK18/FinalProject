using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.DAL.Data;
using MainProject.DAL.Data.Models;
using MainProject.Services.Services;

namespace FinalProject.Controllers
{
    public class SuperVisorController : Controller
    {
        private readonly EmployeeDbContext _context;
        private readonly ISuperVisorService _superVisorService;

        public SuperVisorController(EmployeeDbContext context, ISuperVisorService superVisorService)
        {
            _context = context;
            _superVisorService = superVisorService;

        }

        // GET: SuperVisor
        public async Task<IActionResult> Index()
        {
            return View(await _superVisorService.GetAllSuperVisors());
            //return View(await _context.SuperVisors.ToListAsync());
        }

        // GET: SuperVisor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var superVisor = await _superVisorService.GetSuperVisorById(id);

            //var superVisor = await _context.SuperVisors
            //    .FirstOrDefaultAsync(m => m.ID == id);
            if (superVisor == null)
            {
                return NotFound();
            }

            return View(superVisor);
        }

        // GET: SuperVisor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SuperVisor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SuperVName")] SuperVisor superVisor)
        {
            if (ModelState.IsValid)
            {
              bool result = (await _superVisorService.CreateSuperVisor(superVisor));
                //_context.Add(superVisor);
                //await _context.SaveChangesAsync();
                if(result)
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            return View(superVisor);
        }

        // GET: SuperVisor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var superVisor = await _superVisorService.GetSuperVisorById(id);

            //var superVisor = await _context.SuperVisors.FindAsync(id);
            if (superVisor == null)
            {
                return NotFound();
            }
            return View(superVisor);
        }

        // POST: SuperVisor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SuperVName")] SuperVisor superVisor)
        {
            if (id != superVisor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _superVisorService.UpdateSuperVisor(superVisor); 
                    //_context.Update(superVisor);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuperVisorExists(superVisor.ID))
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
            return View(superVisor);
        }

        // GET: SuperVisor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var superVisor = await _superVisorService.GetSuperVisorById(id);
            //var superVisor = await _context.SuperVisors
            //    .FirstOrDefaultAsync(m => m.ID == id);
            if (superVisor == null)
            {
                return NotFound();
            }

            return View(superVisor);
        }

        // POST: SuperVisor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _superVisorService.DeleteSuperVisor(id);
            //var superVisor = await _context.SuperVisors.FindAsync(id);
            //_context.SuperVisors.Remove(superVisor);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuperVisorExists(int id)
        {
            return _superVisorService.SuperVisorExist(id);
            //return _context.SuperVisors.Any(e => e.ID == id);
        }
    }
}
