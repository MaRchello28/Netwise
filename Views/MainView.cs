using Netwise.Interfaces;
using Netwise.Model;
using Netwise.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netwise.View
{
    public class MainView
    {
        public void ShowMenu()
        {
            Console.WriteLine("1-Connect 2-Display file 3-Exit");
        }

        public string? GetUserChoice()
        {
            return Console.ReadLine();
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplayCatFact(CatFact fact)
        {
            Console.WriteLine($"Cat Fact: {fact.Fact}");
        }
    }
}
