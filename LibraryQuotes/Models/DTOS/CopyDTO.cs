namespace LibraryQuotes.Models.DTOS
{
    public class CopyDTO
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }

        public CopyDTO()
        {
        }

        public CopyDTO(string name, string author, float price)
        {
            Name = name;
            Author = author;
            Price = price;
        }
    }
}
