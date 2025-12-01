using System;
using System.IO;
using System.Threading;

namespace lab14;

public partial class Program
{
    private static readonly string path3 = Path.GetFullPath("../../Task3.txt");
    private static void Task3()
    {
        try
        {
            Console.WriteLine("\nTASK 3");
            Console.Write("Enter n: ");
            int n = int.Parse(Console.ReadLine());
            Thread thread = new(() => CalculatePrimes(n));
            thread.Start();

            while (thread.IsAlive)
            {
                Console.WriteLine($"Thread's ID: {thread.ManagedThreadId}, Priority: {thread.Priority}, State: {thread.ThreadState}");
                Thread.Sleep(500);
            }
            thread.Join();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in Task3: {ex.Message}");
        }
    }

    static void CalculatePrimes(int n)
    {
        using StreamWriter str = new(path3, append: false);
        for (int i = 0; i < n; i++)
        {
            if (IsPrime(i))
            {
                Console.WriteLine($"{i} ");
                str.Write($"{i} ");
                Thread.Sleep(50);
            }
        }
    }

    static bool IsPrime(int num)
    {
        if (num < 2) return false;
        for (int i = 2; i <= Math.Sqrt(num); i++)
            if(num % i == 0) return false;
        return true;
    }
}