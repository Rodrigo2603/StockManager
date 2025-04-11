namespace EstoqueAPI.Models;

public class Produto
{
    public int Id { get; private set; }
    public required string Nome { get; set; }
    public required int Quantidade { get; set; }
    public required decimal Preco { get; set; }
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
}