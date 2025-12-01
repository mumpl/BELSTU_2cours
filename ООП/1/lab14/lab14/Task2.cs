using System;
using System.Reflection;

namespace lab14;

public partial class Program
{
    private static void Task2()
    {
        Console.WriteLine("\nTASK 2\n");

        AppDomain currentDomain = AppDomain.CurrentDomain;
        Console.WriteLine($"Текущий домен: {currentDomain.FriendlyName}");
        Console.WriteLine("Детали конфигурации:");
        Console.WriteLine($"  Базовый каталог: {currentDomain.BaseDirectory}");
        Console.WriteLine($"  Конфигурационный файл: {currentDomain.SetupInformation.ConfigurationFile}");

        Console.WriteLine("\nСборки, загруженные в текущий домен:");
        foreach (Assembly assembly in currentDomain.GetAssemblies())
        {
            Console.WriteLine($"  {assembly.FullName}");
        }

        Console.WriteLine("\nСоздание нового домена...");
        AppDomain newDomain = AppDomain.CreateDomain("NewDomain");

        try
        {
            newDomain.Load("lab14");

            Console.WriteLine($"\nНовый домен: {newDomain.FriendlyName}");
            Console.WriteLine("Сборки в новом домене:");
            foreach (Assembly assembly in newDomain.GetAssemblies())
            {
                Console.WriteLine($"  {assembly.FullName}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке сборки: {ex.Message}");
        }

        AppDomain.Unload(newDomain);
        Console.WriteLine("Новый домен успешно выгружен.");
    }
}