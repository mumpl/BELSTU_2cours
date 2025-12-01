using System;
using System.Collections.ObjectModel;
using System.Linq;

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
    public int GetModule()
    {
        return elements.Sum(Math.Abs);
    }
    public int GetVector()
    {
        return elements.Sum();
    }
}
class Program
{
    static void Main(string[] args)
    {
        List<Vector> vectors = new List<Vector>
        {
            new Vector(new int[]{1,2,3,4 }),
            new Vector(new int[]{-1,-2,-3,-4,-5, -6, -7}),
            new Vector(new int[]{1,-1,2,-2,0,-3, 3}),
            new Vector(new int[]{9,12,15,25,95}),
            new Vector(new int[]{100, 150, 200, 250, 300}),
            new Vector(new int[]{0,0,0}),
            new Vector(new int[]{16,594,375,86,29,4}),
            new Vector(new int[]{375,33,323,60,0,7}),
            new Vector(new int[]{55555}),
            new Vector(new int[]{9,99,999,9999})
        };

        IEnumerable<Vector> vectorWithZero = from vector in vectors where vector.Elements.Contains(0) select vector;
        int vectorWithZeroCount = vectorWithZero.Count();
        Console.WriteLine($"Количество веторов, содержащиx 0: {vectorWithZeroCount }");

        int minModule = (int)vectors.Min(vector => vector.GetModule());
        IEnumerable<Vector> vectorWithMinModule = from vector in vectors where vector.GetModule() == minModule select vector;
        Console.WriteLine($"Вектор с минимальным модулем:");
        foreach (var vector in vectorWithMinModule)
        {
            Console.WriteLine(string.Join(", ", vector.Elements));
        }

        var vectorWithSize = from vector in vectors where vector.Size == 3 || vector.Size == 5 || vector.Size == 7 select vector;
        foreach ( var vector in vectorWithZero )
        {
            Console.WriteLine("Вектор с размером " + vector.Size + ": " + string.Join(", ", vector.elements));
        }

        int maxVector = vectors.Max(vector => vector.GetVector());
        IEnumerable<Vector> vectorWithMax = from vector in vectors where vector.GetVector() == maxVector select vector;
        Console.WriteLine($"Максимальный вектор:");
        foreach( var vector in vectorWithMax )
        {
            Console.WriteLine(string.Join(", ", vector.Elements));
        }

        var firstNegativeVector = vectors.FirstOrDefault(v => v.Elements.Any(x => x < 0)); 
        if ( firstNegativeVector != null )
        {
            Console.WriteLine("Первый вектор с отрицательным значением: ");
            foreach ( var vector in firstNegativeVector.Elements )
            {
                Console.Write(vector + " ");
            }
        }
        else
        {
            Console.WriteLine("Не найдено векторов с отрицательными значениями.");
        }

        IEnumerable < Vector > sortedVectors = from vector in vectors orderby vector.Size select vector;
        Console.WriteLine($"Упорядоченный список векторов по размеру:");
        foreach (var vector in sortedVectors)
        {
            Console.WriteLine(string.Join(", ", vector.Elements));
        }

        var groupedVectors = vectors.Where(v => v.Size > 3 && v.Elements.Any(x => x < 0)).GroupBy(v => v.Size).OrderByDescending(g => g.Count()).Take(2);
        foreach (var group in groupedVectors)
        {
            int size = group.Key;
            int count = group.Count();
            double averageSum = group.Average(v => v.GetVector());
            Console.WriteLine($"Размер векторов:{size}, количество: {count}, средняя сумма элементов: {averageSum}");
            foreach ( var vector in group)
            {
                Console.WriteLine("Элементы вектора: " + string.Join(", ", vector.Elements));
            }
        }

        List<int> modules = new List<int> { 0, 10, 100, 300, 55555 };

        var query = vectors.Join(modules, vector => vector.GetModule(), module => module, (vector, module) => vector);
        Console.WriteLine("Векторы, модуль которых совпадает с одним из значений:");
        foreach( var vector in query)
        {
            Console.WriteLine($"Вектор с элементами: {string.Join(", ", vector.Elements)}, Модуль:{vector.GetModule()}");
        }
    }
}
