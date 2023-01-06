using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccount;
using System;
using MySql.Data.MySqlClient;
using System.Data;
using Bank.BL;

namespace BankTests
{
    [TestClass]
    public class BankAccountTests
    {
        [TestMethod]
        
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount.BankAccount account = new BankAccount.BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act
            account.Debit(debitAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }
        [TestMethod]
        public void Number_of_Customers()
        {
            Customers c = new Customers();
            Assert.AreEqual(6, c.GetAll().Rows.Count);
            // Assert.Pass();
        }
        [TestMethod]
        public void Delete_all_Customers()
        {
            Customers c = new Customers();
            c.DeleteAll();
            Assert.AreEqual(0, c.DeleteAll().Rows.Count);
            // Assert.Pass();
        }
        [TestMethod]
        public void Insert_Customer()
        {
            Customers c = new Customers();
            c.DeleteAll();
            c.name = "Henrik";
            c.balance = 24.99;
            c.Insert();
            DataTable res = c.GetAll();
            Assert.AreEqual(1, res.Rows.Count);
            //StringAssert.AreEqualIgnoringCase("Henrik", (string)res.Rows[0]["Name"]);
            //Assert.AreEqual(24.99, (double)res.Rows[0]["balance"]);
        }
        [TestMethod]
        public void Insert_NameIsNull_NotInsert()
        {
            Customers c = new Customers();
            c.DeleteAll();
            try
            {
                c.name = null;
                c.balance = 24.99;
                c.Insert();
            }
            catch
            {
            }
            DataTable res = c.GetAll();
            Assert.AreEqual(0, res.Rows.Count);
        }
        [TestMethod]
        public void Update_Normal_OK()
        {
            Customers c = new Customers();
            c.DeleteAll();

            c.name = "Henrik";
            c.balance = 24.99;
            c.Insert();

            c.name = "Rasmus";
            c.balance = 32.99;
            c.Update();

            DataTable res = c.GetAll();
            Assert.AreEqual(1, res.Rows.Count);
            //StringAssert.AreEqualIgnoringCase("John", (string)res.Rows[0]["Name"]);
            //StringAssert.AreEqualIgnoringCase("dept2", (string)res.Rows[0]["dept"]);
            //Assert.AreEqual(4000.99, (double)res.Rows[0]["salary"]);
        }
        [TestMethod]
        public void GetByName_Like_MatchAndNotMatch_OK()
        {
            Customers c = new Customers();
            c.DeleteAll();

            c.name = "Tomas";
            c.balance = 3000.05;
            c.Insert();

            Customers c2 = new Customers();
            c2.name = "Tomy";
            c2.balance = 4000.99;
            c2.Insert();

            DataTable res = c.GetByName_Like("Tom");

            //CollectionAssert.IsNotEmpty(res.Rows);
            Assert.AreEqual(1, res.Rows.Count);
            //StringAssert.AreEqualIgnoringCase("Tomas", (string)res.Rows[0]["Name"]);

            //Not match
            //Assert.AreEqual(0, c.GetByName_Like("Tomtom").Rows.Count);
            //CollectionAssert.IsEmpty(c.GetByName_Like("Tomtom").Rows);
        }
    }
}
