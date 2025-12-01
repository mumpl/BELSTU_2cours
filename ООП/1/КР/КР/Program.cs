using System;

public interface IGraph
{
    public int First(int x, int y, int z);
}
public class Point : IGraph
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
    public Point(int x, int y, int z)
    {
        x = X;
        y = Y;
        z = Z;
    }
    public static Point operator +(Point a, Point b)
    {
        return new Point(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }
    public static Point operator -(Point a, Point b)
    {
        return new Point(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }
    public int First(int x, int y, int z)
    {
        if ((x > 0) && (y > 0) && (z > 0))
        {
            Console.WriteLine("1");
            return 1;
        }
        else
        {
            Console.WriteLine("0");
            return 0;
        }    
    }
}

class Program
{
    static void Main()
    {
        Point first = new Point(1, 2, 3);
        Point second = new Point(-2, 3, 4);
        
        
        first.First(1, 2, 3);
        second.First(-2, 3, 4);
        Point sum = first + second;
        Console.WriteLine(sum.ToString());
        Point raznica = first - second;
        Console.WriteLine(raznica.ToString());

        Console.WriteLine("Введите числа");
        int num11 = Convert.ToInt32(Console.ReadLine());
        int num12 = Convert.ToInt32(Console.ReadLine());
        int sum2 = num11 + num12;
        Console.WriteLine($"Сумма = {sum2}");

        string[,] array = { { "lalala" }, { "mamam" } };
        int count = 0;
        foreach (var item in array)
        {
            count += item.Length;
        }
        Console.WriteLine($"Количество символов = {count}");
    }
}
