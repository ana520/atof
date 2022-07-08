using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace atof_improved
{
    public class Csv
    {
        private const string Path = "../../../../input.csv";

        public static List<InputData> ReadCsvFile()
        {
            List<InputData> dataList = new List<InputData>();
            using (var streamReader = new StreamReader(Path))
            {
                using(var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    var records = csvReader.GetRecords<Input>().ToList();

                    char separator;
                    foreach (var record in records)
                    {
                        InputData data = new InputData();
                        data.Date = record.Date;
                        data.Comment = record.Comment;
                        data.Result = record.Result;

                        if (record.Date.Contains("."))
                        {
                            separator = '.';
                        }
                        else
                        {
                            separator = '/';
                        }

                        string[] splittedDate = record.Date.Split(separator);
                        data.Day = int.Parse(splittedDate[0]);
                        data.Month = int.Parse(splittedDate[1]);
                        data.Year = int.Parse(splittedDate[2]);
                        data.DateTime = new DateTime(data.Year, data.Month, data.Day);

                        dataList.Add(data);
                    }
                }
            }
            return dataList;
        }
    }
}
