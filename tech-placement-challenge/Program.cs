using System;
using System.Collections.Generic;

namespace tech_placement_challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PricingRule> pricingRules = new List<PricingRule>();

            pricingRules.Add(new PricingRule("A", 8, new Discount()));
            pricingRules.Add(new PricingRule("B", 12, new QuanityForSetPrice(2, 20)));
            pricingRules.Add(new PricingRule("C", 4, new QuanityForSetPrice(3, 10)));
            pricingRules.Add(new PricingRule("D", 7, new BuyQuanityGetQuanityFree(1, 1)));
            pricingRules.Add(new PricingRule("E", 5, new BuyQuantityForPriceOfQuantity(3, 2)));

            UnidaysDiscountChallenge example = new UnidaysDiscountChallenge(pricingRules);

            example.AddToBasket("A");
            example.AddToBasket("B");

            Dictionary<string, double> results = example.CalculateTotalPrice();
        }
    }

    class UnidaysDiscountChallenge
    {
        public List<PricingRule> pricingRules;
        public List<string> basket;

        public UnidaysDiscountChallenge(List<PricingRule> PricingRules)
        {
            this.pricingRules = PricingRules;
            this.basket = new List<string>();
        }

        public void AddToBasket(string item)
        {
            this.basket.Add(item);
        }

        public Dictionary<string, double> CalculateTotalPrice()
        {
            List<string>[] itemQuantity;

            foreach (string item in basket)
            {
                foreach (PricingRule pricingRule in pricingRules)
                {
                    if (item == pricingRule.item)
                    {

                    }
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

    class QuanityForSetPrice : Discount
    {
        int quantity;
        int price;

        public QuanityForSetPrice(int quantity, int price)
        {
            this.quantity = quantity;
            this.price = price;
        }
    }

    class BuyQuanityGetQuanityFree : Discount
    {
        int quantity;
        int freeQuantity;
        public BuyQuanityGetQuanityFree(int quantity, int freeQuantity)
        {
            this.quantity = quantity;
            this.freeQuantity = freeQuantity;
        }
    }

    class BuyQuantityForPriceOfQuantity : Discount
    {
        int quantity;
        int priceOfQuantity;
        public BuyQuantityForPriceOfQuantity(int quantity, int priceOfQuantity)
        {
            this.quantity = quantity;
            this.priceOfQuantity = priceOfQuantity;
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

    }
}
