namespace LibraryQuotes.Models.DTOS
{
    public class ClientListAndAmountDTO
    {
        public int AntiquityYears { get; set; }
        public List<CopyByIdAndAmountDTO> Copies { get; set; }
    }
}
