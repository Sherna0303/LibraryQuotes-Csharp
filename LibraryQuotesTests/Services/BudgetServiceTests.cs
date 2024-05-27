using LibraryQuotes.Models.DTOS.Budget;
using LibraryQuotes.Models.DTOS.Quoation;
using LibraryQuotes.Models.DTOS.QuoteList;
using LibraryQuotes.Models.Entities;
using LibraryQuotes.Models.Enums;
using LibraryQuotes.Services.Interfaces;
using Moq;

namespace LibraryQuotes.Services.Tests
{
    public class BudgetServiceTests
    {
        private readonly Mock<IQuoteListService> _quoteListService;
        private readonly Mock<IGetCopiesService> _getCopiesService;
        private readonly Mock<ICalculateSeniorityService> _calculateSeniorityService;
        private readonly IBudgetService _budgetService;

        public BudgetServiceTests()
        {
            _quoteListService = new Mock<IQuoteListService>();
            _getCopiesService = new Mock<IGetCopiesService>();
            _calculateSeniorityService = new Mock<ICalculateSeniorityService>();
            _budgetService = new BudgetService(_quoteListService.Object, _getCopiesService.Object, _calculateSeniorityService.Object);
        }

        [Fact]
        public void CalculateBudgetAndConvertToClientDTO_Fail()
        {
            var payload = new ClientListIdDTO();
            var payload2 = new BudgetClientDTO();

            var getCopiesServiceMock = new Mock<IGetCopiesService>();
            var quoteListService = new QuoteListService(null, getCopiesServiceMock.Object, _calculateSeniorityService.Object);

            _getCopiesService.Setup(service => service.GetCopiesByIdAsync(payload))
                                .ReturnsAsync((ClientDTO)null);

            Assert.Throws<ArgumentException>(() => _budgetService.CalculateBudgetAndConvertToClientDTO(payload2, ""));

            _getCopiesService.Verify(service => service.GetCopiesByIdAsync(It.IsAny<ClientListIdDTO>()), Times.Once);
        }

        [Fact]
        public void CalculateBudget_Success()
        {
            var payload = new ClientDTO()
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

            var budget = 320;
            var copiesEntity = new ListCopiesEntity
            {
                AntiquityYears = 0,
                Total = 312.75922f,
                TotalDiscount = 0.0408f,
                Copies = new List<CopyEntity>
                {
                    new NovelEntity { Name = "Novel", Author = "Author", Price = 40.8f, Discount = 0, TotalPrice = 40.8f },
                }
            };

            for (int i = 0; i < 9; i++)
            {
                copiesEntity.Copies.Add(new BookEntity { Name = "Book", Author = "Author", Price = 27.2f, Discount = 0, TotalPrice = 27.2f });
            }

            copiesEntity.Copies.Add(new BookEntity { Name = "Book", Author = "Author", Price = 27.2f, Discount = 0.0408f, TotalPrice = 27.1592f });

            _quoteListService.Setup(service => service.CalculatePriceListCopies(payload))
                                 .Returns(copiesEntity);

            var result = _budgetService.CalculateBudget(payload, budget);

            Assert.NotNull(result);
            Assert.Equal(copiesEntity.Total, result.Total);
            Assert.Equal(copiesEntity.TotalDiscount, result.TotalDiscount);
            Assert.Equal(copiesEntity, result);

            _quoteListService.Verify(service => service.CalculatePriceListCopies(It.IsAny<ClientDTO>()), Times.AtLeastOnce);
        }

        [Fact]
        public void CalculateBudget_Fail_CountNotMoreThanTen()
        {
            var payload = new ClientDTO()
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

            var budget = 100;
            var copiesEntity = new ListCopiesEntity
            {
                AntiquityYears = 0,
                Total = 101,
                TotalDiscount = 0,
                Copies = new List<CopyEntity>
                {
                    new NovelEntity { Name = "Novel", Author = "Author", Price = 51, Discount = 0, TotalPrice = 40 },
                    new BookEntity { Name = "Book", Author = "Author", Price = 50, Discount = 0, TotalPrice = 20 }
                }
            };

            _quoteListService.Setup(service => service.CalculatePriceListCopies(payload))
                                 .Returns(copiesEntity);

            Assert.Throws<ArgumentException>(() => _budgetService.CalculateBudget(payload, budget));

            _quoteListService.Verify(service => service.CalculatePriceListCopies(It.IsAny<ClientDTO>()), Times.AtLeastOnce);
        }

        [Fact]
        public void CalculateBudget_Fail_OnlyOneType()
        {
            var payload = new ClientDTO()
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
                    }
                }
            };

            var budget = 320;
            var copiesEntity = new ListCopiesEntity
            {
                AntiquityYears = 0,
                Total = 321,
                TotalDiscount = 0.0408f,
                Copies = new List<CopyEntity>()
            };

            for (int i = 0; i < 11; i++)
            {
                copiesEntity.Copies.Add(new NovelEntity { Name = "Novel", Author = "Author", Price = 40.8f, Discount = 0, TotalPrice = 40.8f });
            }

            _quoteListService.Setup(service => service.CalculatePriceListCopies(payload))
                                 .Returns(copiesEntity);

            Assert.Throws<ArgumentException>(() => _budgetService.CalculateBudget(payload, budget));

            _quoteListService.Verify(service => service.CalculatePriceListCopies(It.IsAny<ClientDTO>()), Times.Once);
        }
    }
}