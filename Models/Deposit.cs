using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trackity.Models
{
    public class Deposit
    {
        [Key]
        public int DepositId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        //[RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only alphabetic letters are allowed.")]
        [Required(ErrorMessage = "Please enter name of deposit.")]
        [Display(Name = "Deposit Name")]
        [StringLength(200, MinimumLength = 2)]
        public string Name { get; set; }

        //[RegularExpression("^[0 - 9] *$", ErrorMessage = "Must only include numbers.")]
        [Required(ErrorMessage = "Please enter deposit amount.")]
        [Range(1, 10000, ErrorMessage = "Deposit must be between 1 and 10000.")]
        public double? Amount { get; set; }
    }
}
