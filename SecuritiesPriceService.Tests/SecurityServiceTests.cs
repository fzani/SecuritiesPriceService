using BNP.SecuritiesPriceService.Models;
using BNP.SecuritiesPriceService.Repositories;
using BNP.SecuritiesPriceService.Services;
using Moq;

namespace BPN.Tests.Services
{
    public class SecurityServiceTests
    {
        private readonly Mock<ISecurityRepository> _mockRepo;
        private readonly HttpClient _httpClient;
        private readonly SecurityService _securityService;

        public SecurityServiceTests()
        {
            _mockRepo = new Mock<ISecurityRepository>();
            _httpClient = new HttpClient();
            _securityService = new SecurityService(_mockRepo.Object, _httpClient);
        }

        [Fact]
        public async Task RetrieveAndStorePricesAsync_ShouldSavePrices()
        {
            // Arrange
            var isins = new List<string> { "US0378331005", "US02079K3059" };
            _mockRepo.Setup(repo => repo.SaveSecurityAsync(It.IsAny<Security>())).Returns(Task.CompletedTask);

            // Act
            await _securityService.RetrieveAndStorePricesAsync(isins);

            // Assert
            _mockRepo.Verify(repo => repo.SaveSecurityAsync(It.IsAny<Security>()), Times.Exactly(isins.Count));
        }
    }
}
