using System;
using System.Collections.Generic;

namespace tech_placement_challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            PricingRule pricingRule = new PricingRule(); 

            UnidaysDiscountChallenge example = new UnidaysDiscountChallenge(pricingRule);
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
        public List<string> Items;

        public PricingRule()
        {

        }
    }
}
