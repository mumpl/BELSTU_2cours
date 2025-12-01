using System;
using System.Text.RegularExpressions;

public delegate string StringProcessor(string input);
public delegate bool StringPredicate(string input);

class Program
{
    static string RemovePunctuation(string input) =>
        Regex.Replace(input, @"[^\w\s]", "");
    static string AddChars(string input, string chars) => input + chars;
    static string ToUpperCase(string input) => input.ToUpper();
    static string RemoveExtraSpaces(string input) =>
        Regex.Replace(input.Trim(), @"\s+", " ");
    static bool IsStringEmpty(string input) => string.IsNullOrEmpty(input);
    static void Main()
    {
        string input = " Всем привет, люди! Это код    для ООП......";
        StringProcessor removePunctuation = RemovePunctuation;
        StringProcessor toUpperCase = ToUpperCase;
        StringProcessor removeExtraSpaces = RemoveExtraSpaces;

        Func<string, string> addChars = str => AddChars(str, " - ПРОЦЕСС ЗАВЕРШЁН");
        Predicate<string> isEmpty = IsStringEmpty;
        Action<string> log = str => Console.WriteLine("Промежуточный результат: " + str);

        if (isEmpty(input))
        {
            Console.WriteLine("Стока пустая.Проверка не требуется.");
            return;
        }
        Console.WriteLine("Исходная строка");
        Console.WriteLine(input);

        Console.WriteLine("Обработка строки");
        input = removePunctuation(input);
        log(input);
        input = removeExtraSpaces(input);
        log(input);   
        input = toUpperCase(input);
        log(input);
        input = addChars(input);
        log(input);

        Console.WriteLine("Обработанная строка");
        Console.WriteLine(input);
    }

}
