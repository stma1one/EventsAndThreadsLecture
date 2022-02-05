using System;
using System.Threading.Tasks;


namespace CoreCollectionsAsync
{
    public class AccountDemo
    {
        private int balance;

        public AccountDemo(int initialBalance)
        {
            this.balance = initialBalance;
        }

        public int Debit(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "The debit amount cannot be negative.");
            }

            int appliedAmount = 0;
            if (balance >= amount)
            {
                balance -= amount;
                appliedAmount = amount;
            }
            
            return appliedAmount;
        }

        public void Credit(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "The credit amount cannot be negative.");
            }

            balance += amount;
        }

        public int GetBalance()
        {
            return balance;
        }
    }

    class AccountTestDemo
    {
        public static void  StartDemo()
        {
            AccountDemo account = new AccountDemo(0);
            Task [] tasks = new Task[100];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => Update(account));
            }
            Task t =  Task.WhenAll(tasks);
            t.Wait();
            Console.WriteLine($"Account's balance is {account.GetBalance()}");
        }

        static void Update(AccountDemo account)
        {
            int[] amounts = { 1, 2, 3, -6, 2, 1, 8, -11, 6, -6 };
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