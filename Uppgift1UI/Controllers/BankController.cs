using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uppgift1UI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Uppgift1UI.Controllers
{
    public class BankController : Controller
    {
        BankRepository bankRepo = new BankRepository();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Deposit(int accountNr, decimal amount)
        {
            TempData["result"] = "Account was not found.";
            var customerList = bankRepo.GetAllCustomers();
            var account = customerList.SelectMany(c => c.Accounts).SingleOrDefault(a => a.AccountId == accountNr);

            if (account != null)
            {
                if (amount > 0)
                {
                    account.Balance += amount;
                    TempData["result"] = amount + "SEK was successfully deposited to account " + account.AccountId + ". New balance = " + account.Balance + "SEK";
                }
                else
                {
                    TempData["result"] = "Amount has to be more the 0.";
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Withdrawal(int accountNr, decimal amount)
        {
            TempData["result"] = "Account was not found.";
            var customerList = bankRepo.GetAllCustomers();
            var account = customerList.SelectMany(c => c.Accounts).SingleOrDefault(a => a.AccountId == accountNr);

            if (account != null)
            {
                if (amount > 0)
                {
                    if (amount <= account.Balance)
                    {
                        account.Balance -= amount;
                        TempData["result"] = amount + "SEK was successfully withdrawn from account " + account.AccountId + ". New balance = " + account.Balance + "SEK";
                    }
                    else
                    {
                        TempData["result"] = "You can't withdrawal more than what's on the account.";
                    }
                }
                else
                {
                    TempData["result"] = "Amount has to be more the 0.";
                }
            }

            return RedirectToAction("Index");
        }
    }
}
