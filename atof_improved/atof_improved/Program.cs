using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace atof_improved
{
    class Program
    {
        private const string ErrorPath = "../../../../output.err";
        private const string OutputPath = "../../../../output.csv";

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

        static StreamWriter CreateFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            return new StreamWriter(path, true);
        }


        static void Main(string[] args)
        {
            List<InputData> dataList = Csv.ReadCsvFile();
            int line = 1; //because of header

            StreamWriter streamWriter = CreateFile(ErrorPath);

            foreach (var data in dataList)
            {
                line++;
                try
                {
                    data.ResultValue = AtofImproved(data.Result);
                    data.Error = false;
                }
                catch (Exception ex)
                {
                    data.Error = true;
                    streamWriter.WriteLine("Line {0} cannot be converted into a number. Original value {1} date {2}.", line, data.Result, data.DateTime.ToShortDateString());
                }
            }

            var dataByYearAndMounth =
                from d in dataList
                group d by new
                {
                    d.Year,
                    d.Month
                } into groupData
                select new OutputData()
                {
                    Year = groupData.Key.Year,
                    Month = groupData.Key.Month,
                    NumberOfMeasures = groupData.Where(x => x.Error == false).Count(), //count measures only if result is good
                    Sum = groupData.Sum(x => x.ResultValue)
                };

            dataByYearAndMounth = dataByYearAndMounth.OrderBy(x => x.Year).ThenBy(x => x.Month);

            StreamWriter outputWriter = CreateFile(OutputPath);
            outputWriter.WriteLine("Mesec,Godina,UkupnoMerenja,Suma");
            foreach(var x in dataByYearAndMounth)
            {
                outputWriter.WriteLine(x.Year + "," + x.getNameOfMonth(x.Month) + "," + x.NumberOfMeasures + "," + x.Sum);
            }

            outputWriter.Close();
            streamWriter.Close();
        }
    }
}
