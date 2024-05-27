using LibraryQuotes.Models.DataBase.Interfaces;
using LibraryQuotes.Models.DTOS.Quoation;
using LibraryQuotes.Models.DTOS.User;
using LibraryQuotes.Repository.User;
using LibraryQuotes.Services.Interfaces;
using Moq;

namespace LibraryQuotes.Services.Tests
{
    public class RegisterServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IDatabase> _databaseMock;
        private readonly IRegisterService _registerService;

        public RegisterServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _databaseMock = new Mock<IDatabase>();
            _registerService = new RegisterService(_userRepositoryMock.Object, _databaseMock.Object);
        }

        [Fact]
        public async Task RegisterUser_Successful_Registration()
        {
            var userDto = new UserRegisterDTO
            {
                Name = "TestUser",
                Email = "test@example.com",
                Password = "password"
            };

            _userRepositoryMock.Setup(repo => repo.EmailAlreadyRegistered(userDto.Email)).ReturnsAsync(false);
            _databaseMock.Setup(database => database.SaveAsync()).Returns(Task.FromResult(true));

            var result = await _registerService.RegisterUser(userDto);

            Assert.NotNull(result);
            Assert.Equal(userDto.Email, result?.Email);

            _userRepositoryMock.Verify(repo => repo.EmailAlreadyRegistered(It.IsAny<string>()), Times.Once);
            _databaseMock.Verify(database => database.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task RegisterUser_Email_Already_Registered()
        {
            var userDto = new UserRegisterDTO
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "password"
            };

            _userRepositoryMock.Setup(repo => repo.EmailAlreadyRegistered(userDto.Email)).ReturnsAsync(true);

            var result = await _registerService.RegisterUser(userDto);

            Assert.Null(result);

            _userRepositoryMock.Verify(repo => repo.EmailAlreadyRegistered(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task RegisterUser_SaveAsync_Failure()
        {
            var userDto = new UserRegisterDTO
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "password"
            };

            _userRepositoryMock.Setup(repo => repo.EmailAlreadyRegistered(userDto.Email)).ReturnsAsync(false);
            _databaseMock.Setup(db => db.SaveAsync()).ReturnsAsync(false);

            var result = await _registerService.RegisterUser(userDto);

            Assert.Null(result);

            _userRepositoryMock.Verify(repo => repo.EmailAlreadyRegistered(It.IsAny<string>()), Times.Once);
            _databaseMock.Verify(database => database.SaveAsync(), Times.Once);
        }
    }
}