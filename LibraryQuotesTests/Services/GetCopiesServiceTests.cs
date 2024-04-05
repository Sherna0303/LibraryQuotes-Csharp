using LibraryQuotes.Models.DataBase.Interfaces;
using LibraryQuotes.Models.DTOS.Budget;
using LibraryQuotes.Models.DTOS.Quoation;
using LibraryQuotes.Models.DTOS.QuoteList;
using LibraryQuotes.Models.Enums;
using LibraryQuotes.Models.Persistence;
using LibraryQuotes.Services.Interfaces;
using Moq;

namespace LibraryQuotes.Services.Tests
{
    public class GetCopiesServiceTests
    {
        private readonly IGetCopiesService _getCopiesService;
        private readonly Mock<IDatabase> _databaseMock;

        public GetCopiesServiceTests()
        {
            _databaseMock = new Mock<IDatabase>();
            _getCopiesService = new GetCopiesService(_databaseMock.Object);
        }

        [Fact]
        public async void GetCopiesByIdAndAmountAsync_Success()
        {
            var payload = new ClientListAndAmountDTO
            {
                AntiquityYears = 0,
                Copies = new List<CopyByIdAndAmountDTO>
                {
                    new CopyByIdAndAmountDTO()
                    {
                        Id = 2,
                        Amount = 2
                    },
                }
            };

            var clientDTO = new ClientDTO()
            {
                AntiquityYears = 0,
                Copies = new List<CopyDTO>
                {
                    new CopyDTO()
                    {
                        Name = "Novela",
                        Author = "AutorNovela",
                        Price = 40,
                        Type = CopyType.NOVEL
                    },
                    new CopyDTO()
                    {
                        Name = "Novela",
                        Author = "AutorNovela",
                        Price = 40,
                        Type = CopyType.NOVEL
                    }
                }
            };

            var copy = new Copy()
            {
                CopyId = 2,
                Name = "Novela",
                Author = "AutorNovela",
                Price = 40,
                Type = CopyType.NOVEL
            };

            _databaseMock.Setup(db => db.copy.FindAsync(2)).ReturnsAsync(copy);

            var result = await _getCopiesService.GetCopiesByIdAndAmountAsync(payload);

            Assert.NotNull(result);
            Assert.Equal(0, result.AntiquityYears);
            Assert.Equivalent(clientDTO, result);
        }

        [Fact]
        public async void GetCopiesByIdAsync_Success()
        {
            var payload = new ClientListIdDTO
            {
                AntiquityYears = 1,
                Copies = new List<CopyByIdDTO>
                {
                    new CopyByIdDTO()
                    {
                        Id = 2
                    },
                    new CopyByIdDTO()
                    {
                        Id = 1
                    }
                }
            };

            var clientDTO = new ClientDTO()
            {
                AntiquityYears = 1,
                Copies = new List<CopyDTO>
                {
                    new CopyDTO()
                    {
                        Name = "Novela",
                        Author = "AutorNovela",
                        Price = 40,
                        Type = CopyType.NOVEL
                    },
                    new CopyDTO()
                    {
                        Name = "Libro",
                        Author = "AutorLibro",
                        Price = 26.666667938232422f,
                        Type = CopyType.BOOK
                    }
                }
            };

            var novel = new Copy()
            {
                CopyId = 2,
                Name = "Novela",
                Author = "AutorNovela",
                Price = 40,
                Type = CopyType.NOVEL
            };

            var book = new Copy()
            {
                CopyId = 2,
                Name = "Libro",
                Author = "AutorLibro",
                Price = 26.666667938232422f,
                Type = CopyType.BOOK
            };

            _databaseMock.Setup(db => db.copy.FindAsync(1)).ReturnsAsync(book);
            _databaseMock.Setup(db => db.copy.FindAsync(2)).ReturnsAsync(novel);

            var result = await _getCopiesService.GetCopiesByIdAsync(payload);

            Assert.NotNull(result);
            Assert.Equal(1, result.AntiquityYears);
            Assert.Equivalent(clientDTO, result);
        }

        [Fact]
        public async void GetCopiesByIdAndAmountAsync_Fail_IdDoesNotExist()
        {
            var payload = new ClientListAndAmountDTO
            {
                AntiquityYears = 0,
                Copies = new List<CopyByIdAndAmountDTO>
                {
                    new CopyByIdAndAmountDTO()
                    {
                        Id = 22,
                        Amount = 2
                    },
                }
            };

            _databaseMock.Setup(db => db.copy.FindAsync(22)).ReturnsAsync((Copy)null);

            var result = await _getCopiesService.GetCopiesByIdAndAmountAsync(payload);

            Assert.Null(result);
        }

        [Fact]
        public async void GetCopiesByIdAsync_Fail_IdDoesNotExist()
        {
            var payload = new ClientListIdDTO
            {
                AntiquityYears = 1,
                Copies = new List<CopyByIdDTO>
                {
                    new CopyByIdDTO()
                    {
                        Id = 22
                    },
                    new CopyByIdDTO()
                    {
                        Id = 1
                    }
                }
            };

            var novel = new Copy()
            {
                CopyId = 2,
                Name = "Novela",
                Author = "AutorNovela",
                Price = 40,
                Type = CopyType.NOVEL
            };

            _databaseMock.Setup(db => db.copy.FindAsync(22)).ReturnsAsync((Copy)null);
            _databaseMock.Setup(db => db.copy.FindAsync(2)).ReturnsAsync(novel);

            var result = await _getCopiesService.GetCopiesByIdAsync(payload);

            Assert.Null(result);
        }
    }
}