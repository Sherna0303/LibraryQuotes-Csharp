namespace LibraryQuotes.Models.DTOS
{
    public class ClientListDTO
    {
        public int AntiquityYears { get; set; }
        public List<CopyByIdAndAmountDTO> Copies { get; set; }
    }
}
