using ClothesAPI.Helpers;
using ClothesAPI.Models;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace ProductsTest
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Проверяем получилось ли создать пустой файл с 11 столбцами
        /// </summary>
        [Test]
        public void CheckEmptyFileProducts()
        {
            var path = GetPathDirectory();
            var manageFile = ManageFile.GetInstance(path);
            var stringColumns = File.ReadAllText(path).Trim();
            File.Delete(path);
            Assert.AreEqual(ManageFile.csvColumns, stringColumns);
        }

        /// <summary>
        /// Проверяем Правильно ли работает группировка для отчетов
        /// </summary>
        [Test]
        public void CheckGetReportData()
        {
            Product product1 = new Product
            {
                Id = 1,
                Type = "Часы",
                Category_1 = "Российские",
                Category_2 = "Мужские",
                ArticleNumber = 1000,
                Title = "Часы Romanoff Gold-10",
                Description = "Стильные часы с драгоценными камнями",
                Material = "",
                Size = "",
                Color = "",
                Price = 4200
            };

            Product product2 = new Product
            {
                Id = 2,
                Type = "Часы",
                Category_1 = "Российские",
                Category_2 = "Мужские",
                ArticleNumber = 1001,
                Title = "Часы Denissov Enigma",
                Description = "Стильные часы с драгоценными камнями",
                Material = "",
                Size = "",
                Color = "",
                Price = 800
            };

            ReportProduct reportExpected = new ReportProduct
            {
                Type = "Часы",
                Category_1 = "Российские",
                Category_2 = "Мужские",
                CountProduct = 2,
                TotalPrice = product1.Price + product2.Price
            };

            var path = GetPathDirectory();
            var manageFile = ManageFile.GetInstance(path);
            manageFile.AddProduct(product1);
            manageFile.AddProduct(product2);

            var reportActual = manageFile.GetReportData().First();
            Assert.AreEqual(reportExpected.Type, reportActual.Type);
            Assert.AreEqual(reportExpected.Category_1, reportActual.Category_1);
            Assert.AreEqual(reportExpected.Category_2, reportActual.Category_2);
            Assert.AreEqual(reportExpected.CountProduct, reportActual.CountProduct);
            Assert.AreEqual(reportExpected.TotalPrice, reportActual.TotalPrice);
        }

        private string GetPathDirectory()
        {
            string dirname = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return Path.Combine(dirname, $"{Guid.NewGuid()}.csv");
        }
    }
}