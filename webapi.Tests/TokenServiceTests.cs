using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using webapi.Service; 
using webapi.Model;   
using webapi.Interfaces;

public class TokenServiceTests
{
    private readonly ITokenService _tokenService;
    private readonly Mock<IConfiguration> _configMock;
    
    public TokenServiceTests()
    {
        // Set up mock configuration
        _configMock = new Mock<IConfiguration>();

        // Mocking the configuration values
        _configMock.Setup(c => c["JWT:SigningKey"]).Returns("SigningKeySigningKeySigningKeySigningKeySigningKeySigningKeySigningKeySigningKeySigningKeySigningKeySigningKeySigningKeySigningKeySigningKeySigningKeySigningKey");
        _configMock.Setup(c => c["JWT:Issuer"]).Returns("YourIssuer");
        _configMock.Setup(c => c["JWT:Audience"]).Returns("YourAudience");

        // Initialize the TokenService with the mocked configuration
        _tokenService = new TokenService(_configMock.Object);
    }


    [Fact]
    public void CreateToken_ShouldReturnValidToken()
    {
        // Arrange
        var user = new AppUser
        {
            Email = "test@example.com",
            UserName = "testuser"
        };

        // Act
        var token = _tokenService.CreateToken(user);

        // Assert
        Assert.NotNull(token); // Ensure the token is not null
        Assert.IsType<string>(token); // Ensure the token is of type string

        var tokenHandler = new JwtSecurityTokenHandler();
        
        // Validate the token format
        Assert.True(tokenHandler.CanReadToken(token));

        var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        // Ensure the token was successfully parsed
        Assert.NotNull(securityToken);

        // Validate the token issuer
        Assert.Equal("YourIssuer", securityToken.Issuer);

        // Validate the token audience (check all audiences if multiple)
        Assert.Contains("YourAudience", securityToken.Audiences);

        // Validate the claims
        Assert.Contains(securityToken.Claims, c => c.Type == JwtRegisteredClaimNames.Email && c.Value == "test@example.com");
        Assert.Contains(securityToken.Claims, c => c.Type == JwtRegisteredClaimNames.GivenName && c.Value == "testuser");
    }

}
