namespace outsera_back.Models;

/// <summary>
/// Modelo de resposta com contagem de vitórias por estúdio.
/// </summary>
public class StudiosWinsResponseModel
{
    /// <summary>
    /// Contagem de vitórias por estúdio.
    /// </summary>
    public IEnumerable<StudioWinCountModel> Studios { get; set; } = [];
}

