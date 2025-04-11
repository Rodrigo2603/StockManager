namespace EstoqueAPI.Models;
public class Categoria
{
    public int Id { get; private set; }
    public required string Nome { get; set; }
    public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}