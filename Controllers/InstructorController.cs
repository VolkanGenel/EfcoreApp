using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EfcoreApp.Controllers
{
    public class InstructorController: Controller
    {
        private readonly DataContext _context;

        public InstructorController(DataContext context)
        {
            this._context = context;
        }


        public async Task<IActionResult> Index()
        {
            var instructors = await _context.Instructors.ToListAsync();
            return View(instructors);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor model)
        {
            _context.Instructors.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit (int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var instructor = await _context.Instructors.Include(x=>x.Courses).FirstOrDefaultAsync(x=>x.InstructorId==id);
            
            if(instructor==null)
            {
                return NotFound();
            }
            return View(instructor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int? id, Instructor model)
        {
            if(id!=model.InstructorId)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Instructors.Update(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Instructors.Any(x => x.InstructorId == model.InstructorId))
                    {
                        return NotFound();
                    }
                    throw;
                }

            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = await _context.Instructors.FirstOrDefaultAsync(o => o.InstructorId == id);

            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var entity = await _context.Instructors.FirstOrDefaultAsync(o => o.InstructorId == id);

            if (entity == null)
            {
                return NotFound();
            }
            try
            {
                _context.Instructors.Remove(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Instructors.Any(x => x.InstructorId == entity.InstructorId))
                {
                    return NotFound();
                }
                throw;
            }            
        }
    }
}