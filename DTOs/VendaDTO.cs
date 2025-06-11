namespace EstoqueAPI.DTOs
{
    public class VendaDTO
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public required string ProdutoNome { get; set; }
        public required int Quantidade { get; set; }
        public DateTime DataVenda { get; set; }
    }
}