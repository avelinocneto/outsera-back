namespace outsera_back.Models
{
    /// <summary>
    /// Modelo de resposta com filmes.
    /// </summary>
    public class MoviesResponseModel
    {
        /// <summary>
        /// Conteúdo da resposta.
        /// </summary>
        public IEnumerable<MovieModel> Content { get; set; } = [];
        
        /// <summary>
        /// Informações de paginação.
        /// </summary>
        public PageableModel Pageable { get; set; } = new PageableModel();
        
        /// <summary>
        /// Número total de elementos.
        /// </summary>
        public int TotalElements { get; set; } = 0;
        
        /// <summary>
        /// Se é a última página.
        /// </summary>
        public bool Last { get; set; } = false;
        
        /// <summary>
        /// Número total de páginas.
        /// </summary>
        public int TotalPages { get; set; } = 0;
        
        /// <summary>
        /// Se é a primeira página.
        /// </summary>
        public bool First { get; set; } = false;
        
        /// <summary>
        /// Objeto de ordenação.
        /// </summary>
        public SortModel Sort { get; set; } = new SortModel();
        
        /// <summary>
        /// Número de elementos por página.
        /// </summary>
        public int Number { get; set; } = 0;
        
        /// <summary>
        /// Número de elementos.
        /// </summary>
        public int NumberOfElements { get; set; } = 0;
        
        /// <summary>
        /// Tamanho da página
        /// </summary>
        public int Size { get; set; } = 0;
    }
}