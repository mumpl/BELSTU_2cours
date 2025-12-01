using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class ParallelComputingTasks
{
    /*1*/
    static void TPLPrimeSearch()
    {
        Console.WriteLine("\n*** Поиск простых чисел с TPL ***");

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        Task<List<int>> primeTask = Task.Run(() => FindPrimeNumbers(1, 10000000));

        Console.WriteLine($"ID задачи: {primeTask.Id}");
        Console.WriteLine($"Статус задачи до завершения: {primeTask.Status}");

        primeTask.Wait();

        stopwatch.Stop();
        Console.WriteLine($"Статус задачи после завершения: {primeTask.Status}");
        Console.WriteLine($"Найдено простых чисел: {primeTask.Result.Count}");
        Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
    }

    static List<int> FindPrimeNumbers(int start, int end)
    {
        bool[] isPrime = new bool[end + 1];
        for (int i = 2; i <= end; i++) isPrime[i] = true;

        for (int p = 2; p * p <= end; p++)
        {
            if (isPrime[p])
            {
                for (int i = p * p; i <= end; i += p)
                    isPrime[i] = false;
            }
        }

        List<int> primes = new List<int>();
        for (int i = start; i <= end; i++)
        {
            if (isPrime[i]) primes.Add(i);
        }
        return primes;
    }
    /*2*/
    static void CancellationTokenDemo()
    {
        Console.WriteLine("\n*** Отмена задачи с CancellationToken ***");

        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        Task<List<int>> primeTask = Task.Run(() => FindPrimeNumbersWithCancellation(1, 10000000, token), token);

        cts.CancelAfter(10);

        try
        {
            primeTask.Wait();
            Console.WriteLine($"Найдено простых чисел: {primeTask.Result.Count}");
        }
        catch (AggregateException ex)
        {
            if (ex.InnerException is TaskCanceledException)
            {
                Console.WriteLine("Задача была прервана досрочно");
            }
            else
            {
                Console.WriteLine("Произошла ошибка: " + ex.InnerException.Message);
            }
        }
    }

    static List<int> FindPrimeNumbersWithCancellation(int start, int end, CancellationToken token)
    {
        bool[] isPrime = new bool[end + 1];
        for (int i = 2; i <= end; i++) isPrime[i] = true;

        for (int p = 2; p * p <= end; p++)
        {
            if (isPrime[p])
            {
                for (int i = p * p; i <= end; i += p)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Задача отменена!");
                        token.ThrowIfCancellationRequested();
                    }
                    isPrime[i] = false;
                }
            }
        }

        List<int> primes = new List<int>();
        for (int i = start; i <= end; i++)
        {
            if (isPrime[i]) primes.Add(i);
        }
        return primes;
    }

    /*3*/
    static void TasksWithResults()
    {
        Console.WriteLine("\n*** Задачи с возвратом результата ***");

        Task<int> task1 = Task.Run(() => CalculateFirst(10));
        Task<int> task2 = Task.Run(() => CalculateSecond(20));
        Task<int> task3 = Task.Run(() => CalculateThird(30));

        Task<int> finalTask = Task.WhenAll(task1, task2, task3)
            .ContinueWith(t => t.Result.Sum());

        Console.WriteLine($"Итоговый результат: {finalTask.Result}");
    }

    static int CalculateFirst(int x) => x * 2;
    static int CalculateSecond(int x) => x * 3;
    static int CalculateThird(int x) => x * 4;

    /*4*/
    static void ContinuationTasks()
    {
        Console.WriteLine("\n--- Задание 4: Continuation Tasks ---");

        Task<int> initialTask = Task.Run(() => 42);
        Task continuationTask = initialTask.ContinueWith(t =>
            Console.WriteLine($"Результат предыдущей задачи: {t.Result}")
        );

        Task<int> calculationTask = Task.Run(() => 100);
        int result = calculationTask.GetAwaiter().GetResult();
        Console.WriteLine($"Результат задачи (GetAwaiter): {result}");
    }

    /*5*/
    static void ParallelLoops()
    {
        Console.WriteLine("\n*** Параллельные циклы ***");

        Stopwatch sequentialWatch = Stopwatch.StartNew();
        for (int i = 0; i < 1000000; i++)
        {
            Math.Sin(i);
        }
        sequentialWatch.Stop();

        Stopwatch parallelWatch = Stopwatch.StartNew();
        Parallel.For(0, 1000000, i =>
        {
            Math.Sin(i);
        });
        parallelWatch.Stop();

        Console.WriteLine($"Последовательный цикл: {sequentialWatch.ElapsedMilliseconds} мс");
        Console.WriteLine($"Параллельный цикл: {parallelWatch.ElapsedMilliseconds} мс");
    }

    /*6*/
    static void ParallelInvoke()
    {
        Console.WriteLine("\n*** Parallel.Invoke() ");

        Parallel.Invoke(
            () => Console.WriteLine("Операция 1"),
            () => Console.WriteLine("Операция 2"),
            () => Console.WriteLine("Операция 3")
        );
    }

    /*7*/
    static void BlockingCollectionDemo()
    {
        Console.WriteLine("\n*** BlockingCollection ***");

        BlockingCollection<string> warehouse = new BlockingCollection<string>(5);

        var supplierTasks = new Task[5];
        var consumerTasks = new Task[10];

        for (int i = 0; i < 5; i++)
        {
            int supplierId = i;
            supplierTasks[i] = Task.Run(() =>
            {
                string product = $"товар от поставщика {supplierId}";
                warehouse.Add(product);
                Console.WriteLine($"Добавлен {product}");
            });
        }

        for (int i = 0; i < 10; i++)
        {
            int customerId = i;
            consumerTasks[i] = Task.Run(() =>
            {
                try
                {
                    string product = warehouse.Take();
                    Console.WriteLine($"Покупатель {customerId} купил {product}");
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine($"Покупатель {customerId} ушел ни с чем");
                }
            });
        }

        Task.WaitAll(supplierTasks);
        warehouse.CompleteAdding();
        Task.WaitAll(consumerTasks);
    }

    /*8*/
    static async Task AsyncAwaitDemo()
    {
        Console.WriteLine("\n*** Async/Await ***");

        int result = await PerformAsyncOperation();
        Console.WriteLine($"Результат асинхронной операции: {result}");
    }

    static async Task<int> PerformAsyncOperation()
    {
        await Task.Delay(1000);
        return 42;
    }
    static void Main(string[] args)
    {
        Console.WriteLine("параллельные вычисления");

        TPLPrimeSearch();
        CancellationTokenDemo();
        TasksWithResults();
        ContinuationTasks();
        ParallelLoops();
        ParallelInvoke();
        BlockingCollectionDemo();
        AsyncAwaitDemo().Wait();
    }
}
