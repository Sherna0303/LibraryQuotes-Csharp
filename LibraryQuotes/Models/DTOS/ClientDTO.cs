using LibraryQuotes.Models.Enums;

namespace LibraryQuotes.Models.DTOS
{
    public class ClientDTO
    {
        public int AntiquityYears { get; set; }
        public List<CopyDTO> Copies { get; set; }
    }
}
