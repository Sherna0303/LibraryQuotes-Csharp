using LibraryQuotes.Models.Enums;

namespace LibraryQuotes.Models.DTOS
{
    public class CopyDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public float Price { get; set; }
        public CopyType Type { get; set; }

    }
}
