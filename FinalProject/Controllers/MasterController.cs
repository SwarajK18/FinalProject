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
    public class MasterController : Controller
    {
        private readonly EmployeeDbContext _context;
        private readonly IMasterService _masterService;

        public MasterController(EmployeeDbContext context, IMasterService masterService)
        {
            _masterService = masterService;
            _context = context;
        }

        // GET: Master
        public async Task<IActionResult> Index(string Search, int PageNumber = 1)
        {
            var masters = new List<Master>();
            if (Search == null)
            {
                masters = await _masterService.GetAllMasters();
            }
            else
            {
                ViewData["Search"] = Search;
                Search = Search.ToLower();
                var result = await _masterService.GetAllMasters();

                masters = result.Where(s => s.Employee.Name.ToLower().Contains(Search) || s.SuperVisor.SuperVName.ToLower().Contains(Search) || s.JobType.JobTypeName.ToLower().Contains(Search)).ToList();

            }
            var name = masters.OrderBy(m => m.Employee.Name);
            ViewBag.TotalPages = Math.Ceiling(masters.Count() / 5.0);
            masters = name.Skip((PageNumber - 1) * 5).Take(5).ToList();

            return View(masters);
            //return View(await _masterService.GetAllMasters());

            //var employeeDbContext = _context.Masters.Include(m => m.Employee).Include(m => m.JobType).Include(m => m.Location).Include(m => m.SuperVisor);
            //return View(await employeeDbContext.ToListAsync());
        }

        // GET: Master/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await _masterService.GetMasterById(id));

            //var master = await _context.Masters
            //    .Include(m => m.Employee)
            //    .Include(m => m.JobType)
            //    .Include(m => m.Location)
            //    .Include(m => m.SuperVisor)
            //    .FirstOrDefaultAsync(m => m.ID == id);
            //if (master == null)
            //{
            //    return NotFound();
            //}

            //return View(master);
        }

        // GET: Master/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "ID", "Name");
            ViewData["JobTypeId"] = new SelectList(_context.JobTypes, "ID", "JobTypeName");
            ViewData["LocationId"] = new SelectList(_context.Locations, "ID", "LocationName");
            ViewData["SuperVisorId"] = new SelectList(_context.SuperVisors, "ID", "SuperVName");
            return View();
        }

        // POST: Master/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EmployeeId,SuperVisorId,JobTypeId,Start,End,LocationId,Comment")] Master master)
        {
            if (ModelState.IsValid)
            {
               bool result = await _masterService.CreateMasters(master);
                //_context.Add(master);
                //await _context.SaveChangesAsync();
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "ID", "Name", master.EmployeeId);
            ViewData["JobTypeId"] = new SelectList(_context.JobTypes, "ID", "JobTypeName", master.JobTypeId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "ID", "LocationName", master.LocationId);
            ViewData["SuperVisorId"] = new SelectList(_context.SuperVisors, "ID", "SuperVName", master.SuperVisorId);
            return View(master);
        }

        // GET: Master/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var master = await _masterService.GetMasterById(id);
            //var master = await _context.Masters.FindAsync(id);
            if (master == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "ID", "Name", master.EmployeeId);
            ViewData["JobTypeId"] = new SelectList(_context.JobTypes, "ID", "JobTypeName", master.JobTypeId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "ID", "LocationName", master.LocationId);
            ViewData["SuperVisorId"] = new SelectList(_context.SuperVisors, "ID", "SuperVName", master.SuperVisorId);
            return View(master);
        }

        // POST: Master/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EmployeeId,SuperVisorId,JobTypeId,Start,End,LocationId,Comment")] Master master)
        {
            if (id != master.ID)
            {
                return NotFound();
            }
           
            if (ModelState.IsValid)
            {
                try
                {
                    await _masterService.UpdateMasters(master);
                    //_context.Update(master);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MasterExists(master.ID))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "ID", "Name", master.EmployeeId);
            ViewData["JobTypeId"] = new SelectList(_context.JobTypes, "ID", "JobTypeName", master.JobTypeId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "ID", "LocationName", master.LocationId);
            ViewData["SuperVisorId"] = new SelectList(_context.SuperVisors, "ID", "SuperVName", master.SuperVisorId);
            return View(master);
        }

        // GET: Master/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var master = await _masterService.GetMasterById(id);

            //var master = await _context.Masters
                //.Include(m => m.Employee)
                //.Include(m => m.JobType)
                //.Include(m => m.Location)
                //.Include(m => m.SuperVisor)
                //.FirstOrDefaultAsync(m => m.ID == id);
            if (master == null)
            {
                return NotFound();
            }

            return View(master);
        }

        // POST: Master/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var master = await _context.Masters.FindAsync(id);
            //_context.Masters.Remove(master);
            //await _context.SaveChangesAsync();
            await _masterService.DeleteMasters(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MasterExists(int id)
        {
            return _masterService.MasterExists(id);
            //return _context.Masters.Any(e => e.ID == id);
        }
    }
}
