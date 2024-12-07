using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sephoraMsystem
{
    public class Sales
    {
        public List<Order> Orders { get; set; } = new List<Order>();

        // Create a new order and add it to the order list
        public Order CreateOrder(int orderId, Customer customer, string stateCode)
        {
            var order = new Order(orderId, customer, stateCode);
            Orders.Add(order);
            return order;
        }

        // Add products to the order
        public void AddProductsToOrder(Order order, Inventory inventory)
        {
            inventory.AddProductsToOrder(order);
        }
        // Handle payment
        public void HandlePayment(Order order)
        {
            Console.WriteLine("Select Payment Method:");
            Console.WriteLine("1. Cash");
            Console.WriteLine("2. Credit Card");
            int paymentChoice = int.Parse(Console.ReadLine());

            IPayment paymentMethod = null;

            if (paymentChoice == 1)
            {
                paymentMethod = new CashPayment();

                decimal amountPaid = 0;
                while (amountPaid < order.TotalAmount)
                {
                    Console.WriteLine($"Enter the amount paid by the customer (Total Amount: {order.TotalAmount:C}): ");
                    amountPaid = decimal.Parse(Console.ReadLine());

                    if (amountPaid < order.TotalAmount)
                    {
                        decimal remainingBalance = order.TotalAmount - amountPaid;
                        Console.WriteLine($"Insufficient funds. Please pay the remaining balance of {remainingBalance:C}.");
                    }

                    else if (amountPaid > order.TotalAmount)
                    {
                        decimal change = amountPaid - order.TotalAmount;
                        Console.WriteLine($"Payment successful. The change to be returned is: {change:C}");
                    }
                    else
                    {
                        Console.WriteLine("Payment successful. Exact amount received.");
                    }
                }
            }

            else if (paymentChoice == 2)
            {
                paymentMethod = new CreditCardPayment();
                Console.WriteLine("Payment successful using Credit Card.");
            }
            else
            {
                Console.WriteLine("Invalid payment method selected.");
                return;
            }

            // Assign the chosen payment method to the order
            order.PaymentMethod = paymentMethod;

            // Proceed with the order processing
            order.ProcessOrder();
        }
       
        // Modify an existing order
        public static void ModifyOrder(Sales sales, Inventory inventory)
        {
            Console.WriteLine("Modify Order:");
            Console.WriteLine("-------------");
            Console.Write("Enter Order ID: ");
            int orderId = int.Parse(Console.ReadLine());

            // Find the order by ID
            var order = sales.Orders.FirstOrDefault(o => o.OrderId == orderId);

            if (order != null)
            {
                decimal totalPriceDifference = 0m;

                // Loop for modifying the order
                while (true)
                {
                    Console.Write("Enter Product ID to modify or remove (0 to finish): ");
                    int productId = int.Parse(Console.ReadLine());

                    if (productId == 0)
                    {
                        break; // Exit the loop when user enters 0
                    }

                    Console.Write("Enter new quantity (0 to remove product): ");
                    int newQuantity = int.Parse(Console.ReadLine());

                    // Modify the order by updating the product quantity and get price difference
                    decimal priceDifference = order.ModifyProductQuantity(productId, newQuantity);

                    // Accumulate the price difference for payment adjustment
                    totalPriceDifference += priceDifference;

                    // Display updated order summary after modification
                    order.DisplayOrderSummary();
                }

                // Adjust payment after modification is complete
                sales.AdjustPayment(order, totalPriceDifference);

                Console.WriteLine("Order modifications completed.");
            }
            else
            {
                Console.WriteLine("Order not found.");
            }
        }
        public void AdjustPayment(Order order, decimal subtotalDifference)
        {
            // Calculate the tax difference based on the subtotal change
            decimal taxDifference = TaxCalculator.CalculateTax(subtotalDifference, order.StateCode);

            // Total price difference, including tax
            decimal totalDifference = subtotalDifference + taxDifference;

            if (totalDifference > 0)
            {
                Console.WriteLine($"You need to pay an additional {totalDifference:C} (including tax).");
                ProcessAdditionalPayment(order, totalDifference);
            }
            else if (totalDifference < 0)
            {
                Console.WriteLine($"Refunding {Math.Abs(totalDifference):C} (including tax).");
                ProcessRefund(order, Math.Abs(totalDifference));
            }
            else
            {
                Console.WriteLine("No payment adjustment needed.");
            }
        }

        public void ProcessAdditionalPayment(Order order, decimal amount)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Please process an additional payment of {amount:C}.");
            Console.WriteLine("------------------------------------------");
            order.PaymentMethod.ProcessPayment(amount); 
        }

        public void ProcessRefund(Order order, decimal amount)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Refund {amount:C} to the customer.");
            Console.WriteLine("------------------------------------------");
            order.TotalAmount -= amount; 
            order.CalculateTotalWithTax(); 
            order.PaymentMethod.ProcessPayment(-amount); 
        }
    }
}
