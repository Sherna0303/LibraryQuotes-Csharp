using LibraryQuotes.Models.Enums;

namespace LibraryQuotes.Models.Persistence
{
    public class Copy
    {
        public int CopyId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public CopyType Type { get; set; }
    }
}
