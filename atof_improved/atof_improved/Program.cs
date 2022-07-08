﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace atof_improved
{
    class Program
    {
        private const string ErrorPath = "../../../../output.err";

        public static double AtofImproved(string number)
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
            List<InputData> dataList =  Csv.ReadCsvFile();
            int line = 0;
            if(File.Exists(ErrorPath))
            {
                File.Delete(ErrorPath);
            }

            StreamWriter streamWriter = new StreamWriter(ErrorPath, true);

            foreach (var data in dataList)
            {
                line++;
                try
                {
                    data.ResultValue = AtofImproved(data.Result);
                }
                catch (Exception ex)
                {
                    streamWriter.WriteLine("Line {0} cannot be converted into a number. Original value {1} date {2}.", line, data.Result, data.DateTime.ToShortDateString());
                }
            }

            streamWriter.Close();
        }
    }
}
