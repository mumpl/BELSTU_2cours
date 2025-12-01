using System;
class Program5
{
    static void Main()
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7 };
        string text = "Apple";

        (int max, int min, int sum, char first) Func(int[] arr, string str)
        {
            int max = arr[0];
            int min = arr[0];
            int sum = 0;

            foreach (int num in arr)
            {
                if (num > max) max = num;
                if (num < min) min = num;
                sum += num;
            }

            char first = str.Length > 0 ? str[0] : '\0'; // Проверка, чтобы строка не была пустой
            return (max, min, sum, first);
        }

        var result = Func(numbers, text);
        Console.WriteLine($"Максимум = {result.max}, Минимум = {result.min}, Сумма = {result.sum}, Первая буква: {result.first}");
    }
}
