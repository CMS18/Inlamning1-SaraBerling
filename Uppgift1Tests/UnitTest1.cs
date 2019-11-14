using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Uppgift1UI.Models;

namespace Uppgift1Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanDeposit()
        {
            //Arrange
            var bankRepo = new BankRepository();
            var account = bankRepo.Customers.SelectMany(c => c.Accounts).SingleOrDefault(a => a.AccountId == 100);
            var initialBalance = account.Balance;

            //Act
            bankRepo.Deposit(account.AccountId, 1000m);
            var expectedBalance = initialBalance + 1000m;

            //Assert
            Assert.AreEqual(expectedBalance, account.Balance);
        }

        [TestMethod]
        public void CanWithdrawal()
        {
            //Arrange
            var bankRepo = new BankRepository();
            var account = bankRepo.Customers.SelectMany(c => c.Accounts).SingleOrDefault(a => a.AccountId == 100);
            var initialBalance = account.Balance;

            //Act
            bankRepo.Withdrawal(account.AccountId, 900m);
            var expectedBalance = initialBalance - 900m;

            //Assert
            Assert.AreEqual(expectedBalance, account.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CheckOverdraw()
        {
            var bankRepo = new BankRepository();
            bankRepo.Withdrawal(100, 847594738958);
        }
    }
}
