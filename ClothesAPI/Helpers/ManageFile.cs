using ClothesAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClothesAPI.Helpers
{
    public class ManageFile
    {
        private static ManageFile _manageFile;
        private static object lockObject = new object();
        private readonly string _fullPath;
        private static IEnumerable<Product> _products = new List<Product>();
        public const string csvColumns = "Id;Type;Category 1;Category 2;Article number;Title;Description;Material;Size;Color;Price";

        private ManageFile() { }

        private ManageFile(string fullPath)
        {
            _fullPath = fullPath;
            _products = GetProductFromPath();
        }

        //Сделал для него арг
        /// <summary>
        /// Устанавливаем значения из файла для модели  продуктов
        /// </summary>
        /// <param name="rootPath"></param>
        public IEnumerable<Product> GetProductFromPath()
        {
            List<Product> products = new List<Product>();
            if (File.Exists(_fullPath))
            {
                string[] csvLines = File.ReadAllLines(_fullPath, Encoding.UTF8);
                for(int i = 1; i < csvLines.Length; i++)
                {
                    var row = csvLines[i].Split(';');
                    if (row.Length < 11)
                        continue;

                    if (!Int32.TryParse(row[0], out int Id))
                        throw new Exception("значение столбца Id имел неверный формат");
                    if (!Int64.TryParse(row[4], out long ArticleNumber))
                        throw new Exception("значение столбца ArticleNumber имел неверный формат");
                    if (!Int64.TryParse(row[10], out long Price))
                        throw new Exception("значение столбца Price имел неверный формат");

                    products.Add(new Product 
                    {
                        Id = Id,
                        Type = row[1],
                        Category_1 = row[2],
                        Category_2 = row[3],
                        ArticleNumber = ArticleNumber,
                        Title = row[5],
                        Description = row[6],
                        Material = row[7],
                        Size = row[8],
                        Color = row[9],
                        Price = Price,
                    });
                }
            }
            else
                CreateEmptyFile();

            return products;
        }

        /// <summary>
        /// Создаем пустой файл, если егшо не существуует
        /// </summary>
        public void CreateEmptyFile()
        {
            lock (lockObject)
            {
                using (var fileStream = new StreamWriter(_fullPath, false, Encoding.UTF8))
                {
                    fileStream.WriteLine(csvColumns);
                }
            }
        }

        /// <summary>
        /// Получаем объект синглетон
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static ManageFile GetInstance(string fullPath)
        {
            if (_manageFile == null)
            {
                lock (lockObject)
                {
                    if(_manageFile == null)
                        _manageFile = new ManageFile(fullPath);
                }
            }

            return _manageFile;
        }

        public IEnumerable<Product> GetProducts() => _products;

        /// <summary>
        /// Очищаем синглтон, если нужно заново считать файл
        /// </summary>
        public void ClearManage() => _manageFile = null;

        public string AddProduct(Product product)
        {
            string error = "";
            lock (lockObject)
            {
                if (_products.Any(c => c.Id == product.Id))
                    error = "Продукт с таким Id уже существует";
                else
                {
                    if (!File.Exists(_fullPath))
                        CreateEmptyFile();

                    using (var sr = new StreamWriter(_fullPath, true, Encoding.UTF8))
                    {
                        sr.WriteLine($"{product.Id};{product.Type};{product.Category_1};{product.Category_2};{product.ArticleNumber};{product.Title};" +
                            $"{product.Description};{product.Material};{product.Size};{product.Color};{product.Price}");
                    }

                    _products = _products.Concat(new List<Product> { product });
                }
            }
            return error;
        }
    }
}
