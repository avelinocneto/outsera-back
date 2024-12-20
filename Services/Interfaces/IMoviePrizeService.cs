using outsera_back.Models;

namespace outsera_back.Services.Interfaces;

/// <summary>
/// Interface de servi�o respons�vel por manipular os dados de pr�mios de filmes.
/// </summary>
public interface IMoviePrizeService
{
    /// <summary>
    /// Obtêm o produtor com maior e menor intervalo entre prêmios consecutivos, e o mais rápido a obter dois prêmios consecutivos./// Obtêm o produtor com maior e menor intervalo entre prêmios consecutivos, e o mais rápido a obter dois prêmios consecutivos.
    /// </summary>
    /// <returns></returns>
    PrizeIntervalInfoResponseModel GetPrizeIntervalInfo();

    /// <summary>
    /// Obtêm os estúdios com maior quantidade de prêmios.
    /// </summary>
    /// <returns></returns>
    StudiosWinsResponseModel GetStudiosWithWinCount();

    /// <summary>
    /// Obtêm os top 3 estúdios com maior quantidade de prêmios.
    /// </summary>
    /// <returns></returns>
    StudiosWinsResponseModel GetTop3Studios();

    /// <summary>
    /// Obtêm os anos com mais de um vencedor.
    /// </summary>
    /// <returns></returns>
    YearsWithMultipleWinnersResponseModel GetYearsWithMultipleWinners();

    /// <summary>
    /// Obtêm os filmes por ano e se é vencedor.
    /// </summary>
    /// <param name="winner"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    IEnumerable<MovieModel> GetMoviesByYearAndWinner(int year, bool winner);

    /// <summary>
    /// Obtêm os filmes por ano e se é vencedor, paginado.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <param name="winner"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    MoviesResponseModel GetMovies(int page, int size, bool? winner = null, int? year = null);

}