using FluentValidation;
using LibraryQuotes.Models.DTOS.Quoation;

namespace LibraryQuotes.Models.DTOS.QuoteList
{
    public class ClientDTO
    {
        public int AntiquityYears { get; set; }
        public List<CopyDTO> Copies { get; set; }
    }
}
