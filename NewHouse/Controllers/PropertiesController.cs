using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewHouse.Data;
using Type = NewHouse.Data.Type;

namespace NewHouse.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly NewHouseDbContext _context;

        public PropertiesController(NewHouseDbContext context)
        {
            _context = context;
        }

        // GET: Properties
        public async Task<IActionResult> Index()
        {
            var newHouseDbContext = _context.Properties.Include(p => p.Type);
            return View(await newHouseDbContext.ToListAsync());

        }
        public async Task<IActionResult> Index1(string SearchString)
        {
            ViewData["CurrentFilter"] = SearchString;
            var prop = from p in _context.Properties select p;
            if (!String.IsNullOrEmpty(SearchString))
            {
                prop = prop.Where(p => p.Type.TypeName.Contains(SearchString));
            } 
            return View(prop);
           
        }
        

        // GET: Properties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Properties == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties
                .Include(p => p.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // GET: Properties/Create
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.Types, "TypeName", "TypeName");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryName", "CategoryName");
            return View();
        }

        // POST: Properties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create([Bind("Id,Floor,ImageUrl,Quadrature,Status,Address,Description,Price,TypeId,Type")] Property @property)
        {
            if (ModelState.IsValid)
            {
                property.RegisterOn = DateTime.Now;
                _context.Add(@property);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "TypeName", @property.TypeId);
            //ViewData["Type"] = new SelectList(_context.Categories, "Id", "CategoryName", @property.Type.Categories);
            return View(@property);
        }

        // GET: Properties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Properties == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties.FindAsync(id);
            if (@property == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.Types, "TypeName", "TypeName", @property.TypeId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryName", "CategoryName");
            return View(@property);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Floor,ImageUrl,Quadrature,Status,Address,Description,Price,TypeId,Type")] Property @property)
        {
            if (id != @property.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    property.RegisterOn = DateTime.Now;
                    _context.Update(@property);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyExists(@property.Id))
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
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "TypeName", @property.TypeId);
            return View(@property);
        }

        // GET: Properties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Properties == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties
                .Include(p => p.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Properties == null)
            {
                return Problem("Entity set 'NewHouseDbContext.Properties'  is null.");
            }
            var @property = await _context.Properties.FindAsync(id);
            if (@property != null)
            {
                _context.Properties.Remove(@property);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyExists(int id)
        {
          return (_context.Properties?.Any(e => e.Id == id)).GetValueOrDefault(); 
        }
    }
}
