namespace outsera_back.Models;

/// <summary>
/// Modelo de resposta com contagem de vitórias por estúdio.
/// </summary>
public class StudioWinCountModel
{
    /// <summary>
    /// Nome do estúdio.
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Quantidade de vitórias.
    /// </summary>
    public required int WinCount { get; set; }
}