using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using test10.Data;
using test10.Models;

namespace test10.Controllers
{
   
    public class producttypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public producttypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: producttypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.producttype.ToListAsync());
        }

        // GET: producttypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producttype = await _context.producttype
                .SingleOrDefaultAsync(m => m.ID == id);
            if (producttype == null)
            {
                return NotFound();
            }

            return View(producttype);
        }

        // GET: producttypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: producttypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,prodtype")] producttype producttype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producttype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producttype);
        }

        // GET: producttypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producttype = await _context.producttype.SingleOrDefaultAsync(m => m.ID == id);
            if (producttype == null)
            {
                return NotFound();
            }
            return View(producttype);
        }

        // POST: producttypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,prodtype")] producttype producttype)
        {
            if (id != producttype.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producttype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!producttypeExists(producttype.ID))
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
            return View(producttype);
        }

        // GET: producttypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producttype = await _context.producttype
                .SingleOrDefaultAsync(m => m.ID == id);
            if (producttype == null)
            {
                return NotFound();
            }

            return View(producttype);
        }

        // POST: producttypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producttype = await _context.producttype.SingleOrDefaultAsync(m => m.ID == id);
            _context.producttype.Remove(producttype);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool producttypeExists(int id)
        {
            return _context.producttype.Any(e => e.ID == id);
        }
    }
}
