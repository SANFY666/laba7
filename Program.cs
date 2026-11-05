using System;
using System.Threading.Tasks;

namespace laba7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine(" 1.Семафор та м'ютекс");
            RunSimulation(new BankAccountSemaphore(100));

            Console.WriteLine("\n 2.Монітори (lock / Monitor)");
            RunSimulation(new BankAccountMonitor(100));

            Console.WriteLine("\nГотово!");
        }

        static void RunSimulation(AbstractBankAccount account)
        {
            int peopleCount = 4;
            Task[] tasks = new Task[peopleCount];

            for (int i = 0; i < peopleCount; i++)
            {
                Person person = new Person($"Клієнт {i + 1}", account, 3);
                tasks[i] = Task.Run(person.Run);
            }

            Task.WaitAll(tasks);
            Console.WriteLine($"Кінцевий баланс: {account.Balance} грн");
        }
    }
}