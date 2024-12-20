namespace outsera_back.Models;

/// <summary>
/// Modelo de resposta para ordenação.
/// </summary>
public class SortModel
{
    /// <summary>
    /// Se a ordenação é ordenada.
    /// </summary>
    public bool Sorted { get; set; }
    
    /// <summary>
    /// Se a ordenação é desordenada.
    /// </summary>
    public bool Unsorted { get; set; }
}