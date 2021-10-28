using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTasks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // First task
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            var tasks = new List<Task<int>>()
            {
                Task.Run(() => CalculateFibonacсiNumberByIndex(10, token)),
                Task.Run(() => CalculateFibonacсiNumberByIndex(20, token)),
                Task.Run(() => CalculateFibonacсiNumberByIndex(5, token))
            };

            var results = Task.WhenAll(tasks);

            foreach (var result in results.GetAwaiter().GetResult())
            {
                Console.WriteLine(result);
            }

            // Second task
            var timer = 10000;
            cancellationTokenSource.CancelAfter(timer);

            try
            {
                var task = Task.Run(() => CalculateFibonacсiNumberByIndexAsync(50, token));

                var result = task.GetAwaiter().GetResult();

                Console.WriteLine($"Fibonacci number is {result}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public static async Task<int> CalculateFibonacсiNumberByIndexAsync(int index, CancellationToken token)
        {
            return await Task.Run(() => CalculateFibonacсiNumberByIndex(index, token));
        }

        public static int CalculateFibonacсiNumberByIndex(int index, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (index == 0 || index == 1)
            {
                return index;
            }

            return CalculateFibonacсiNumberByIndex(index - 1, token) + CalculateFibonacсiNumberByIndex(index - 2, token);
        }
    }
}
