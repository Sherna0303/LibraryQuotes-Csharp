namespace LibraryQuotes.Models.Entities
{
    public class ListCopiesEntity
    {
        public int AntiquityYears { get; set; }
        public float Total { get; set; }
        public float TotalDiscount { get; set; }
        public List<CopyEntity> Copies { get; set; }

        public ListCopiesEntity()
        {
        }

        public ListCopiesEntity(int antiquityYears, List<CopyEntity> copies, float total, float totalDiscount)
        {
            AntiquityYears = antiquityYears;
            Copies = copies;
            Total = total;
            TotalDiscount = totalDiscount;
        }
    }
}
