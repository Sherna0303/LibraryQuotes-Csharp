namespace LibraryQuotes.Models.Entities
{
    public class ListCopies
    {
        public int AntiquityYears { get; set; }
        public float Total { get; set; }
        public float TotalDiscount { get; set; }
        public List<Copy> Copies { get; set; }

        public ListCopies()
        {
        }

        public ListCopies(int antiquityYears, List<Copy> copies, float total, float totalDiscount)
        {
            AntiquityYears = antiquityYears;
            Copies = copies;
            Total = total;
            TotalDiscount = totalDiscount;
        }
    }
}
