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
            return View(await context.Expenses.ToListAsync());
        }

        // GET: Expense/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.ExpenseTypes = context.ExpenseTypes.OrderBy(e => e.Name).ToList();
            var expense = await context.Expenses
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
            ViewBag.Action = "Create";
            //Add? var expense = context.Expenses.Find(Id)
            ViewBag.ExpenseTypes = context.ExpenseTypes.OrderBy(e => e.Name).ToList();
            return View("Create", new Expense());
        }

        // POST: Expense/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpenseId,Date,Name,Cost")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                context.Add(expense);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //added ViewBag
                ViewBag.Action = (expense.ExpenseId == 0) ? "Create" : "Edit";
                ViewBag.ExpenseTypes = context.ExpenseTypes.OrderBy(e => e.Name).ToList();
                return View(expense);
            }
            
        }

        // GET: Expense/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Action = "Edit";
            ViewBag.ExpenseTypes = context.ExpenseTypes.OrderBy(e => e.Name).ToList();
            var expense = await context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }

        // POST: Expense/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpenseId,Date,Name,Cost")] Expense expense)
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
            return View(expense);
        }

        // GET: Expense/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.ExpenseTypes = context.ExpenseTypes.OrderBy(e => e.Name).ToList();
            var expense = await context.Expenses
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
