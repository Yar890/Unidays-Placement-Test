using System;
using System.Collections.Generic;

namespace tech_placement_challenge
{
    class Program
    {
        /// <summary>
        /// This is the first method that is ran in the program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string userInput = null;

            // Creates a dictionary containing the item name (key) and the pricing rule (value)
            Dictionary<string, Item> pricingRules = new Dictionary<string, Item>();

            // Add the pricing rule of each item to PricingRules dictionary
            pricingRules.Add("A", new Item("A", 8));
            pricingRules.Add("B", new Item("B", 12, new QuantityForSetPrice("Buy 2 for £20", 2, 20)));
            pricingRules.Add("C", new Item("C", 4, new QuantityForSetPrice("Buy 3 for £10", 3, 10)));
            pricingRules.Add("D", new Item("D", 7, new BuyQuantityGetQuantityFree("Buy 1 get 1 free", 1, 1)));
            // Note: Get 3 for the price of 2 is the same as buy 3 get 1 free
            pricingRules.Add("E", new Item("E", 5, new BuyQuantityGetQuantityFree("Get 3 for the price of 2", 3, 1)));

            // Create a new basket with the pricingRules dictionary
            Basket mainBasket = new Basket(pricingRules);

            // Outputs the list of avaliable items to purchase
            Console.WriteLine("Welcome to the Unidays");
            Console.WriteLine("Below you will find a list of items that are avaliable to purchase: ");
            foreach (KeyValuePair<string, Item> item in pricingRules)
            {
                Console.WriteLine(string.Concat(item.Value.item, ": £", item.Value.price.ToString(), " (" + item.Value.discount.name + ")"));
            }
            Console.WriteLine();

            // A while loop that runs until user wants to get total price
            while (userInput != "/")
            {
                // Get user input and convert user input to upper caps
                Console.Write("Please enter the item you want to add to the basket, or type / to get total price: ");
                userInput = Console.ReadLine().ToUpper();

                // If user input is a valid item, then added item to basket
                if (pricingRules.ContainsKey(userInput) == true)
                {
                    mainBasket.AddToBasket(userInput);
                    Console.WriteLine(string.Concat("Item " + userInput + " has been added to the basket."));
                    Console.WriteLine();
                    
                    // Outputs current basket 
                    Console.WriteLine("Current Basket: ");
                    foreach (KeyValuePair<string, int> item in mainBasket.GetBasket())
                    {
                        Console.WriteLine(string.Concat(item.Key, ": ", item.Value));
                    }
                    Console.WriteLine();
                }
                // If user input is /, exit while loop
                else if (userInput == "/")
                {
                    break;
                }
                // Invalid input detected, let user know that they entered an invalid input
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
            Console.WriteLine();

            // Gets the total price of all items in basket, including delivery charge
            Dictionary<string, double> results = mainBasket.CalculateTotalPrice();

            // Outputs the total price and delivery charge
            Console.WriteLine(string.Concat("Total Price: £", results["Total"]));
            Console.WriteLine(string.Concat("Delivery Charge: £", results["DeliveryCharge"]));
            Console.WriteLine(string.Concat("Overall Total: £", (results["Total"] + results["DeliveryCharge"])));

            Console.ReadLine();
        }
    }



}
