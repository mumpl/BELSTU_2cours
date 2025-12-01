using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices.JavaScript;

public class Organization
{
    public int Id { get; private set; }
    public string Name { get; protected set; }
    public string ShortName { get; protected set; }
    public string Adress { get; protected set; }
    public DateTime TimeStamp { get; protected set; }

    public Organization()
    {
        TimeStamp = DateTime.Now;
    }
    public Organization( string name, string shortName, string adress)
    {
        Name = name;
        ShortName = shortName;
        Adress = adress;
    }
    public void SetId(int id)  => Id = id;
    public void SetName(string name) => Name = name;    
    public void SetShortName(string shortName) => ShortName = shortName;
    public void SetAdress(string adress) => Adress = adress;
    public void SetTimeStamp(DateTime timeStamp) => TimeStamp = timeStamp;
    public void PrintInfo()
    {
        Console.WriteLine($"ID - {Id}, Имя - {Name}, короткое имя - {ShortName},адрес - {Adress}, время - {TimeStamp}");
    }
}
public class University : Organization
{
    protected List<Faculty> faculties = new List<Faculty>();
    public University() : base() { }
    public University(string name, string shortName, string adress) : base(name, shortName, adress) { }
    public int AddFaculty(Faculty faculty)
    {
        faculties.Add(faculty);
        return faculties.Count;
    }
    public List<Faculty> GetFaculties() => faculties;   
    public new void PrintInfo()
    {
        Console.WriteLine($"University: {Name}, Short name: {ShortName}, Address: {Adress}");
    }
}
public class Faculty : Organization
{
    protected List<Department> departments = new List<Department>();
    public Faculty() : base() { }
    public Faculty(string name, string shortName, string adress) : base(name, shortName, adress) { }
    public int AddDepartment(Department department)
    {
        departments.Add(department);
        return departments.Count;
    }
    public List<Department> GetDepartments() => departments;
    public new void PrintInfo()
    {
        Console.WriteLine($"Faculty: {Name}, Short name: {ShortName}, Address: {Adress}");
    }
}
public class Department
{
    public int id { get; set; }
    public string DepartmentName { get; set; }
}
public class Program
{
    public static void Main(string[] args)
    {
        Organization org1 = new Organization("Mr.Finch Corp","MFC", "12 street");
        org1.SetId(1);
        org1.PrintInfo();

        University uni1 = new University("Tech University", "BelSTU", "123 street");
        uni1.SetId(2);
        uni1.PrintInfo();

        Faculty faculty1 = new Faculty("Information Technology", "iT", "456 street");
        faculty1.SetId(3);
        faculty1 .PrintInfo();

        uni1.AddFaculty(faculty1);
        Console.WriteLine($"Количество факультетов в {uni1.Name}: {uni1.GetFaculties().Count}");

        Department dep1 = new Department { id = 4, DepartmentName = "What" };
        faculty1.AddDepartment(dep1);
        faculty1.PrintInfo();
    }
}
