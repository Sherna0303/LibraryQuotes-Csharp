namespace LibraryQuotes.Models.Entities
{
    public class ListCopies
    {
        public float Total { get; set; }
        public float TotalDiscount { get; set; }
        public List<Copy> Copies { get; set; }

        public ListCopies()
        {
        }

        public ListCopies(List<Copy> copies, float total, float totalDiscount)
        {
            Copies = copies;
            Total = total;
            TotalDiscount = totalDiscount;
        }
    }
}
