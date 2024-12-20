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
                        Producer = "Joel Silver",
                        Interval = 1,
                        PreviousWin = 1990,
                        FollowingWin = 1991
                    }
                ],
                Max =
                [
                    new ProducerInfoModel
                    {
                        Producer = "Matthew Vaughn",
                        Interval = 13,
                        PreviousWin = 2002,
                        FollowingWin = 2015
                    }
                ]
            };

            _mockMoviePrizeService.Setup(service => service.GetPrizeIntervalInfo()).Returns(expectedData);
            var result = _controller.GetPrizeIntervalInfo();
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedData, okResult.Value);
        }

        /// <summary>
        /// Testa o método <see cref="MoviesController.GetPrizeIntervalInfo"/> esperando OK como resultado.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetTop3Studios_ReturnsOk()
        {
            var expectedData = new StudiosWinsResponseModel()
            {
                Studios = [
                    new StudioWinCountModel{
                        Name = "Studio A",
                        WinCount = 10
                    },
                    new StudioWinCountModel{
                        Name = "Studio B",
                        WinCount = 5
                    }
                ]
            };

            _mockMoviePrizeService.Setup(service => service.GetTop3Studios()).Returns(expectedData);
            var result = _controller.GetPrizeIntervalInfo(projection: "top3-studios");
            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(expectedData, okResult.Value);
            Assert.NotNull(result);
            Assert.Equal(2, ((StudiosWinsResponseModel)okResult.Value).Studios.Count());
        }


        /// <summary>
        /// Testa o método <see cref="MoviesController.GetPrizeIntervalInfo"/> esperando OK como resultado.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetYearsWithMultipleWinners_ReturnsOk()
        {
            var expectedData = new YearsWithMultipleWinnersResponseModel()
            {
                Years = [
                    new YearWinnerCountModel{
                        Year = 1986,
                        WinnerCount = 2
                    }
                ]
            };

            _mockMoviePrizeService.Setup(service => service.GetYearsWithMultipleWinners()).Returns(expectedData);
            var result = _controller.GetPrizeIntervalInfo(projection: "years-with-multiple-winners");
            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(expectedData, okResult.Value);
            Assert.Single(((YearsWithMultipleWinnersResponseModel)okResult.Value).Years);
            Assert.Equal(1986, ((YearsWithMultipleWinnersResponseModel)okResult.Value).Years.First().Year);
        }


        /// <summary>
        /// Testa o método <see cref="MoviesController.GetPrizeIntervalInfo"/> esperando OK como resultado.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetMoviesByYearAndWinner_ReturnsOk()
        {
            var result = _controller.GetPrizeIntervalInfo(year: 2018, winner: true);
            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(result);
            Assert.Equal(2018, ((IEnumerable<MovieModel>)okResult.Value).FirstOrDefault().Year);
            Assert.True(((IEnumerable<MovieModel>)okResult.Value).FirstOrDefault().Winner);
        }

        /// <summary>
        /// Testa o método <see cref="MoviesController.GetPrizeIntervalInfo"/> esperando OK como resultado.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetMovies_ReturnsOk()
        {
            var result = _controller.GetPrizeIntervalInfo(page: 0, size: 2);
            var okResult = Assert.IsType<OkObjectResult>(result);
            
            Assert.NotNull(result);
            Assert.Equal(2, ((MoviesResponseModel)okResult.Value).Content.Count());
            Assert.Equal(2, ((MoviesResponseModel)okResult.Value).TotalElements);
        }
    }
}