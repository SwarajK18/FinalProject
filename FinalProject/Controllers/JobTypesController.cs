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
    public class JobTypesController : Controller
    {
        private readonly EmployeeDbContext _context;
        private readonly IJobTypeService _jobTypeService;

        public JobTypesController(EmployeeDbContext context, IJobTypeService jobTypeService)
        {
            _jobTypeService = jobTypeService;
            _context = context;
        }

        // GET: JobTypes
        public async Task<IActionResult> Index()
        {
            //return View(await _context.JobTypes.ToListAsync());
            return View(await _jobTypeService.GetAllJobTypes());
        }

        // GET: JobTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }
            return View(await _jobTypeService.GetJobTypeById(id));

            //var jobType = await _context.JobTypes
            //    .FirstOrDefaultAsync(m => m.ID == id);
            //if (jobType == null)
            //{
            //    return NotFound();
            //}

            //return View(jobType);
        }

        // GET: JobTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,JobTypeName")] JobType jobType)
        {
            if (ModelState.IsValid)
            {
                bool result = (await _jobTypeService.CreateJobTypes(jobType));
                //_context.Add(jobType);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(jobType);
        }

        // GET: JobTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var jobType = await _jobTypeService.GetJobTypeById(id);

            //var jobType = await _context.JobTypes.FindAsync(id);
            if (jobType == null)
            {
                return NotFound();
            }
            return View(jobType);
        }

        // POST: JobTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,JobTypeName")] JobType jobType)
        {
            if (id != jobType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobTypeExists(jobType.ID))
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
            return View(jobType);
        }

        // GET: JobTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var jobType = await _jobTypeService.GetJobTypeById(id);
            //var jobType = await _context.JobTypes
            //    .FirstOrDefaultAsync(m => m.ID == id);
            if (jobType == null)
            {
                return NotFound();
            }

            return View(jobType);
        }

        // POST: JobTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _jobTypeService.DeleteJobType(id);
            //_context.JobTypes.Remove(jobType);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobTypeExists(int id)
        {
            return _jobTypeService.JobTypeExist(id);
        }
    }
}
