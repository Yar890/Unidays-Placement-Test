using System;
using System.Collections.Generic;
using System.Text;

namespace tech_placement_challenge
{
    class Item
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
        public Item(string item, double price, Discount discount)
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
        public Item(string item, double price)
        {
            this.item = item;
            this.price = price;
            this.discount = new Discount("No Discount");
        }
    }
}
