using Microsoft.AspNetCore.Mvc;
using Moq;
using outsera_back.Controllers;
using outsera_back.Models;
using outsera_back.Services.Interfaces;
using Xunit;

namespace outsera_back.IntegrationTests
{
    /// <summary>
    /// Testes de integra��o para <see cref="MoviesController"/>.
    /// </summary>
    public class MoviesControllerTest
    {
        private readonly Mock<IMoviePrizeService> _mockMoviePrizeService;
        private readonly MoviesController _controller;

        /// <summary>
        /// ctor de <see cref="MoviesControllerTest"/>.
        /// </summary>
        public MoviesControllerTest()
        {
            _mockMoviePrizeService = new Mock<IMoviePrizeService>();
            _controller = new MoviesController(_mockMoviePrizeService.Object);
        }

        /// <summary>
        /// Testa o m�todo <see cref="MoviesController.GetPrizeIntervalInfo"/> esperando OK como resultado.
        /// </summary>
        [Fact]
        public void GetPrizeIntervalInfo_ReturnsOkResult_WithExpectedData()
        {
            var expectedData = new PrizeIntervalInfoResponseModel
            {
                Min =
                [
                    new ProducerInfoModel
                    {
                        Producer = "Producer A",
                        Interval = 1,
                        PreviousWin = 2000,
                        FollowingWin = 2001
                    }
                ],
                Max =
                [
                    new ProducerInfoModel
                    {
                        Producer = "Producer B",
                        Interval = 10,
                        PreviousWin = 1990,
                        FollowingWin = 2000
                    }
                ]
            };

            _mockMoviePrizeService.Setup(service => service.GetPrizeIntervalInfo()).Returns(expectedData);
            var result = _controller.GetPrizeIntervalInfo();
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedData, okResult.Value);
        }

        /// <summary>
        /// Testa o m�todo <see cref="MoviesController.GetPrizeIntervalInfo"/> esperando um BadRequest como resultado.
        /// </summary>
        [Fact]
        public void GetPrizeIntervalInfo_ReturnsBadRequest_OnServiceError()
        {
            _mockMoviePrizeService.Setup(service => service.GetPrizeIntervalInfo()).Throws(new Exception());
            var result = _controller.GetPrizeIntervalInfo();
            Assert.IsType<BadRequestResult>(result);
        }
    }
}