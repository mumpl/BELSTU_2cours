using System;
using System.Text;
class Program2
{
    static void Main()
    {
        //2a
        string str1 = "Hello";
        string str2 = "Bye!";
        int result = String.Compare(str1, str2);
        if (result < 0)
        {
            Console.WriteLine("str1 перед str2");
        }
        else if (result > 0)
        {
            Console.WriteLine("str1 после str2");
        }
        else
        {
            Console.WriteLine("строки идентичны");
        }

        //2b
        string str3 = "Hello everyone";
        string str4 = "Mama";
        string str5 = "Apple";

        Console.WriteLine(String.Concat(str3, str4)); //сцепление

        string copy = String.Copy(str5);//копирование
        Console.WriteLine(copy);

        string sub = str3.Substring(6); //выделение подстроки
        Console.WriteLine(sub);

        string[] words = str3.Split(' '); //разделение на слова
        foreach (string word in words)
        {
            Console.WriteLine(word);
        }

        str4 = str4.Insert(2, str5); //вставка в заданную позицию
        Console.WriteLine(str4);

        int rem = str3.Length - 4; //удаление подстроки
        str3 = str3.Remove(rem);
        Console.WriteLine(str3);

        int x = 8; //интерполирование
        int y = 7;
        string result2 = $"{x} + {y} = {x + y}";
        Console.WriteLine(result2);

        //2c
        string str6 = string.Empty;
        string str7 = null;

        Console.WriteLine($"{String.IsNullOrEmpty(str6)}");
        Console.WriteLine($"{String.IsNullOrEmpty(str7)}");

        //2d
        StringBuilder sb = new StringBuilder("Пример строки");
        sb.Remove(7, 6); // Удалит "строки", начиная с индекса 7, удалим 6 символов

        // Добавление символов в начало строки
        sb.Insert(0, "Начало ");

        // Добавление символов в конец строки
        sb.Append(" Конец");
        Console.WriteLine(sb.ToString());
    }
}
