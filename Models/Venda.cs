namespace EstoqueAPI.Models;

public class Venda
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;
    public int Quantidade { get; set; }
    public DateTime DataVenda { get; set; }
}
