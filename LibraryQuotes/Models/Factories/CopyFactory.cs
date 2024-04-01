using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;
using LibraryQuotes.Models.Enums;

namespace LibraryQuotes.Models.Factories
{
    public class CopyFactory : ICopyFactory
    {
        public Copy Create(CopyDTO payload)
        {
            var copyChildren = new Dictionary<CopyType, Copy>
            {
                { CopyType.BOOK, new Book(payload.Name, payload.Author, payload.Price) },
                { CopyType.NOVEL, new Novel(payload.Name, payload.Author, payload.Price) },
            };

            var copy = copyChildren.GetValueOrDefault(payload.Type);

            if (copy == null)
            {
                throw new ArgumentException("Incorret Type");
            }

            return copy;
        }
    }
}
