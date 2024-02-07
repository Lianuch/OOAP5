using System;

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
    double GetBonus();
    void SetQualification(QualificationLevel newQaulification);
    void SetWorkHours(int hours);
}

public class Employee : IEmployee
{
    public string Name { get; set; }
    public QualificationLevel Qualification { get; set; }
    public virtual double BaseSalary { get; set; }

    public Employee(string name, QualificationLevel qualification, double baseSalary)
    {
        Name = name;
        Qualification = qualification;
        BaseSalary = baseSalary;
    }

    public double GetBonus()
    {
        return BaseSalary * 0.1;
    }

    public  double GetTotalSalary()
    {
        return BaseSalary + GetBonus();
    }
    public void SetQualification(QualificationLevel qualification)
    {
        Qualification = qualification;
    }
    public void SetWorkHours(int hours)
    {
        int WorkHours = hours;
    }

}

public abstract class EmployeeDecorator : IEmployee
{
    protected IEmployee employee;

    public EmployeeDecorator(IEmployee employee)
    {
        this.employee = employee;
    }

    public abstract double GetBonus();
   

    public virtual double GetTotalSalary()
    {
        var totalSalary = ((Employee)employee).BaseSalary + GetBonus();
        return totalSalary;
    }
    public virtual void SetQualification(QualificationLevel qualification)
    {
        employee.SetQualification(qualification);
    }

    public virtual void SetWorkHours(int hours)
    {
        employee.SetWorkHours(hours);
    }
}

public class StaffEmployee : EmployeeDecorator
{
    public StaffEmployee(IEmployee employee) : base(employee)
    {
    }

    public override double GetBonus()
    {
        var baseSalary = ((Employee)employee).BaseSalary;
        return baseSalary * 0.2;
    }
}
public class Contractor : EmployeeDecorator
{
    public Contractor(IEmployee employee) : base(employee)
    {

    }
    public override double GetBonus()
    {
        var baseSalary = ((Employee)employee).BaseSalary;
        return baseSalary * 0.1;
    }
}
public class EmployeeManager
{
    private List<Employee> employees;

    public EmployeeManager()
    {
        employees = new List<Employee>();
    }

    public void AddEmployee(Employee employee)
    {
        employees.Add(employee);
    }

    public List<Employee> GetTopPerformers()
    {
        return employees.OrderByDescending(e => e.Qualification).Take(10).ToList();
    }

  
}


class Program
{
    static void Main(string[] args)
    {

    }
}