using LibraryQuotes.Models.DataBase.Interfaces;
using LibraryQuotes.Models.DTOS.Quoation;
using LibraryQuotes.Models.Entities;
using LibraryQuotes.Models.Enums;
using LibraryQuotes.Models.Factories;
using LibraryQuotes.Models.Persistence;
using LibraryQuotes.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LibraryQuotes.Services.Tests
{
    public class QuotationServiceTests
    {
        private readonly Mock<ICopyFactory> _copyFactory;
        private readonly Mock<DbSet<Copy>> _dbSetMock;
        private readonly Mock<IDatabase> _databaseMock;
        private readonly IQuotationService _quotationService;

        public QuotationServiceTests()
        {
            _dbSetMock = new Mock<DbSet<Copy>>();
            _copyFactory = new Mock<ICopyFactory>();
            _databaseMock = new Mock<IDatabase>();
            _databaseMock.SetupGet(database => database.copy).Returns(_dbSetMock.Object);
            _quotationService = new QuotationService(_copyFactory.Object, _databaseMock.Object);
        }

        [Fact]
        public async void CalculatePrice_Success_Novel()
        {
            var copyEntity = new NovelEntity()
            {
                Name = "Novela",
                Author = "AutorNovela",
                Price = 20,
                Discount = 0,
                TotalPrice = 20,
            };

            var copy = new Copy()
            {
                Name = "Novela",
                Author = "AutorNovela",
                Price = 40,
                Type = CopyType.NOVEL,
            };

            var copyDTO = new CopyDTO()
            {
                Name = "Novela",
                Author = "AutorNovela",
                Price = 20,
                Type = CopyType.NOVEL
            };

            _copyFactory
                .Setup(factory => factory.Create(It.IsAny<CopyDTO>()))
                .Returns(copyEntity);

            _databaseMock
                .Setup(database => database.SaveAsync())
                .Returns(Task.FromResult(true));

            var result = await _quotationService.CalculatePrice(copyDTO);

            Assert.NotNull(result);
            Assert.Equivalent(copy, result);

            _databaseMock.Verify(database => database.copy.AddAsync(It.IsAny<Copy>(), default), Times.Once);
            _databaseMock.Verify(database => database.SaveAsync(), Times.Once);
            _copyFactory.Verify(factory => factory.Create(It.IsAny<CopyDTO>()), Times.Once);
        }

        [Fact]
        public async void CalculatePrice_Success_Book()
        {
            var copyEntity = new BookEntity()
            {
                Name = "Book",
                Author = "AutorBook",
                Price = 20,
                Discount = 0,
                TotalPrice = 20,
            };

            var copy = new Copy()
            {
                Name = "Book",
                Author = "AutorBook",
                Price = 20f * (4f / 3f),
                Type = CopyType.BOOK,
            };

            var copyDTO = new CopyDTO()
            {
                Name = "Book",
                Author = "AutorBook",
                Price = 20,
                Type = CopyType.BOOK
            };

            _copyFactory
                .Setup(factory => factory.Create(It.IsAny<CopyDTO>()))
                .Returns((copyEntity));

            _databaseMock
                .Setup(database => database.SaveAsync())
                .Returns(Task.FromResult(true));

            var result = await _quotationService.CalculatePrice(copyDTO);

            Assert.NotNull(result);
            Assert.Equivalent(copy, result);

            _databaseMock.Verify(database => database.copy.AddAsync(It.IsAny<Copy>(), default), Times.Once);
            _databaseMock.Verify(database => database.SaveAsync(), Times.Once);
            _copyFactory.Verify(factory => factory.Create(It.IsAny<CopyDTO>()), Times.Once);
        }

        [Fact]
        public async void CalculatePrice_ReturnsNullOnDatabaseSaveFailure()
        {
            var copyDTO = new CopyDTO
            {
                Name = "Test",
                Author = "Test",
                Price = 10,
                Type = CopyType.NOVEL
            };

            _copyFactory.Setup(f => f.Create(It.IsAny<CopyDTO>())).Returns(new NovelEntity());
            _databaseMock.Setup(d => d.SaveAsync()).Returns(Task.FromResult(false));

            var result = await _quotationService.CalculatePrice(copyDTO);

            Assert.Null(result);
        }

        [Fact]
        public async void CalculatePrice_SetsTypeBasedOnCopyDTO()
        {
            var copyDTO = new CopyDTO
            {
                Name = "Test",
                Author = "Test",
                Price = 10,
                Type = CopyType.NOVEL
            };

            _copyFactory.Setup(f => f.Create(It.IsAny<CopyDTO>())).Returns(new NovelEntity());
            _databaseMock.Setup(d => d.SaveAsync()).Returns(Task.FromResult(true));

            var result = await _quotationService.CalculatePrice(copyDTO);

            Assert.NotNull(result);
            Assert.Equal(copyDTO.Type, result.Type);
        }
    }
}