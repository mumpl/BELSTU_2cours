using System;
using System.Collections;

public class Car : IList<Car>
{
    public int Id { get; set; }
    public string Model { get; set; }
    public string Manufacturer { get; set; }
    public int Year { get; set; }

    private List<Car> cars = new List<Car>();
     public Car(int id, string model, string manufacturer, int year)
    {
        Id = id;
        Model = model;
        Manufacturer = manufacturer;
        Year = year;
    }
    public override string ToString()
    {
        return $"ID - {Id}, модель - {Model}, производитель - {Manufacturer}, год выпуска - {Year}";
    }
    public Car this[int index]
    {
        get => cars[index];
        set => cars[index] = value;
    }
    public int Count => cars.Count;
    public bool IsReadOnly => false;
    public void Add(Car car) => cars.Add(car);
    public void Clear() => cars.Clear();
    public bool Contains(Car car) => cars.Contains(car);
    public void CopyTo(Car[] array, int arrayIndex) => cars.CopyTo(array, arrayIndex);
    public IEnumerator<Car> GetEnumerator() => cars.GetEnumerator();
    public int IndexOf(Car car) => car.IndexOf(car);
    public void Insert(int index, Car car) => cars.Insert(index, car);
    public bool Remove(Car car) => cars.Remove(car);
    public void RemoveAt(int index) => cars.RemoveAt(index);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static void Main()
    {
        Car car1 = new Car(1, "Tiggo 3", "Chery", 2015);
        Car car2 = new Car(2, "Sandero", "Dacia", 2009);
        Car car3 = new Car(3, "Pajero Sport", "Mitsubishi", 2008);

        Dictionary<int, Car> carDictionary = new Dictionary<int, Car>
        {
            { car1.Id, car1},
            { car2.Id, car2 },
            { car3.Id, car3 }
        };
        Car car4 = new Car(4, "Qashqai", "Nissan", 2017);
        carDictionary.Add(car4.Id, car4);
        Console.WriteLine("Автомобиль добавлен: " + car4);

        int searchId = 3;
        if (carDictionary.TryGetValue(searchId, out Car foundCar))
        {
            Console.WriteLine($"\nАвтомобиль с ID {searchId} найден: {foundCar}");
        }
        else
        {
            Console.WriteLine($"\nАвтомобиль с ID {searchId} не найден.");
        }
        int deleteId = 4;
        if (carDictionary.Remove(deleteId))
        {
            Console.WriteLine($"\nАвтомобиль с ID {deleteId} удален.");
        }
        else
        {
            Console.WriteLine($"\nАвтомобиль с ID {deleteId} не найден.");
        }
        Console.WriteLine("писок автомобилей");
        foreach (var car in carDictionary.Values)
        {
            Console.WriteLine(car);
        }
    }

}
