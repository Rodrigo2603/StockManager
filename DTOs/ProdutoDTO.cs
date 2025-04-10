namespace EstoqueAPI.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public required string CategoriaNome { get; set; }
    }
}
