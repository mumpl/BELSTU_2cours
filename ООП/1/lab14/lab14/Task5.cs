using System;
using System.Threading;

namespace lab14;

public partial class Program
{
    private static void Task5()
    {
        Console.WriteLine("\n\nTASK 5");
        TimerCallback tcb = new(TimeNow);
        Timer timer = new(tcb, null, 1000 - DateTime.Now.Millisecond, 1000);
        Console.WriteLine("Timer started. Press Enter to stop...");
        Console.ReadLine();

        timer.Dispose();
        Console.WriteLine("Timer stopped.");
    }

    static void TimeNow(object obj)
    {
        Console.WriteLine($"Time: {DateTime.Now:HH:mm:ss}");
    }
}