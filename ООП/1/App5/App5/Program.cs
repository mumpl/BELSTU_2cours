using System;
using System.ComponentModel.DataAnnotations;
using static App5.Software;
using static App5.Exceptions;
using App5;
using System.IO;
using System.Diagnostics;

namespace App5
{
    public abstract partial class Software
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public SoftwareType Type { get; set; }
        public SystemRequirements Requirements { get; set; }
        public Software(string name, string version, SoftwareType type, SystemRequirements requirements)
        { 
            Name = name;
            Version = version;
            Type = type;
            Requirements = requirements;
        }
    }
    public class TextProcessor : Software
    {
        public TextProcessor(string name, string version, SoftwareType type, SystemRequirements requirements) : base(name, version, type, requirements) { }
        public override void Shutdown()
        {
            Console.WriteLine($"{Name} отключается");
        }
        public override string ToString()
        {
            return $"{Name} - Текстовый процессор, Версия - {Version}";
        }
    }
    class Word : TextProcessor
    {
        public Word(string version) : base("Microsoft Word", version, SoftwareType.TextProcessor, new SystemRequirements("Windows 10", 2048, 1024)) { }
        public override void Run()
        {
            Console.WriteLine($"Текстовый процессор {Name} версии {Version} запущен");
        }
        public override string ToString()
        {
            return $"{Name} - Текстовый процессор, Версия - {Version}, Системные требования - {Requirements.ToString()}";
        }
    }
    class Virus : Software
    {
        public Virus(string name, string version) : base(name, version, SoftwareType.Virus, new SystemRequirements("Windows 7", 512, 50)) { }
        public override void Shutdown()
        {
            Console.WriteLine($"Вирус {Name} найден и обезврежен");
        }
    }
    class Conficker : Virus
    {
        public Conficker(string version) : base("Conficker", version) { }
        public override void Run()
        {
            Console.WriteLine($"Внимание! Вирус {Name} версии {Version} распространяется!");
        }
        public override string ToString()
        {
            return $"Вирус - {Name}, Версия - {Version}";
        }
    }
    public class Game : Software
    {
        public Game(string name, string version) : base(name, version, SoftwareType.Game, new SystemRequirements("Windows 10", 4096, 2048)) { }
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
            try
            {
                static int AccessArrayElement(int[] array, int index)
                {
                    try
                    {
                        return array[index];
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        Console.WriteLine($"Локальная обработка в AccessArrayElement - неверный индекс");
                        throw;
                    }
                }
                try
                {
                    int age = GetUserAge(-15);
                    Console.WriteLine($"Возраст - {age}");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Ошибка: неверный аргумент. {ex.Message}");
                }
                finally
                {
                    Console.WriteLine($"Завершение работы программы");
                }
                try
                {
                    int element = AccessArrayElement(new int[] { 1, 2, 3 }, 5);
                    Console.WriteLine($"Элемент массива - {element}");
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine($"Ошибка в Main: {ex.Message}");
                }
                try
                {
                    int z = 10;
                    int y = 0;
                    int result = z / y;
                    Console.WriteLine($"Результат деления - {result}");
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine($"Ошибка: деление на ноль.{ex.Message}");
                }
                finally
                {
                    Console.WriteLine("Блок finally: операция деления завершена.");
                }
                try
                {
                    string filePath = "file.txt";
                    string fileContent = File.ReadAllText(filePath);
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine($"Исключение: {ex.GetType().Name}");
                    Console.WriteLine($"Сообщение: {ex.Message}");
                    Console.WriteLine($"Метод: {ex.TargetSite}");
                    Console.WriteLine($"Стек вызовов: {ex.StackTrace}");
                }
                try
                {
                    Software invalidSofware = new Word("");
                }
                catch (InvalidSoftwareDataException ex)
                {
                    Console.WriteLine($"Ошибка: неверные данные. {ex.Message}");
                }
                try
                {
                    Software word = new Word("2024");
                    Software conficker = new Conficker("1.0");
                    Software sapper = new Sapper("1.2");

                    Developer dev = new Developer("Mr.Finch");

                    word.Run();
                    word.Shutdown();

                    if (conficker is Virus)
                    {
                        throw new VirusDetectedException($"{conficker.Name} обнаружен на компьютере");
                    }

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

                    Console.WriteLine(word.ToString());

                    SoftwareType[] softwareTypes = (SoftwareType[])Enum.GetValues(typeof(SoftwareType));
                    Console.WriteLine("Software types: ");
                    foreach (var software in softwareTypes)
                    {
                        Console.WriteLine(software + ", ");
                    }

                    Computer myComputer = new Computer();
                    myComputer.AddSoftware(word);
                    myComputer.AddSoftware(conficker);
                    myComputer.AddSoftware(sapper);
                    myComputer.PrintSoftwareList();
                    myComputer.RemoveSoftware(conficker);
                    myComputer.PrintSoftwareList();

                    Controller controller = new Controller(myComputer);
                    var games = controller.FindGamesByType(Software.SoftwareType.Game);
                    if (games.Count == 0)
                    {
                        throw new GameNotFoundException();
                    }
                    Console.WriteLine("Найденные игры:");
                    for (int i = 0; i < games.Count; i++)
                    {
                        Game? game1 = games[i];
                        Console.WriteLine(game.ToString());
                    }

                    var textProcessor = controller.FindTextProcessorByVersion("2024");
                    if (textProcessor != null)
                    {
                        Console.WriteLine($"Найден текстовый процессор: {textProcessor.ToString()}");
                    }
                    else
                    {
                        Console.WriteLine("Текстовый процессор не найден.");
                    }

                    controller.PrintSoftwareAlphabet();
                }
                catch (VirusDetectedException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                catch (IncompatibleSoftwareException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                catch (GameNotFoundException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошло неожиданное исключение: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Завершение работы программы.");
            }
            //int newAge = -5;
            //Debug.Assert(newAge >= 0);
        }
        static int GetUserAge(int age)
        {
            if (age < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(age), "Возраст не может быть отрицательным");
            }
            return age;
        }
    }
}


