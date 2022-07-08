using System;
using System.Text.RegularExpressions;

namespace atof_improved
{
    class Program
    {
        static double AtofImproved(string number)
        {
            number = number.ToLower();
            string pattern = @"^[+-]?[0-9]+(\.[0-9]+)?([e][+-]?[0-9]+)?$"; //because there is used method ToLower there is no need for asking if number contains E
            Regex regex = new Regex(pattern);
            if(regex.IsMatch(number))
            {
                if(number.Contains('e'))
                {
                    string firstPart = number.Split('e')[0];
                    string secondPart = number.Split('e')[1];
                    char sign = secondPart[0];

                    double num1 = double.Parse(firstPart);
                    double num2;
                    if(sign == '-' || sign == '+')
                    {
                        num2 = double.Parse(secondPart.Substring(1));
                    } 
                    else
                    {
                        num2 = double.Parse(secondPart);
                    }

                    if (sign == '-')
                    {
                       return num1 / Math.Pow(10, num2);
                    }
                    else
                    {
                       return num1 * Math.Pow(10, num2);
                    }
                }
                else
                {
                    return double.Parse(number);
                }
            }
            else
            {
                throw new Exception("Not a number");
            }
        }

        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("Unesi broj");
                string number = Console.ReadLine();

                try
                {
                    Console.WriteLine(AtofImproved(number));
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
