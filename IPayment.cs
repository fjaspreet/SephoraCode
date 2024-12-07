using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sephoraMsystem
{
    public interface IPayment
    {
        void ProcessPayment(decimal amount);
    }

    public class CashPayment : IPayment
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Payment of {amount:C} processed via Cash.");
        }
    }

    public class CreditCardPayment : IPayment
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Payment of {amount:C} processed via Credit Card.");
        }
    }
}
