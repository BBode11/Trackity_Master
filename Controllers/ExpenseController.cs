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
        private readonly ExpenseContext _context;

        public ExpenseController(ExpenseContext context)
        {
            _context = context;
        }

        // GET: Expense
        //public async Task<IActionResult> Index()
        //{
        //    var expenseContext = _context.Expenses.Include(e => e.ExpenseType);
        //    return View(await expenseContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CostSortParm"] = sortOrder == "Cost" ? "cost_desc" : "Cost";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;


            var expenses = from e in _context.Expenses.Include(e => e.ExpenseType)
                           select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                expenses = expenses.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    expenses = expenses.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    expenses = expenses.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    expenses = expenses.OrderByDescending(s => s.Date);
                    break;
                case "Cost":
                    expenses = expenses.OrderBy(s => s.Cost);
                    break;
                case "cost_desc":
                    expenses = expenses.OrderByDescending(s => s.Cost);
                    break;
                default:
                    expenses = expenses.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Expense>.CreateAsync(expenses.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        
        // GET: Expense/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
    
            var expense = await _context.Expenses
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
            //added viewbag statement to pass to view
            ViewBag.ExpenseTypes = _context.ExpenseTypes.OrderBy(t => t.Name).ToList();
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "ExpenseTypeId", "ExpenseTypeId");
            return View();
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
                _context.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //added viewbag statement to pass to view
            ViewBag.ExpenseTypes = _context.ExpenseTypes.OrderBy(t => t.Name).ToList();
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "ExpenseTypeId", "ExpenseTypeId", expense.ExpenseTypeId);
            return View(expense);
        }

        // GET: Expense/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //added viewbag statement to pass to view
            ViewBag.ExpenseTypes = _context.ExpenseTypes.OrderBy(t => t.Name).ToList();
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "ExpenseTypeId", "ExpenseTypeId", expense.ExpenseTypeId);
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
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
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
            //added viewbag statement to pass to view
            ViewBag.ExpenseTypes = _context.ExpenseTypes.OrderBy(t => t.Name).ToList();
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "ExpenseTypeId", "ExpenseTypeId", expense.ExpenseTypeId);
            return View(expense);
        }

        // GET: Expense/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
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
            var expense = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.ExpenseId == id);
        }
    }
}
