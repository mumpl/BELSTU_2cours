using System;
public class C1
{
    private const int A = 1;
    public const int B = 2;
    protected const int C = 3;

    private int privateField;
    public int publicField;
    protected int protectedField;

    private int PrivateProperty { get; set; }
    public int PublicProperty { get; set; }
    protected int ProtectedProperty { get; set; }

    //конструктор по умолчанию
    public C1()
    {
        privateField = 1;
        publicField = 2;
        protectedField = 3;
        PrivateProperty = 4;
        PublicProperty = 5;
        ProtectedProperty = 6;
    }

    //конструктор с параметрами
    public C1(int publicField, int privateField, int protectedField)
    {
        this.publicField = publicField;
        this.privateField = privateField;
        this.protectedField = protectedField;
    }

    //копирующий конструктор
    public C1(C1 obj)
    {
        this.publicField= obj.publicField;
        this.privateField= obj.privateField;    
        this.protectedField= obj.protectedField;
        this.PublicProperty = obj.PublicProperty;
        this.ProtectedProperty = obj.ProtectedProperty;
        this.PrivateProperty = obj.PrivateProperty;
    }

    private void PrivateMethod()
    {
        Console.WriteLine("приватный метод вызван");
    }

    public void PublicMethod()
    {
        Console.WriteLine("Публичный метод вызван");
        PrivateMethod();
    }

    protected void ProtectedMethod()
    {
        Console.WriteLine("Защищенный метод вызван");
    }

    //мотед для вывода значений полей
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
    }
}

class Program1
{
    static void Main()
    {
        //создание объекта с конструктором по умолчанию
        C1 obj1 = new C1();
        obj1.DisplayFields();
        obj1.DisplayProperties();
        obj1.PublicMethod();

        //создание объекта с конструктором с параметрами
        C1 obj2 = new C1(10, 20, 30);
        obj2.DisplayFields();
        obj2.DisplayProperties();

        //созд-е объекта с копирующим конструктором
        C1 obj3 = new C1(obj2);
        obj3.DisplayFields();
        obj3.DisplayProperties();
    }
}
