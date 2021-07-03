using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceOutlierLib
{
    public abstract class OutlierBase
    {
        protected Dictionary<DateTime, decimal> cleanData = new Dictionary<DateTime, decimal>();
        protected Dictionary<DateTime, decimal> outlierData = new Dictionary<DateTime, decimal>();
        private readonly decimal outlierDeviation = 5.0M;
        private decimal prevPrice1 = 0.0M;
        private decimal prevPrice2 = 0.0M;

        public Dictionary<DateTime, decimal> CleanData { get => cleanData; }
        public Dictionary<DateTime, decimal> OutlierData { get => outlierData; }

        /// <summary>
        /// Clears the data in structure
        /// </summary>
        public void Clear()
        {
            this.cleanData.Clear();
            this.outlierData.Clear();
        }

        /// <summary>
        /// Process the data into our data structures.
        /// Will detect outlier and store in a clean structure as well as an outlier structure
        /// Made virtual in case outlier algorithm needs to be overridden or changed
        /// Simple detection algortihm that only looks at past 2 data points
        /// </summary>
        /// <param name="date"></param>
        /// <param name="price"></param>
        protected virtual void ProcessData(DateTime date, decimal price)
        {
            // assume 1st/2nd data is clean
            if (cleanData.Count < 2)
            {
                cleanData.Add(date, price);
                prevPrice2 = prevPrice1;
                prevPrice1 = price;
                return;
            }

            var diff1 = Math.Abs(price - prevPrice1);
            var diff2 = Math.Abs(price - prevPrice2);

            // compare last 2 data points and see if current one is off
            if (diff1 > outlierDeviation && diff2 > outlierDeviation)
                outlierData.Add(date, price);            
            else            
                cleanData.Add(date, price);            

            prevPrice2 = prevPrice1;
            prevPrice1 = price;
        }

        /// <summary>
        /// Method retrieves data only. All child class needs to implement their own ReadData method,
        /// whether it is from file, DB, network, or other sources
        /// Extracts only the date, price and calls method ProcessData() internally
        /// </summary>
        /// <returns></returns>
        public abstract int ReadData();
    }
}
