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
            // Creates a dictionary containing the item name (key) and the pricing rule (value)
            Dictionary<string, PricingRule> pricingRules = new Dictionary<string, PricingRule>();

            // Add the pricing rule of each item to PricingRules dictionary
            pricingRules.Add("A", new PricingRule("A", 8));
            pricingRules.Add("B", new PricingRule("B", 12, new QuantityForSetPrice("Buy 2 for £20", 2, 20)));
            pricingRules.Add("C", new PricingRule("C", 4, new QuantityForSetPrice("Buy 3 for £10", 3, 10)));
            pricingRules.Add("D", new PricingRule("D", 7, new BuyQuantityGetQuantityFree("Buy 1 get 1 free", 1, 1)));
            // Note: Get 3 for the price of 2 is the same as buy 3 get 1 free
            pricingRules.Add("E", new PricingRule("E", 5, new BuyQuantityGetQuantityFree("Get 3 for the price of 2", 3, 1)));

            // Create a new basket with the pricingRules dictionary
            UnidaysDiscountChallenge mainBasket = new UnidaysDiscountChallenge(pricingRules);

            Console.WriteLine("Welcome to the Unidays");
            Console.WriteLine("Below you will find a list of items that are avaliable to purchase: ");
            foreach (KeyValuePair<string, PricingRule> item in pricingRules)
            {
                Console.WriteLine(string.Concat(item.Value.item, ": £", item.Value.price.ToString(), " (" + item.Value.discount.name + ")"));
            }
            Console.WriteLine();

            string userInput = null;

            while (userInput != "/")
            {
                Console.Write("Please enter the item you want to add to the basket, or type / to get total price: ");
                userInput = Console.ReadLine().ToUpper();

                if (pricingRules.ContainsKey(userInput) == true)
                {
                    mainBasket.AddToBasket(userInput);
                    Console.WriteLine(string.Concat("Item " + userInput + " has been added to the basket."));
                    Console.WriteLine();
                    Console.WriteLine("Current Basket: ");
                    foreach (KeyValuePair<string, int> item in mainBasket.basket)
                    {
                        Console.WriteLine(string.Concat(item.Key, ": ", item.Value));
                    }
                    Console.WriteLine();
                }
                else if (userInput == "/")
                {
                    // Do nothing
                }
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

    class UnidaysDiscountChallenge
    {
        public Dictionary<string, PricingRule> pricingRules;
        public Dictionary<string, int> basket;

        /// <summary>
        /// Sets up UnidaysDiscountChallange by declaring the pricing rule and setting up a new basket
        /// </summary>
        /// <param name="PricingRules">Contains the pricing of all the items, including discount</param>
        public UnidaysDiscountChallenge(Dictionary<string, PricingRule> PricingRules)
        {
            this.pricingRules = PricingRules;
            this.basket = new Dictionary<string, int>();
        }

        /// <summary>
        /// Adds a new item to the basket
        /// </summary>
        /// <param name="item">The item you want to place into the basket</param>
        public void AddToBasket(string item)
        {
            if (basket.ContainsKey(item) == false)
            {
                basket.Add(item, 1);
            }
            else
            {
                basket[item] = basket[item] + 1;
            }
        }

        /// <summary>
        /// Calculates the total price of all items in basket, including applying discounts
        /// and delivery charge
        /// </summary>
        /// <returns>Returns a dictionary with total and delivery charge</returns>
        public Dictionary<string, double> CalculateTotalPrice()
        {
            Dictionary<string, double> totalCost = new Dictionary<string, double>();
            double total = 0;
            double deliveryCharge = 0;

            foreach (KeyValuePair<string, int> item in basket)
            {
                if (pricingRules[item.Key].discount != null)
                {
                    string derivedClassName = pricingRules[item.Key].discount.GetType().UnderlyingSystemType.Name;

                    switch (derivedClassName)
                    {
                        case "QuantityForSetPrice":
                            QuantityForSetPrice quanityForSetPriceDiscount = pricingRules[item.Key].discount as QuantityForSetPrice;
                            total = total + quanityForSetPriceDiscount.applyDiscount(item.Value, pricingRules[item.Key].price);
                            break;
                        case "BuyQuantityGetQuantityFree":
                            BuyQuantityGetQuantityFree buyQuantityGetQuantityFreeDiscount = pricingRules[item.Key].discount as BuyQuantityGetQuantityFree;
                            total = total + buyQuantityGetQuantityFreeDiscount.applyDiscount(item.Value, pricingRules[item.Key].price);
                            break;
                        default:
                            total = total + (item.Value * pricingRules[item.Key].price);
                            break;
                    }
                }
                else
                {
                    total = total + (item.Value * pricingRules[item.Key].price);
                }
                
            }

            if (total > 0 && total < 50)
            {
                deliveryCharge = 7;
            }

            totalCost.Add("Total", total);
            totalCost.Add("DeliveryCharge", deliveryCharge);

            return totalCost;
        }
    }

    class Discount
    {
        public string name;

        public Discount(string discountName)
        {
            this.name = discountName;
        }
    }

    class QuantityForSetPrice : Discount
    {
        public int purchaseQuantity;
        public int atPrice;

        /// <summary>
        /// Sets up QuantityForSetPrice to contain the purchase quantity for the price to change to a set price.
        /// For example: Buy 2 for £10
        /// </summary>
        /// <param name="quantity">The purchase quantity required for discount to apply</param>
        /// <param name="price">The set price that the item will change to if purchase quantity has been met</param>
        public QuantityForSetPrice(string discountName, int quantity, int price) : base(discountName)
        {
            this.purchaseQuantity = quantity;
            this.atPrice = price;
        }

        /// <summary>
        /// Applies the discount to the items based on item quantity
        /// </summary>
        /// <param name="itemQuantity">The quantity of a specific item that is in the basket</param>
        /// <param name="normalPrice">The normal price of the item</param>
        /// <returns>The overall price of the items after discount has been applied</returns>
        public double applyDiscount(int itemQuantity, double normalPrice)
        {
            double discountedTotal;

            int remainder = itemQuantity % purchaseQuantity;
            discountedTotal = normalPrice * remainder;
            itemQuantity = itemQuantity - remainder;
            discountedTotal = discountedTotal + ((itemQuantity / purchaseQuantity) * atPrice);

            return discountedTotal;
        }
    }

    class BuyQuantityGetQuantityFree : Discount
    {
        int purchaseQuantity;
        int getFreeQuantity;

        /// <summary>
        /// Sets up BuyQuantityGetQuantityFree to contains the purchase quantity required for the user to recieve
        /// a free quantity
        /// For example: Buy 2 Get 1 Free.
        /// </summary>
        /// <param name="quantity">The purchase quantity required for discount to apply</param>
        /// <param name="freeQuantity">The quantity of items that will become free if purchase quantity has been met</param>
        public BuyQuantityGetQuantityFree(string discountName, int quantity, int freeQuantity) : base(discountName)
        {
            this.purchaseQuantity = quantity;
            this.getFreeQuantity = freeQuantity;
        }

        /// <summary>
        /// Applies the discount to the items based on item quantity
        /// </summary>
        /// <param name="itemQuantity">The quantity of a specific item that is in the basket</param>
        /// <param name="normalPrice">The normal price of the item</param>
        /// <returns>The overall price of the items after discount has been applied</returns>
        public double applyDiscount(int itemQuantity, double normalPrice)
        {
            double discountedTotal;

            int remainder = (itemQuantity) % (purchaseQuantity + getFreeQuantity);
            discountedTotal = normalPrice * remainder;
            itemQuantity = itemQuantity - remainder;
            double numberOfSetsOfQuanity = (itemQuantity / (purchaseQuantity + getFreeQuantity));
            discountedTotal = discountedTotal + (purchaseQuantity * normalPrice) * numberOfSetsOfQuanity;

            return discountedTotal;
        }
    }

    class PricingRule
    {
        public string item;
        public double price;
        public Discount discount;

        /// <summary>
        /// Sets up the PricingRule of a specific item by declaring the item name, the price of item and 
        /// the type of discount
        /// </summary>
        /// <param name="item">The item name</param>
        /// <param name="price">The normal price of the item</param>
        /// <param name="discount">The type of discount</param>
        public PricingRule(string item, double price, Discount discount)
        {
            this.item = item;
            this.price = price;
            this.discount = discount;
        }

        /// <summary>
        /// Sets up UnidaysDiscountChallange by declaring the item, the price of item and setting discount to null
        /// (no discount)
        /// </summary>
        /// <param name="item">The item name</param>
        /// <param name="price">The normal price of the item</param>
        public PricingRule(string item, double price)
        {
            this.item = item;
            this.price = price;
            this.discount = new Discount("No Discount");
        }
    }
}
