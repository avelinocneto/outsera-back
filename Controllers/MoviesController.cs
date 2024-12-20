using Microsoft.AspNetCore.Mvc;
using outsera_back.Models;
using outsera_back.Services.Interfaces;
using System.Diagnostics.Eventing.Reader;

namespace outsera_back.Controllers;

/// <summary>
/// Controller responsável por expor os endpoints relacionados a filmes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MoviesController : ControllerBase
{
    private readonly IMoviePrizeService _moviePrizeService;
    
    /// <summary>
    /// ctor de <see cref="MoviesController"/>.
    /// </summary>
    /// <param name="moviePrizeService"></param>
    public MoviesController(IMoviePrizeService moviePrizeService)
    {
        _moviePrizeService = moviePrizeService;
    }

    /// <summary>
    /// Obtêm o produtor com maior e menor intervalo entre prêmios consecutivos, e o mais rápido a obter dois prêmios consecutivos.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <param name="winner"></param>
    /// <param name="year"></param>
    /// <param name="projection">Parâmetro de consulta opcional para projeção.</param>
    /// <returns>
    /// Um objeto JSON contendo os produtores e os intervalos entre os prêmios, com a estrutura especificada.
    /// </returns>
    /// <response code="200">Retorna os produtores com os maiores e menores intervalos entre prêmios</response>
    /// <response code="400">Se houver erro na requisição</response>
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PrizeIntervalInfoResponseModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetPrizeIntervalInfo(
        [FromQuery] int? page = null, 
        [FromQuery] int? size = null, 
        [FromQuery] bool? winner = null, 
        [FromQuery] int? year = null, 
        [FromQuery] string? projection = null
    ) {
        try
        {
            if (!string.IsNullOrWhiteSpace(projection))
            {
                switch (projection) {
                    case "max-min-win-interval-for-producers": {
                            var result = _moviePrizeService.GetPrizeIntervalInfo();
                            return Ok(result);
                        }
                    case "studios-with-win-count": {
                            var result = _moviePrizeService.GetStudiosWithWinCount();
                            return Ok(result);
                        }
                    case "top3-studios": {
                            var result = _moviePrizeService.GetTop3Studios();
                            return Ok(result);
                        }
                    case "years-with-multiple-winners": {
                            var result = _moviePrizeService.GetYearsWithMultipleWinners();
                            return Ok(result);
                        }
                    default: {
                            return BadRequest("Invalid projection parameter.");
                        }
                }
            }

            if (year.HasValue && !(page.HasValue && size.HasValue))
            {
                var result = _moviePrizeService.GetMoviesByYearAndWinner(year.Value, winner ?? true);
                return Ok(result);
            }

            if (page.HasValue && size.HasValue)
            {
                var result = _moviePrizeService.GetMovies(page.Value, size.Value, winner, year);
                return Ok(result);
            }

            return BadRequest("Invalid query parameters.");
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}
