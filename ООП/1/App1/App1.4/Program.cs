using System;
class Program4
{
    static void Main()
    {
        //4a,b
        (int, string, char, string, ulong) t = (5, "lisa", 'j', "liza", 5698);
        Console.WriteLine(t);
        Console.WriteLine($" { t.Item1} , {t.Item3}, {t.Item4}");

        //4c
        var myTuple = (1, "hello", 1.234);

        //1
        (int num, string word, double digit) = myTuple;
        Console.WriteLine($"Число: {num}, слово: {word}, число с плав точкой: {digit}");

        //2
        (int onlynum, string onlyWord, _) = myTuple;
        Console.WriteLine($"{onlynum} and {onlyWord}");


        //4d
        var myTuple2 = (1, "hello", 1.234);
        var myTuple3 = (2, "Bye", 4.321);

        bool eaqual1 = myTuple == myTuple2;
        bool eaqual2 = myTuple2 == myTuple3;

        Console.WriteLine($"{eaqual1}");
        Console.WriteLine($"{eaqual2}");
    }
}
