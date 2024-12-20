
namespace outsera_back.Models;

/// <summary>
/// Modelo de resposta com informações sobre intervalo de prêmios.
/// </summary>
public class PrizeIntervalInfoResponseModel
{
    /// <summary>
    /// Produtor(es) com menor intervalo entre prêmios.
    /// </summary>
    public IEnumerable<ProducerInfoModel> Min { get; set; } = [];
    
    /// <summary>
    /// Produtor(es) com maior intervalo entre prêmios.
    /// </summary>
    public IEnumerable<ProducerInfoModel> Max { get; set; } = [];
}