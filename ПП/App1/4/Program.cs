using System;
public class C3
{
    private int privateField = 1;
    protected int protectedField = 2;
    public int publicField = 3;

    private int PrivateProperty { get; set; } = 4;
    protected int ProtectedProperty { get; set; } = 5;
    public int PublicProperty { get; set; } = 6;   
    public C3()
    {
        Console.WriteLine("Конструктор С3 по умолчанию вызван");
    }
    private void PrivateMethod()
    {
        Console.WriteLine("Приватный метод вызван");
    }
    protected void ProtectedMethod()
    {
        Console.WriteLine("Защищенный метод вызван");
    }
    public void PublicMethod()
    {
        Console.WriteLine("Публичны метод С3 вызван");
        PrivateMethod();
    }
    public void DisplayFields()
    {
        Console.WriteLine($"Private Field: {privateField}");
        Console.WriteLine($"Protected Field: {protectedField}");
        Console.WriteLine($"Public Field: {publicField}");
    }
    public void DisplayProperties()
    {
        Console.WriteLine($"Private Property: {PrivateProperty}");
        Console.WriteLine($"Protected Property: {ProtectedProperty}");
        Console.WriteLine($"Public Property: {PublicProperty}");
    }
}

public class C4 : C3
{
    private int privateFieldC4 = 20;
    public int publicFieldC4 = 25;
    private int PrivatePropertyC4 { get; set; } = 30;
    public int PublicPropertyC4 { get; set; } = 35;
    public C4()
    {
        Console.WriteLine("Конструктор С4 по умолчанию вызван");
    }
    private void PrivateMethodC4()
    {
        Console.WriteLine("приватный метод С4 вызван");
    }
    public void PublicMethodC4()
    {
        Console.WriteLine("Публичный метод С4 вызван");
        PrivateMethodC4();
        ProtectedMethod();
    }
    public void DisplayFieldsC4()
    {
        Console.WriteLine($"Private Field C4: {privateFieldC4}");
        Console.WriteLine($"Public Field C4: {publicFieldC4}");
    }
    public void DisplayPropertiesC4()
    {
        Console.WriteLine($"Private Property C4: {PrivatePropertyC4}");
        Console.WriteLine($"Public Property C4: {PublicPropertyC4}");
    }
}
class Prograam4
{
    static void Main(string[] args)
    {
        C4 obj = new C4();

        obj.DisplayFields();
        obj.DisplayProperties();
        obj.PublicMethod();

        obj.DisplayFieldsC4();
        obj.DisplayPropertiesC4();
        obj.PublicMethodC4();
    }
}
