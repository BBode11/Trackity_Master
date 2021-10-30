using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trackity.Models;

namespace Trackity.Controllers
{
    public class ExpenseTypeController : Controller
    {
        private readonly ExpenseContext _context;

        public ExpenseTypeController(ExpenseContext context)
        {
            _context = context;
        }

        // GET: ExpenseType
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExpenseTypes.ToListAsync());
        }

        // GET: ExpenseType/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseType = await _context.ExpenseTypes
                .FirstOrDefaultAsync(m => m.ExpenseTypeId == id);
            if (expenseType == null)
            {
                return NotFound();
            }

            return View(expenseType);
        }

        // GET: ExpenseType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExpenseType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpenseTypeId,Name")] ExpenseType expenseType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenseType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenseType);
        }

        // GET: ExpenseType/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseType = await _context.ExpenseTypes.FindAsync(id);
            if (expenseType == null)
            {
                return NotFound();
            }
            return View(expenseType);
        }

        // POST: ExpenseType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ExpenseTypeId,Name")] ExpenseType expenseType)
        {
            if (id != expenseType.ExpenseTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenseType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseTypeExists(expenseType.ExpenseTypeId))
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
            return View(expenseType);
        }

        // GET: ExpenseType/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseType = await _context.ExpenseTypes
                .FirstOrDefaultAsync(m => m.ExpenseTypeId == id);
            if (expenseType == null)
            {
                return NotFound();
            }

            return View(expenseType);
        }

        // POST: ExpenseType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var expenseType = await _context.ExpenseTypes.FindAsync(id);
            _context.ExpenseTypes.Remove(expenseType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseTypeExists(string id)
        {
            return _context.ExpenseTypes.Any(e => e.ExpenseTypeId == id);
        }
    }
}
