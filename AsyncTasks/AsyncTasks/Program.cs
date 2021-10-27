using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTasks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;
            var timer = new Stopwatch();

            timer.Start();

            try
            {
                var task = Task.Run(() => CalculateFibonacсiNumberByIndexAsync(50, timer, token, cancellationTokenSource));

                int? result = task.GetAwaiter().GetResult();

                Console.WriteLine($"Fibonacci number is {result.Value}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public static async Task<int> CalculateFibonacсiNumberByIndexAsync(int index, Stopwatch timer, CancellationToken token, CancellationTokenSource cancellationTokenSource)
        {
            return await Task.Run(() => CalculateFibonacсiNumberByIndex(index, timer, token, cancellationTokenSource));
        }

        public static int CalculateFibonacсiNumberByIndex(int index, Stopwatch timer, CancellationToken token, CancellationTokenSource cancellationTokenSource)
        {
            if (timer.ElapsedMilliseconds > 10000)
            {
                cancellationTokenSource.Cancel();
            }

            if (token.IsCancellationRequested)
            {
                throw new Exception("Execution time was more than 10 seconds!");
            }

            if (index == 0 || index == 1)
            {
                return index;
            }

            return CalculateFibonacсiNumberByIndex(index - 1, timer, token, cancellationTokenSource) + CalculateFibonacсiNumberByIndex(index - 2, timer, token, cancellationTokenSource);
        }
    }
}
