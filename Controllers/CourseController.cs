using EfcoreApp.Data;
using EfcoreApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EfcoreApp.Controllers;

public class CourseController : Controller
{
    private readonly DataContext _context;

    public CourseController(DataContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var courses = await _context.Courses.Include(x=>x.Instructor).ToListAsync();
        return View(courses);
    }
    public async Task<IActionResult> Create()
    {
        ViewBag.Instructors = new SelectList(await _context.Instructors.ToListAsync(), "InstructorId", "NameSurname");
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CourseDTO model)
    {
        if(ModelState.IsValid)
        {        
        _context.Courses.Add(new Course() {CourseId = model.CourseId, Title = model.Title, InstructorId = model.InstructorId });
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
        }
        ViewBag.Instructors = new SelectList(await _context.Instructors.ToListAsync(), "InstructorId", "NameSurname");
        return View(model);
    }
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var course = await _context.Courses.Include(x=>x.CourseRegistrations).ThenInclude(x=>x.Student).Select(x=>  new CourseDTO {
            CourseId = x.CourseId, CourseRegistrations = x.CourseRegistrations, InstructorId = x.InstructorId, Title = x.Title
        }).FirstOrDefaultAsync(x => x.CourseId == id);
        if (course == null)
        {
            return NotFound();
        }
        ViewBag.Instructors = new SelectList(await _context.Instructors.ToListAsync(), "InstructorId", "NameSurname");
        return View(course);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, CourseDTO model)
    {
        if (id != model.CourseId)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                _context.Courses.Update(new Course() {CourseId = model.CourseId, Title = model.Title, InstructorId = model.InstructorId });
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Courses.Any(x => x.CourseId == model.CourseId))
                {
                    return NotFound();
                }
                throw;
            }
        }
        ViewBag.Instructors = new SelectList(await _context.Instructors.ToListAsync(), "InstructorId", "NameSurname");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var course = await _context.Courses.FindAsync(id);

        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] int id)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(o => o.CourseId == id);

        if (course == null)
        {
            return NotFound();
        }
        try
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Courses.Any(x => x.CourseId == course.CourseId))
            {
                return NotFound();
            }
            throw;
        }
    }
}