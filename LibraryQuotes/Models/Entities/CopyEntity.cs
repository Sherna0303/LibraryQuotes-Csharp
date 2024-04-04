namespace LibraryQuotes.Models.Entities
{
    public abstract class CopyEntity
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public float TotalPrice { get; set; }
        protected float INCREASE_PRICE = 0;

        protected CopyEntity()
        {
        }

        protected CopyEntity(string name, string author, float price, float discount)
        {
            Name = name;
            Author = author;
            Price = price;
            Discount = discount;
        }

        public abstract float CalculateIncrease();
        public abstract float CalculateIncreaseDetal(float RETAIL_INCREASE);

        public float CalculateDiscount(int AntiquityYears)
        {
            float DISCOUNT_ANTIQUITY = ValidateAntiquityDiscount(AntiquityYears);
            Discount = Math.Max(Discount, Price * DISCOUNT_ANTIQUITY);
            TotalPrice = Price - Discount;
            return Discount;
        }

        public float CalculateWholesaleDiscount(int count)
        {
            float WHOLESALE_DISCOUNT = ValidateWholesaleDiscount(count);
            Discount = Price * WHOLESALE_DISCOUNT;
            return Discount;
        }

        private float ValidateWholesaleDiscount(int count)
        {
            const float WHOLESALE_DISCOUNT = 0.15f;
            float discount = (count - 10) * WHOLESALE_DISCOUNT / 100;
            return (discount < 1) ? discount : 1f;
        }

        private float ValidateAntiquityDiscount(int years)
        {
            float DISCOUNT;
            if (years <= 0)
            {
                DISCOUNT = 0;
            }
            else if (years <= 2)
            {
                DISCOUNT = 0.12f;
            }
            else
            {
                DISCOUNT = 0.17f;
            }

            return DISCOUNT;
        }
    }
}
