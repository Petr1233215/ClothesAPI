namespace ClothesAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Category_1 { get; set; }

        public string Category_2 { get; set; }

        public long ArticleNumber { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Material { get; set; }

        public string Size { get; set; }

        public string Color { get; set; }

        public long Price { get; set; }
    }
}
