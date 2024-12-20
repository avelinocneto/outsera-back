namespace outsera_back.Models;

/// <summary>
/// Modelo de resposta com anos com múltiplos vencedores.
/// </summary>
public class YearsWithMultipleWinnersResponseModel {
    
    /// <summary>
    /// Anos com múltiplos vencedores.
    /// </summary>
    public IEnumerable<YearWinnerCountModel> Years { get; set; } = [];
}