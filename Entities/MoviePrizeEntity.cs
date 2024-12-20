namespace outsera_back.Entities;
/// <summary>
/// Representa a premiação de um filme.
/// </summary>
public class MoviePrize
{
    /// <summary>
    /// ID da premiação.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Ano da Premiação.
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Título do filme.
    /// </summary>
    public string Title { get; set; } = "";

    /// <summary>
    /// Estúdio associados ao filme.
    /// </summary>
    public string Studio { get; set; } = "";

    /// <summary>
    /// Produtores associados ao filme.
    /// </summary>
    public string Producers { get; set; } = "";

    /// <summary>
    /// Indica se o filme foi vencedor.
    /// </summary>
    public bool Winner { get; set; } = false;
}