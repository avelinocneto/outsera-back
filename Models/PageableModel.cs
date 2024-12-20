namespace outsera_back.Models;

/// <summary>
/// Modelo de resposta para paginação.
/// </summary>
public class PageableModel
{
    /// <summary>
    /// Modelo de ordenação.
    /// </summary>
    public SortModel Sort { get; set; } = new SortModel();
    
    /// <summary>
    /// Tamanho da página.
    /// </summary>
    public int PageSize { get; set; } = 0;
    
    /// <summary>
    /// Número da página.
    /// </summary>
    public int PageNumber { get; set; } = 0;
    
    /// <summary>
    /// Quantidade total de elementos.
    /// </summary>
    public int Offset { get; set; } = 0;
    
    /// <summary>
    /// Se a página é paginada.
    /// </summary>
    public bool Paged { get; set; } = false;
    
    /// <summary>
    /// Se a página é despaginada.
    /// </summary>
    public bool Unpaged { get; set; } = false;
}