using sephoraMsystem;

class Program
{
    public static void Main(string[] arg)
    {
        Inventory inventory = new Inventory();
        Sales sales = new Sales();
        List<Customer> customers = new List<Customer>();
        List<Order> orders = new List<Order>();  // Store orders to manage cancellations and modifications

        // Load categories and sample products
        var categories = Category.GetDefaultCategories();
        inventory.LoadSampleProducts(categories);

        while (true)
        {
            Console.WriteLine("Welcome to Sephora Management System");
            Console.WriteLine("=====================================");
            Console.WriteLine("1. Add Customer");
            Console.WriteLine("2. View Inventory");
            Console.WriteLine("3. Create Order");
            Console.WriteLine("4. Modify Order");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Customer.AddCustomer(customers);
                    break;

                case "2":
                    ViewInventory(inventory);
                    break;

                case "3":
                    CreateOrder(sales, customers, inventory, orders);
                    break;

                case "4":
                    Sales.ModifyOrder(sales, inventory);
                    break;

                case "5":
                    Console.WriteLine("Exiting... Thank you for using Sephora Management System!");
                    return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
    }

    static void ViewInventory(Inventory inventory)
    {
        Console.WriteLine("View Inventory:");
        Console.WriteLine("===============");
        inventory.DisplayInventory();
    }

    static void ModifyOrder(Sales sales )
    {
        Console.WriteLine("Modify Order:");
        Console.WriteLine("-------------");
        Console.Write("Enter Order ID: ");
        int orderId = int.Parse(Console.ReadLine());
        var order = sales.Orders.FirstOrDefault(o => o.OrderId == orderId);

        if (order != null)
        {
            decimal totalPriceDifference = 0m;  // Accumulate price differences here

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

            // Adjust payment after all modifications are complete
            sales.AdjustPayment(order, totalPriceDifference);

            Console.WriteLine("Order modifications completed.");
        }
        else
        {
            Console.WriteLine("Order not found.");
        }
    }

    // Create Order function with proper payment handling
    static void CreateOrder(Sales sales, List<Customer> customers, Inventory inventory, List<Order> orders)
    {
        Console.WriteLine("Create Order");
        Console.WriteLine("============");

        // Get customer
        Console.Write("Enter Customer ID: ");
        int customerId = int.Parse(Console.ReadLine());
        var customer = customers.FirstOrDefault(c => c.Id == customerId);

        if (customer == null)
        {
            Console.WriteLine("Customer not found.");
            return;
        }

        // Get valid state code using TaxCalculator
        string stateCode;
        if (!TaxCalculator.ValidateStateCode(out stateCode))
        {
            Console.WriteLine("State code validation failed.");
            return; // Exit if the state code is invalid
        }

        // Get valid 7-digit Order ID from Order class
        int orderId = Order.GetValidOrderId();
        var order = sales.CreateOrder(orderId, customer, stateCode);

        // Add products to order
        sales.AddProductsToOrder(order, inventory);

        // Display order summary
        order.DisplayOrderSummary();

        // Handle payment
        sales.HandlePayment(order);

    }
}