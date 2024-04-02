namespace LibraryQuotes.Models.Entities
{
    public class Novel : Copy
    {
        public Novel()
        {
        }

        public Novel(string name, string author, float price) : base(name, author, price)
        {
            INCREASE_PRICE = 2;
        }

        public override float CalculateIncrease(float RETAIL_INCREASE, float WHOLESALE_DISCOUNT)
        {
            TotalPrice = Price * INCREASE_PRICE * RETAIL_INCREASE * WHOLESALE_DISCOUNT;
            return TotalPrice;
        }
    }
}
