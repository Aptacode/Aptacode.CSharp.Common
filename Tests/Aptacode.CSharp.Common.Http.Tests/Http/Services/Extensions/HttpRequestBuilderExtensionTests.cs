using System;
using System.Net.Http;
using System.Text;
using Aptacode.CSharp.Common.Http.Interfaces;
using Aptacode.CSharp.Common.Http.Services.Extensions;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Aptacode.CSharp.Common.Http.Tests.Http.Services.Extensions
{
    public class HttpRequestBuilderExtensionTests
    {
        [Fact]
        public void AddAuthentication()
        {
            //Arrange
            var sut = new HttpRequestMessage();
            var mockTokenService = new Mock<IAccessTokenService>();
            const string expectedToken = "TestToken";
            mockTokenService.Setup(m => m.AccessToken).Returns(expectedToken);

            //Act
            sut.AddAuthentication(mockTokenService.Object);

            //Assert
            Assert.Equal("Bearer", sut.Headers.Authorization.Scheme);
            Assert.Equal(expectedToken, sut.Headers.Authorization.Parameter);
        }

        [Fact]
        public void AddContent()
        {
            //Arrange
            var sut = new HttpRequestMessage();

            var testObject = ("Test", "Object");
            HttpContent expectedContent = new StringContent(JsonConvert.SerializeObject(testObject), Encoding.UTF8,
                MimeTypes.MimeTypes.Application.Json.ToString());
            //Act
            sut.AddContent(testObject);

            //Assert
            Assert.Matches(expectedContent.ReadAsStringAsync().Result, sut.Content.ReadAsStringAsync().Result);
        }

        [Fact]
        public void CanSetMethod()
        {
            //Arrange
            var sut = new HttpRequestMessage {Method = HttpMethod.Get};

            //Act
            sut.SetMethod(HttpMethod.Post);

            //Assert
            Assert.Equal(HttpMethod.Post, sut.Method);
        }

        [Fact]
        public void SetRoute()
        {
            //Arrange
            var sut = new HttpRequestMessage {RequestUri = new Uri("https://localhost/old")};
            const string expectedRoute = "https://localhost/new";

            //Act
            sut.SetRoute(expectedRoute);

            //Assert
            Assert.Equal(expectedRoute, sut.RequestUri.ToString());
        }
    }
}