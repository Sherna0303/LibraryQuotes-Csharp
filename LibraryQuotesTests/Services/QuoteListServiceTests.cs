using LibraryQuotes.Models.DTOS.Quoation;
using LibraryQuotes.Models.DTOS.QuoteList;
using LibraryQuotes.Models.Entities;
using LibraryQuotes.Models.Enums;
using LibraryQuotes.Models.Factories;
using LibraryQuotes.Services.Interfaces;
using Moq;

namespace LibraryQuotes.Services.Tests
{
    public class QuoteListServiceTests
    {
        private readonly Mock<ICopyFactory> _copyFactory;
        private readonly IQuoteListService _quoteListService;

        public QuoteListServiceTests()
        {
            _copyFactory = new Mock<ICopyFactory>();
            _quoteListService = new QuoteListService(_copyFactory.Object, null);
        }

        [Fact]
        public void CalculatePriceListCopiesAndConvertToClientDTO_Fail()
        {
            var payload = new ClientListAndAmountDTO();

            var getCopiesServiceMock = new Mock<IGetCopiesService>();
            var quoteListService = new QuoteListService(null, getCopiesServiceMock.Object);

            getCopiesServiceMock.Setup(service => service.GetCopiesByIdAndAmountAsync(payload))
                                .ReturnsAsync((ClientDTO)null);

            Assert.Throws<ArgumentException>(() => quoteListService.CalculatePriceListCopiesAndConvertToClientDTO(payload));

            getCopiesServiceMock.Verify(service => service.GetCopiesByIdAndAmountAsync(It.IsAny<ClientListAndAmountDTO>()), Times.Once);

        }

        [Fact]
        public void CalculatePriceListCopies_Success_Detal()
        {
            var client = new ClientDTO()
            {
                AntiquityYears = 0,
                Copies = new List<CopyDTO>
                {
                    new CopyDTO()
                    {
                        Name = "Novel",
                        Author = "Author",
                        Price = 20,
                        Type = CopyType.NOVEL
                    },
                    new CopyDTO()
                    {
                        Name = "Book",
                        Author = "Author",
                        Price = 10,
                        Type = CopyType.BOOK
                    }
                }
            };

            var copyEntities = new List<CopyEntity>
            {
                new BookEntity("Book", "Author", 10f, 0),
                new NovelEntity("Novel", "Author", 20f, 0)
            };

            var total = 30.599998474121094;
            var discount = 0.0;

            _copyFactory.SetupSequence(factory => factory.Create(It.IsAny<CopyDTO>()))
                .Returns(copyEntities[0])
                .Returns(copyEntities[1]);

            var result = _quoteListService.CalculatePriceListCopies(client);

            Assert.NotNull(result);
            Assert.Equal(client.AntiquityYears, result.AntiquityYears);
            Assert.Equal(total, result.Total);
            Assert.Equal(discount, result.TotalDiscount);
            Assert.Equal(copyEntities, result.Copies);

            _copyFactory.Verify(factory => factory.Create(It.IsAny<CopyDTO>()), Times.Exactly(2));
        }

        [Fact]
        public void CalculatePriceListCopies_Success_Wholesale()
        {
            var client = new ClientDTO()
            {
                AntiquityYears = 0,
                Copies = new List<CopyDTO>()
            };

            for (int i = 0; i < 12; i++)
            {
                client.Copies.Add(new CopyDTO()
                {
                    Name = "Novel",
                    Author = "Author",
                    Price = 20,
                    Type = CopyType.NOVEL
                });
            }

            var copyEntities = new List<CopyEntity>();

            for (int i = 0; i < 12; i++)
            {
                copyEntities.Add(new NovelEntity("Novel", "Author", 20f, 0));
            }

            var total = 239.28001403808594;
            var discount = 0.72000002861022949;

            for (int i = 0; i < 12; i++)
            {
                _copyFactory.Setup(factory => factory.Create(It.IsAny<CopyDTO>())).Returns(copyEntities[i]).Verifiable();
            }

            var result = _quoteListService.CalculatePriceListCopies(client);

            Assert.NotNull(result);
            Assert.Equal(client.AntiquityYears, result.AntiquityYears);
            Assert.Equal(total, result.Total);
            Assert.Equal(discount, result.TotalDiscount);

            for (int i = 0; i < 12; i++)
            {
                Assert.Equal(copyEntities[i].Name, result.Copies[i].Name);
                Assert.Equal(copyEntities[i].Author, result.Copies[i].Author);
                Assert.Equal(copyEntities[i].Price, result.Copies[i].Price);
            }

            _copyFactory.Verify(factory => factory.Create(It.IsAny<CopyDTO>()), Times.Exactly(12));
        }

        [Fact]
        public void CalculatePriceListCopies_Success_OneOrTwoAntiquityYearsDiscount()
        {
            var client = new ClientDTO()
            {
                AntiquityYears = 1,
                Copies = new List<CopyDTO>
                {
                    new CopyDTO()
                    {
                        Name = "Novel",
                        Author = "Author",
                        Price = 20,
                        Type = CopyType.NOVEL
                    },
                    new CopyDTO()
                    {
                        Name = "Book",
                        Author = "Author",
                        Price = 10,
                        Type = CopyType.BOOK
                    }
                }
            };

            var copyEntities = new List<CopyEntity>
            {
                new BookEntity("Book", "Author", 10f, 0),
                new NovelEntity("Novel", "Author", 20f, 0)
            };

            var total = 26.927999496459961;
            var discount = 3.6719999313354492;

            _copyFactory.SetupSequence(factory => factory.Create(It.IsAny<CopyDTO>()))
                .Returns(copyEntities[0])
                .Returns(copyEntities[1]);

            var result = _quoteListService.CalculatePriceListCopies(client);

            Assert.NotNull(result);
            Assert.Equal(client.AntiquityYears, result.AntiquityYears);
            Assert.Equal(total, result.Total);
            Assert.Equal(discount, result.TotalDiscount);
            Assert.Equal(copyEntities, result.Copies);

            _copyFactory.Verify(factory => factory.Create(It.IsAny<CopyDTO>()), Times.Exactly(2));

        }

        [Fact]
        public void CalculatePriceListCopies_Success_MoreThanTwoAntiquityYearsDiscount()
        {
            var client = new ClientDTO()
            {
                AntiquityYears = 3,
                Copies = new List<CopyDTO>
                {
                    new CopyDTO()
                    {
                        Name = "Novel",
                        Author = "Author",
                        Price = 20,
                        Type = CopyType.NOVEL
                    },
                    new CopyDTO()
                    {
                        Name = "Book",
                        Author = "Author",
                        Price = 10,
                        Type = CopyType.BOOK
                    }
                }
            };

            var copyEntities = new List<CopyEntity>
            {
                new BookEntity("Book", "Author", 10f, 0),
                new NovelEntity("Novel", "Author", 20f, 0)
            };

            var total = 25.397998809814453;
            var discount = 5.2019996643066406;

            _copyFactory.SetupSequence(factory => factory.Create(It.IsAny<CopyDTO>()))
                .Returns(copyEntities[0])
                .Returns(copyEntities[1]);

            var result = _quoteListService.CalculatePriceListCopies(client);

            Assert.NotNull(result);
            Assert.Equal(client.AntiquityYears, result.AntiquityYears);
            Assert.Equal(total, result.Total);
            Assert.Equal(discount, result.TotalDiscount);
            Assert.Equal(copyEntities, result.Copies);

            _copyFactory.Verify(factory => factory.Create(It.IsAny<CopyDTO>()), Times.Exactly(2));
        }
    }
}
