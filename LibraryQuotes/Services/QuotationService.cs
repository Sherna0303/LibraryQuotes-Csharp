using LibraryQuotes.Models.DataBase.Interfaces;
using LibraryQuotes.Models.DTOS.Quoation;
using LibraryQuotes.Models.Factories;
using LibraryQuotes.Models.Persistence;
using LibraryQuotes.Services.Interfaces;

namespace LibraryQuotes.Services
{
    public class QuotationService : IQuotationService
    {
        private readonly ICopyFactory _copyFactory;
        private readonly IDatabase _database;

        public QuotationService(ICopyFactory copyFactory, IDatabase database)
        {
            _copyFactory = copyFactory;
            _database = database;
        }

        public async Task<Copy> CalculatePrice(CopyDTO payload)
        {
            var copy = _copyFactory.Create(payload);
            copy.CalculateIncrease();

            var copyDb = new Copy()
            {
                Name = copy.Name,
                Author = copy.Author,
                Price = copy.Price,
                Type = payload.Type,
            };

            await _database.copy.AddAsync(copyDb);

            if (!await _database.SaveAsync())
            {
                return null;
            }

            return copyDb;
        }
    }
}
