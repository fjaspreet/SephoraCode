using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sephoraMsystem
{
    public class Order
    {

        public int OrderId { get; set; }
        public Customer Customer { get; set; }
        public List<(Product Product, int Quantity)> Products { get; set; } = new List<(Product, int)>();
        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public IPayment PaymentMethod { get; set; }
        public string StateCode { get; set; }

        public Order(int orderId, Customer customer, string stateCode)
        {
            OrderId = orderId;
            Customer = customer;
            StateCode = stateCode;
            PaymentMethod = null;
        }
        // Static method to get a valid Order ID
        public static int GetValidOrderId()
        {
            int orderId;
            while (true)
            {
                Console.Write("Enter Order ID (7 digits): ");
                string orderIdInput = Console.ReadLine();

                // Check if OrderId is 7 digits long and a valid integer
                if (orderIdInput.Length == 7 && int.TryParse(orderIdInput, out orderId))
                {
                    return orderId; // Return the valid Order ID
                }
                else
                {
                    Console.WriteLine("Invalid Order ID. It must be exactly 7 digits.");
                }
            }
        }
        public void AddProduct(Product product, int quantity)
        {
            Products.Add((product, quantity));
            Subtotal += product.Price * quantity;
            CalculateTotalWithTax();
        }

        // Method to cancel or modify the order (remove or change product quantities)
        public decimal ModifyProductQuantity(int productId, int newQuantity)
        {
            var productTuple = Products.FirstOrDefault(p => p.Product.ProductId == productId);

            if (productTuple.Product != null)
            {
                int oldQuantity = productTuple.Quantity;
                Products.Remove(productTuple);

                decimal priceDifference = 0m;

                if (newQuantity == 0)
                {
                    Console.WriteLine($"{productTuple.Product.Name} removed from the order.");
                    priceDifference = productTuple.Product.Price * oldQuantity;
                }
                else
                {
                    Products.Add((productTuple.Product, newQuantity));
                    Console.WriteLine($"{productTuple.Product.Name} quantity updated to {newQuantity}. Old quantity was {oldQuantity}.");
                    priceDifference = (newQuantity - oldQuantity) * productTuple.Product.Price;
                }

                RecalculateTotal();
                return priceDifference;
            }
            else
            {
                Console.WriteLine("Product not found in the order.");
                return 0m;
            }
        }
        public void RecalculateTotal()
        {
            Subtotal = Products.Sum(p => p.Product.Price * p.Quantity);
            CalculateTotalWithTax();
        }

        public void CalculateTotalWithTax()
        {
            TaxAmount = TaxCalculator.CalculateTax(Subtotal, StateCode);
            TotalAmount = Subtotal + TaxAmount;
        }

        public void DisplayOrderSummary()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Order ID: {OrderId}, Customer: {Customer.Name}");
            Console.WriteLine("------------------------------------------");
            foreach (var (product, quantity) in Products)
            {
                Console.WriteLine($"Product ID: {product.ProductId} - {product.Name} - {product.Brand} - {product.Price:C} x {quantity}");
            }
            Console.WriteLine("-----------------------");
            Console.WriteLine($"Subtotal: {Subtotal:C}");
            decimal taxRate = TaxCalculator.GetTaxRate(StateCode) * 100;
            Console.WriteLine($"Tax ({StateCode.ToUpper()} {taxRate:F2}%): {TaxAmount:C}");
            Console.WriteLine($"Total: {TotalAmount:C}");
        }

        public void ProcessOrder()
        {
            if (PaymentMethod == null)
            {
                Console.WriteLine("Payment method has not been set.");
                return;
            }

            Console.WriteLine("\nProcessing Order...");
            Console.WriteLine("===================");
            DisplayOrderSummary();
            PaymentMethod.ProcessPayment(TotalAmount);

      
        }
    }
}