using System;
using System.IO;
using System.Threading;

namespace lab14;

public partial class Program
{
    private static readonly object locker = new();
    private static readonly string path4 = Path.GetFullPath("../../Task4.txt");
    public static int min = 0;
    public static int max = 40;

    private static void Task4()
    {
        Console.WriteLine("\n\nTASK 4");
        Console.WriteLine("1 or 2");
        int a = int.Parse(Console.ReadLine());
        using StreamWriter writer = new(path4);

        switch(a)
        {
            case 1:
            {
                Thread evenThread = new(() => EvenNums(false, writer));
                Thread oddThread = new(() => OddNums(false, writer));

                evenThread.Start();
                evenThread.Join();
                oddThread.Start();
                oddThread.Join();

                Console.WriteLine();
                Console.ReadKey();
                break;
            }
            case 2:
            {
                Thread evenThread = new(() => EvenNums(true, writer));
                Thread oddThread = new(() => OddNums(true, writer));
                evenThread.Start();
                oddThread.Start();

                evenThread.Join();
                oddThread.Join();

                Console.WriteLine();
                Console.ReadKey();
                break;
            }
            default:
            {
                Console.WriteLine("uncorrect input");
                break;
            }
        }
    }


    static void EvenNums(bool seq, StreamWriter writer)
    {
        for(int i = 0; i < max; i += 2)
        {
            if(seq)
            {
                lock (locker)
                {
                    Console.Write($"{i} ");
                    writer.Write($"{i} ");
                    Monitor.Pulse(locker);
                    if (i + 2 < max)
                        Monitor.Wait(locker);
                }
            }
            else
            {
                Console.Write($"{i} ");
                writer.Write($"{i} ");
            }
            Thread.Sleep(30);
        }
    }

    static void OddNums(bool seq, StreamWriter writer)
    {
        for (int i = 1; i < max; i += 2)
        {
            if(seq)
            {
                lock (locker)
                {
                    Console.Write($"{i} ");
                    writer.Write($"{i} ");
                    Monitor.Pulse(locker);
                    if (i + 2 < max)
                        Monitor.Wait(locker);
                }
            }
            else
            {
                Console.Write($"{i} ");
                writer.Write($"{i} ");
            }
            Thread.Sleep(30);
        }
    }
}