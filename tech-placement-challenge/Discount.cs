using System;
using System.Collections.Generic;
using System.Text;

namespace tech_placement_challenge
{
    class Discount
    {
        public string promotionalMessage;

        /// <summary>
        /// Sets up discount to contain discount name
        /// </summary>
        /// <param name="discountName">The name of the discount deal</param>
        public Discount(string discountName)
        {
            this.promotionalMessage = discountName;
        }

        /// <summary>
        /// Applies the discount
        /// </summary>
        /// <param name="itemQuantity">The item quantity in basket</param>
        /// <param name="normalPrice">The normal price of the item</param>
        /// <returns>Discounted total price of items</returns>
        public virtual double ApplyDiscount(int itemQuantity, double normalPrice)
        {
            return itemQuantity * normalPrice;
        }

        /// <summary>
        /// Gets the set of items based on item quantity
        /// </summary>
        /// <param name="itemsPerSet">Number of items in each set</param>
        /// <param name="ItemQuantity">Number of overall items</param>
        /// <returns>Set of items</returns>
        internal int GetSetsOfItems(int itemsPerSet, int ItemQuantity)
        {
            return ItemQuantity / itemsPerSet;
        }

        /// <summary>
        /// Gets the quantity of items that are not on offer
        /// </summary>
        /// <param name="itemQuantity">Number of overall items</param>
        /// <param name="offerQuantity">Number of items that is on offer</param>
        /// <returns>Items that are on offer</returns>
        internal int GetItemQuantityOnOffer(int itemQuantity, int offerQuantity)
        {
            int itemsNotOnOffer = itemQuantity % offerQuantity;
            return itemQuantity - itemsNotOnOffer;
        }

        /// <summary>
        /// Calculates the price of items at a set price
        /// </summary>
        /// <param name="price">The cost of an item</param>
        /// <param name="itemQuantity">Number of overall items</param>
        /// <returns>Price of items based on quantity</returns>
        internal double GetPriceAtQuantity(double price, int itemQuantity)
        {
            return itemQuantity * price;
        }
    }



    class QuantityForSetPrice : Discount
    {
        public int offerQuantity;
        public int discountPrice;

        /// <summary>
        /// Sets up QuantityForSetPrice discount
        /// </summary>
        /// <param name="discountName">The name of the discount deal</param>
        /// <param name="quantity">The offer quantity</param>
        /// <param name="price">The price for the offer quantity</param>
        public QuantityForSetPrice(string discountName, int quantity, int price) : base(discountName)
        {
            this.offerQuantity = quantity;
            this.discountPrice = price;
        }

        /// <summary>
        /// Applies the discount to the items based on item quantity
        /// </summary>
        /// <param name="itemQuantity">The quantity of a specific item that is in the basket</param>
        /// <param name="normalPrice">The normal price of the item</param>
        /// <returns>The overall price of the items after discount has been applied</returns>
        override public double ApplyDiscount(int itemQuantity, double normalPrice)
        {
            // Initialise variables
            double discountedTotal;
            int itemQuantityOnOffer;
            int itemQuantityNotOnOffer;

            // Gets items that are on offer and items that are not on offer
            // (EG: 3 items, Buy 2 for £10 deal, the third item is not on offer)
            itemQuantityOnOffer = GetItemQuantityOnOffer(itemQuantity, offerQuantity);
            itemQuantityNotOnOffer = itemQuantity - itemQuantityOnOffer;

            // Calculates the number of sets of items based on item quantity that is on offer
            // (EG: 4 items, Buy 2 for £10, there are 4 / 2 = 2 sets of items that are on offer)
            int setsOfItems = GetSetsOfItems(offerQuantity, itemQuantityOnOffer);

            // Calculates price of items on offer
            discountedTotal = GetPriceAtQuantity(discountPrice, setsOfItems);
            // Calculates price of items not on offer and adds it to discountedTotal
            discountedTotal = discountedTotal + GetPriceAtQuantity(normalPrice, itemQuantityNotOnOffer);

            return discountedTotal;
        }
    }



    class BuyQuantityForPriceOfQuantity : Discount
    {
        int offerQuantity;
        int priceOfQuantity;

        /// <summary>
        /// Sets up BuyQuantityForPriceOfQuantity discount
        /// </summary>
        /// <param name="discountName">The name of the discount deal</param>
        /// <param name="offerQuantity">The offer quantity</param>
        /// <param name="priceOfQuantity">For the price of quantity</param>
        public BuyQuantityForPriceOfQuantity(string discountName, int offerQuantity, int priceOfQuantity) : base(discountName)
        {
            this.offerQuantity = offerQuantity;
            this.priceOfQuantity = priceOfQuantity;
        }

        /// <summary>
        /// Applies the discount to the items based on item quantity
        /// </summary>
        /// <param name="itemQuantity">The quantity of a specific item that is in the basket</param>
        /// <param name="normalPrice">The normal price of the item</param>
        /// <returns>The overall price of the items after discount has been applied</returns>
        public override double ApplyDiscount(int itemQuantity, double normalPrice)
        {
            // Initialise variables
            double discountedTotal;
            int itemQuantityOnOffer;
            int itemQuantityNotOnOffer;
            double discountPrice;

            // Gets items that are on offer and items that are not on offer
            // (EG: 3 items, Buy 2 for price of 1, the third item is not on offer)
            itemQuantityOnOffer = GetItemQuantityOnOffer(itemQuantity, offerQuantity);
            itemQuantityNotOnOffer = itemQuantity - itemQuantityOnOffer;

            // Calculates the number of sets of items based on item quantity that is on offer
            // (EG: 4 items, Buy 2 for price of 1, there are 4 / 2 = 2 sets of items that are on offer)
            int setsOfItems = GetSetsOfItems(offerQuantity, itemQuantityOnOffer);

            // Gets discount price of 1 set of items
            discountPrice = priceOfQuantity * normalPrice;
            // Calculates price of items on offer
            discountedTotal = GetPriceAtQuantity(discountPrice, setsOfItems);
            // Calculates price of items not on offer and adds it to discountedTotal
            discountedTotal = discountedTotal + GetPriceAtQuantity(normalPrice, itemQuantityNotOnOffer);

            return discountedTotal;
        }
    }
}
