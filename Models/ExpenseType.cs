using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Trackity.Models
{
    public class ExpenseType
    {
        [Key]
        public string ExpenseTypeId { get; set; }

        public string Name { get; set; }
    }
}
