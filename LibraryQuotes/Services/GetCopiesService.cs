using LibraryQuotes.Models.DataBase.Interfaces;
using LibraryQuotes.Models.DTOS.Budget;
using LibraryQuotes.Models.DTOS.Quoation;
using LibraryQuotes.Models.DTOS.QuoteList;
using LibraryQuotes.Models.Persistence;
using LibraryQuotes.Services.Interfaces;

namespace LibraryQuotes.Services
{
    public class GetCopiesService : IGetCopiesService
    {
        private readonly IDatabase _database;

        public GetCopiesService(IDatabase database)
        {
            _database = database;
        }

        public async Task<ClientDTO> GetCopiesByIdAndAmountAsync(ClientListAndAmountDTO payload)
        {
            var copies = new List<CopyDTO>();

            foreach (var item in payload.Copies)
            {
                Copy copy = await _database.copy.FindAsync(item.Id);

                if (copy == null)
                {
                    return null;
                }

                copies.AddRange(ConvertToCopyDTOs(copy, item.Amount));
            }

            return new ClientDTO { Copies = copies };
        }

        public async Task<ClientDTO> GetCopiesByIdAsync(ClientListIdDTO payload)
        {
            var copies = new List<CopyDTO>();

            foreach (var item in payload.Copies)
            {
                Copy copy = await _database.copy.FindAsync(item.Id);

                if (copy == null)
                {
                    return null;
                }

                copies.AddRange(ConvertToCopyDTOs(copy, 1));
            }

            return new ClientDTO { Copies = copies };
        }

        private List<CopyDTO> ConvertToCopyDTOs(Copy copy, int amount)
        {
            var copyDTOs = new List<CopyDTO>();

            for (int i = 0; i < amount; i++)
            {
                copyDTOs.Add(new CopyDTO { Name = copy.Name, Author = copy.Author, Price = (float)copy.Price, Type = copy.Type });
            }

            return copyDTOs;
        }
    }
}
