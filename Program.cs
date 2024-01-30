using System;
using static System.Console;


//declare const and variables
double salary = 42500;
double paychecksPerYear = 24;
double grossCheck;
double netCheck;

//declare tax variables 
double fedPercent = .12;
//double fedFlatAmount = 9.35;
double stateTax;
double socialSecurity;
double medicare;


//delcare deductions 
double heathIns = 139.70;
double dentalIns = 13.45;
double vision = 1.18;
double four01k = .06;


//calculate gross pay per pay period 
grossCheck = salary / paychecksPerYear;
WriteLine("Your gross pay per paycheck will be {0}", grossCheck);

//deduct taxes and deductions and use subtraction to create a writeline of your net pay 
// will have to write an if statement for fed percent to tax the percent amount after 1038

//declare fed taxable wages. For later notes, the equation for fed taxable wages is (grosspay - pretax deductions - 401k)
double fedTaxableWages = grossCheck - heathIns - dentalIns - vision;
