﻿namespace LibraryQuotes.Models.Entities
{
    public class Book : Copy
    {
        public Book()
        {
        }

        public Book(string name, string author, float price, float discount) : base(name, author, price, discount)
        {
            INCREASE_PRICE = 4f / 3f;
        }

        public override float CalculateIncrease(float RETAIL_INCREASE)
        {
            Price = Price * INCREASE_PRICE * RETAIL_INCREASE;
            return Price;
        }
    }
}
