namespace LibraryQuotes.Models.Entities
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

        public override float CalculateDiscount(int AntiquityYears)
        {
            float DISCOUNT_ANTIQUITY = ValidateAntiquityDiscount(AntiquityYears);
            Discount += Price * DISCOUNT_ANTIQUITY;
            TotalPrice -= Discount;
            return TotalPrice;
        }

        public override float CalculateWholesaleDiscount(int count)
        {
            float WHOLESALE_DISCOUNT = ValidateWholesaleDiscount(count - 10);
            Discount = Price * WHOLESALE_DISCOUNT;
            return Discount;
        }

        private float ValidateWholesaleDiscount(int count)
        {
            const float WHOLESALE_DISCOUNT = 0.15f;
            return count * WHOLESALE_DISCOUNT / 100;
        }

        private float ValidateAntiquityDiscount(int years)
        {
            float DISCOUNT;
            if (years <= 0)
            {
                DISCOUNT = 0;
            }
            else if (years >= 1 && years <= 2)
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
