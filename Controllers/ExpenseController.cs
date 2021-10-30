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
    public class ExpenseController : Controller
    {
        private ExpenseContext context { get; set; }

        public ExpenseController(ExpenseContext ctx)
        {
            context = ctx;
        }

        // GET: Expense
        public async Task<IActionResult> Index()
        {
            ViewBag.ExpenseTypes = context.ExpenseTypes.OrderBy(e => e.Name).ToList();
            return View(await context.Expenses.Include(e => e.ExpenseType).ToListAsync());
        }

        // GET: Expense/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var expense = await context.Expenses
                .Include(e => e.ExpenseType)
                .FirstOrDefaultAsync(m => m.ExpenseId == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expense/Create
        public IActionResult Create()
        {
            ViewBag.ExpenseTypes = context.ExpenseTypes.OrderBy(e => e.Name).ToList();
            ViewData["ExpenseTypeId"] = new SelectList(context.ExpenseTypes, "ExpenseTypeId", "ExpenseTypeId");
            return View("Create", new Expense());
        }

        // POST: Expense/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpenseId,Date,Name,Cost,ExpenseTypeId")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                context.Add(expense);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ExpenseTypes = context.ExpenseTypes.OrderBy(e => e.Name).ToList();
            ViewData["ExpenseTypeId"] = new SelectList(context.ExpenseTypes, "ExpenseTypeId", "ExpenseTypeId");
            return View(expense);

        }

        // GET: Expense/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            var expense = await context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            ViewBag.ExpenseTypes = context.ExpenseTypes.OrderBy(e => e.Name).ToList();
            ViewData["ExpenseTypeId"] = new SelectList(context.ExpenseTypes, "ExpenseTypeId", "ExpenseTypeId", expense.ExpenseTypeId);
            return View(expense);
        }

        // POST: Expense/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpenseId,Date,Name,Cost,ExpenseTypeId")] Expense expense)
        {
            if (id != expense.ExpenseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(expense);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.ExpenseId))
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
            ViewBag.ExpenseTypes = context.ExpenseTypes.OrderBy(e => e.Name).ToList();
            ViewData["ExpenseTypeId"] = new SelectList(context.ExpenseTypes, "ExpenseTypeId", "ExpenseTypeId",expense.ExpenseTypeId);
            return View(expense);
        }

        // GET: Expense/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var expense = await context.Expenses
                .Include(e => e.ExpenseType)
                .FirstOrDefaultAsync(m => m.ExpenseId == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await context.Expenses.FindAsync(id);
            context.Expenses.Remove(expense);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
            return context.Expenses.Any(e => e.ExpenseId == id);
        }
    }
}
//return View(_context.Albums .Include(a => a.Artist) .OrderBy(a => a.Artist.Name) .Include(a => a.Genre) .ToList());
//return View(await _context.Expenses .Include(e=>e.Type) .ToListAsync());