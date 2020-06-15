using System.Net.Http;
using Aptacode.CSharp.Common.Http.Interfaces;
using Aptacode.CSharp.Common.Http.Services;
using Moq;
using Xunit;

namespace Aptacode.CSharp.Common.Http.Tests.Http.Services
{
    public class AuthenticatedRequestBuilderTests
    {
        [Fact]
        public void CreateRequest()
        {
            //Arrange
            const string expectedToken = "TestValue";
            var mockAccessTokenService = new Mock<IAccessTokenService>();
            mockAccessTokenService.Setup(a => a.AccessToken).Returns(expectedToken);

            var sut = new AuthenticatedRequestBuilder(mockAccessTokenService.Object);

            //Act
            var requestMessage = sut.CreateRequest(HttpMethod.Post, "https://localhost/test");

            //Assert
            Assert.Equal(HttpMethod.Post, requestMessage.Method);
            Assert.Equal("https://localhost/test", requestMessage.RequestUri.ToString());
            Assert.Equal("Bearer", requestMessage.Headers.Authorization.Scheme);
            Assert.Equal(expectedToken, requestMessage.Headers.Authorization.Parameter);

        }
    }
}