using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCollectionsAsync
{
    class DeadLock
    {
        static AccountWithLocks ac1, ac2;
        static void TransferMoney(AccountWithLocks from, AccountWithLocks to, int amount)
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Thread {id} - locking {from.Name}");
            lock(from)
            {
                Console.WriteLine($"Thread {id} - Waiting for locking {to.Name}");
                lock (to)
                {
                    from.Debit(amount);
                    to.Credit(amount);
                }
            }
        }
        static void TransferMoneyFromAc1ToAc2()
        {
            TransferMoney(ac1, ac2, 100);
        }
        static void TransferMoneyFromAc2ToAc1()
        {
            TransferMoney(ac2, ac1, 500);
        }
        public static void StartTest()
        {
            ac1 = new AccountWithLocks(1000)
            {
                Name = "Ac1"
            };

            ac2 = new AccountWithLocks(1000)
            {
                Name = "Ac2"
            };

            
            Task t1 = Task.Run(TransferMoneyFromAc1ToAc2);
            Task t2 = Task.Run(TransferMoneyFromAc2ToAc1);
            Task.WhenAll(t1, t2).Wait();

            Console.WriteLine("Operation Completed!");
        }
    }


}
