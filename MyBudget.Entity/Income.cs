using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget.Entity
{
    public class Income
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
