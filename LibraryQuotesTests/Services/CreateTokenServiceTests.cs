using LibraryQuotes.Models.DTOS.User;
using LibraryQuotes.Models.Persistence;
using LibraryQuotes.Repository.User;
using LibraryQuotes.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LibraryQuotes.Services.Tests
{
    public class CreateTokenServiceTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly ICreateTokenService _createTokenService;

        public CreateTokenServiceTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _createTokenService = new CreateTokenService(_configurationMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public async Task GenerateToken_Success()
        {
            var user = new Users()
            {
                UserId = 1,
                Name = "Test User",
                Email = "test@example.com"
            };

            var userDTO = new UserDTO()
            {
                Email = "test@example.com",
                Password = "password"
            };

            _configurationMock.Setup(x => x.GetSection("JWT:Key").Value).Returns("AKLSJDKLASJFIJKASLJKLASDJKL92783189OWJQFIOioqweuqiweuASJD2134128421jsajdhkasHASJKDHJA");
            _userRepositoryMock.Setup(repo => repo.GetByEmail(It.IsAny<string>())).ReturnsAsync(user);

            var result = await _createTokenService.GenerateToken(userDTO);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(result);

            Assert.Equal("1", decodedToken.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value);
            Assert.Equal("Test User", decodedToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value);

            _configurationMock.Verify(x => x.GetSection("JWT:Key").Value, Times.Once);
            _userRepositoryMock.Verify(x => x.GetByEmail(It.IsAny<string>()), Times.Once);
        }
    }
}