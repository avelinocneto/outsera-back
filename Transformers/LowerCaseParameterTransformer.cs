namespace outsera_back.Transformers;

/// <summary>
/// Transformador de par�metros de sa�da que converte o valor para min�sculas.
/// </summary>
public class LowercaseParameterTransformer : IOutboundParameterTransformer
{
    /// <summary>
    /// Transforma o valor de entrada para min�sculas.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public string? TransformOutbound(object? value)
    {
        return value?.ToString()?.ToLowerInvariant();
    }
}