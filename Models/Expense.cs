using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Trackity.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        //[RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only alphabetic letters are allowed.")]
        [Required(ErrorMessage = "Please enter name of expense.")]
        [Display(Name = "Expense Name")]
        [StringLength(200, MinimumLength = 2)]
        public string Name { get; set; }

        //[RegularExpression("^[0 - 9] *$", ErrorMessage = "Must only include numbers.")]
        [Required(ErrorMessage = "Please enter cost of expense.")]
        [Range(1, 10000, ErrorMessage = "Expense must be between 1 and 10000.")]
        public double? Cost { get; set; }

        [ForeignKey("ExpenseTypeId")]
        [Required(ErrorMessage = "Please enter expense type.")]
        [Display(Name = "Expense Type")]
        public string ExpenseTypeId { get; set; }
        public ExpenseType ExpenseType { get; set; }




    }
}
