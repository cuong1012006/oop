internal class Program
{
    
    private static void Main(string[] args)
    {
        MyDate md1 = new MyDate(DateTime.Now.Day,DateTime.Now.Month, DateTime.Now.Year);
        Console.WriteLine("{0}/{1}/{2}",md1.Day,md1.Month,md1.Year);
        Console.ReadKey();
    }
}
using System;
using System.Collections.Generic;

class Person
{
    private string name;
    private string address;
    private double salary;

    public Person(string name, string address, double salary)
    {
        this.name = name;
        this.address = address;
        this.salary = salary;
    }

    public string Name { get => name; set => name = value; }
    public string Address { get => address; set => address = value; }
    public double Salary { get => salary; set => salary = value; }

    // Hiển thị thông tin
    public void DisplayPersonInfo()
    {
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Address: " + Address);
        Console.WriteLine("Salary: " + Salary);
        Console.WriteLine();
    }
}

class Program
{
    // Hàm nhập thông tin có validate salary
    static Person InputPersonInfo()
    {
        Console.Write("Please input name: ");
        string name = Console.ReadLine();

        Console.Write("Please input address: ");
        string address = Console.ReadLine();

        double salary;
        while (true)
        {
            Console.Write("Please input salary: ");
            string input = Console.ReadLine();

            if (!double.TryParse(input, out salary))
            {
                Console.WriteLine("You must input digit.");
                continue;
            }

            if (salary <= 0)
            {
                Console.WriteLine("Salary is greater than zero");
                continue;
            }

            break;
        }

        return new Person(name, address, salary);
    }

    // Sắp xếp danh sách Person theo salary (Bubble Sort)
    static void SortBySalary(List<Person> persons)
    {
        for (int i = 0; i < persons.Count - 1; i++)
        {
            for (int j = 0; j < persons.Count - i - 1; j++)
            {
                if (persons[j].Salary > persons[j + 1].Salary)
                {
                    var temp = persons[j];
                    persons[j] = persons[j + 1];
                    persons[j + 1] = temp;
                }
            }
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("=====Management Person Program=====");

        List<Person> persons = new List<Person>();

        // Nhập thông tin cho 3 người
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine("\nInput Information of Person " + (i + 1));
            Person p = InputPersonInfo();
            persons.Add(p);
        }

        Console.WriteLine("\nInformation of Person you have entered:");
        foreach (var p in persons)
        {
            p.DisplayPersonInfo();
        }

        // Sắp xếp theo salary
        SortBySalary(persons);

        Console.WriteLine("Information of Person after sorting by salary:");
        foreach (var p in persons)
        {
            p.DisplayPersonInfo();
        }
    }
}
