namespace outsera_back.Models;

/// <summary>
/// Modelo de resposta com contagem de vencedores por ano.
/// </summary>
public class YearWinnerCountModel {
    /// <summary>
    /// Ano do prêmio.
    /// </summary>
    public int Year { get; set; }
    
    /// <summary>
    /// Quantidade de vencedores.
    /// </summary>
    public int WinnerCount { get; set; }
}