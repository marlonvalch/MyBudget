using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MyBudget.Entity
{
    public class FixedExpense
    {
        public int ID { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]

        public double Amount { get; set; }

        [Required]
        public DateOnly Date { get; set; }
    }
}
