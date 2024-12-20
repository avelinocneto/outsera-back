namespace outsera_back.Models;

/// <summary>
/// Modelo de resposta com informações de um produtor.
/// </summary>
public class ProducerInfoModel
{
    /// <summary>
    /// Nome do Produtor.
    /// </summary>
    public required string Producer { get; set; }
    
    /// <summary>
    /// Intervalo entre prêmios.
    /// </summary>
    public int Interval { get; set; }
    
    /// <summary>
    /// Ano do prêmio anterior.
    /// </summary>
    public int PreviousWin { get; set; }
    
    /// <summary>
    /// Ano do prêmio seguinte.
    /// </summary>
    public int FollowingWin { get; set; }

    /// <summary>
    /// Informativo sobre o prêmio/produtor.
    /// </summary>
    public string Descripton { get; set; } = "";
}