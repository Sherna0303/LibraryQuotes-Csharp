namespace LibraryQuotes.Models.Entities
{
    public class ListCopies
    {
        public List<Copy> copies {  get; set; }
        public float Total { get; set; }
        public float TotalDiscount { get; set; }

        public ListCopies()
        {
        }

        public ListCopies(List<Copy> copies, float total, float totalDiscount)
        {
            this.copies = copies;
            Total = total;
            TotalDiscount = totalDiscount;
        }
    }
}
