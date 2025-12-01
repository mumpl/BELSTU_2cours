using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Linq;
using System.Diagnostics;

public interface IOperations
{
    void Add(int item);
    bool Remove(int item);
    void Watch();
}
public class Array : IOperations
{
    private List<int> arr;

    public Array(int size, Production prod, Developer dev)
    {
        arr = new List<int>(new int[size]);
        Prod = prod;
        Dev = dev;
    }
    public int this[int index]
    {
        get { return arr[index]; }
        set { arr[index] = value; }
    }
    public int[] GetArray()
    {
        return arr.ToArray();
    }
    public void SetArray(int[] newArray)
    {
        arr = new List<int>(newArray);
    }
    public void DisplayArray()
    {
        Console.WriteLine(string.Join(", ", arr));
    }

    public static Array operator *(Array a, Array b)
    {
        int minSize = Math.Min(a.arr.Count, b.arr.Count);
        Array result = new Array(minSize, a.Prod, a.Dev);
        for (int i = 0; i < minSize; i++)
        {
            result[i] = a[i] * b[i];
        }
        return result;
    }

    public static bool operator true(Array a)
    {
        return a.arr.All(x => x >= 0);
    }
    public static bool operator false(Array a)
    {
        return a.arr.All(x => x < 0);
    }

    public static explicit operator int(Array a)
    {
        return a.arr.Count;
    }

    public static bool operator ==(Array a, Array b)
    {
        return a.arr.SequenceEqual(b.arr);
    }
    public static bool operator !=(Array a, Array b)
    {
        return !(a == b);
    }

    public static bool operator <(Array a, Array b)
    {
        return a.arr.Sum() < b.arr.Sum();
    }
    public static bool operator >(Array a, Array b)
    {
        return a.arr.Sum() > b.arr.Sum();
    }
    public void Add(int item)
    {
        arr.Add(item);
    }
    public bool Remove(int item)
    {
        return arr.Remove(item);
    }
    public void Watch()
    {
        DisplayArray();
    }

    public class Production
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; }
        public Production(int id, string orgName)
        {
            Id = id;
            OrganizationName = orgName;
        }
        public override string ToString()
        {
            return $"Production id: {Id}, organization: {OrganizationName}";
        }
    }

    public class Developer
    {
        public string FullName { get; set; }
        public int Id { get; set; }
        public string Department { get; set; }
        public Developer(string fullName, int id, string department)
        {
            FullName = fullName;
            Id = id;
            Department = department;
        }
        public override string ToString()
        {
            return $"Developer: {FullName}, id: {Id}, department: {Department}";
        }
    }
    public Production Prod { get; set; }
    public Developer Dev { get; set; }
}
public static class StatisticOperation
{
    public static int Sum(Array array)
    {
        return array.GetArray().Sum();
    }
    public static int DifferenceMaxMin(Array array)
    {
        int max = array.GetArray().Max();
        int min = array.GetArray().Min();
        return max - min;
    }
    public static int CountElements(Array array)
    {
        return array.GetArray().Length;
    }
    public static bool ContainsChar(this string str, char c)
    {
        return str.Contains(c);
    }
    public static string RemoveNegativeChars(this string str)
    {
        return new string(str.Where(c => c != '-').ToArray());
    }
    public static void RemoveNegative(this Array array)
    {
        array.SetArray(array.GetArray().Where(x => x >= 0).ToArray());
    }
}
class Program
{
    static void Main(string[] args)
    {
        var production = new Array.Production(1, "AnitaBy");
        var developer = new Array.Developer("John Resee", 7, "MrFinch Corporation");
        Array array1 = new Array(5, production, developer);
        Array array2 = new Array(5, production, developer);

        array1[0] = 1; array1[1] = 2; array1[2] = -3; array1[3] = 4; array1[4] = 5;
        array2[0] = 2; array2[1] = 4; array2[2] = 6; array2[3] = 8; array2[4] = 10;

        Array resultArray = array1 * array2;
        Console.WriteLine("Умноженный массив: ");
        resultArray.DisplayArray();

        bool isEqual = array1 == array2;
        Console.WriteLine($"Массивы равны? {isEqual}");

        bool arrayComparsion = array1 < array2;
        Console.WriteLine($"array1 меньше array2? {arrayComparsion}");

        if(array1)
        {
            Console.WriteLine("array1 содержит только положительные элементы");
        }
        else
        {
            Console.WriteLine("array1 содержит отрицательные элементы");
        }

        Console.WriteLine("Сумма элементов array1 " + StatisticOperation.Sum(array1));
        Console.WriteLine("Разница между макс и мин " + StatisticOperation.DifferenceMaxMin(array1));
        Console.WriteLine("Кол-во элементов в массиве " + StatisticOperation.CountElements(array1));

        string text = "Hello, my name is Lisa!";
        char symbolToCheck = 'k';
        Console.WriteLine($"Строка содержит символ '{symbolToCheck}': " + text.ContainsChar(symbolToCheck));

        string negative = "12 -9 -20 05";
        Console.WriteLine($"Строка до удаления символа '-' : {negative}");
        Console.WriteLine($"Строка после удаления символа '-' : {negative.RemoveNegativeChars()}");

        Console.WriteLine("array1 до удаления отрицательных элементов: ");
        array1.Watch();
        array1.RemoveNegative();
        Console.WriteLine("array1 после удаления отрицательных элементов:");
        array1.Watch();

        array1.Add(9);
        Console.WriteLine("array1 после добавления элемента:");
        array1.Watch();

        array1.Remove(9);
        Console.WriteLine("array1 после удаления этого элемента:");
        array1.Watch();
    }
}