namespace LibraryQuotes.Models.Persistence
{
    public class Users
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnly CreationDate { get; set; }
    }
}
