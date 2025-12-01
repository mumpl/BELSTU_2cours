using System;
using System.Text;
class Program3
{
    static void Main()
    {
            //3a
            int[,] matrix = {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t"); //выводит элемент массива с табуляцией для выравнивания.
                }
                Console.WriteLine(); // Переход на новую строку
            }

            //3b
            string[] weekDays = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

            Console.WriteLine("Текущий массив:");
            for (int i = 0; i < weekDays.Length; i++)
            Console.Write(weekDays[i] + " ");
            Console.WriteLine();

            Console.WriteLine("Введите идекс элемента для изменения от 0 до 6");
            int index = Convert.ToInt32(Console.ReadLine());

            if (index >= 0 && index < weekDays.Length)
            {
                Console.WriteLine("Введите новое значение дня");
                string newValue = Console.ReadLine();

                weekDays[index] = newValue;

                Console.WriteLine("Новый массив");
                for (int i = 0; i < weekDays.Length; i++)
                    Console.Write(weekDays[i] + " ");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Такой индекс не существует");
            }
            Console.WriteLine("Длина массива = " + weekDays.Length);

        //3c
        // Создаем ступенчатый массив с 3 строками
        double[][] jaggedArray = new double[3][];

        // Каждая строка имеет разное количество столбцов
        jaggedArray[0] = new double[2];
        jaggedArray[1] = new double[3];  
        jaggedArray[2] = new double[4]; 

        // Вводим значения массива с консоли
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                Console.Write($"Введите элемент для массива[{i}][{j}]: ");
                jaggedArray[i][j] = Convert.ToDouble(Console.ReadLine());
            }
        }

        // Выводим элементы массива
        Console.WriteLine("\nВведенные значения ступенчатого массива:");
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                Console.Write(jaggedArray[i][j] + "\t");
            }
            Console.WriteLine();
        }

        //3d
        var numbers = new[] { 1, 2, 3, 4, 5 };

        var text = "Hello, World!";

        Console.WriteLine("Массив чисел:");
        foreach (var num in numbers)
        {
            Console.WriteLine(num + " ");
        }
        Console.WriteLine("Строка:");
        Console.WriteLine(text);

    }
}



