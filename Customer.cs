using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sephoraMsystem
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Customer(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        // Static method to add a new customer
        public static void AddCustomer(List<Customer> customers)
        {
            Console.WriteLine("Add New Customer");

            // Validate Customer ID to ensure it's exactly 5 digits
            int id;
            while (true)
            {
                Console.Write("Enter Customer ID (5 digits): ");
                string input = Console.ReadLine();

                if (input.Length == 5 && int.TryParse(input, out id))
                {
                    break; // Exit the loop if it's a valid 5-digit integer
                }
                else
                {
                    Console.WriteLine("Invalid Customer ID. Please enter exactly 5 digits.");
                }
            }

            // Validate Customer Name to ensure it contains only alphabets
            string name;
            while (true)
            {
                Console.Write("Enter Customer Name: ");
                name = Console.ReadLine();
                if (IsValidName(name))
                {
                    break; // Exit the loop if the name is valid
                }
                else
                {
                    Console.WriteLine("Invalid Name. Please enter a name with only alphabets.");
                }
            }

            // Validate Email
            string email;
            while (true)
            {
                Console.Write("Enter Customer Email: ");
                email = Console.ReadLine();

                if (IsValidEmail(email))
                {
                    break; // Exit the loop if the email is valid
                }

                Console.WriteLine("Invalid email address. Please try again.");
            }

            var customer = new Customer(id, name, email);
            customers.Add(customer);

            Console.WriteLine("Customer added successfully.");
        }

        // Name validation method
        private static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            // Check if the name contains only alphabets
            return Regex.IsMatch(name, @"^[a-zA-Z\s]+$");
        }

        // Email validation method
        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            // Basic format validation using Regex
            string emailPattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, emailPattern, RegexOptions.IgnoreCase))
            {
                return false;
            }

            // Validate using the MailAddress class for stricter checks
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}

