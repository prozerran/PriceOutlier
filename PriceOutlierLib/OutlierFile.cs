using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceOutlierLib
{
    public class OutlierFile : OutlierBase
    {
        // hardcode file for now, could be set in app.config file
        private readonly string FileName = "Outliers.csv";
        private string filePath = string.Empty;

        public OutlierFile()
        {
            filePath = Directory.GetCurrentDirectory() + @"\..\..\..\" + FileName;
        }

        public void SetFilePath(string path)
        {
            filePath = path;
        }
            
        public override int ReadData()
        {
            int count = 0;

            if (!File.Exists(filePath))
                return 0;

            Clear();    // start from clean state

            using (var sr = new StreamReader(filePath))
            {
                var line = sr.ReadLine();   // skip header

                while ((line = sr.ReadLine()) != null)
                {
                    // assume just 2 data [date/price]
                    var ss = line.Split(',');

                    DateTime dt;
                    decimal dp;

                    try
                    {
                        dt = DateTime.ParseExact(ss[0], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        dp = Convert.ToDecimal(ss[1]);
                    }
                    catch   // ignore bad data
                    {
                        continue;
                    }

                    ProcessData(dt, dp);    // calls our outlier detection and processing method
                    count++;
                }
                sr.Close();
            }
            return count;
        }
    }
}
