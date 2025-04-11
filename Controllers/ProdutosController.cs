using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EstoqueAPI.Data;
using EstoqueAPI.Models;
using EstoqueAPI.DTOs; 

namespace EstoqueAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutos()
    {
        var produtos = await _context.Produtos
            .Include(p => p.Categoria)
            .Select(p => new ProdutoDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Preco = p.Preco,
                Quantidade = p.Quantidade,
                CategoriaNome = p.Categoria != null ? p.Categoria.Nome : "Sem categoria"
            })
            .ToListAsync();

        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoDTO>> GetProduto(int id)
    {
        var produto = await _context.Produtos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (produto is null)
            return NotFound();

        var produtoDto = new ProdutoDTO
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Preco = produto.Preco,
            Quantidade = produto.Quantidade,
            CategoriaNome = produto.Categoria != null ? produto.Categoria.Nome : "Sem categoria"
        };

        return Ok(produtoDto);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDTO>> CreateProduto(CreateProdutoDTO dto)
    {
        var produto = new Produto
        {
            Nome = dto.Nome,
            Preco = dto.Preco,
            Quantidade = dto.Quantidade,
            CategoriaId = dto.CategoriaId
        };

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        // Carrega a categoria para preencher o nome corretamente no DTO de retorno
        await _context.Entry(produto).Reference(p => p.Categoria).LoadAsync();

        var produtoDto = new ProdutoDTO
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Preco = produto.Preco,
            Quantidade = produto.Quantidade,
            CategoriaNome = produto.Categoria != null ? produto.Categoria.Nome : "Sem categoria"
        };

        return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produtoDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduto(int id, UpdateProdutoDTO dto)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto is null)
            return NotFound();

        // Atualiza os campos manualmente
        produto.Nome = dto.Nome;
        produto.Preco = dto.Preco;
        produto.Quantidade = dto.Quantidade;
        produto.CategoriaId = dto.CategoriaId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto is null) return NotFound();

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
