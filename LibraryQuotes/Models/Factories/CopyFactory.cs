using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Models.Entities;
using LibraryQuotes.Models.Enums;

namespace LibraryQuotes.Models.Factories
{
    public class CopyFactory : ICopyFactory
    {
        public CopyEntity Create(CopyDTO payload)
        {
            var copyChildren = new Dictionary<CopyType, CopyEntity>
            {
                { CopyType.BOOK, new BookEntity(payload.Name, payload.Author, payload.Price, 0) },
                { CopyType.NOVEL, new NovelEntity(payload.Name, payload.Author, payload.Price, 0) },
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
