namespace LibraryQuotes.Models.Entities
{
    public class NovelEntity : CopyEntity
    {
        public NovelEntity()
        {
        }

        public NovelEntity(string name, string author, float price, float discount) : base(name, author, price, discount)
        {
            INCREASE_PRICE = 2;
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
