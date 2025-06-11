namespace EstoqueAPI.DTOs
{
    public class CreateVendaDTO
    {
        public int ProdutoId { get; set; }
        public required int Quantidade { get; set; }
    }
}