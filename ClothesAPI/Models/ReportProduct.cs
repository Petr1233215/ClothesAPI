using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesAPI.Models
{
    public class ReportProduct
    {
        public string Type { get; set; }

        public string Category_1 { get; set; }

        public string Category_2 { get; set; }

        /// <summary>
        /// Общее кол-во товаров
        /// </summary>
        public int CountProduct { get; set; }

        /// <summary>
        /// общая стоимость
        /// </summary>
        public long TotalPrice { get; set; }
    }
}
