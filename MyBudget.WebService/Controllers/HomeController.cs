using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyBudget.WebService.Models;
using MyBudget.Entity;
using Microsoft.VisualBasic;

namespace MyBudget.WebService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 

        private readonly MyBudgetDBContext _context; //Dependency Injection

        public HomeController(ILogger<HomeController> logger, MyBudgetDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var totalFlexible = _context.flexibleExpenses.Sum(e => e.Amount);
            var totalFixed = _context.fixedExpenses.Sum(e => e.Amount);
            var totalIncome = _context.income.Sum(e => e.Amount);
            var totalSaving = _context.savings.Sum(e => e.Amount);
            var totalDebt = _context.debts.Sum(e => e.Amount);

            ViewBag.PieLabels = new[] { "Flexible Expenses", "Fixed Expenses", "Income", "Savings", "Debts" };
            ViewBag.PieData = new[] { totalFlexible, totalFixed, totalIncome, totalSaving, totalDebt };

            return View();
        }

        #region Flexible Expenses
        public IActionResult FlexibleExpenses()
        {


            var allFlexibleExpenses = _context.flexibleExpenses.ToList();

            var TotalFlexibleExpenses = allFlexibleExpenses.Sum(FlexibleExpense=>FlexibleExpense.Amount);

            ///This section is for the Balance ViewBag creation
            var TotalFixed = _context.fixedExpenses;
            var TotalIncome = _context.income;
            var TotalSaving = _context.savings;
            var TotalDebt = _context.debts;
            var TotalFixedExpense = TotalFixed.Sum(FixedExpense => FixedExpense.Amount);
            var TotalIncomes = TotalIncome.Sum(Income => Income.Amount);
            var TotalSavings = TotalSaving.Sum(Saving => Saving.Amount);
            var TotalDebts = TotalDebt.Sum(Debts => Debts.Amount);

            var Total = TotalFixedExpense + TotalFlexibleExpenses + TotalSavings + TotalDebts;

            var Balance = TotalIncomes - Total;

            ViewBag.Balance = Balance.ToString("C");//Currency Format

            if (ViewBag.Balance.StartsWith("(") && ViewBag.Balance.EndsWith(")")) //Formating to Neg currency
            {
                ViewBag.Balance = ViewBag.Balance.Insert(2,"-");
                ViewBag.Balance = ViewBag.Balance.Replace("(", "").Replace(")", "");
            }


            ViewBag.FlexibleExpenses = TotalFlexibleExpenses.ToString("C");

            return View(allFlexibleExpenses);
        }

        public IActionResult FlexibleExpensesAddEdit(int? Id)
        {
            if(Id != null)
            {
                //Update mode will load the Flexible Expenses selected ID 
                var FlexibleExpenseinDb = _context.flexibleExpenses.SingleOrDefault(FlexibleExpense => FlexibleExpense.ID == Id);
                return View(FlexibleExpenseinDb);
            }
            return View();
        }

        public IActionResult FlexibleExpensesDelete(int? id) 
        {
            var FlexibleExpenseinDb = _context.flexibleExpenses.SingleOrDefault(FlexibleExpense=>FlexibleExpense.ID==id);
            _context.flexibleExpenses.Remove(FlexibleExpenseinDb);
            _context.SaveChanges();
            return RedirectToAction("FlexibleExpenses");

        }
        public IActionResult FlexibleExpensesAddEditForm(FlexibleExpense model)
        {
            if(model.ID == 0)
            {
                // Create mode
                _context.flexibleExpenses.Add(model);

            }
            else
            {
                //Update mode
                _context.flexibleExpenses.Update(model);
            }
            _context.SaveChanges();

            return RedirectToAction("FlexibleExpenses");
        }

        #endregion

        #region Fixed Expenses

        public IActionResult FixedExpenses()
        {
            var allFixedExpenses = _context.fixedExpenses.ToList();

            var TotalFixedExpenses = allFixedExpenses.Sum(FixedExpense => FixedExpense.Amount);

            
            ///This section is for the Balance ViewBag creation
            var TotalFixed = _context.fixedExpenses;
            var TotalFlexible = _context.flexibleExpenses;
            var TotalIncome = _context.income;
            var TotalSaving = _context.savings;
            var TotalDebt = _context.debts;
            var TotalFlexibleExpense = TotalFlexible.Sum(FlexibleExpense => FlexibleExpense.Amount);
            var TotalIncomes = TotalIncome.Sum(Income => Income.Amount);
            var TotalSavings = TotalSaving.Sum(Saving => Saving.Amount);
            var TotalDebts = TotalDebt.Sum(Debts => Debts.Amount);

            var Total = TotalFixedExpenses + TotalFlexibleExpense + TotalSavings + TotalDebts;

            var Balance = TotalIncomes - Total;

            ViewBag.Balance = Balance.ToString("C"); 

            if (ViewBag.Balance.StartsWith("(") && ViewBag.Balance.EndsWith(")")) //Formating to Neg currency
            {
                ViewBag.Balance = ViewBag.Balance.Insert(2, "-");
                ViewBag.Balance = ViewBag.Balance.Replace("(", "").Replace(")", "");
            }
            ViewBag.FixedExpenses = TotalFixedExpenses.ToString("C"); 

            return View(allFixedExpenses);
        }


        public IActionResult FixedExpensesAddEdit(int? Id)
        {
            if (Id != null)
            {
                //Update mode will load the Fixed Expenses selected ID 
                var FixedExpenseinDb = _context.fixedExpenses.SingleOrDefault(FixedExpense => FixedExpense.ID == Id);
                return View(FixedExpenseinDb);
            }
            return View();
        }

        public IActionResult FixedExpensesDelete(int? id)
        {
            var FixedExpenseinDb = _context.fixedExpenses.SingleOrDefault(FixedExpense => FixedExpense.ID == id);
            _context.fixedExpenses.Remove(FixedExpenseinDb);
            _context.SaveChanges();
            return RedirectToAction("FixedExpenses");

        }
        public IActionResult FixedExpensesAddEditForm(FixedExpense model)
        {
            if (model.ID == 0)
            {
                // Create mode
                _context.fixedExpenses.Add(model);

            }
            else
            {
                //Update mode
                _context.fixedExpenses.Update(model);
            }
            _context.SaveChanges();

            return RedirectToAction("FixedExpenses");
        }

        #endregion

        #region Income
        public IActionResult Income()
        {
            var allIncome = _context.income.ToList();

            var TotalIncome = allIncome.Sum(Income => Income.Amount);

            ///This section is for the Balance ViewBag creation
            var TotalFixed = _context.fixedExpenses;
            var TotalFlexible = _context.flexibleExpenses;
            var TotalSaving = _context.savings;
            var TotalDebt = _context.debts;
            var TotalFixedExpense = TotalFixed.Sum(FixedExpense => FixedExpense.Amount);
            var TotalFlexibleExpense = TotalFlexible.Sum(FlexibleExpense => FlexibleExpense.Amount);
            var TotalSavings = TotalSaving.Sum(Saving => Saving.Amount);
            var TotalDebts = TotalDebt.Sum(Debts => Debts.Amount);

            var Total = TotalFixedExpense + TotalFlexibleExpense + TotalSavings + TotalDebts;

            var Balance = TotalIncome - Total;

            ViewBag.Balance = Balance.ToString("C"); 
            if (ViewBag.Balance.StartsWith("(") && ViewBag.Balance.EndsWith(")")) //Formating to Neg currency
            {
                ViewBag.Balance = ViewBag.Balance.Insert(2, "-");
                ViewBag.Balance = ViewBag.Balance.Replace("(", "").Replace(")", "");
            }

            ViewBag.Income = TotalIncome.ToString("C"); 
            return View(allIncome);
        }

        public IActionResult IncomeAddEdit(int? Id)
        {
            if (Id != null)
            {
                //Update mode will load the Income selected ID 
                var IncomeinDb = _context.income.SingleOrDefault(Income => Income.ID == Id);
                return View(IncomeinDb);
            }
            return View();
        }

        public IActionResult IncomeDelete(int? id)
        {
            var IncomeinDb = _context.income.SingleOrDefault(Income => Income.ID == id);
            _context.income.Remove(IncomeinDb);
            _context.SaveChanges();
            return RedirectToAction("Income");

        }
        public IActionResult IncomeAddEditForm(Income model)
        {
            if (model.ID == 0)
            {
                // Create mode
                _context.income.Add(model);

            }
            else
            {
                //Update mode
                _context.income.Update(model);
            }
            _context.SaveChanges();

            return RedirectToAction("Income");
        }
        #endregion

        #region Savings
        public IActionResult Savings()
        {
            var allSavings = _context.savings.ToList();

            var TotalSavings = allSavings.Sum(Savings => Savings.Amount);

            ///This section is for the Balance ViewBag creation
            var TotalFixed = _context.fixedExpenses;
            var TotalFlexible = _context.flexibleExpenses;
            var TotalIncome = _context.income;
            var TotalDebt = _context.debts;
            var TotalFixedExpense = TotalFixed.Sum(FixedExpense => FixedExpense.Amount);
            var TotalFlexibleExpense = TotalFlexible.Sum(FlexibleExpense => FlexibleExpense.Amount);
            var TotalIncomes = TotalIncome.Sum(Income => Income.Amount);
            var TotalDebts = TotalDebt.Sum(Debts => Debts.Amount);

            var Total = TotalFixedExpense + TotalFlexibleExpense + TotalSavings + TotalDebts;

            var Balance = TotalIncomes - Total;

            ViewBag.Balance = Balance.ToString("C");
            if (ViewBag.Balance.StartsWith("(") && ViewBag.Balance.EndsWith(")")) //Formating to Neg currency
            {
                ViewBag.Balance = ViewBag.Balance.Insert(2, "-");
                ViewBag.Balance = ViewBag.Balance.Replace("(", "").Replace(")", "");
            }
            ViewBag.Savings = TotalSavings.ToString("C"); 
            return View(allSavings);
        }

        public IActionResult SavingsAddEdit(int? Id)
        {
            if (Id != null)
            {
                //Update mode will load the Saving selected ID 
                var SavinginDb = _context.savings.SingleOrDefault(Saving => Saving.ID == Id);
                return View(SavinginDb);
            }
            return View();
        }

        public IActionResult SavingsDelete(int? id)
        {
            var SavinginDb = _context.savings.SingleOrDefault(Saving => Saving.ID == id);
            _context.savings.Remove(SavinginDb);
            _context.SaveChanges();
            return RedirectToAction("Savings");

        }
        public IActionResult SavingsAddEditForm(Saving model)
        {
            if (model.ID == 0)
            {
                // Create mode
                _context.savings.Add(model);

            }
            else
            {
                //Update mode
                _context.savings.Update(model);
            }
            _context.SaveChanges();

            return RedirectToAction("Savings");
        }
        #endregion

        #region Debts
        public IActionResult Debts()
        {
            var allDebts = _context.debts.ToList();

            var TotalDebts = allDebts.Sum(Debt => Debt.Amount);

            ///This section is for the Balance ViewBag creation
            var TotalFixed = _context.fixedExpenses;
            var TotalFlexible = _context.flexibleExpenses;
            var TotalIncome = _context.income;
            var TotalSaving = _context.savings;
            var TotalFixedExpense = TotalFixed.Sum(FixedExpense => FixedExpense.Amount);
            var TotalFlexibleExpense = TotalFlexible.Sum(FlexibleExpense => FlexibleExpense.Amount);
            var TotalIncomes = TotalIncome.Sum(Income => Income.Amount);
            var TotalSavings = TotalSaving.Sum(Saving => Saving.Amount);

            var Total = TotalFixedExpense + TotalFlexibleExpense + TotalSavings + TotalDebts;

            var Balance = TotalIncomes - Total;

            ViewBag.Balance = Balance.ToString("C");
            if (ViewBag.Balance.StartsWith("(") && ViewBag.Balance.EndsWith(")")) //Formating to Neg currency
            {
                ViewBag.Balance = ViewBag.Balance.Insert(2, "-");
                ViewBag.Balance = ViewBag.Balance.Replace("(", "").Replace(")", "");
            }
            ViewBag.Debts = TotalDebts.ToString("C");
            return View(allDebts);
        }

        public IActionResult DebtsAddEdit(int? Id)
        {
            if (Id != null)
            {
                //Update mode will load the Fixed Expenses selected ID 
                var DebtinDb = _context.debts.SingleOrDefault(Debt => Debt.ID == Id);
                return View(DebtinDb);
            }
            return View();
        }

        public IActionResult DebtsDelete(int? id)
        {
            var DebtinDb = _context.debts.SingleOrDefault(Debt => Debt.ID == id);
            _context.debts.Remove(DebtinDb);
            _context.SaveChanges();
            return RedirectToAction("Debts");

        }
        public IActionResult DebtsAddEditForm(Debt model)
        {
            if (model.ID == 0)
            {
                // Create mode
                _context.debts.Add(model);

            }
            else
            {
                //Update mode
                _context.debts.Update(model);
            }
            _context.SaveChanges();

            return RedirectToAction("Debts");
        }
        #endregion

            #region Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        #region Error
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
    }
}
