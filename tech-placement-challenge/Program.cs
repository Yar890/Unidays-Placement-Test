using System;
using System.Collections.Generic;

namespace tech_placement_challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PricingRule> pricingRules = new List<PricingRule>();

            pricingRules.Add(new PricingRule("A", 8, "None"));
            pricingRules.Add(new PricingRule("B", 12, "2 for £20.00"));
            pricingRules.Add(new PricingRule("C", 4, "3 for £10.00"));
            pricingRules.Add(new PricingRule("D", 7, "Buy 1 get 1 free"));
            pricingRules.Add(new PricingRule("E", 5, "3 for the price of 2"));

            UnidaysDiscountChallenge example = new UnidaysDiscountChallenge(pricingRules);
        }
    }

    class UnidaysDiscountChallenge
    {
        public List<PricingRule> pricingRules;
        public List<string> basket;

        public UnidaysDiscountChallenge(List<PricingRule> PricingRules)
        {
            this.pricingRules = PricingRules;
        }

        static void AddToBasket(string item)
        {
            
        }

        static void CalculateTotalPrice()
        {

        }

        
    }

    class PricingRule
    {
        public string item;
        public double price;
        public string discount;

        public PricingRule(string Item, double Price, string Discount)
        {
            this.item = Item;
            this.price = Price;
            this.discount = Discount;
        }

    }
}
