using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
namespace _AccountTask
{ 
    public class Account // Class for Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string name = "Unnamed Account", double balance = 0.0)
        {
            this.Name = name;
            this.Balance = balance;
        }

        public virtual bool Deposit(double amount) // Method for deposit
        {
            if (amount < 0)
                return false;
            else
            {
                Balance += amount;
                return true;
            }
        }

        public virtual bool Withdraw(double amount) // Method for withdraw
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            return $"Name: {Name}, Balance: {Balance:C}";
        }
    }

    public class SavingsAccount : Account // Class for SavingsAccount
    {
        public double TaxRate { get; set; }
        public SavingsAccount(string name = "Unnamed Savings Account", double balance = 0.0, double taxRate = 0.0)
            : base(name, balance)
        {
           TaxRate = taxRate;
        }
        public override bool Deposit(double amount)
        {
            if (amount < 0) return false;
            double totalWithInterest = amount + (amount * (TaxRate / 100));
            return base.Deposit(totalWithInterest);
        }
        public override string ToString()
        {
            return $"Name: {Name}, Balance: {Balance:C}, Tax Rate: {TaxRate}%";
        }
    }

    public class CheckingAccount : Account // Class for CheckingAccount
    {
        private const double Fee = 1.50;
        public CheckingAccount(string name = "Unnamed Checking Account", double balance = 0.0)
            : base(name, balance)
        { 
        }
        public override bool Withdraw(double amount)
        {
            return base.Withdraw(amount + Fee);
        }
        public override string ToString()
        {
            return $"Name: {Name}, Balance: {Balance:C}, Fee: {Fee:C}";
        }
    }

    public class TrustAccount : SavingsAccount // Class for TrustAccount
    {
        private const double BounsThreshold = 5000.0;
        private const double BounsAmount = 50.0;
        private const int MaxWithdrawals = 3;
        private const double MaxWithdrawalsPercent = 0.20;
        public int WithdrawalsCount = 0;
        public TrustAccount(string name = "Unnamed Trust Account", double balance = 0.0, double taxRate = 0.0)
            : base(name, balance, taxRate)
        {
        }
        public override bool Deposit(double amount)
        {
            if (amount < 0) return false;
            if (amount >= BounsThreshold)
            {
                amount += BounsAmount;
            }
            return base.Deposit(amount);
        } 
        public override bool Withdraw(double amount)
        {
            if (WithdrawalsCount >= MaxWithdrawals || amount > (Balance * MaxWithdrawalsPercent))
            {
                return false;
            }
            if (base.Withdraw(amount))
            {
                WithdrawalsCount++;
                return true;            
            }
            return false;
        }
        public override string ToString()
        {
            return $"Name: {Name}, Balance: {Balance:C}, Tax Rate: {TaxRate}%, Bouns Threshold: {BounsThreshold}, Bouns Amount: {BounsAmount}, Max Withdrawals: {MaxWithdrawals}, Max Withdrawals Percent: {MaxWithdrawalsPercent}, Withdrawals Count: {WithdrawalsCount}";
        }
    }

    public static class AccountUtil
    {
        // Utility helper functions for Account class
        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }

        // Helper functions for SavingsAccount
        public static void DepositSavings(List<SavingsAccount> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Savings Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void WithdrawSavings(List<SavingsAccount> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Savings Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }

        // Helper functions for CheckingAccount
        public static void DepositChecking(List<CheckingAccount> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Checking Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void WithdrawChecking(List<CheckingAccount> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Checking Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }

        // Helper functions for TrustAccount
        public static void DepositTrust(List<TrustAccount> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Trust Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void WithdrawTrust(List<TrustAccount> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Trust Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }
    } 

    internal class Program
    {
        static void Main(string[] args)
        {
            // Accounts
            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            // Savings
            var savAccounts = new List<SavingsAccount>();
            savAccounts.Add(new SavingsAccount());
            savAccounts.Add(new SavingsAccount("Superman"));
            savAccounts.Add(new SavingsAccount("Batman", 2000));
            savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

            AccountUtil.DepositSavings(savAccounts, 1000);
            AccountUtil.WithdrawSavings(savAccounts, 2000);

            // Checking
            var checAccounts = new List<CheckingAccount>();
            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));

            AccountUtil.DepositChecking(checAccounts, 1000);
            AccountUtil.WithdrawChecking(checAccounts, 2000);
            AccountUtil.WithdrawChecking(checAccounts, 2000);

            // Trust
            var trustAccounts = new List<TrustAccount>();
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount("Superman2"));
            trustAccounts.Add(new TrustAccount("Batman2", 2000));
            trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

            AccountUtil.DepositTrust(trustAccounts, 1000);
            AccountUtil.DepositTrust(trustAccounts, 6000);
            AccountUtil.WithdrawTrust(trustAccounts, 2000);
            AccountUtil.WithdrawTrust(trustAccounts, 3000);
            AccountUtil.WithdrawTrust(trustAccounts, 500);

            Console.WriteLine(); 
        }
    }
}