namespace outsera_back.Transformers;

/// <summary>
/// Transformador de parâmetros de saída que converte o valor para minúsculas.
/// </summary>
public class LowercaseParameterTransformer : IOutboundParameterTransformer
{
    /// <summary>
    /// Transforma o valor de entrada para minúsculas.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public string? TransformOutbound(object? value)
    {
        return value?.ToString()?.ToLowerInvariant();
    }
}