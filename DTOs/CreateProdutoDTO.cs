namespace EstoqueAPI.DTOs
{
    public class CreateProdutoDTO
    {
        public required string Nome { get; set; }
        public required decimal Preco { get; set; }
        public required int Quantidade { get; set; }
        public int CategoriaId { get; set; }
    }
}
