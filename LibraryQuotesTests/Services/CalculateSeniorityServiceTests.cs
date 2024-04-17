using LibraryQuotes.Models.DataBase.Interfaces;
using LibraryQuotes.Models.Persistence;
using LibraryQuotes.Services.Interfaces;
using Moq;

namespace LibraryQuotes.Services.Tests
{
    public class CalculateSeniorityServiceTests
    {
        private readonly ICalculateSeniorityService _calculateSeniorityService;
        private readonly Mock<IDatabase> _databaseMock;

        public CalculateSeniorityServiceTests()
        {
            _databaseMock = new Mock<IDatabase>();
            _calculateSeniorityService = new CalculateSeniorityService(_databaseMock.Object);
        }

        [Fact]
        public async void GetSeniority_Success()
        {
            string userId = "1";

            var user = new Users()
            {
                UserId = 1,
                Name = "Sebas",
                Email = "sebas@gmail.com",
                Password = "password",
                CreationDate = new DateOnly(2021, 04, 16)
            };

            _databaseMock.Setup(db => db.users.FindAsync(1)).ReturnsAsync(user);

            var result = _calculateSeniorityService.GetSeniority(userId);

            Assert.NotNull(result);
            Assert.Equal(3, result);
            Assert.IsType<int>(result);

            _databaseMock.Verify(database => database.users.FindAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async void GetSeniority_Fail()
        {
            string userId = "a";

            var user = new Users()
            {
                UserId = 1,
                Name = "Sebas",
                Email = "sebas@gmail.com",
                Password = "password",
                CreationDate = new DateOnly(2021, 04, 16)
            };

            _databaseMock.Setup(db => db.users.FindAsync(1)).ReturnsAsync(user);

            Assert.Throws<ArgumentException>(() => _calculateSeniorityService.GetSeniority(userId));

            _databaseMock.Verify(database => database.users.FindAsync(It.IsAny<int>()), Times.Never);
        }
    }
}