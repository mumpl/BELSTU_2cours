using System;
using System.Net.NetworkInformation;
public interface I1
{
    int Property { get; set; }

    void Method();

    event EventHandler NewEvent;

    int this[int index] { get; set; }
}
public class C2 : I1
{
    private int privateField;
    public int publicField;
    protected int protectedField;

    public int Property { get; set; }
    private int PrivateProperty { get; set; }
    public int PublicProperty { get; set; }
    protected int ProtectedProperty { get; set; }

    public event EventHandler NewEvent;

    private int[] array = new int[10];
    public int this[int index]
    {
        get => array[index];
        set => array[index] = value;
    }

    public C2()
    {
        privateField = 1;
        publicField = 2;
        protectedField = 3;
        PrivateProperty = 4;
        PublicProperty = 5;
        ProtectedProperty = 6;
        Property = 100;
    }

    public C2(int publicField, int privateField, int protectedField)
    {
        this.publicField = publicField;
        this.privateField = privateField;
        this.protectedField = protectedField;
        Property = 200;
    }

    public C2(C2 obj)
    {
        this.publicField = obj.publicField;
        this.privateField = obj.privateField;
        this.protectedField = obj.protectedField;
        this.PublicProperty = obj.PublicProperty;
        this.ProtectedProperty = obj.ProtectedProperty;
        this.PrivateProperty = obj.PrivateProperty;
        this.Property = obj.Property;
    }

    public void Method()
    {
        Console.WriteLine("Метод из I1 внедрён в С2");
    }

    private void PrivateMethod()
    {
        Console.WriteLine("Приватный метод вызван");
    }

    // Публичный метод
    public void PublicMethod()
    {
        Console.WriteLine("Публичный метод вызван");
        PrivateMethod(); // Вызов приватного метода
    }

    public void TriggerEvent()
    {
        NewEvent?.Invoke(this, EventArgs.Empty);
    }

    public void DisplayFields()
    {
        Console.WriteLine($"Приватное поле - {privateField}");
        Console.WriteLine($"Публичное поле - {publicField}");
        Console.WriteLine($"Защищенное поле - {protectedField}");
    }

    public void DisplayProperties()
    {
        Console.WriteLine($"Приватное свойство - {PrivateProperty}");
        Console.WriteLine($"Публичное свойство - {PublicProperty}");
        Console.WriteLine($"Защищенное свойство - {ProtectedProperty}");
        Console.WriteLine($"Свойство из интерфейса - {Property}");
    }
}
class Program3
{
    static void Main()
    {
        C2 obj1 = new C2();
        obj1.DisplayFields();
        obj1.DisplayProperties();
        obj1.PublicMethod();
        obj1.Method();

        obj1.NewEvent += (sender, e) => Console.WriteLine($"Событие вызвано");
        obj1.TriggerEvent();

        obj1[0] = 10;
        Console.WriteLine($"Индекс 0 - {obj1[0]}");

        //создание объекта с конструктором с параметрами
        C2 obj2 = new C2(10, 20, 30);
        obj2.DisplayFields();
        obj2.DisplayProperties();

        //созд-е объекта с копирующим конструктором
        C2 obj3 = new C2(obj2);
        obj3.DisplayFields();
        obj3.DisplayProperties();

    }
}