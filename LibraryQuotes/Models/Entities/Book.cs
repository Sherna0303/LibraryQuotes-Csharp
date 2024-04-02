namespace LibraryQuotes.Models.Entities
{
    public class Book : Copy
    {
        public Book()
        {
        }

        public Book(string name, string author, float price) : base(name, author, price)
        {
            INCREASE_PRICE = 4f/3f;
        }

        public override float CalculateIncrease(float RETAIL_INCREASE, float WHOLESALE_DISCOUNT)
        {
            TotalPrice = Price * INCREASE_PRICE * RETAIL_INCREASE * WHOLESALE_DISCOUNT;
            return TotalPrice;
        }
    }
}
