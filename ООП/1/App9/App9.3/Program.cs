using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
public class Car
{
    public string Model { get; set; }
    public string Manufacturer { get; set; }
    public int Year { get; set; }

    public Car(string model, string manufacturer, int year)
    {
        Model = model;
        Manufacturer = manufacturer;
        Year = year;
    }

    public override string ToString()
    {
        return $"{Manufacturer} {Model} ({Year})";
    }
}

class Program
{
    private static void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                foreach (Car newCar in e.NewItems)
                {
                    Console.WriteLine($"Автомобиль добавлен: {newCar}");
                }
                break;

            case NotifyCollectionChangedAction.Remove:
                foreach (Car oldCar in e.OldItems)
                {
                    Console.WriteLine($"Автомобиль удален: {oldCar}");
                }
                break;

            case NotifyCollectionChangedAction.Replace:
                Console.WriteLine("Элемент заменен");
                break;

            case NotifyCollectionChangedAction.Move:
                Console.WriteLine("Элемент перемещен");
                break;

            case NotifyCollectionChangedAction.Reset:
                Console.WriteLine("Коллекция очищена");
                break;
        }
    }
    static void Main()
    {
        ObservableCollection<Car> cars = new ObservableCollection<Car>();
        cars.CollectionChanged += OnCollectionChanged;
        Console.WriteLine("Добавление автомобилей в коллекцию");
        cars.Add(new Car("Tiggo 3", "Chery", 2015));
        cars.Add(new Car("Pajero Sport", "Mitsubishi", 2008));
        cars.Add(new Car("Sandero", "Dacia", 2009));
        cars.Add(new Car("Qashqai", "Nissan", 2017));

        Console.WriteLine("Удаление автомобиля из коллекции");
        cars.RemoveAt(3);

        Console.WriteLine("Текущая коллекция автомобилей");
        foreach (var car in cars)
        {
            Console.WriteLine(car);
        }
    }
}
