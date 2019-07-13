﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;

namespace PRUD.Weather.Data
{
    public static class Utilities
    {
        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

        public static IEnumerable<string> ReadATextFile(string fullPath)
        {
            string filedata = string.Empty;
            using (System.IO.StreamReader sr = new System.IO.StreamReader(fullPath))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    filedata += line;
                }
            }

            return filedata.Split(',').ToList();
        }

        public static IEnumerable<string> ReadATextFile(string fullPath, char separator)
        {
            string filedata = string.Empty;
            using (System.IO.StreamReader sr = new System.IO.StreamReader(fullPath))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    filedata += line;
                }
            }

            return filedata.Split(separator).ToList(); ;
        }

        internal static dynamic SaveWeatherReportForCity(string city, string content)
        {
            var file = city + "_" + DateTime.Now.Date.ToString("ddMMMyyyy") + ".txt";
            var fullPath = Path.Combine("outputfolder", city);
            var pathToSave = Path.Combine(@"C:\", fullPath, file);
            //var pathToSave = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fullPath, file);
            if (Directory.Exists(pathToSave))
                if (File.Exists(pathToSave))
                    return new { City = city, Report = "Report is already Exist", ReportName = file };
                else
                {
                    using (FileStream fs = File.Create(pathToSave))
                    {
                        // Add some text to file    
                        Byte[] title = new UTF8Encoding(true).GetBytes(content);
                        fs.Write(title, 0, title.Length);
                        byte[] author = new UTF8Encoding(true).GetBytes("Weather Report by Shailendra Dukane");
                        fs.Write(author, 0, author.Length);
                    }
                }
            else
            {
                Directory.CreateDirectory(pathToSave);
                using (FileStream fs = File.Create(pathToSave))
                {
                    // Add some text to file    
                    Byte[] title = new UTF8Encoding(true).GetBytes(content);
                    fs.Write(title, 0, title.Length);
                    byte[] author = new UTF8Encoding(true).GetBytes("Weather Report by Shailendra Dukane");
                    fs.Write(author, 0, author.Length);
                }
            }

            return new { City = city, Status = "New report created", Name = file };
        }
    }
}
