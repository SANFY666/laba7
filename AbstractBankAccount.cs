using System;
using System.Collections.Generic;
using System.Text;

namespace laba7
{
    public abstract class AbstractBankAccount
    {
        public int Balance { get; protected set; }

        public AbstractBankAccount(int initialBalance = 0)
        {
            if (initialBalance < 0)
                throw new ArgumentException("Початковий баланс не може бути від'ємним.");
            Balance = initialBalance;
        }

        public abstract void Deposit(string personName, int amount);
        public abstract void Withdraw(string personName, int amount);
    }
}
