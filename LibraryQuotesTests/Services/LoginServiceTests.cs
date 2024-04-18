using LibraryQuotes.Models.DTOS.User;
using LibraryQuotes.Models.Persistence;
using LibraryQuotes.Repository.User;
using LibraryQuotes.Services.Interfaces;
using Moq;
using System.Security.Cryptography;
using System.Text;

namespace LibraryQuotes.Services.Tests
{
    public class LoginServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly ILoginService _loginService;

        public LoginServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loginService = new LoginService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetUser_Success()
        {
            var userDto = new UserDTO 
            { 
                Email = "test@example.com", 
                Password = "password" 
            };
            var hashedPassword = HashPassword(userDto.Password);
            var user = new Users 
            { 
                Email = userDto.Email, 
                Password = hashedPassword 
            };

            _userRepositoryMock.Setup(repo => repo.VerifyAuthentication(userDto.Email, hashedPassword)).ReturnsAsync(user);

            var result = await _loginService.GetUser(userDto);

            Assert.NotNull(result);
            Assert.Equal(user.Email, result?.Email);

            _userRepositoryMock.Verify(x => x.VerifyAuthentication(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetUser_Password_Incorrect()
        {
            var userDto = new UserDTO
            {
                Email = "test@example.com",
                Password = "password"
            };
            var hashedPassword = HashPassword("123123");
            var user = new Users
            {
                Email = userDto.Email,
                Password = hashedPassword
            };

            _userRepositoryMock.Setup(repo => repo.VerifyAuthentication(userDto.Email, hashedPassword)).ReturnsAsync(user);

            var result = await _loginService.GetUser(userDto);

            Assert.Null(result);

            _userRepositoryMock.Verify(x => x.VerifyAuthentication(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetUser_Email_Incorrect()
        {
            var userDto = new UserDTO
            {
                Email = "test@example.com",
                Password = "password"
            };
            var hashedPassword = HashPassword("123123");
            var user = new Users
            {
                Email = "carlito@test.com",
                Password = hashedPassword
            };

            _userRepositoryMock.Setup(repo => repo.VerifyAuthentication(userDto.Email, hashedPassword)).ReturnsAsync(user);

            var result = await _loginService.GetUser(userDto);

            Assert.Null(result);

            _userRepositoryMock.Verify(x => x.VerifyAuthentication(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetUser_Invalid_User_Returns_Null()
        {
            var userDto = new UserDTO
            {
                Email = "test@example.com",
                Password = "password"
            };

            _userRepositoryMock.Setup(repo => repo.VerifyAuthentication(userDto.Email, It.IsAny<string>())).ReturnsAsync((Users)null);

            var result = await _loginService.GetUser(userDto);

            Assert.Null(result);

            _userRepositoryMock.Verify(x => x.VerifyAuthentication(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}