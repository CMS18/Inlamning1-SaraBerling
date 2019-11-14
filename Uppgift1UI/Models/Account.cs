using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uppgift1UI.Models;

namespace Uppgift1UI.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }

        public void Transfer(int fromAcc, int TooAcc, decimal amount)
        {
            BankRepository bankRepo = new BankRepository();
            var customers = bankRepo.GetAllCustomers();
            var fromAccount = customers.SelectMany(c => c.Accounts).SingleOrDefault(a => a.AccountId == fromAcc);
            var toAccount = customers.SelectMany(c => c.Accounts).SingleOrDefault(a => a.AccountId == TooAcc);

            if (amount > fromAccount.Balance || amount < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                fromAccount.Balance -= amount;
                toAccount.Balance += amount;
            }
        }
    }
}
