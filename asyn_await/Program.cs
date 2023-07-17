using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Multithreading
{
    class AsyncAwaitTasks
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Starting program...");

            // Start a new task
            Task<int> task1 = Task.Run(() => Sum(100));
            Console.WriteLine("Task 1 started.");

            // Use await to wait for the task to complete
            //wait for the first task to complete before starting the second task
            int result1 = await task1;
            Console.WriteLine("Task 1 result: {0}", result1);

            // Start another task
            Task<int> task2 = Task.Run(() => Sum(200));
            Console.WriteLine("Task 2 started.");

            // Use Task.WaitAll to wait for all tasks to complete
            Task.WaitAll(task1, task2);

            int result2 = task2.Result;
            Console.WriteLine("Task 2 result: {0}", result2);

            // Using Parallel.ForEach to process a collection in parallel
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Parallel.ForEach(numbers, async (number) =>
            {
                int result = await Double(number);
                Console.WriteLine("Number: {0}, Result: {1}", number, result);
            });

            Console.WriteLine("Program finished.");
        }

        static int Sum(int n)
        {
            int sum = 0;
            for (int i = 1; i <= n; i++)
            {
                sum += i;
            }
            return sum;
        }

        static async Task<int> Double(int n)
        {
            await Task.Delay(1000);
            return n * 2;
        }
    }
}