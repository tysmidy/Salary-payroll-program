using System;
using System.Reflection.Metadata.Ecma335;
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

    public double HealthPercheck()
    {
        return HealthInsurance;
    }

    public double DentalPercheck()
    {
        return DentalInsurance;
    }

    public double VisionPercheck()
    {
        return Vision;
    }

    public double Four01kPercheck()
    {
        return Four01k;
    }

}

public class Taxes
{
    private double FedPercent;
    private double StatePercent;
    private const double SocialSecurityPercent = 0.062;
    private const double MedicarePercent = 0.0145;
    private double FederalTaxRate;

    public double SocialSecurityTax(double grossPay)
    {
        return grossPay * SocialSecurityPercent;
    }

    public double StateTax(double taxableIncome)
    {
        var tax = 0;
        //need to finish building out state tac table
        if (taxableIncome > 7200)
        {
            StatePercent = 0.075;
            tax +=

        }

        return tax;
    }

    public double MedicareTax(double grossPay)
    {
        return grossPay * MedicarePercent;
    }

    public double FederalTax(double grossPay, double pretaxDeductions)
    {
        double taxableIncome = grossPay - pretaxDeductions;

        //table added amount is based off of 24 pay checks per year. Will have to differentant between semi and bi weekly

        if (taxableIncome > 103351)
        {
            FederalTaxRate = 0.012;
            return taxableIncome * FederalTaxRate + 614.63;
        }
        else if (taxableIncome >= 48476 && taxableIncome <= 103350)
        {
            FederalTaxRate = .22;
            return taxableIncome * FederalTaxRate + 194.34;
        }
        else if (taxableIncome >= 11926 && taxableIncome <= 48475)
        {
            FederalTaxRate = .12;
            return taxableIncome * FederalTaxRate + 41.46;
        }
        else if (taxableIncome > 0 && taxableIncome <= 11925)
        {
            FederalTaxRate = .10;
            return taxableIncome * 0.10;
        }
        return 0;




        //return taxableIncome * FederalTaxRate + 614.63;
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


        //after this, can create if statments to specify biweekly/semi-monthly pay, input vaues that way. 
        //Next steps after this will be to go back and calulate variables on user input.



        //enter in salary
        WriteLine("Hello User.\nPlease enter how much you get payed per year.");
        string salaryInput = ReadLine();
        double salary;

        //validate inout is correct
        while (!double.TryParse(salaryInput, out salary) || salary <= 0)
        {
            WriteLine("That input in incorrect. Please enter in your salary as a number and greater than 0");
            salaryInput = ReadLine();

        }

        //enter in number of paychecks
        WriteLine("Thank you for that!\nNow, please enter in the number of paychecks you receive per year.");
        string paycheckInput = ReadLine();
        int paychecksPerYear;

        //validate input is correct
        while (!int.TryParse(paycheckInput, out paychecksPerYear) || paychecksPerYear <= 0)
        {
            WriteLine("That input in incorrect.\nPlease rekey the number of paychecks you recive per year.");
            paycheckInput = ReadLine();

        }

        //need to add taxes into this.
        Employee employee = new Employee(salary, paychecksPerYear);

        double grossPay = employee.GrossPayPerCheck();
        double netPay = employee.NetPay();
        double healthcare = employee.Deductions.HealthPercheck();
        double dental = employee.Deductions.DentalPercheck();
        double vision = employee.Deductions.VisionPercheck();
        double Four01k = employee.Deductions.Four01kPercheck();


        WriteLine($"Gross pay per paycheck: {grossPay:C}.");
        WriteLine($"Net pay per paycheck: {netPay:C}.\n");

        WriteLine("Below are your deductions per paycheck.\n");

        WriteLine($"Healthcare deduction - {healthcare:C}.");
        WriteLine($"Dental Insurance deduction - {dental:C}.");
        WriteLine($"Dental Insurance deduction - {vision:C}.");
        WriteLine($"401k deduction - {Four01k:C}.");


    }
}
