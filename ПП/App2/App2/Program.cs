using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Collections.Generic;
public interface IStaff
{
    List<JobVacancy> GetJobVacancies();
    List<Employee> GetEmployees();
    List<JobTitle> GetJobTitles();
    int AddJobTitle (JobTitle jobTitle);
    string PrintJobVacancies();
    bool DelJobTitle(int id);
    bool OpenJobVacancy(JobVacancy jobVacancy);
    bool CloseJobVacancy (int id);
    Employee Recruit (JobVacancy jobVacancy, Person person);
    bool Dismiss(int id, Reason reason);
}
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
    public Organization(int id)
    {
        Id = id;
        TimeStamp = DateTime.Now;
    }
    public Organization(string name, string shortName, string adress)
    {
        Name = name;
        ShortName = shortName;
        Adress = adress;
        TimeStamp = DateTime.Now;
    }
    public void SetId(int id) => Id = id;
    public void SetName(string name) => Name = name;
    public void SetShortName(string shortName) => ShortName = shortName;
    public void SetAdress(string adress) => Adress = adress;
    public void SetTimeStamp(DateTime timeStamp) => TimeStamp = timeStamp;
    public void PrintInfo()
    {
        Console.WriteLine($"Id: {Id}, Name: {Name}, Short name: {ShortName}, Adress: {Adress}, TimeStamp: {TimeStamp}");
    }

}
public class University : IStaff
{
    public string Name { get; protected set; }
    public string ShortName { get; protected set; }
    public string Adress { get; protected set; }
    private List<Faculty> faculties = new List<Faculty>();
    private List<JobVacancy> jobVacancies = new List<JobVacancy>();
    public University() { }
    public University(string name, string shortName, string adress)
    {
        Name=name;
        ShortName=shortName;
        Adress=adress;
    }
    public int AddFaculty(Faculty faculty)
    {
        faculties.Add(faculty);
        return faculties.Count;
    }
    public bool UpdFaculty(Faculty faculty)
    {
        var existingFaculty = faculties.Find(f => f.Id == faculty.Id);
        if (existingFaculty != null)
        {
            existingFaculty = faculty;
            return true;
        }
        return false;
    }
    public bool RemoveFaculty(int id)
    {
        var faculty = faculties.Find(f => f.Id == id);
        if (faculty != null)
        {
            faculties.Remove(faculty);
            return true;
        }
        return false;
    }
    protected List<Faculty> Getfaculties() => faculties;
    public List<JobVacancy> GetJobVacancies() => jobVacancies;
    public void PrintInfo()
    {
        Console.WriteLine($"University: {Name}, Short name: {ShortName}, Adress:{Adress}");
    }
    public int AddJobTitle(JobTitle jobTitle)
    {
        return jobTitle.Id;
    }
    public bool OpenJobVacancy(JobVacancy jobVacancy)
    {
        jobVacancies.Add(jobVacancy);
        return true;
    }
    public bool CloseJobVacancy(int id)
    {
        var vacancy = jobVacancies.Find(v => v.Id == id);
        if(vacancy != null)
        {
            jobVacancies.Remove(vacancy);
            return true;
        }
        return false;
    }
    public bool Dismiss(int id, Reason reason)
    {
        return true;
    }
    public string PrintJobVacancies()
    {
        return string.Join(", ", jobVacancies);
    }
    public List<Employee> GetEmployees()
    {
        return new List<Employee>();
    }
    public List<JobTitle> GetJobTitles()
    {
        return new List<JobTitle>();
    }
    public bool DelJobTitle(int id)
    {
        return true;
    }
    public Employee Recruit(JobVacancy jobVacancy, Person person)
    {
        return new Employee{Name = person.Name};
    }
}
public class Faculty
{
    public int Id { get; set; }
    public string FacultyName { get; set; }
    public string ShortName { get; set; }
    public string Address { get; set; }
    protected List<Department> departments = new List<Department>();
    public Faculty(string facultyName, string shortName, string adress)
    {
        FacultyName = facultyName;
        ShortName = shortName;
        Address = adress;
    }

    public Faculty() { }

    public int AddDepartment(Department department)
    {
        departments.Add(department);
        return departments.Count;
    }
    public List<Department> GetDepartments() => departments;
}

public class JobVacancy
{
    public int Id { get; set; }
    public string Title { get; set; }
}
public class JobTitle
{
    public int Id { get; set; }
    public string Title { get; set; }
}
public class Department
{
    public string DepartmentName { get; set; }
}
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public class Person
{
    public string Name { get; set; } 
}
public class Reason
{
    public string Description { get; set; }
}
public class Program
{
    public static void Main(string[] args)
    {
        Organization org1 = new Organization();
        org1.SetId(1);
        org1.SetName("Mr.Finch Corp");
        org1.SetShortName("MFC");
        org1.SetAdress("221-B Baker street");
        org1.SetTimeStamp(DateTime.Now);
        org1.PrintInfo();

        Organization org2 = new Organization("I Don't Know", "IDK", "123 Street");
        org2.PrintInfo();

        University uni1 = new University("Tech University" ,"BSTU", "456 Street");
        uni1.PrintInfo();

        Faculty faculty1 = new Faculty("Information Technology", "IT", "15 Street");
        int facultyCount = uni1.AddFaculty(faculty1);
        Console.WriteLine($"Коичество факультетов после добавления - {facultyCount}");

        bool updated = uni1.UpdFaculty(faculty1);
        Console.WriteLine($"Обновленный факультет - {updated}");

        bool removed = uni1.RemoveFaculty(faculty1.Id);
        Console.WriteLine($"Удаленный факультет - {removed}");

        Faculty faculty2 = new Faculty("Print Technology", "PT", "36 street");

        Department department1 = new Department { DepartmentName = "Mechanical Engeneering" };
        int departmentCount = faculty2.AddDepartment(department1);
        Console.WriteLine($"Количество отделов после добавления - {departmentCount}");

        JobVacancy vacancy1 = new JobVacancy { Id = 1, Title = "Professor" };
        uni1.OpenJobVacancy(vacancy1);
        Console.WriteLine($"Создана вакансия с ID - {vacancy1.Id}");

        Person candidate = new Person { Name = "Merlin" };
        bool vacancyClosed = uni1.CloseJobVacancy(vacancy1.Id);
        Console.WriteLine($"Вакансия закрыта - {vacancyClosed}");

    }
}
