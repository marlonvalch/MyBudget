using Microsoft.EntityFrameworkCore;
using MyBudget.Entity;

namespace MyBudget.WebService.Models
{
    public class MyBudgetDBContext : DbContext
    {
        public DbSet<FlexibleExpense> flexibleExpenses { get; set; }
        public DbSet<FixedExpense> fixedExpenses { get; set; }

        public DbSet<Income> income { get; set; }

        public DbSet<Saving> savings { get; set; }

        public DbSet<Debt> debts { get; set; }



        public MyBudgetDBContext(DbContextOptions<MyBudgetDBContext>options)
            :base(options)
        {
            
        }
    }
}
