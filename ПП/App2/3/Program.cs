using System;
using System.Net;
public interface IStaff
{
    string GetName();
    void SetName(string name);
    string GetPosition();
    void SetPosition(string position);
}
public class Organization : IStaff
{
    public int OrganizationID { get; set; }
    public string Name { get; set; }
    public string Adress { get; set; }
    public string Phone { get; set; }
    public string Website { get; set; }
    public Organization(int organizationID, string name, string adress, string phone, string website)
    {
        OrganizationID = organizationID;
        Name = name;
        Adress = adress;
        Phone = phone;
        Website = website;
    }
    public string GetName()
    {
        return Name;
    }
    public void SetName(string name)
    {
        Name = name;
    }
    public string GetPosition()
    {
        return "Position in Organization";
    }
    public void SetPosition(string position) { }
    public virtual string GetDetails()
    {
        return $"ID: {OrganizationID}, Name: {Name}, Address: {Adress}, Phone: {Phone}, Website: {Website}";
    }
}
public class University : Organization
{
    public string UniversityType { get; set; }
    public int Ranking {  get; set; }
    public University(int organizationID, string name, string adress, string phone, string website, string universityType, int ranking): base(organizationID, name, adress, phone, website)
    {
        Ranking = ranking;
        UniversityType = universityType;
    }
    public string GetUniversityInfo()
    {
        return $"University Type: {UniversityType}, Ranking: {Ranking}";
    }
    public override string GetDetails()
    {
        return base.GetDetails() + $", University Type: {UniversityType}, Ranking: {Ranking}";
    }
}
public class Faculty : Organization
{
    public string FacultyName { get; set; }
    public int FacultySize { get; set; }
    public Faculty(int organizationID, string name, string adress, string phone, string website, string facultyName, int facultySize) : base(organizationID, name, adress, phone, website)
    {
        FacultyName = facultyName;
        FacultySize = facultySize;
    }
    public string GetFacultyInfo()
    {
        return $"Faculty name: {FacultyName}, Faculty size: {FacultySize}";
    }
    public override string GetDetails()
    {
        return base.GetDetails() + $", Faculty name: {FacultyName}, Faculty size: {FacultySize}";
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        University university = new University(1, "Tech University", "1 street", "111111", "belstu.by", "Public", 5);

        Faculty faculty = new Faculty(2, "Information Technology", "2 street", "222222", "belstu.by/fit", "Programming", 1234);

        Console.WriteLine(university.GetDetails());
        Console.WriteLine(university.GetUniversityInfo());

        Console.WriteLine(faculty.GetDetails());
        Console.WriteLine(faculty.GetFacultyInfo());
    }
}
