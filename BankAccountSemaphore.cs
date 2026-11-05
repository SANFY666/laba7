using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace laba7
{
    public class BankAccountSemaphore : AbstractBankAccount
    {
        private readonly Mutex mutex = new Mutex();
        private readonly SemaphoreSlim depositSemaphore = new SemaphoreSlim(0);

        public BankAccountSemaphore(int initialBalance = 0) : base(initialBalance) { }

        public override void Deposit(string personName, int amount)
        {
            mutex.WaitOne();
            try
            {
                Balance += amount;
                Console.WriteLine($"{personName} поповнив рахунок на +{amount} грн. Баланс: {Balance} грн");
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            depositSemaphore.Release();
        }

        public override void Withdraw(string personName, int amount)
        {
            while (true)
            {
                mutex.WaitOne();
                if (Balance >= amount)
                {
                    Balance -= amount;
                    Console.WriteLine($"{personName} зняв -{amount} грн. Баланс: {Balance} грн");
                    mutex.ReleaseMutex();
                    break;
                }

                Console.WriteLine($"{personName} чекає: хоче зняти {amount} грн, але на рахунку {Balance} грн");
                mutex.ReleaseMutex();

                if (!depositSemaphore.Wait(2000))
                {
                    Console.WriteLine($"{personName} не дочекався поповнення і скасував операцію.");
                    break;
                }
            }
        }
    }
}
