using Microsoft.EntityFrameworkCore;
using outsera_back.Context;
using outsera_back.Entities;

namespace outsera_back.Helpers;

/// <summary>
/// Importador de dados do arquivo CSV contendo as premiações.
/// </summary>
public class CsvImporter
{
    /// <summary>
    /// Contexto do banco de dados.
    /// </summary>
    private readonly MoviePrizeContext _context;
    
    /// <summary>
    /// ctor de <see cref="CsvImporter"/>.
    /// </summary>
    /// <param name="context"></param>
    public CsvImporter(MoviePrizeContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Importa os dados do arquivo CSV para o banco de dados.
    /// </summary>
    /// <param name="filePath"></param>
    public void ImportCsvData(string filePath)
    {
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();

        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines.Skip(1))
        {
            var columns = line.Split(';');
            
            // Ideal seria criar uma entidade para produtores separada, bem como para o estúdio.
            var movie = new MoviePrize
            {
                Year = int.Parse(columns[0]),
                Title = columns[1],
                Studio = columns[2],
                Producers = columns[3],
                Winner = columns[4] == "yes" || columns[4] == "sim"
            };
            _context.MoviePrizes.Add(movie);
        }
        _context.SaveChanges();
    }
}