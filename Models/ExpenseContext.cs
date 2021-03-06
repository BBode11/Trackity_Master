using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Trackity.Models
{
    public class ExpenseContext : DbContext
    {
        public ExpenseContext (DbContextOptions<ExpenseContext> options) : base(options) { }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Deposit> Deposits { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseType>().HasData(
                new ExpenseType { ExpenseTypeId = "F", Name = "Fixed" },
                new ExpenseType { ExpenseTypeId = "R", Name = "Recurring" },
                new ExpenseType { ExpenseTypeId = "N", Name = "Non-Recurring" },
                new ExpenseType { ExpenseTypeId = "W", Name = "Whammy" }
                );
            modelBuilder.Entity<Expense>().HasData(
                new Expense
                {
                    ExpenseId = 1,
                    Date = DateTime.Parse("09-24-2021"),
                    Name = "Rent Payment",
                    Cost = 600,
                    ExpenseTypeId = "F"
                },
                new Expense
                {
                    ExpenseId = 2,
                    Date = DateTime.Parse("09-25-2021"),
                    Name = "Netflix",
                    Cost = 8.99,
                    ExpenseTypeId = "F"
                },
                new Expense
                {
                    ExpenseId = 3,
                    Date = DateTime.Parse("09-25-2021"),
                    Name = "Gas",
                    Cost = 42.01,
                    ExpenseTypeId = "R"
                },
                new Expense
                {
                    ExpenseId = 4,
                    Date = DateTime.Parse("09-26-2021"),
                    Name = "Bubba's",
                    Cost = 21.85,
                    ExpenseTypeId = "R"
                }
                );
            modelBuilder.Entity<Deposit>().HasData(
                new Deposit
                {
                    DepositId = 1,
                    Date = DateTime.Parse("10-24-2021"),
                    Name = "Pay Check",
                    Amount = 1500
                },
                new Deposit
                {
                    DepositId = 2,
                    Date = DateTime.Parse("11-04-2021"),
                    Name = "Pay Check",
                    Amount = 1500
                },
                new Deposit
                {
                    DepositId = 3,
                    Date = DateTime.Parse("11-05-2021"),
                    Name = "Scratch Off Ticket",
                    Amount = 500
                },
                new Deposit
                {
                    DepositId = 4,
                    Date = DateTime.Parse("11-11-2021"),
                    Name = "Stock Returns",
                    Amount = 47.50
                }
                );
        }
    }

    
}
