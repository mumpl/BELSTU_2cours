using System;
using System.Text;
class Program1
{
    static void Main()
    {
        //1а,b
        Console.WriteLine($"input bool");
        bool myBool = Convert.ToBoolean(Console.ReadLine());
        Console.WriteLine($"{myBool}");

        Console.WriteLine($"input byte"); //беззнак 8-бит
        byte myByte = Convert.ToByte(Console.ReadLine());
        Console.WriteLine($"{myByte}");

        Console.WriteLine($"input sbyte");  //знак 8-бит 
        sbyte mySbyte = Convert.ToSByte(Console.ReadLine());  //от -128 до 127
        Console.WriteLine($"{mySbyte}");

        Console.WriteLine($"input char");
        char myChar = Convert.ToChar(Console.ReadLine());
        Console.WriteLine($"{myChar}");

        Console.WriteLine($"input decimal");
        decimal myDecimal = Convert.ToDecimal(Console.ReadLine());
        Console.WriteLine($"{myDecimal}");

        Console.WriteLine($"input float");
        float myFloat = Convert.ToSingle(Console.ReadLine());
        Console.WriteLine($"{myFloat}");

        Console.WriteLine($"input double");
        double myDouble = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine($"{myDouble}");

        Console.WriteLine($"input int");
        int myInt = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($"{myInt}");

        Console.WriteLine($"input uint");  //беззнак 32-бит целое
        uint myUint = Convert.ToUInt32(Console.ReadLine());
        Console.WriteLine($"{myUint}");

        Console.WriteLine("input nint"); //знак 32/64-бит целое
        nint myNint = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($"{myNint}");

        Console.WriteLine("input nuint"); //беззнак 32/64-бит целое
        nuint myNuint = Convert.ToUInt32(Console.ReadLine());
        Console.WriteLine($"{myNuint}");

        Console.WriteLine("input long");  //знак 64-бит целое
        long myLong = Convert.ToInt64(Console.ReadLine());
        Console.WriteLine($"{myLong}");

        Console.WriteLine("input short");  //знак 16-бит целое
        short myShort = Convert.ToInt16(Console.ReadLine());
        Console.WriteLine($"{myShort}");

        Console.WriteLine($"input ulong");  //беззнак 64-бит целое
        ulong myUlong = Convert.ToUInt64(Console.ReadLine(), 16);
        Console.WriteLine($"{myUlong}");

        Console.WriteLine($"input ushort");  //беззнак 16-бит целое
        ushort myUshort = Convert.ToUInt16(Console.ReadLine(), 16);
        Console.WriteLine($"{myUshort}");

        //1с
        int i = 123;
        object o = i;
        int j = (int)o;
        Console.WriteLine(o);
        Console.WriteLine(j);

        bool b = true;
        object s = b;
        b = false;
        Console.WriteLine(s);
        Console.WriteLine(b);

        //1d
        var e = 5;
        var f = 6;
        Console.WriteLine(e + f);

        var str = "Hello";
        Console.WriteLine(str);

        //1e
        int? c = null; // обозначение nullable типа, который может принимать значение null в дополнение к значению обычного типа
        int d = c ?? -1; // оператор "слияния с null", который возвращает значение слева, если оно не равно null
        Console.WriteLine($"d is {d}");

        //1f
        //var h = 45;
        //var h = "hi";
    }
}

        



