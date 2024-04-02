namespace LibraryQuotes.Models.Entities
{
    public abstract class Copy
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public float TotalPrice { get; set; }
        protected float INCREASE_PRICE = 0;

        protected Copy()
        {
        }

        protected Copy(string name, string author, float price, float discount)
        {
            Name = name;
            Author = author;
            Price = price;
            Discount = discount;
        }

        public abstract float CalculateIncrease(float RETAIL_INCREASE);
        public abstract float CalculateDiscount(int AntiquityYears);
        public abstract float CalculateWholesaleDiscount(int count);
    }
}
