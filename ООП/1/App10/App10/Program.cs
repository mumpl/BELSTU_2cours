using System.Linq;
using System;

class Program
{
    static void Main()
    {
        string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        IEnumerable<string> monthWithLenght = from m in months where m.Length == 9 select m;
        foreach (string month in monthWithLenght )
        {
            Console.WriteLine(month);
        }
        Console.WriteLine("--------------------------");
        IEnumerable<string> sumAutMonths = from m in months where m =="June" || m =="July" || m=="August" || m=="December" || m =="January" || m=="February" select m;
        foreach(string month in sumAutMonths )
        {
            Console.WriteLine(month);
        }
        Console.WriteLine("--------------------------");
        IEnumerable<string> monthByAlphabet = from m in months orderby m select m;
        foreach( string month in monthByAlphabet )
        {
            Console.WriteLine(month);
        }
        Console.WriteLine("--------------------------");
        int uMonthsWithLenght = (from m in months where m.Length >= 4 && m.Contains('u') select m).Count();
        Console.WriteLine(uMonthsWithLenght);
    }

}
