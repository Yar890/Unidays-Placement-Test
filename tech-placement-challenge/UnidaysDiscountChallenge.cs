using System;
using System.Collections.Generic;
using System.Text;

namespace tech_placement_challenge
{
    class UnidaysDiscountChallenge
    {
        private readonly Dictionary<string, Item> pricingRules;
        private Dictionary<string, int> basket;

        /// <summary>
        /// Sets up UnidaysDiscountChallange by declaring the pricing rule and setting up a new basket
        /// </summary>
        /// <param name="PricingRules">Contains the pricing of all the items, including discount</param>
        public UnidaysDiscountChallenge(Dictionary<string, Item> PricingRules)
        {
            this.pricingRules = PricingRules;
            this.basket = new Dictionary<string, int>();
        }

        /// <summary>
        /// Adds a new item to the basket
        /// </summary>
        /// <param name="item">The item you want to place into the basket</param>
        public void AddToBasket(string itemName)
        {
            // If item is not already in basket, then add item to basket
            if (basket.ContainsKey(itemName) == false)
            {
                basket.Add(itemName, 1);
            }
            // If item is already in basket, increase value by 1
            else
            {
                basket[itemName] = basket[itemName] + 1;
            }
        }

        /// <summary>
        /// Gets and returns basket dictionary
        /// </summary>
        /// <returns>Returns current basket</returns>
        public Dictionary<string, int> GetBasket()
        {
            return basket;
        }

        /// <summary>
        /// Calculates the total price of all items in basket, including applying discounts
        /// and delivery charge
        /// </summary>
        /// <returns>Returns a dictionary with total and delivery charge</returns>
        public Dictionary<string, double> CalculateTotalPrice()
        {
            // Initialise variables
            Dictionary<string, double> totalCost = new Dictionary<string, double>();
            double total = 0;
            double deliveryCharge = 0;

            // Go through each item in basket
            foreach (KeyValuePair<string, int> item in basket)
            {
                // Gets item name, item quantity and item price
                string itemName = item.Key;
                int itemQuantity = item.Value;
                double itemPrice = pricingRules[itemName].price;

                // If there is a discount, apply discount. Otherwise, add to total as normal price
                if (pricingRules[itemName].discount != null)
                {
                    // Gets discount and applied discoutn and then gets the total.
                    Discount discount = pricingRules[itemName].discount;
                    total = total + discount.ApplyDiscount(itemQuantity, itemPrice);
                }
                // If there is no discount, then add quantity of items * price of item and add it to total
                else
                {
                    total = total + (itemQuantity * pricingRules[itemName].price);
                }
            }

            // Sets delivery charge if total is above 0 and under 50
            if (total > 0 && total < 50)
            {
                deliveryCharge = 7;
            }

            // Adds total and delivery charge to total cost dictionary
            totalCost.Add("Total", total);
            totalCost.Add("DeliveryCharge", deliveryCharge);

            return totalCost;
        }
    }
}
