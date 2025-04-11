namespace EstoqueAPI.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required decimal Preco { get; set; }
        public required int Quantidade { get; set; }
        public required string CategoriaNome { get; set; }
    }
}
