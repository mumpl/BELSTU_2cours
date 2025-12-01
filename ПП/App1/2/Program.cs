using System;
using System.Net.NetworkInformation;
public interface I1
{
    int Property { get; set; }

    void Method(); //должен быть реализован классом

    event EventHandler NewEvent;

    int this[int index] { get; set; }
}

public class C1 : I1
{
    public int Property { get; set; }

    public void Method()
    {
        Console.WriteLine("Метод из I1 вызван");
    }

    public event EventHandler NewEvent;

    private int[] array = new int[10];
    public int this[int index]
    {
        get => array[index];
        set => array[index] = value;
    }

    public void TriggerEvent() //метод вызывает событие
    {
        NewEvent?.Invoke(this, EventArgs.Empty); //если событие не равно null, оно будет вызвано
    }
}

class Program2
{
    static void Main()
    {
        C1 obj = new C1();

        obj.Property = 10;
        Console.WriteLine($"Свойство - {obj.Property}");

        obj.Method();

        obj.NewEvent += (sender, e) => Console.WriteLine($"Событие вызвано"); //добавляется обработчик для события

        obj.TriggerEvent(); //вызов события

        obj[0] = 5;
       Console.WriteLine($"Индекс 0 - {obj[0]}");

    }
}

