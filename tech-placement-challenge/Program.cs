using System;
using System.Collections.Generic;

namespace tech_placement_challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, PricingRule> pricingRules = new Dictionary<string, PricingRule>();

            pricingRules.Add("A", new PricingRule("A", 8));
            pricingRules.Add("B", new PricingRule("B", 12, new QuantityForSetPrice(2, 20)));
            pricingRules.Add("C", new PricingRule("C", 4, new QuantityForSetPrice(3, 10)));
            pricingRules.Add("D", new PricingRule("D", 7, new BuyQuantityGetQuantityFree(1, 1)));
            // Note: 3 for the price of 2 is the same as buy 3 get 1 free
            pricingRules.Add("E", new PricingRule("E", 5, new BuyQuantityGetQuantityFree(3, 1)));

            UnidaysDiscountChallenge example = new UnidaysDiscountChallenge(pricingRules);

            example.AddToBasket("A");
            example.AddToBasket("D");
            example.AddToBasket("D");
            example.AddToBasket("D");

            Dictionary<string, double> results = example.CalculateTotalPrice();
        }
    }

    class UnidaysDiscountChallenge
    {
        public Dictionary<string, PricingRule> pricingRules;
        public Dictionary<string, int> basket;

        public UnidaysDiscountChallenge(Dictionary<string, PricingRule> PricingRules)
        {
            this.pricingRules = PricingRules;
            this.basket = new Dictionary<string, int>();
        }

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

        public Dictionary<string, double> CalculateTotalPrice()
        {
            double total = 0;

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

            

            return null;
        }
    }

    class Discount
    {
        public Discount()
        {
        }

    }

    class QuantityForSetPrice : Discount
    {
        public int purchaseQuantity;
        public int atPrice;

        public QuantityForSetPrice(int quantity, int price)
        {
            this.purchaseQuantity = quantity;
            this.atPrice = price;
        }

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
        public BuyQuantityGetQuantityFree(int quantity, int freeQuantity)
        {
            this.purchaseQuantity = quantity;
            this.getFreeQuantity = freeQuantity;
        }

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

        public PricingRule(string item, double price, Discount discount)
        {
            this.item = item;
            this.price = price;
            this.discount = discount;
        }

        public PricingRule(string item, double price)
        {
            this.item = item;
            this.price = price;
            this.discount = null;
        }
    }
}
