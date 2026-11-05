using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace laba7
{
    public class Person
    {
        private static readonly Random random = new Random();

        public string Name { get; }
        public AbstractBankAccount Account { get; }
        public int OperationsCount { get; }

        public Person(string name, AbstractBankAccount account, int operationsCount)
        {
            Name = name;
            Account = account;
            OperationsCount = operationsCount;
        }

        public void Run()
        {
            Console.WriteLine($"{Name} почав роботу з рахунком.");
            for (int i = 0; i < OperationsCount; i++)
            {
                bool isDeposit = random.Next(2) == 0;
                int amount = random.Next(50, 250);

                if (isDeposit)
                    Account.Deposit(Name, amount);
                else
                    Account.Withdraw(Name, amount);

                Thread.Sleep(random.Next(400, 900));
            }
            Console.WriteLine($"{Name} завершив свої операції.");
        }
    }
}
