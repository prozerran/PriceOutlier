using NUnit.Framework;
using System;

using PriceOutlierLib;
using System.IO;

namespace PriceOutlierUnitTest
{
    public class OutlierTest
    {
        private OutlierFile obj = new OutlierFile();

        [OneTimeSetUp]
        public void OneTimeSetUp()
        { }

        [SetUp]
        public void SetUp()
        { }

        [TearDown]
        public void TearDown()
        { }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        { }

        [Test, Order(1)]
        public void TestReadData()
        {
            // Arrange
            var filePath = Directory.GetCurrentDirectory() + @"\..\..\..\..\Outliers.csv";
            obj.SetFilePath(filePath);

            // Act
            var cnt = obj.ReadData();

            // Assert
            Assert.Greater(cnt, 0);
        }

        [Test, Order(2)]
        public void TestGetCleanData()
        {
            // Arrange
            var cds = obj.CleanData;

            // Act
            foreach (var cd in cds)
            {
                Console.WriteLine($"{cd.Key} : {cd.Value}");
            }
        }
    }
}