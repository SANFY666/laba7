using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace laba7
{
    public class BankAccountMonitor : AbstractBankAccount
    {
        private readonly object lockObject = new object();

        public BankAccountMonitor(int initialBalance = 0) : base(initialBalance) { }

        public override void Deposit(string personName, int amount)
        {
            lock (lockObject)
            {
                Balance += amount;
                Console.WriteLine($"{personName} поповнив рахунок на +{amount} грн. Баланс: {Balance} грн");
                Monitor.PulseAll(lockObject);
            }
        }

        public override void Withdraw(string personName, int amount)
        {
            lock (lockObject)
            {
                while (Balance < amount)
                {
                    Console.WriteLine($"{personName} чекає: хоче зняти {amount} грн, але на рахунку {Balance} грн");

                    if (!Monitor.Wait(lockObject, 2000))
                    {
                        Console.WriteLine($"{personName} не дочекався поповнення і скасував операцію.");
                        return;
                    }
                }

                Balance -= amount;
                Console.WriteLine($"{personName} зняв -{amount} грн. Баланс: {Balance} грн");
            }
        }
    }
}
