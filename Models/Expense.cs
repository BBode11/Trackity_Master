using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Trackity.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please enter name of expense.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter cost of expense.")]
        [Range(1, 10000, ErrorMessage = "Expense must be between 1 and 10000.")]
        public double? Cost { get; set; }

        [Required(ErrorMessage = "Please enter expense type.")]
        public string ExpenseTypeId { get; set; }
        public ExpenseType ExpenseType { get; set; }




    }
}
