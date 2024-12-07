using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sephoraMsystem
{
    public class TaxCalculator
    {
        public static Dictionary<string, decimal> stateTaxRates = new Dictionary<string, decimal>
        {
    {"AL", 0.04m}, {"AK", 0.00m}, {"AZ", 0.056m}, {"AR", 0.065m},
    {"CA", 0.0725m}, {"CO", 0.029m}, {"CT", 0.0635m},
    {"DE", 0.00m}, {"DC", 0.06m},
    {"FL", 0.06m},
    {"GA", 0.04m},
    {"HI", 0.04m},
    {"ID", 0.06m}, {"IL", 0.0625m}, {"IN", 0.07m}, {"IA", 0.06m},
    {"KS", 0.065m}, {"KY", 0.06m},
    {"LA", 0.0445m},
    {"ME", 0.055m}, {"MD", 0.06m}, {"MA", 0.0625m}, {"MI", 0.06m}, {"MN", 0.06875m}, {"MS", 0.07m}, {"MO", 0.04225m}, {"MT", 0.00m},
    {"NE", 0.055m}, {"NV", 0.0685m}, {"NH", 0.00m}, {"NJ", 0.06625m}, {"NM", 0.05125m}, {"NY", 0.0875m}, {"NC", 0.0475m}, {"ND", 0.05m},
    {"OH", 0.0575m}, {"OK", 0.045m}, {"OR", 0.00m},
    {"PA", 0.06m},
    {"RI", 0.07m},
    {"SC", 0.06m}, {"SD", 0.045m},
    {"TN", 0.07m}, {"TX", 0.0625m},
    {"UT", 0.047m},
    {"VT", 0.06m}, {"VA", 0.053m},
    {"WA", 0.065m}, {"WV", 0.06m}, {"WI", 0.05m}, {"WY", 0.04m},
};

        public static decimal GetTaxRate(string stateCode)
        {
            if (stateTaxRates.TryGetValue(stateCode.ToUpper(), out decimal rate))
            {
                return rate;
            }
            return 0; // Default to 0% if state not found
        }

        public static decimal CalculateTax(decimal amount, string stateCode)
        {
            decimal taxRate = GetTaxRate(stateCode);
            return amount * taxRate;
        }

        // static method to validate the state code
        public static bool ValidateStateCode(out string validStateCode)
        {
            validStateCode = string.Empty;
            while (true)
            {
                Console.Write("Enter State Code (e.g., NY, CA): ");
                validStateCode = Console.ReadLine();

                // Ensure the state code is in uppercase
                if (validStateCode != validStateCode.ToUpper())
                {
                    Console.WriteLine("State code must be uppercase letters. Please try again.");
                    continue;
                }

                // Validate state code (2 letters, valid tax rate, and exists in the state tax dictionary)
                if (validStateCode.Length == 2 && validStateCode.All(char.IsLetter) && GetTaxRate(validStateCode) > 0)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Invalid state code or tax rate not found.");
                }
            }
        }
    }
}
