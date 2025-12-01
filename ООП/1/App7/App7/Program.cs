using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
public abstract class Software : IComparable<Software>
{
    public string Name { get; set; }
    public string Version { get; set; }
    public Software(string name, string version)
    {
        Name = name;
        Version = version;
    }
    public virtual void Run()
    {
        Console.WriteLine($"{Name} версия {Version} запущена");
    }
    public abstract void Shutdown();
    public override string ToString()
    {
        return $"ПО: {Name}, Версия: {Version}";
    }
    public int CompareTo(Software other)
    {
        if (other == null) return 1;
        return string.Compare(Version, other.Version, StringComparison.Ordinal);
    }
    public static Software FromString(string str)
    {
        var parts = str.Split(',');
        if (parts.Length == 2)
        {
            return new TextProcessor(parts[0], parts[1]);
        }
        throw new FormatException("Некорректный формат строки для Software");
    }
}
public class TextProcessor : Software
{ 
    public TextProcessor(string name, string version) : base(name, version) { }
    public override void Shutdown()
    {
        Console.WriteLine($"{Name} отключается");
    }
}
public class Word : TextProcessor
{
    public Word(string version) : base("Microsoft Word", version) { }
    public override void Run()
    {
        Console.WriteLine($"Текстовый процессор {Name} версии {Version} запущен");
    }
    public override string ToString()
    {
        return $"{Name} - Текстовый процессор, Версия - {Version}";
    }
}
public interface IOperations<T>
{
    void Add(T item);
    bool Remove(T item);
    void Watch();
    T Find(Predicate<T> match);
}
public class CollectionType<T> : IOperations<T> where T : IComparable<T>
{
    private List<T> collection;
    public CollectionType(int size = 0)
    {
        collection = new List<T>(new T[size]);
    }
    public T this[int index]
    {
        get { return collection[index]; }
        set { collection[index] = value; }
    }
    public T[] GetArray()
    {
        return collection.ToArray();
    }
    public void SetArray(T[] newArray)
    {
        collection = new List<T>(newArray);
    }
    public void Watch()
    {
        Console.WriteLine("Коллекция: ");
        Console.WriteLine(string.Join(", ", collection));
    }
    public void Add(T item)
    {
        try
        {
            collection.Add(item);
            Console.WriteLine($"Значение {item} добавлено в коллекцию");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка! {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Добавление завершено");
        }
    }
    public bool Remove(T item)
    {
        try
        {
            if (collection.Remove(item))
            {
                Console.WriteLine($"Элемент {item} удален из коллекции");
                return true;
            }
            else
            {
                Console.WriteLine($"Элемент {item} не найден в коллекции");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка! {ex.Message}");
            return false;
        }
        finally
        {
            Console.WriteLine("Удаление завершено");
        }
    }
    public T Find(Predicate<T> match)
    {
        try
        {
            T result = collection.Find(match);
            if (!result.Equals(default(T)))
            {
                Console.WriteLine($"Элемент найден - {result}");
                return result;
            }
            else
            {
                Console.WriteLine("Элемент не найден");
                return default(T);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
            return default(T);
        }
        finally
        {
            Console.WriteLine("Поиск завершен");
        }
    }
    public void SaveToFile(string filePath)
    {
        try
        {
            using StreamWriter writer = new StreamWriter(filePath);
            foreach (var item in collection)
            {
                writer.WriteLine(item.ToString());
            }
            Console.WriteLine($"Коллекция записана в файл {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при соединении. {ex.Message}");
        }
    }
    public void LoadFromFile(string filePath, Func<string, T> fromString)
    {
        try
        {
            collection.Clear();
            foreach (var line in File.ReadLines(filePath))
            {
                collection.Add(fromString(line));
            }
            Console.WriteLine("Коллекция загружена из файла");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка.{ex.Message}");
        }
    }
    public static CollectionType<T> operator *(CollectionType<T> a, CollectionType<T> b)
    {
        int minSize = Math.Min(a.collection.Count, b.collection.Count);
        var result = new CollectionType<T>(minSize);
        for (int i = 0; i < minSize; i++)
        {
            dynamic valA = a[i];
            dynamic valB = b[i];
            result[i] = (T)(valA * valB);
        }
        return result;
    }

    public static bool operator true(CollectionType<T> a)
    {
        return a.collection.All(x => Convert.ToDouble(x) >= 0);
    }
    public static bool operator false(CollectionType<T> a)
    {
        return a.collection.All(x => Convert.ToDouble(x) < 0);
    }

    public static explicit operator int(CollectionType<T> a)
    {
        return a.collection.Count;
    }

    public static bool operator ==(CollectionType<T> a, CollectionType<T> b)
    {
        return a.collection.SequenceEqual(b.collection);
    }
    public static bool operator !=(CollectionType<T> a, CollectionType<T> b)
    {
        return !(a == b);
    }

    public static bool operator <(CollectionType<T> a, CollectionType<T> b)
    {
        return a.collection.Sum(item => Convert.ToDouble(item)) < b.collection.Sum(item => Convert.ToDouble(item));
    }
    public static bool operator >(CollectionType<T> a, CollectionType<T> b)
    {
        return a.collection.Sum(item => Convert.ToDouble(item)) > b.collection.Sum(item => Convert.ToDouble(item));
    }
    public override bool Equals(object obj)
    {
        return obj is CollectionType<T> collection && EqualityComparer<List<T>>.Default.Equals(this.collection, collection.collection);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(collection);
    }
}
public static class StatisticOperation
{
    public static double Sum<T>(CollectionType<T> array) where T : IComparable<T>
    {
        return array.GetArray().Sum(item => Convert.ToDouble(item));
    }
    public static double DifferenceMaxMin<T>(CollectionType<T> array) where T : IComparable<T>
    {
        double max = array.GetArray().Max(item => Convert.ToDouble(item));
        double min = array.GetArray().Min(item => Convert.ToDouble(item));
        return max - min;
    }
    public static int CountElements<T>(CollectionType<T> array) where T : IComparable<T>
    {
        return array.GetArray().Length;
    }
}
class Program
{
    static void Main(string[] args)
    {
        var softwareCollection = new CollectionType<Software>();
        softwareCollection.Add(new TextProcessor("Microsoft Word","2024"));
        softwareCollection.Add(new TextProcessor("WriteBender", "1.11"));
        softwareCollection.Watch();

        var foundSoftware = softwareCollection.Find(s => s.Name == "Microsoft Word");
        Console.WriteLine(foundSoftware != null ? $"Найден элемент: {foundSoftware}" : "Элемент не найден");

        string filePath = "file.txt";
        softwareCollection.SaveToFile(filePath);

        var collection1 = new CollectionType<int>(5);
        var collection2 = new CollectionType<int>(5);

        var doubleCollection = new CollectionType<double>();
        doubleCollection.Add(1.2);
        doubleCollection.Add(0.9);
        doubleCollection.Watch();

        var boolCollection = new CollectionType<bool>();
        boolCollection.Add(true);
        boolCollection.Add(false);
        boolCollection.Add(true);
        boolCollection.Watch();

        var charCollection = new CollectionType<char>();
        charCollection.Add('l');
        charCollection.Add('i');
        charCollection.Add('z');
        charCollection.Add('a');
        charCollection.Watch();

        collection1[0] = 1; collection1[1] = 2; collection1[2] = -3; collection1[3] = 4; collection1[4] = 5;
        collection2[0] = 2; collection2[1] = 4; collection2[2] = 6; collection2[3] = 8; collection2[4] = 10;

        var resultCollection = collection1 * collection2;
        Console.WriteLine("Умноженный массив:");
        resultCollection.Watch();

        bool isEqual = collection1 == collection2;
        Console.WriteLine($"Коллекции равны? { isEqual}");
        bool comparsion = collection1 < collection2;    
        Console.WriteLine($"Первая коллекция меньше второй?{comparsion}");

        if (collection1)
        {
            Console.WriteLine("Первая коллекция содержит только положительные элементы");
        }
        else
        {
            Console.WriteLine("Первая коллекция содержит отрицательные элементы");
        }

        Console.WriteLine("Сумма элементов первой колеекции: " + StatisticOperation.Sum(collection1));
        Console.WriteLine("Разница между макс и мин " + StatisticOperation.DifferenceMaxMin(collection1));
        Console.WriteLine("Кол-во элементов в массиве " + StatisticOperation.CountElements(collection1));

        collection1.Add(9);
        Console.WriteLine("Первая коллекция после добавления элемента:");
        collection1.Watch();

        collection1.Remove(9);
        Console.WriteLine("Первая коллекция после удаления этого элемента:");
        collection1.Watch();
    }
}
