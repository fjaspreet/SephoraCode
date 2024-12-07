using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sephoraMsystem
{
    // Base class
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public Category ProductCategory { get; set; }
        public int StockQuantity { get; set; }

        public Product(int productId, string name, string brand, decimal price, Category productCategory, int stockQuantity)
        {
            ProductId = productId;
            Name = name;
            Brand = brand;
            Price = price;
            ProductCategory = productCategory;
            StockQuantity = stockQuantity;
        }

        public virtual void DisplayProductInfo()
        {
            Console.WriteLine($"ID: {ProductId}, Name: {Name}, Brand: {Brand}, Price: {Price:C}, Category: {ProductCategory}, Stock: {StockQuantity}");
        }
    }

    // Dervied class for Makeup Products
    public class Makeup : Product
    {
        public string Color { get; set; }

        public Makeup(int productId, string name, string brand, decimal price, Category category, int stockQuantity, string color)
            : base(productId, name, brand, price, category, stockQuantity)
        {
            Color = color;
        }

        public override void DisplayProductInfo()
        {
            base.DisplayProductInfo();
            Console.WriteLine($"Color: {Color}");
        }
    }

    // Derived class for Skincare Products 
    public class Skincare : Product
    {
        public string SkinType { get; set; }

        public Skincare(int productId, string name, string brand, decimal price, Category category, int stockQuantity, string skinType)
            : base(productId, name, brand, price, category, stockQuantity)
        {
            SkinType = skinType;
        }

        public override void DisplayProductInfo()
        {
            base.DisplayProductInfo();
            Console.WriteLine($"Skin Type: {SkinType}");
        }
    }

    // Derived class for Fragrance Products
    public class Fragrance : Product
    {
        public string FragranceType { get; set; } // e.g., Floral, Woody, Oriental

        public Fragrance(int productId, string name, string brand, decimal price, Category category, int stockQuantity, string fragranceType)
            : base(productId, name, brand, price, category, stockQuantity)
        {
            FragranceType = fragranceType;
        }

        public override void DisplayProductInfo()
        {
            base.DisplayProductInfo();
            Console.WriteLine($"Fragrance Type: {FragranceType}");
        }
    }

    // Derived class for Haircare Products
    public class HairCare : Product
    {
        public string HairType { get; set; } // e.g., Dry, Oily, Curly, Straight

        public HairCare(int productId, string name, string brand, decimal price, Category category, int stockQuantity, string hairType)
            : base(productId, name, brand, price, category, stockQuantity)
        {
            HairType = hairType;
        }

        public override void DisplayProductInfo()
        {
            base.DisplayProductInfo();
            Console.WriteLine($"Hair Type: {HairType}");
        }
    }

    // Derived class for Nailcare Products
    public class NailCare : Product
    {
        public string PolishType { get; set; } // e.g., Gel, Matte, Glossy

        public NailCare(int productId, string name, string brand, decimal price, Category category, int stockQuantity, string polishType)
            : base(productId, name, brand, price, category, stockQuantity)
        {
            PolishType = polishType;
        }

        public override void DisplayProductInfo()
        {
            base.DisplayProductInfo();
            Console.WriteLine($"Polish Type: {PolishType}");
        }
    }

}

