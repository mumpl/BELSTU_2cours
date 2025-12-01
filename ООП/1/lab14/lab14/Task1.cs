using System;
using System.Diagnostics;
using System.Threading;

namespace lab14;

public partial class Program
{
    private static void Task1()
    {
        Console.WriteLine("TASK 1\n");
        Console.WriteLine("Processes:");
        foreach (var process in Process.GetProcesses())
        {
            try
            {
                Console.WriteLine(
                    $"{process.Id} - {process.ProcessName ?? "Unknown"} | " +
                    $"priority: {process.BasePriority}, " +
                    $"started: {process.StartTime}, " +
                    $"status: {(process.HasExited ? "exited" : "running")}, " +
                    $"responding: {(process.Responding ? "responding" : "not responding")}, " +
                    $"processor time: {process.TotalProcessorTime.TotalMinutes:F2} minutes\n"
                );
            }
            catch { }
        }
    }
}