using System;
using System.Collections.Generic;

namespace tech_placement_challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //PricingRule pricingRule = new PricingRule(); 

            //UnidaysDiscountChallenge example = new UnidaysDiscountChallenge(pricingRule);
        }
    }

    class UnidaysDiscountChallenge
    {
        public List<double> basket;

        public UnidaysDiscountChallenge(PricingRule pricingRule)
        {

        }

        static void AddToBasket()
        {

        }

        static void CalculateTotalPrice()
        {

        }
    }

    class PricingRule
    {
        public List<string> items;

        public PricingRule(List<string[]> Items)
        {
            
        }
    }

    class Item
    {
        public string name;
        public double price;
        public string discount;

        public Item(string Name, double Price, string Discount)
        {
            this.name = Name;
            this.price = Price;
            this.discount = Discount;
        }
    }
}
