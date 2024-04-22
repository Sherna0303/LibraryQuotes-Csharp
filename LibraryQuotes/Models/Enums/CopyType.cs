using System.Text.Json.Serialization;

namespace LibraryQuotes.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CopyType
    {
        BOOK,
        NOVEL
    }
}
