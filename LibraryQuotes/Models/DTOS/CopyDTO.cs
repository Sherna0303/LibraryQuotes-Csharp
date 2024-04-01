namespace LibraryQuotes.Models.DTOS
{
    public class CopyDTO
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public float precio { get; set; }

        public CopyDTO()
        {
        }

        public CopyDTO(string name, string author, float precio)
        {
            Name = name;
            Author = author;
            this.precio = precio;
        }
    }
}
