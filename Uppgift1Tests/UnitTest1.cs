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
            var customers = bankRepo.GetAllCustomers();
            var account = customers.SelectMany(c => c.Accounts).SingleOrDefault(a => a.AccountId == 100);
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
            var customers = bankRepo.GetAllCustomers();
            var account = customers.SelectMany(c => c.Accounts).SingleOrDefault(a => a.AccountId == 100);
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

        [TestMethod]
        public void CheckTransfer()
        {
            //Arrange
            var bankRepo = new BankRepository();
            var customers = bankRepo.GetAllCustomers();
            var accRepo = new Account();
            var fromAccount = customers.SelectMany(c => c.Accounts).SingleOrDefault(a => a.AccountId == 100);
            var toAccount = customers.SelectMany(c => c.Accounts).SingleOrDefault(a => a.AccountId == 200);

            var fromBalance = fromAccount.Balance;
            var toBalance = toAccount.Balance;

            //Act
            accRepo.Transfer(fromAccount.AccountId, toAccount.AccountId, 50m);

            //Assert
            Assert.AreEqual((fromBalance - 50m), fromAccount.Balance);
            Assert.AreEqual((toBalance + 50m), toAccount.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CheckOverTransfer()
        {
            //Arrange
            var accRepo = new Account();

            accRepo.Transfer(100, 200, 504678754456789m);
        }
    }
}
