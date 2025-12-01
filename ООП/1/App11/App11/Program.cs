using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class Car
{
    public string Model { get; set; }
    public string Manufacturer { get; set; }
    public int Year { get; set; }

    public Car(string model, string manufacturer, int year)
    {
        Model = model;
        Manufacturer = manufacturer;
        Year = year;
    }
    public string DisplayInfo(string prefix, int value)
    {
        return $"{prefix}: {Manufacturer} {Model} ({Year}) - {value}";
    }
    public override string ToString()
    {
        return $"{Manufacturer} {Model} ({Year})";
    }
}
public class Vector
{
    public int[] elements;
    public int size;
    public int id;
    public int count;
    public int ID => id;
    public int[] Elements
    {
        get { return elements; }
        set { elements = value; }
    }
    public int Size
    {
        get { return size; }
        set { size = value; }
    }
    public Vector(int[] elements)
    {
        this.size = elements.Length;
        this.elements = elements;
        this.id = this.GetHashCode();
        count++;
    }
}
public static class Reflector
{
    public static void GetAssemblyName(string className)
    {
        Type type = Type.GetType(className);
        if (type == null)
        {
            Console.WriteLine($"Класс {className} не найден.");
            return;
        }
        Console.WriteLine($"Сборка: {type.Assembly.FullName}");
    }
    public static void HasPublicConstructors(string className)
    {
        Type type = Type.GetType(className);
        if (type == null)
        {
            Console.WriteLine($"Класс {className} не найден.");
            return;
        }
        bool hasPublicConstructors = type.GetConstructors().Any();
        Console.WriteLine(hasPublicConstructors ? $"Есть публичные конструкторы в {className}" : $"Нет публичных конструкторов в {className}" );
    }
    public static IEnumerable<string> GetPublicMethods(string className)
    {
        Type type = Type.GetType(className);
        if (type == null)
        {
            yield return $"Класс {className} не найден.";
            yield break;
        }
        foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
        {
            yield return method.Name; 
        }
    }
    public static IEnumerable<string> GetFieldAndProperties(string className)
    {
        Type type = Type.GetType(className);
        if (type == null)
        {
            yield return $"Класс {className} не найден.";
            yield break;
        }
        foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance| BindingFlags.Static))
        {
            yield return $"Поле: {field.Name}";
        }
        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
        {
            yield return $"Свойство: {property.Name}";
        }
    }
    public static IEnumerable<string> GetImplementedInterfaces(string className)
    {
        Type type = Type.GetType(className);
        if (type == null)
        {
            yield return $"Класс {className} не найден.";
            yield break;
        }
        foreach(var implementedInterface in type.GetInterfaces())
        {
            yield return implementedInterface.Name;
        }
    }
    public static IEnumerable<string> GetMethodsByParameterType(string className, Type parameterType)
    {
        Type type = Type.GetType(className);
        if (type == null)
        {
            yield return $"Класс {className} не найден.";
            yield break;
        }
        foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
        {
            if (method.GetParameters().Any(p => p.ParameterType == parameterType))
            {
                yield return method.Name;
            }
        }
    }
    public static void Invoke(string className, string methodName, string filePath = null, object obj = null)
    {
        Type type = Type.GetType(className);
        if (type == null)
        {
            Console.WriteLine($"Класс {className} не найден.");
            return;
        }

        var method = type.GetMethod(methodName);
        if (method == null)
        {
            Console.WriteLine($"Метод {methodName} не найден.");
            return;
        }

        var parameters = method.GetParameters();
        object[] parameterValues = new object[parameters.Length];

        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
        {
            Console.WriteLine($"Чтение параметров из файла: {filePath}");
            var fileContent = File.ReadAllLines(filePath);
            var paramDict = fileContent.Select(line => line.Split('=')).ToDictionary(parts => parts[0].Trim(), parts => parts[1].Trim());

            for (int i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                if (paramDict.ContainsKey(param.Name))
                {
                    parameterValues[i] = Convert.ChangeType(paramDict[param.Name], param.ParameterType);
                }
                else
                {
                    parameterValues[i] = GenerateValue(param.ParameterType);
                }
            }
        }
        else
        {
            Console.WriteLine("Генерация значений для параметров.");
            for (int i = 0; i < parameters.Length; i++)
            {
                parameterValues[i] = GenerateValue(parameters[i].ParameterType);
            }
        }
        object result = method.Invoke(obj, parameterValues);
        Console.WriteLine($"Результат вызова метода {methodName}: {result}");        
    }
    private static object GenerateValue(Type type)
    {
        if (type == typeof(int))
            return 15;
        if (type == typeof(string))
            return "default";
        if (type == typeof(double))
            return 3.14;
        if (type == typeof(bool))
            return true;
        return Activator.CreateInstance(type);
    }
    public static T Create<T>() where T : class
    {
        Type type = typeof(T);
        var constructor = type.GetConstructors().FirstOrDefault();
        if (constructor == null)
        {
            throw new InvalidOperationException($"Класс {type.Name} не имеет публичного конструктора");
        }
        var parameters = constructor.GetParameters();
        object[] parameterValues = new object[parameters.Length];
        for (int i = 0; i < parameters.Length; i++)
        {
            parameterValues[i] = GenerateValue(parameters[i].ParameterType);
        }
        return (T)constructor.Invoke(parameterValues);
    }
}
public class Program
{
    public static void Main()
    {
        Reflector.GetAssemblyName("Car");
        Reflector.GetAssemblyName("Vector");

        Reflector.HasPublicConstructors("Car");
        Reflector.HasPublicConstructors("Vector");

        Console.WriteLine("----- Методы Car -----");
        foreach (var method in Reflector.GetPublicMethods("Car"))
            Console.WriteLine(method);

        Console.WriteLine("----- Поля и свойства Vector -----");
        foreach (var member in Reflector.GetFieldAndProperties("Vector"))
            Console.WriteLine(member);

        Console.WriteLine("----- Интерфейсы Vector -----");
        foreach (var iface in Reflector.GetImplementedInterfaces("Car"))
            Console.WriteLine(iface);

        Console.WriteLine("----- Методы с заданным типом параметра Car -----");
        foreach (var method in Reflector.GetMethodsByParameterType("Car", typeof(string)))
            Console.WriteLine(method);

        var car = new Car("ModelX", "Tesla", 2022);
        string filePath = "params.txt";
        File.WriteAllText(filePath, "prefix=Car\nvalue=10");
        Reflector.Invoke("Car", "DisplayInfo", filePath, car);

    }
}
