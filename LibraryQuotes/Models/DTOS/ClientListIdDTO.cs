namespace LibraryQuotes.Models.DTOS
{
    public class ClientListIdDTO
    {
        public int AntiquityYears { get; set; }
        public List<CopyByIdDTO> Copies { get; set; }
    }
}
