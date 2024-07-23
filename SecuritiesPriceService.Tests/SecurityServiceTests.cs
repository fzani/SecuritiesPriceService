using BNP.SecuritiesPriceService.Models;
using BNP.SecuritiesPriceService.Repositories;
using BNP.SecuritiesPriceService.Services;
using Moq;
using Moq.Contrib.HttpClient;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


namespace YourNamespace.Tests
{
    public class SecurityServiceTests
    {
        private readonly Mock<ISecurityRepository> _mockRepo;
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly SecurityService _securityService;

        public SecurityServiceTests()
        {
            _mockRepo = new Mock<ISecurityRepository>();
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = _mockHttpMessageHandler.CreateClient();
            _httpClient.BaseAddress = new Uri("https://securities.dataprovider.com/");

            _securityService = new SecurityService(_mockRepo.Object, _httpClient);
        }

        [Fact]
        public async Task RetrieveAndStorePricesAsync_ShouldSavePrices()
        {
            // Arrange
            var isins = new List<string> { "US0378331005", "US02079K3059" };
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("123.45")
            };
            _mockHttpMessageHandler.SetupAnyRequest()
                .ReturnsAsync(mockResponse);

            _mockRepo.Setup(repo => repo.SaveSecurityAsync(It.IsAny<Security>())).Returns(Task.CompletedTask);

            // Act
            await _securityService.RetrieveAndStorePricesAsync(isins);

            // Assert
            _mockRepo.Verify(repo => repo.SaveSecurityAsync(It.IsAny<Security>()), Times.Exactly(isins.Count));
        }
    }
}
