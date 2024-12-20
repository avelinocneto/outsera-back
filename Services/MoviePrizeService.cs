using outsera_back.Context;
using outsera_back.Entities;
using outsera_back.Models;
using outsera_back.Services.Interfaces;
using System.Text.RegularExpressions;

namespace outsera_back.Services;

/// <summary>
/// Serviço responsável por manipular os dados de prêmios de filmes.
/// </summary>
public class MoviePrizeService : IMoviePrizeService
{
    private readonly MoviePrizeContext _context;
    
    /// <summary>
    /// ctor de <see cref="MoviePrizeService"/>.
    /// </summary>
    /// <param name="context"></param>
    public MoviePrizeService(MoviePrizeContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtêm o produtor com maior e menor intervalo entre prêmios consecutivos, e o mais rápido a obter dois prêmios consecutivos.
    /// </summary>
    /// <returns></returns>
    public PrizeIntervalInfoResponseModel GetPrizeIntervalInfo()
    {
        var moviePrizes = _context.MoviePrizes.Where(mp => mp.Winner).ToList();
        if (moviePrizes.Count == 0)
        {
            return new PrizeIntervalInfoResponseModel
            {
                Min = [],
                Max = []
            };
        }

        var producerIntervals = new List<(string Producer, int Interval, int PreviousWin, int FollowingWin)>();
        foreach (var moviePrize in moviePrizes)
        {
            var producers = SplitProducers(moviePrize.Producers);

            foreach (var producer in producers)
            {
                var producerPrizes = moviePrizes
                    .Where(mp => SplitProducers(mp.Producers).Contains(producer))
                    .OrderBy(mp => mp.Year)
                    .ToList();

                for (int i = 0; i < producerPrizes.Count - 1; i++)
                {
                    var interval = producerPrizes[i + 1].Year - producerPrizes[i].Year;
                    producerIntervals.Add((producer, interval, producerPrizes[i].Year, producerPrizes[i + 1].Year));
                }
            }
        }

        var minInterval = producerIntervals
            .Where(i => i.Interval > 0)
            .OrderBy(i => i.Interval)
            .ThenBy(i => i.PreviousWin)
            .FirstOrDefault();

        var maxInterval = producerIntervals
            .Where(i => i.Interval > 0)
            .OrderByDescending(i => i.Interval)
            .ThenBy(i => i.PreviousWin)
            .FirstOrDefault();
        
        return new PrizeIntervalInfoResponseModel
        {
            Min = !minInterval.Equals(default) ?
            [
                new ProducerInfoModel
                {
                    Producer = minInterval.Producer,
                    Interval = minInterval.Interval,
                    PreviousWin = minInterval.PreviousWin,
                    FollowingWin = minInterval.FollowingWin
                }
            ] : [],
            Max = !maxInterval.Equals(default) ? 
            [
                new ProducerInfoModel
                {
                    Producer = maxInterval.Producer,
                    Interval = maxInterval.Interval,
                    PreviousWin = maxInterval.PreviousWin,
                    FollowingWin = maxInterval.FollowingWin
                }
            ] : []
        };
    }

    /// <summary>
    /// Divide os produtores de um filme.
    /// </summary>
    /// <param name="producers"></param>
    /// <returns></returns>
    private List<string> SplitProducers(string producers)
    {
        var result = new List<string>();    
        var parts = Regex.Split(producers, @",\s*|\s+and\s+");

        foreach (var part in parts)
        {
            if (!string.IsNullOrWhiteSpace(part))
            {
                result.Add(part.Trim());
            }
        }

        return result;
    }


    /// <summary>
    /// Obtêm os estúdios com maior quantidade de prêmios.
    /// </summary>
    /// <returns></returns>
    public StudiosWinsResponseModel GetStudiosWithWinCount()
    {
        var moviePrizes = _context.MoviePrizes.Where(mp => mp.Winner).ToList();
        if (!moviePrizes.Any()) return new StudiosWinsResponseModel() { Studios = [] };

        var studios = moviePrizes
            .SelectMany(mp => mp.Studio.Split(", "), (mp, studio) => new { Studio = studio.Trim(), mp })
            .GroupBy(m => m.Studio)
            .Select(g => new StudioWinCountModel()
            {
                Name = g.Key,
                WinCount = g.Count()
            })
            .ToList();
        
        return new StudiosWinsResponseModel
        {
            Studios = studios
        } ?? new StudiosWinsResponseModel(){
            Studios = []
        };
    }

    /// <summary>
    /// Obtêm os top 3 estúdios com maior quantidade de prêmios.
    /// </summary>
    /// <returns></returns>
    public StudiosWinsResponseModel GetTop3Studios()
    {
        var moviePrizes = _context.MoviePrizes.Where(mp => mp.Winner).ToList();
        if (!moviePrizes.Any()) return new StudiosWinsResponseModel() { Studios = [] };

        var studios = moviePrizes
            .SelectMany(mp => mp.Studio.Split(", "), (mp, studio) => new { Studio = studio.Trim(), mp })
            .GroupBy(m => m.Studio)
            .Select(g => new StudioWinCountModel()
            {
                Name = g.Key,
                WinCount = g.Count()
            })
            .OrderByDescending(s => s.WinCount)
            .Take(3)
            .ToList();

        return new StudiosWinsResponseModel
        {
            Studios = studios
        } ?? new StudiosWinsResponseModel()
        {
            Studios = []
        };
    }

    /// <summary>
    /// Obtêm os anos com mais de um vencedor.
    /// </summary>
    /// <returns></returns>
    public YearsWithMultipleWinnersResponseModel GetYearsWithMultipleWinners() {
        var moviePrizes = _context.MoviePrizes.Where(mp => mp.Winner).ToList();
        if (!moviePrizes.Any()) return new YearsWithMultipleWinnersResponseModel() { Years = [] };

        var years = moviePrizes.GroupBy(m => m.Year).Select(g => new YearWinnerCountModel()
        {
            Year = g.Key,
            WinnerCount = g.Count()
        }).Where(y => y.WinnerCount > 1).ToList();

        return new YearsWithMultipleWinnersResponseModel
        {
            Years = years
        } ?? new YearsWithMultipleWinnersResponseModel(){
            Years = []
        };
    }

    /// <summary>
    /// Obtêm os filmes.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <param name="winner"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    public MoviesResponseModel GetMovies(int page, int size, bool? winner = null, int? year = null)
    {
        var query = _context.MoviePrizes.AsQueryable();
        if (winner.HasValue) query = query.Where(mp => mp.Winner == winner.Value);
        if (year.HasValue) query = query.Where(mp => mp.Year == year.Value);

        var totalElements = query.Count();
        var totalPages = (int)Math.Ceiling(totalElements / (double)size);

        var moviePrizes = query
            .OrderBy(mp => mp.Year)
            .Skip(page * size)
            .Take(size)
            .ToList();

        var movies = moviePrizes
            .GroupBy(mp => new { mp.Id, mp.Year, mp.Title, mp.Winner })
            .Select(g => new MovieModel
            {
                Id = g.Key.Id,
                Year = g.Key.Year,
                Title = g.Key.Title,
                Studios = g.SelectMany(mp => mp.Studio.Split(", ")).Distinct().ToArray(),
                Producers = [.. SplitProducers(string.Join(", ", g.Select(mp => mp.Producers)))],
                Winner = g.Key.Winner
            })
            .OrderBy(m => m.Year)
            .ToList();

        return new MoviesResponseModel
        {
            Content = movies,
            Pageable = new PageableModel
            {
                Sort = new SortModel
                {
                    Sorted = false,
                    Unsorted = true
                },
                PageSize = size,
                PageNumber = page,
                Offset = page * size,
                Paged = true,
                Unpaged = false
            },
            TotalElements = totalElements,
            Last = page == totalPages - 1,
            TotalPages = totalPages,
            First = page == 0,
            Sort = new SortModel
            {
                Sorted = false,
                Unsorted = true
            },
            Number = page,
            NumberOfElements = movies.Count(),
            Size = size
        };
    }

    /// <summary>
    /// Obtêm um filme por ano e vencedor.
    /// </summary>
    /// <param name="year">Filtrar por ano.</param>
    /// <param name="winner">Se é vencedor.</param>
    /// <returns>Dados do filme no formato especificado.</returns>
    public IEnumerable<MovieModel> GetMoviesByYearAndWinner(int year, bool winner)
    {
        var query = _context.MoviePrizes
            .Where(mp => mp.Year == year && mp.Winner == winner)
            .OrderBy(mp => mp.Year)
            .ToList();

        if (query != null && query.Count != 0)
        {
            var movieModels = query.Select(mp => new MovieModel
            {
                Id = mp.Id,
                Year = mp.Year,
                Title = mp.Title,
                Studios = mp.Studio?.Split(", ") ?? [], 
                Producers = [.. SplitProducers(string.Join(", ", mp.Producers))],
                Winner = mp.Winner
            }).ToList();

            return movieModels;
        }

        return [];
    }
}