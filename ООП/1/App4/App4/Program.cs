using System;
abstract class Software
{
    public string Name { get; set; }
    public string Version { get; set; }
    public Software (string name, string version)
    {  
        Name = name; 
        Version = version;
    }
    public virtual void Run()
    {
        Console.WriteLine($"{Name} version {Version} is running");
    }
    public abstract void Shutdown();
    public override string ToString()
    {
        return $"ПО:{Name}, Версия:{Version}";
    }
}
class TextProcessor : Software
{
    public TextProcessor(string name, string version) : base(name, version) { }
    public override void Shutdown()
    {
        Console.WriteLine($"{Name} отключается");
    }
}
class Word : TextProcessor
{
    public Word(string version) : base("Microsoft Word", version) { }
    public override void Run()
    {
        Console.WriteLine($"Текстовый процессор {Name} версии {Version} запущен");
    }
    public override string ToString()
    {
        return $"{Name} - Текстовый процессор, Версия - {Version}";
    }
}
class Virus : Software
{
    public Virus(string name, string version) : base(name, version) { }
    public override void Shutdown()
    {
        Console.WriteLine($"Вирус {Name} найден и обезврежен");
    }
}
class Conficker : Virus
{
    public Conficker(string version) : base ("Conficker", version) { }
    public override void Run()
    {
        Console.WriteLine($"Внимание! Вирус {Name} версии {Version} распространяется!");
    }
    public override string ToString()
    {
        return $"Вирус - {Name}, Версия - {Version}";
    }
}
class Game : Software
{
    public Game(string name, string version) : base(name, version) { }
    public override void Shutdown()
    {
        Console.WriteLine($"{Name} игра выключается");
    }
}
class Sapper : Game
{
    public Sapper(string version) : base("Sapper", version) { }
    public override void Run()
    {
        Console.WriteLine($"{Name} игра начинается");
    }
    public override string ToString()
    {
        return $"Игра - {Name}, Версия - {Version}";
    }
}
sealed class Developer
{
    public string FullName { get; set; }
    public Developer(string fullName)
    {
        FullName = fullName;
    }
    public override string ToString()
    {
        return $"Developer: {FullName}";
    }
}
interface ICloneable
{
    bool DoClone();
}
abstract class BaseClone
{
    public abstract bool DoClone();
}
class WordProcessor : BaseClone, ICloneable
{
    public override bool DoClone()
    {
        Console.WriteLine($"Клонирование WordProcessor как BaseClone");
        return true;
    }
    bool ICloneable.DoClone()
    {
        Console.WriteLine($"Клонирование WordProcessor как ICloneable");
        return false;
    }
}
class Printer
{
    public void IAmPrinting(Software software)
    {
        Console.WriteLine(software.ToString());
    }
}
class Program
{
    static void Main(string[] args)
    {
        Software word = new Word("2024");
        Software conficker = new Conficker("1.0");
        Software sapper = new Sapper("1.2");

        Developer dev = new Developer("Mr.Finch");

        word.Run();
        word.Shutdown();
        Console.WriteLine(word.ToString());

        conficker.Run();
        conficker.Shutdown();
        Console.WriteLine(conficker.ToString());

        sapper.Run();
        sapper.Shutdown();
        Console.WriteLine(sapper.ToString());

        Console.WriteLine(dev.ToString());

        Printer printer = new Printer();
        printer.IAmPrinting(word);
        printer.IAmPrinting(conficker);
        printer.IAmPrinting(sapper);

        WordProcessor processor = new WordProcessor();
        processor.DoClone();
        ((ICloneable)processor).DoClone();

        if (conficker is Virus)
        {
            Console.WriteLine("Conficker is Virus");
        }
        Game game = sapper as Game;
        if (game != null)
        {
            Console.WriteLine("Sapper приведен к типу Game");
            game.Run();
        }
    }
}

