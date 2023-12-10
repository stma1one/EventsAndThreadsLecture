using System;
using System.Threading;
using System.Threading.Tasks;


namespace CoreCollectionsAsync
{
    public class Account
    {
        private int balance;

        public Account(int initialBalance)
        {
            this.balance = initialBalance;
        }

        public void Debit(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "The debit amount cannot be negative.");
            }

            if (balance >= amount)
            {
                balance = balance - amount;
            }
            return;
        }

        public void Credit(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "The credit amount cannot be negative.");
            }

            balance = balance + amount;
        }

        public int GetBalance()
        {
             return balance;
        }
    }

    public class AccountWithLocks
    {
        public string Name { get; set; }
        private object balanceLock;
        private int balance;
        public AccountWithLocks(int initialBalance)
        {
            this.balanceLock = new object();
            this.balance = initialBalance;
        }
        public void Debit(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "The debit amount cannot be negative.");
            }

            lock (balanceLock)
            {
                if (balance >= amount)
                {
                    balance = balance - amount;
                }
            }
            return;
        }
        public void Credit(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "The credit amount cannot be negative.");
            }

            lock (balanceLock)
            {
                balance = balance + amount;
            }
        }
        public int GetBalance()
        {
            lock (balanceLock)
            {
                return balance;
            }
        }
    }

    class AccountTest
    {
        static Account account;
        public static void StartDemo1()
        {
            account = new Account(0);
            
            Task[] tasks = new Task[100]; 
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(Update);    
            }

            //Wait for all tasks to finish
            Task.WhenAll(tasks).Wait();
            
            Console.WriteLine($"Account's balance is {account.GetBalance()}");
        }

        static void Update()
        {
            int [] amounts = { 1, 2, 3, -6, 2, 1, 8, -11, 6, -6, 1, 2, 3, -6, 2, 1, 8, -11, 6, -6 };
            foreach (int amount in amounts)
            {
                if (amount >= 0)
                {
                    account.Credit(amount);
                }
                else
                {
                    account.Debit(Math.Abs(amount));
                }
            }
        }
    }
}