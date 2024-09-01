namespace Assignment5
{
    using System;
    using System.Collections.Generic;

    public abstract class Employee
    {
        private static int nextId = 1; // Static counter for unique IDs
        public int Id { get; }
        public string Name { get; set; }
        public string ReportingManager { get; set; }

        public Employee(string name, string reportingManager)
        {
            Id = nextId++;
            Name = name;
            ReportingManager = reportingManager;
        }

        public abstract void DisplayDetails();
    }

    public class ContractEmployee : Employee
    {
        public DateTime ContractDate { get; set; }
        public int Duration { get; set; } // Duration in months
        public double Charges { get; set; }

        public ContractEmployee(string name, string reportingManager, DateTime contractDate, int duration, double charges)
            : base(name, reportingManager)
        {
            ContractDate = contractDate;
            Duration = duration;
            Charges = charges;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine($"Contract Employee: ID={Id}, Name={Name}, Reporting Manager={ReportingManager}, " +
                              $"Contract Date={ContractDate.ToShortDateString()}, Duration={Duration} months, Charges={Charges}");
        }
    }

    public class PayrollEmployee : Employee
    {
        public DateTime JoiningDate { get; set; }
        public int Experience { get; set; } // Experience in years
        public double BasicSalary { get; set; }
        public double DA { get; private set; } // Dearness Allowance
        public double HRA { get; private set; } // House Rent Allowance
        public double PF { get; private set; } // Provident Fund

        public PayrollEmployee(string name, string reportingManager, DateTime joiningDate, int experience, double basicSalary)
            : base(name, reportingManager)
        {
            JoiningDate = joiningDate;
            Experience = experience;
            BasicSalary = basicSalary;
            CalculateBenefits();
        }

        private void CalculateBenefits()
        {
            if (Experience > 10)
            {
                DA = 0.10 * BasicSalary;
                HRA = 0.085 * BasicSalary;
                PF = 6200;
            }
            else if (Experience > 7 && Experience <= 10)
            {
                DA = 0.07 * BasicSalary;
                HRA = 0.065 * BasicSalary;
                PF = 4100;
            }
            else if (Experience > 5 && Experience <= 7)
            {
                DA = 0.041 * BasicSalary;
                HRA = 0.038 * BasicSalary;
                PF = 1800;
            }
            else
            {
                DA = 0.019 * BasicSalary;
                HRA = 0.02 * BasicSalary;
                PF = 1200;
            }
        }

        public double CalculateNetSalary()
        {
            return BasicSalary + DA + HRA - PF;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine($"Payroll Employee: ID={Id}, Name={Name}, Reporting Manager={ReportingManager}, " +
                              $"Joining Date={JoiningDate.ToShortDateString()}, Experience={Experience} years, " +
                              $"Basic Salary={BasicSalary}, DA={DA}, HRA={HRA}, PF={PF}, Net Salary={CalculateNetSalary()}");
        }
    }

    public class EmployeeProgram
    {
        private List<Employee> employees = new List<Employee>();

        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
        }

        public void DisplayAllEmployees()
        {
            foreach (var employee in employees)
            {
                employee.DisplayDetails();
                Console.WriteLine();
            }
        }

        public int GetTotalEmployees()
        {
            return employees.Count;
        }

        public static void Main(string[] args)
        {
            EmployeeProgram program = new EmployeeProgram();

            // Adding some sample employees
            program.AddEmployee(new ContractEmployee("Alice", "Manager1", DateTime.Parse("2023-01-15"), 12, 50000));
            program.AddEmployee(new PayrollEmployee("Bob", "Manager2", DateTime.Parse("2015-05-20"), 9, 70000));
            program.AddEmployee(new PayrollEmployee("Charlie", "Manager3", DateTime.Parse("2018-07-10"), 3, 45000));

            // Displaying all employees
            program.DisplayAllEmployees();

            // Displaying total number of employees
            Console.WriteLine($"Total number of employees: {program.GetTotalEmployees()}");
        }
    }
}