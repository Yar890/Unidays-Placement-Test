using System;
using System.Collections.Generic;

namespace tech_placement_challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Item> pricingRule = new List<Item>();

            pricingRule.Add(new Item("A", 8, "None"));
            pricingRule.Add(new Item("B", 12, "2 for £20.00"));
            pricingRule.Add(new Item("C", 4, "3 for £10.00"));
            pricingRule.Add(new Item("D", 7, "Buy 1 get 1 free"));
            pricingRule.Add(new Item("E", 5, "3 for the price of 2"));

            //UnidaysDiscountChallenge example = new UnidaysDiscountChallenge(pricingRule);
        }
    }

    class UnidaysDiscountChallenge
    {
        public List<double> basket;

        public UnidaysDiscountChallenge(List<Item> pricingRule)
        {

        }

        static void AddToBasket()
        {

        }

        static void CalculateTotalPrice()
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
