﻿namespace LibraryQuotes.Models.Entities
{
    public class Novel : Copy
    {
        public Novel()
        {
        }

        public Novel(string name, string author, float price, float discount) : base(name, author, price, discount)
        {
            INCREASE_PRICE = 2;
        }

        public override float CalculateIncrease(float RETAIL_INCREASE)
        {
            Price = Price * INCREASE_PRICE * RETAIL_INCREASE;
            return Price;
        }
    }
}
