using System;
using static System.Console;

public class Employee
{
    public double Salary { get; set; }
    public int PaychecksPerYear { get; set; }
    public Deductions Deductions { get; set; }
    public Taxes Taxes { get; set; }

    public Employee(double salary, int paychecksPerYear)
    {
        Salary = salary;
        PaychecksPerYear = paychecksPerYear;
        Deductions = new Deductions();
        Taxes = new Taxes();
    }

    public double GrossPayPerCheck()
    {
        return Salary / PaychecksPerYear;
    }

    public double NetPay()
    {
        double grossPay = GrossPayPerCheck();
        double totalDeductions = Deductions.TotalDeductions(grossPay);
        double totalTaxes = Taxes.TotalTaxes(grossPay, Deductions.PretaxDeductions(grossPay));
        return grossPay - totalDeductions - totalTaxes;
    }
}

public class Deductions
{
    public double HealthInsurance { get; set; } = 139.70;
    public double DentalInsurance { get; set; } = 13.45;
    public double Vision { get; set; } = 1.18;
    public double Four01k { get; set; } = 51.23;

    public double PretaxDeductions(double grossPay)
    {
        return HealthInsurance + DentalInsurance + Vision + Four01k;
    }

    public double TotalDeductions(double grossPay)
    {
        return PretaxDeductions(grossPay);
    }
}

public class Taxes
{
    private const double FedPercent = 0.12;
    private const double StatePercent = 0.045;
    private const double SocialSecurityPercent = 0.062;
    private const double MedicarePercent = 0.0145;
    private const double FederalTaxRate = 0.0935;

    public double SocialSecurityTax(double grossPay)
    {
        return grossPay * SocialSecurityPercent;
    }

    public double StateTax(double grossPay)
    {
        return grossPay * StatePercent;
    }

    public double MedicareTax(double grossPay)
    {
        return grossPay * MedicarePercent;
    }

    public double FederalTax(double grossPay, double pretaxDeductions)
    {
        double taxableIncome = grossPay - pretaxDeductions;
        return taxableIncome * FederalTaxRate;
    }

    public double TotalTaxes(double grossPay, double pretaxDeductions)
    {
        return SocialSecurityTax(grossPay) + StateTax(grossPay) + MedicareTax(grossPay) + FederalTax(grossPay, pretaxDeductions);
    }
}

public class Program
{
    public static void Main()
    {
        // Example: Employee with a salary of 50000, paid biweekly (26 paychecks per year)
        Employee employee = new Employee(50000, 26);

        double grossPay = employee.GrossPayPerCheck();
        double netPay = employee.NetPay();

        WriteLine($"Gross pay per paycheck: {grossPay:C}");
        WriteLine($"Net pay per paycheck: {netPay:C}");
        WriteLine("Below are your deductions per paycheck");
        WriteLine("");
            
            
            }
}
