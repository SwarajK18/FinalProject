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
    public class LocationController : Controller
    {
        private readonly EmployeeDbContext _context;
        private readonly ILocationService _locationService;

        public LocationController(EmployeeDbContext context, ILocationService locationService)
        {

            _locationService = locationService;
            _context = context;
        }

        // GET: Location
        public async Task<IActionResult> Index()
        {
            return View(await _locationService.GetAllLocations());
            //return View(await _context.Locations.ToListAsync());
        }

        // GET: Location/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var location = await _locationService.GetLocationById(id);
            //var location = await _context.Locations
            //    .FirstOrDefaultAsync(m => m.ID == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Location/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LocationName")] Location location)
        {
            if (ModelState.IsValid)
            {
                bool result = (await _locationService.CreateLocations(location));
                //_context.Add(location);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(location);
        }

        // GET: Location/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var location = await _locationService.GetLocationById(id);
            //var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // POST: Location/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LocationName")] Location location)
        {
            if (id != location.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.ID))
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
            return View(location);
        }

        // GET: Location/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var location = await _locationService.GetLocationById(id);
            //var location = await _context.Locations
            //    .FirstOrDefaultAsync(m => m.ID == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _locationService.DeleteLocation(id);
            //_context.Locations.Remove(location);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _locationService.LocationExist(id);
        }
    }
}
