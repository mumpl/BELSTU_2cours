using System;
using System.Collections.Generic;
namespace App2 { 

public partial class Vector
{
    private int[] elements;
    private int size;
    private readonly int id;
    private static int count;
    private int errorCode;

    public const int DefaultSize = 5;
    public const int NoError = 0;
    public const int IndexOfError = 1;

    //статический конструктор
    static Vector()
    {
        count = 0;
        Console.WriteLine("Статический конструктор инициализирован");
    }

    //конструктор без параметров
    public Vector() : this(DefaultSize)
    {

    }
     
    //конструктор с параметрами по умолчанию
    public Vector(int size = DefaultSize)
    {
        this.size = size;   
        elements = new int[size];
        id = this.GetHashCode(); //автоматич уникальный хеш
        count++; //отслеживает количество созданных объектов
        errorCode = NoError;
    }

    //конструктор с параметрами
    public Vector(int[] initialElements)
    {
        this.size = initialElements.Length;
        this.elements = initialElements; //ссылка на тот же массив, который был передн в конструктор через initialElements
        id = this.GetHashCode();
        count++;
        errorCode = NoError;
    }

    //закрытый конструктор
    private Vector(bool generateRandomElements)
    {
        this.size = DefaultSize;
        this.elements = new int[DefaultSize];

        if (generateRandomElements)
        {
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                elements[i] = random.Next(100); //заполняем случ числами
            }
        }

        id = this.GetHashCode();
        count++;
        errorCode = NoError;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Vector[] vectors = new Vector[3]
            {
                //создание объектов
                new Vector(),
                new Vector(new int[] { 0, 1, 2, 3, -4, 5 }),
                Vector.CreaterandomVector()
            };


            //информация об объектах
            Console.WriteLine(vectors[0].ToString());
            Console.WriteLine(vectors[1].ToString());
            Console.WriteLine(vectors[2].ToString());
            
            Console.WriteLine("Векторы, содержащие 0:");
            foreach (var vector in vectors)
            {
                if(vector.ContainZero())
                {
                    vector.Display();
                }
            }
            
            Console.WriteLine("Векторы с наименьшей суммой модулей:");
            int minSumOfAbs = int.MaxValue;
            List<Vector> minAbsVectors = new List<Vector>();

            foreach (var vector in vectors)
            {
                int sumOfAbs = vector.GetSumOfAbs();
                if (sumOfAbs < minSumOfAbs)
                {
                    minSumOfAbs = sumOfAbs;
                    minAbsVectors.Clear();
                    minAbsVectors.Add(vector);
                }
                else if (sumOfAbs == minSumOfAbs)
                {
                    minAbsVectors.Add(vector);
                }
            }

            foreach (var vector in minAbsVectors)
            {
                vector.Display();
            }

            Console.WriteLine($"ID первого вектора: {vectors[0].ID}");
            Console.WriteLine($"Размер третьего вектора: {vectors[2].Size}");
            Console.WriteLine($"Элемент второго вектора номер 3: {vectors[1][2]}");

            //метод Add с ref и out
            int numberToAdd = 5;
            int result;
            vectors[0].Add(ref numberToAdd, out result);
            Console.Write($"Сумма элементов v1 после добавления {numberToAdd} = {result}");
            vectors[0].Display();

            vectors[1].Multipy(2);
            vectors[1].Display();

            Console.WriteLine($"Векторы v1 и v2 равны? {vectors[0].Equals(vectors[1])}");
            Console.WriteLine($"HashCode v3: {vectors[2].GetHashCode()}");

            //тип объекта
            Console.WriteLine($"Тип объекта v2: {vectors[1].GetType()}");

            Console.WriteLine($"Всего создано объектов: {Vector.GetObjectCount()}");

            try
            {
                int value = vectors[2][12];
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Ошибка! Выбран несуществующий индекс. Код ошибки: " + vectors[2].ErrorCode);
            }
        }
    }
}
}

