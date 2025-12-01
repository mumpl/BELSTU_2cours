using System;
using System.Net.Security;

public class Circle
{
    private double _radius;
    public Circle(double _radius)
    {
        this._radius = _radius;
    }
    public double CalcDlina()
    {
        return 2 * Math.PI * _radius;
    }
    public double CalcArea()
    {
        return Math.PI * Math.Pow(_radius, 2);
    }
}
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
    public bool isAdult()
    {
        return Age >= 18;
    }
}
public class Employee
{
    public string Name { get; set; }
    public string Status { get; set; }
    public int Salary { get; set; }

    public Employee(string name, string status, int salary)
    {
        Name = name;
        Status = status;
        Salary = salary;
    }
    public void PrintInfo()
    {
        Console.WriteLine($"Имя сотрудника: {Name}, должность: {Status}, зарплата: {Salary}");
    }
}
public class BankAccount
{
    private double _balance;
    public BankAccount(double balance)
    {
        _balance = balance;
    }
    public void Deposit(double amount)
    {
        if (amount < 0)
                throw new ArgumentException("Сумма вклада должна быть положительной");
        _balance += amount;

    }
    public void Withdraw(double amount)
    {
        if (amount < 0)
            throw new ArgumentException("Сумма должна быть положительной");
        if (amount > _balance)
            throw new ArgumentException("Недостаточно средств");
        _balance -= amount;
    }
    public double ShowBalance()
    {
        return _balance;
    }
}
public class Rectangle
{
    public double _a;
    public Rectangle(double _a)
    {
        this._a = _a;
    }
    public double getArea()
    {
        return _a*_a;
    }
    public double getPerimeter()
    {
        return _a*4;
    }
}
public class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }
    public void ApplyDiscount(double persent)
    {
        Price *= (100 - persent) / 100;
    }
}
public class Books
{
    public string Name { get; set; }
    public string Author { get; set; }
    public int Pages { get; set; }
    public Books(string name, string author, int pages)
    {
        Name = name;
        Author = author;
        Pages = pages;
    }
    public void PrintBooks() => Console.WriteLine($"Название: {Name}, Автор: {Author}, Кол-во страниц: {Pages}");
}

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("----------- ЗАДАЧА 1 НА РАДИУС ---------------");

        Console.WriteLine("Введите радиус");
        double radius = double.Parse(Console.ReadLine());

        Circle circle2 = new Circle(radius);

        Console.WriteLine($"Длина окружности = {circle2.CalcDlina()}");
        Console.WriteLine($"Площадь круга = {circle2.CalcArea()}");

        Console.WriteLine("----------- ЗАДАЧА 2 НА СОВЕРШЕННОЛЕТИЕ ---------------");

        Person person = new Person("Lisa", 19);
        Console.WriteLine($"{person.Name} сшвершеннолетний? - {person.isAdult()}");

        Console.WriteLine("----------- ЗАДАЧА 3 НА РАБОТНИКОВ ---------------");

        Employee emp1 = new Employee("lisa", "headmaster", 15);
        Employee emp2 = new Employee("lisa", "headmaster", 16);

        Console.WriteLine($"Информация о сотрудниках:");
        emp1.PrintInfo();
        emp2.PrintInfo();

        Console.WriteLine("----------- ЗАДАЧА 4 НА БАНК ---------------");
        BankAccount bankAccount = new BankAccount(500);
        bankAccount.Deposit(50);
        bankAccount.Withdraw(50);
        Console.WriteLine($"Ваш текущий баланс: {bankAccount.ShowBalance()}");

        Console.WriteLine("----------- ЗАДАЧА 5 НА КВАДРАТ ---------------");
        Rectangle rectangle = new Rectangle(4);
        Console.WriteLine($"Периметр квадрата: {rectangle.getPerimeter()}");
        Console.WriteLine($"Площадь квадрата: {rectangle.getArea()}");

        Console.WriteLine("Введите длину стороны квадрата");
        double storona = double.Parse(Console.ReadLine());
        Rectangle rectangle2 = new Rectangle(storona);
        Console.WriteLine($"Периметр квадрата: {rectangle2.getPerimeter()}");
        Console.WriteLine($"Площадь квадрата: {rectangle2.getArea()}");

        Console.WriteLine("----------- ЗАДАЧА 6 НА СКИДКИ ---------------");
        Product product = new Product("la", 1000);
        product.ApplyDiscount(50);
        Console.WriteLine($"Цена товара после скидки: {product.Price}");

        Console.WriteLine("----------- ЗАДАЧА 7 НА СПИСОК КНИГ ---------------");
        var books = new List<Books>
        {
            new Books("lala", "lala", 1500),
            new Books("ldldld", "slsls", 200),
            new Books("pdpdp", "a,ldl", 300)
        };
        foreach(var book in books)
            book.PrintBooks();


    }
}
