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
     double BaseSalary { get;  }
    double GetTotalSalary();

    double GetBonus();
    void SetQualification(QualificationLevel newQaulification);
    void SetWorkHours(int hours);
    QualificationLevel Qualification { get; set; }


}

public class Employee : IEmployee
{
    public string Name { get; set; }
    public QualificationLevel Qualification { get; set; }

    public double BaseSalary { get;  }

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
    public override string ToString()
    {
        return $"Bonus for {Name}: {BaseSalary}";

    }

}

public abstract class EmployeeDecorator 
{
    public IEmployee employee { get; set; }

 
   

    public virtual double GetTotalSalary()
    {
        var totalSalary = employee.BaseSalary + employee.GetBonus();
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
    public StaffEmployee(IEmployee employee) 
    {
        this.employee = employee;
    }

    public  double GetBonus()
    {
        var baseSalary = employee.BaseSalary;
        return baseSalary * 0.2;
    }
}
public class Contractor : EmployeeDecorator
{
    public Contractor(IEmployee employee) 
    {
        this.employee = employee;
    }
    public  double GetBonus()
    {
        var baseSalary = employee.BaseSalary;
        return baseSalary * 0.1;
    }
}
public class EmployeeManager
{
    private List<IEmployee> employees;

    public EmployeeManager()
    {
        employees = new List<IEmployee>();
    }

    public void AddEmployee(EmployeeDecorator employee)
    {
        employees.Add(employee.employee);
    }

    public List<IEmployee> GetTopPerformers()
    {
        return employees.OrderByDescending(e => e.Qualification).Take(10).ToList();
    }

  
}


class Program
{
    static void Main(string[] args)
    {
        EmployeeManager manager = new EmployeeManager();
        IEmployee employee = new Employee("John", QualificationLevel.Senior, 5000);
        IEmployee employee2 = new Employee("Alah", QualificationLevel.Middle, 4000);

        EmployeeDecorator contractorDecorator = new Contractor(employee);
        EmployeeDecorator staffEmployeeDecorator = new StaffEmployee(employee2);

        manager.AddEmployee(staffEmployeeDecorator);
        manager.AddEmployee(contractorDecorator);

        staffEmployeeDecorator.SetQualification(QualificationLevel.Junior);
        List<IEmployee> topPerformers = manager.GetTopPerformers();

        foreach (var item in topPerformers)
        {
            Console.WriteLine(item);
        }
       
    }
}