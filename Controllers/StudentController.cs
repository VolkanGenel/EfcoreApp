using EfcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfcoreApp.Controllers
{
    public class StudentController: Controller
    {
        private readonly DataContext _context;

        public StudentController(DataContext context)
        {
            _context = context;
        }
        // var context = new DataContext(); Bunu yazmaya gerek yok. Onun yerine constructor injection yapacağız.

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student model)
        {
            _context.Students.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            return View(students);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var entity = await _context.Students.FindAsync(id); //Alttaki ile aynı anlama gelir. Async metot async olduğu için.
            // FindAsync sade id içinken FinOrDefaultAsync ile her değişken aranabilir.
            // var entity = await _context.Students.FirstOrDefaultAsync(o => o.StudentId==id);
            
            if(entity==null)
            {
                return NotFound();
            }
            return View(entity);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]       
        public async Task<IActionResult> Edit(int? id, Student model)
        {
            if(id != model.StudentId)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Students.Update(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!_context.Students.Any(x=>x.StudentId == model.StudentId))
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
        if(id == null)
        {
            return NotFound();
        }
        var entity = await _context.Students.FirstOrDefaultAsync(o => o.StudentId==id);
        
        if(entity==null)
        {
            return NotFound();
        }
        return View(entity);
    }

    }
}