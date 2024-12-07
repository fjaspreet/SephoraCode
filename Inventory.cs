using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sephoraMsystem
{
    public class Inventory
    {
        public List<Product> Products { get; set; } = new List<Product>();

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public Product GetProductById(int productId)
        {
            return Products.FirstOrDefault(p => p.ProductId == productId);
        }

        public void DisplayInventory()
        {
            foreach (var product in Products)
            {
                product.DisplayProductInfo();
            }
        }
        public void LoadSampleProducts(List<Category> categories)
        {
            AddProduct(new Makeup(1, "Lipstick", "Dior", 48.5m, categories[1], 100, "Red"));
            AddProduct(new Skincare(2, "Moisturizer", "Kiehl's", 67.0m, categories[0], 50, "Dry"));
            AddProduct(new Makeup(3, "Mascara", "Huda Beauty", 30.75m, categories[1], 200, "Black"));
            AddProduct(new Fragrance(4, "Eau de Parfum", "Prada", 135.0m, categories[2], 30, "Floral"));
            AddProduct(new HairCare(5, "Shampoo", "Loreal Paris", 40.0m, categories[3], 60, "Dry"));
            AddProduct(new NailCare(6, "Nail Polish", "Gucci", 35.0m, categories[4], 150, "Glossy"));
        }

        // Update the inventory stock when a product is returned or removed from the order
        public void UpdateStockAfterReturn(Product product, int quantity)
        {
            var inventoryProduct = GetProductById(product.ProductId);
            if (inventoryProduct != null)
            {
                inventoryProduct.StockQuantity += quantity;
                Console.WriteLine($"Stock updated: {quantity} {product.Name} returned to inventory.");
            }
            else
            {
                Console.WriteLine("Product not found in inventory.");
            }
        }
        public void AddProductsToOrder(Order order)
        {
            Console.WriteLine("Select products to add to the order (0 to finish):");

            while (true)
            {
                Console.Write("Enter Product ID: ");
                int productId = int.Parse(Console.ReadLine());

                if (productId == 0)
                    break;

                var product = GetProductById(productId);
                if (product != null && product.StockQuantity > 0)
                {
                    Console.Write("Enter Quantity: ");
                    int quantity = int.Parse(Console.ReadLine());

                    if (quantity > 0 && quantity <= product.StockQuantity)
                    {
                        order.AddProduct(product, quantity);
                        product.StockQuantity -= quantity;  // Decrease stock by the quantity added
                        Console.WriteLine($"{quantity} {product.Name} added to the order.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity or insufficient stock.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid product or out of stock.");
                }
                
            }
        }
    }
}


