using Microsoft.EntityFrameworkCore;
using outsera_back.Entities;

namespace outsera_back.Context;

/// <summary>
/// Contexto do banco de dados para prêmios de filmes.
/// </summary>
public class MoviePrizeContext : DbContext
{
    /// <summary>
    /// ctor de <see cref="MoviePrizeContext"/>.
    /// </summary>
    /// <param name="options"></param>
    public MoviePrizeContext(DbContextOptions<MoviePrizeContext> options) : base(options) { }

    /// <summary>
    /// Entidade de prêmios de filmes.
    /// </summary>
    public DbSet<MoviePrize> MoviePrizes { get; set; }
}