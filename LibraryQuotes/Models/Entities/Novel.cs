namespace LibraryQuotes.Models.Entities
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

        public override float CalculateDiscount(int AntiquityYears)
        {
            float DISCOUNT_ANTIQUITY = ValidateAntiquityDiscount(AntiquityYears);
            Discount += Price * DISCOUNT_ANTIQUITY;
            TotalPrice = Price - Discount;
            return Discount;
        }

        public override float CalculateWholesaleDiscount(int count)
        {
            float WHOLESALE_DISCOUNT = ValidateWholesaleDiscount(count);
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
