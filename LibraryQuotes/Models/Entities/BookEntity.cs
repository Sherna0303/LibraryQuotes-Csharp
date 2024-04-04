namespace LibraryQuotes.Models.Entities
{
    public class BookEntity : CopyEntity
    {
        public BookEntity()
        {
        }

        public BookEntity(string name, string author, float price, float discount) : base(name, author, price, discount)
        {
            INCREASE_PRICE = 4f / 3f;
        }

        public override float CalculateIncrease()
        {
            Price = Price * INCREASE_PRICE;
            return Price;
        }

        public override float CalculateIncreaseDetal(float RETAIL_INCREASE)
        {
            Price = Price * RETAIL_INCREASE;
            return Price;
        }
    }
}
