using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift1UI.Models
{
    public class BankRepository
    {
        public List<Customer> Customers = new List<Customer>
        {
            new Customer
            {
                Id = 1,
                Name = "Sara",
                Accounts = new List<Account>
                {
                    new Account
                    {
                        AccountId = 100,
                        Balance = 1000
                    }
                }
            },
            new Customer
            {
                Id = 2,
                Name = "Malin",
                Accounts = new List<Account>
                {
                    new Account
                    {
                        AccountId = 200,
                        Balance = 2000
                    }
                }
            },
            new Customer
            {
                Id = 3,
                Name = "Fredrik",
                Accounts = new List<Account>
                {
                    new Account
                    {
                        AccountId = 300,
                        Balance = 3000
                    }
                }
            }

        };
        public List<Customer> GetAllCustomers()
        {
            return Customers;
        }

        public void Deposit(int accountNr, decimal amount)
        {
            var account = Customers.SelectMany(c => c.Accounts).SingleOrDefault(a => a.AccountId == accountNr);

            if (account == null || amount < 0) throw new Exception();

            account.Balance += amount;



        }

        public void Withdrawal(int accountNr, decimal amount)
        {
            var account = Customers.SelectMany(c => c.Accounts).SingleOrDefault(a => a.AccountId == accountNr);

            if (amount > account.Balance || amount < 0) throw new ArgumentOutOfRangeException();
            account.Balance -= amount;
        }
    }

}
