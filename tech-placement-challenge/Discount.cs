using System;
using System.Collections.Generic;
using System.Text;

namespace tech_placement_challenge
{
    class Discount
    {
        public string name;

        /// <summary>
        /// Sets up discount to contain discount name
        /// </summary>
        /// <param name="discountName">The name of the discount deal</param>
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
            // Initialise variables
            double discountedTotal;

            // Get the remainder that won't be part of discount (EG: 3 items, Buy 2 for £10 deal, the third item won't get discount)
            int remainder = itemQuantity % purchaseQuantity;
            discountedTotal = normalPrice * remainder;
            itemQuantity = itemQuantity - remainder;

            // Calculates sets of items eligable for discount by working out how many sets of items that can get discount 
            // (EG: 4 items, Buy 2 for £10, there are 4 / 2 = 2 sets of items that can get discount). Then 
            // times set of items by discounted price, which should get you the discounted total for items
            // eligable for discount
            int setsOfItems = itemQuantity / purchaseQuantity;

            // Gets discounted total by getting set of items time by discounted price
            // EG: 4 Items (cost £2), Buy 2 for £3, they are 2 * 3 = 6 pounds 
            discountedTotal = discountedTotal + (setsOfItems * atPrice);

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
            // Initialise variables
            double discountedTotal;

            // Get the remainder that won't be part of discount (EG: 5 items, Buy 2 get 2 free, the fifth item won't get discount)
            int remainder = (itemQuantity) % (purchaseQuantity + getFreeQuantity);
            discountedTotal = normalPrice * remainder;
            itemQuantity = itemQuantity - remainder;

            // Calculates sets of items eligable for discount by working out how many sets of items that can get discount 
            // (EG:8 items, Buy 2 get 2 free, there are 4 / (2 + 2) = 2 sets of items that can get discount).
            double setsOfItems = (itemQuantity / (purchaseQuantity + getFreeQuantity));

            // Gets discounted total by getting purchase quanity times by normal price, and then times by set of Items
            // EG: 8 Items (cost £1), Buy 2 get 2 free, they are (2 * 1) * 2 = 4 pounds
            discountedTotal = discountedTotal + (purchaseQuantity * normalPrice) * setsOfItems;

            return discountedTotal;
        }
    }
}
