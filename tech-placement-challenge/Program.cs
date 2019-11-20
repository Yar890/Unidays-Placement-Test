﻿using System;
using System.Collections.Generic;

namespace tech_placement_challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, PricingRule> pricingRules = new Dictionary<string, PricingRule>();

            pricingRules.Add("A", new PricingRule("A", 8, new Discount()));
            pricingRules.Add("B", new PricingRule("B", 12, new QuanityForSetPrice(2, 20)));
            pricingRules.Add("C", new PricingRule("C", 4, new QuanityForSetPrice(3, 10)));
            pricingRules.Add("D", new PricingRule("D", 7, new BuyQuanityGetQuanityFree(1, 1)));
            pricingRules.Add("E", new PricingRule("E", 5, new BuyQuantityForPriceOfQuantity(3, 2)));

            UnidaysDiscountChallenge example = new UnidaysDiscountChallenge(pricingRules);

            example.AddToBasket("A");
            example.AddToBasket("B");

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
                string derivedClassName = pricingRules[item.Key].discount.GetType().UnderlyingSystemType.Name;

                switch (derivedClassName)
                {
                    case "QuantityForSetPrice":
                        QuanityForSetPrice deprivedDiscount = pricingRules[item.Key].discount as QuanityForSetPrice;
                            total = total + deprivedDiscount.applyDiscount(item.Value);
                        break;
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
        public int quantity;
        public int price;

        public QuanityForSetPrice(int quantity, int price)
        {
            this.quantity = quantity;
            this.price = price;
        }

        public double applyDiscount(int itemQuantity)
        {
            double cost;

            double remainder = itemQuantity % quantity;

            

            return 0;
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
