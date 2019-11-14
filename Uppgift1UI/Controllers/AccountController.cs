using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uppgift1UI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Uppgift1UI.Controllers
{
    public class AccountController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Transfer(int fromAcc, int toAcc, decimal amount)
        {
            var model = new BankRepository();
            var customers = model.GetAllCustomers();
            TempData["result"] = "Account was not found.";
            var fromAccount = customers.SelectMany(c => c.Accounts).SingleOrDefault(a => a.AccountId == fromAcc);
            var toAccount = customers.SelectMany(c => c.Accounts).SingleOrDefault(a => a.AccountId == toAcc);

            if(fromAccount != null && toAccount != null)
            {
                if(amount <= 0)
                {
                    TempData["result"] = $"Invalid amount!";
                }
                if (fromAccount.Balance < amount)
                {
                    TempData["result"] = $"Not enough balance on account!";
                }
                else
                {
                    TempData["result"] = $"Success! Transfered {amount} :- to account {toAcc}, New balance: {toAccount.Balance}";
                    fromAccount.Balance -= amount;
                    toAccount.Balance += amount;
                }
            }
            return RedirectToAction("Index");
        }
    }
}
