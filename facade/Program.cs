using System;
using System.Collections.Generic;
using System.Linq;

public enum QualificationLevel
{
    Trainee,
    Junior,
    Middle,
    Senior
}

public interface IEmployee
{
    double GetTotalSalary();
    void SetQualification(QualificationLevel newQualification);
    void SetWorkHours(int hours);
}

public class StaffEmployee : IEmployee
{
    public string Name { get; set; }
    public QualificationLevel Qualification { get; set; }
    public double BaseSalary { get; set; }
    public int WorkHours { get; private set; }

    public StaffEmployee(string name, QualificationLevel qualification, double baseSalary)
    {
        Name = name;
        Qualification = qualification;
        BaseSalary = baseSalary;
    }

    public double GetTotalSalary()
    {
        double bonus = BaseSalary * 0.1; // Fixed bonus for simplicity
        return BaseSalary + bonus;
    }

    public void SetQualification(QualificationLevel newQualification)
    {
        Qualification = newQualification;
    }

    public void SetWorkHours(int hours)
    {
        WorkHours = hours;
    }
}

public class Contractor : IEmployee
{
    public string Name { get; set; }
    public QualificationLevel Qualification { get; set; }
    public double HourlyRate { get; set; }
    public int WorkHours { get; private set; }

    public Contractor(string name, QualificationLevel qualification, double hourlyRate)
    {
        Name = name;
        Qualification = qualification;
        HourlyRate = hourlyRate;
    }

    public double GetTotalSalary()
    {
        return HourlyRate * WorkHours;
    }

    public void SetQualification(QualificationLevel newQualification)
    {
        Qualification = newQualification;
    }

    public void SetWorkHours(int hours)
    {
        WorkHours = hours;
    }
}

public class PayrollFacade
{
    private List<IEmployee> employees;

    public PayrollFacade()
    {
        employees = new List<IEmployee>();
    }

    public void AddStaffEmployee(string name, QualificationLevel qualification, double baseSalary)
    {
        var employee = new StaffEmployee(name, qualification, baseSalary);
        employees.Add(employee);
    }

    public void AddContractor(string name, QualificationLevel qualification, double hourlyRate)
    {
        var contractor = new Contractor(name, qualification, hourlyRate);
        employees.Add(contractor);
    }

    public void SetEmployeeQualification(string name, QualificationLevel newQualification)
    {
        Console.WriteLine(name  + newQualification);
    }

    public List<IEmployee> GetTopPerformers()
    {
        return employees.OrderByDescending(e => e.GetTotalSalary()).Take(10).ToList();
    }

 
}

class Program
{
    static void Main(string[] args)
    {
        PayrollFacade payrollFacade = new PayrollFacade();
        payrollFacade.AddStaffEmployee("John Doe", QualificationLevel.Senior, 50000);
        payrollFacade.AddContractor("Jane Smith", QualificationLevel.Middle, 3000);

        payrollFacade.SetEmployeeQualification("John Doe ", QualificationLevel.Senior);

        var topPerformers = payrollFacade.GetTopPerformers();
        Console.WriteLine("Top performers:");
        foreach (var employee in topPerformers)
        {
            if (employee is StaffEmployee)
            {
                var staffEmployee = (StaffEmployee)employee;
                Console.WriteLine($"Name: {staffEmployee.Name}, Qualification: {staffEmployee.Qualification}, Base Salary: {staffEmployee.BaseSalary}");
            }
            else if (employee is Contractor)
            {
                var contractor = (Contractor)employee;
                Console.WriteLine($"Name: {contractor.Name}, Qualification: {contractor.Qualification}, Hourly Rate: {contractor.HourlyRate}");
            }
        }

    }
}
