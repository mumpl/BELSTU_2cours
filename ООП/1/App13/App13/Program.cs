using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Serialization.Formatters.Soap;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

public abstract class Software
{
    [NonSerialized]
    public int Year;
    public string Name { get; set; }
    public string Version { get; set; }
    public Software(string name, string version)
    {
        Name = name;
        Version = version;
    }
    public virtual void Run()
    {
        Console.WriteLine($"{Name} версии {Version} запущен.");
    }
    public abstract void Shutdown();
}

public class SerializableSoftware : Software
{
    public string Developer {  get; set; }
    private string _privateInfo = "Не для сериализации";
    public SerializableSoftware(string name, string version, string developer) : base (name, version)
    {
        Developer = developer;
    }
    public override void Shutdown()
    {
        Console.WriteLine($"{Name} завершает работу.");
    }
}

interface ISerializer
{
    void Serialize<T>(T obj, string filePath);
    T Deserialize<T>(string filePath);
}
/*
class BinarySerializer : ISerializer
{
    public void serialize<T>(T obj, string filePath)
    {
        IFormatter formatter = new BinaryFormatter();
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            formatter.Serialize(stream, obj);
        }
    }
    public T Deserialize<T>(string filePath)
    {
        IFormatter formatter = new BinaryFormatter();
        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            return (T)formatter.Deserialize(stream);
        }
    }
}

class SoapSerializer : ISerializer
{
    public void Serialize<T>(T obj, string filePath)
    {
        IFormatter formatter = new SoapFormatter();
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            formatter.Serialize(stream, obj);
        }
    }

    public T Deserialize<T>(string filePath)
    {
        IFormatter formatter = new SoapFormatter();
        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            return (T)formatter.Deserialize(stream);
        }
    }
}
*/
class JsonSerializerWrapper : ISerializer
{
    public void Serialize<T>(T obj, string filePath)
    {
        var json = JsonSerializer.Serialize(obj);
        File.WriteAllText(filePath, json);
    }

    public T Deserialize<T>(string filePath)
    {
        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(json);
    }
}

class XmlSerializerWrapper : ISerializer
{
    public void Serialize<T>(T obj, string filePath)
    {
        var serializer = new XmlSerializer(typeof(T));
        using (var writer = new StreamWriter(filePath))
        {
            serializer.Serialize(writer, obj);
        }
    }

    public T Deserialize<T>(string filePath)
    {
        var serializer = new XmlSerializer(typeof(T));
        using (var reader = new StreamReader(filePath))
        {
            return (T)serializer.Deserialize(reader);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var software = new SerializableSoftware("TestApp", "1.0", "Liza");

        var softwareList = new List<SerializableSoftware>
        {
            new SerializableSoftware("App1", "1.0", "Developer A"),
            new SerializableSoftware("App2", "2.1", "Developer B"),
            new SerializableSoftware("App3", "3.5", "Developer C"),
        };

        var serializers = new ISerializer[]
        {
            /*new BinarySerializer(),
            new SoapSerializer(),*/
            new JsonSerializerWrapper(),
            new XmlSerializerWrapper()
        };

        var formats = new[] { "binary.dat", "soap.soap", "json.json", "xml.xml" };
        var collectionFormats = new[] { "software_list.dat", "software_list.soap", "software_list.json", "software_list.xml" };

        for (int i = 0; i < serializers.Length; i++)
        {
            var serializer = serializers[i];
            var singleFilePath = formats[i];
            var collectionFilePath = collectionFormats[i];

            serializer.Serialize(software, singleFilePath);
            Console.WriteLine($"Один объект сериализован в {singleFilePath}");

            var deserializedSingle = serializer.Deserialize<SerializableSoftware>(singleFilePath);
            Console.WriteLine($"Один объект десериализован в {singleFilePath}: {deserializedSingle.Name}, {deserializedSingle.Version}, {deserializedSingle.Developer}");

            serializer.Serialize(softwareList, collectionFilePath);
            Console.WriteLine($"Коллекция сериализована в {collectionFilePath}");

            var deserializedCollection = serializer.Deserialize<List<SerializableSoftware>>(collectionFilePath);
            Console.WriteLine($"Коллекция десериализована в {collectionFilePath}:");
            foreach (var item in deserializedCollection)
            {
                Console.WriteLine($"- {item.Name}, {item.Version}, {item.Developer}");
            }

            var cars1 = new XElement("Cars",
                new XElement("Car",
                new XElement("Name", "Chery"),
                new XElement("Model", "Toggo 3"),
                new XElement("Year", 2012)
                ),
                new XElement("Car",
                new XElement("Name", "Dacha"),
                new XElement("Model", "Sandero"),
                new XElement("Year", 2012)));
            Console.WriteLine(cars1);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(cars1.ToString());

            var names = doc.SelectNodes("//Cars/Name");
            Console.WriteLine("\nNames of the cars1");
            foreach (XmlNode n in names)
            {
                Console.WriteLine(n.InnerText);
            }

            var years = doc.SelectNodes("//Cars[Year='2012']");
            Console.WriteLine("\n Cars with year 2012");
            foreach (XmlNode ye in years)
            {
                Console.WriteLine(ye.InnerText);
            }

            var cars2012 = cars1.Descendants("Car").Where(b => (int)b.Element("Year") == 2012).Select(b => new
            {
                Name = b.Element("Name").Value,
                Model = b.Element("Model").Value,
                Year = b.Element("Year").Value
            });
            Console.WriteLine("\nCars of 2012");
            foreach(var car in cars2012)
            {
                Console.WriteLine($"{car.Name} model {car.Model} of {car.Year} year");
            }

            var carModels = cars1.Descendants("Model").Select(a => a.Value);
            Console.WriteLine("\nModels of cars");
            foreach(var model in carModels)
            {
                Console.WriteLine(model);
            }
        }
    }
}
