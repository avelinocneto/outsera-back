namespace outsera_back.Models
{
    /// <summary>
    /// Modelo de filme.
    /// </summary>
    public class MovieModel
    {
        /// <summary>
        /// Identificador do filme.
        /// </summary>
        public int Id { get; set; } = 0;
        
        /// <summary>
        /// Ano do filme.
        /// </summary>
        public int Year { get; set; } = 0;
        
        /// <summary>
        /// Título do filme.
        /// </summary>
        public string Title { get; set; } = "";
        
        /// <summary>
        /// Estúdio associados ao filme.
        /// </summary>
        public string[] Studios { get; set; } = [];
        
        /// <summary>
        /// Produtor associados ao filme.
        /// </summary>
        public string[] Producers { get; set; } = [];
        
        /// <summary>
        /// Indica se o filme foi vencedor.
        /// </summary>
        public bool Winner { get; set; } = false;
    }
}