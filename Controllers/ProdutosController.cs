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
    public async Task<ActionResult<Produto>> GetProduto(int id)
    {
        var produto = await _context.Produtos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id);

        return produto is null ? NotFound() : Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult<Produto>> CreateProduto(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduto(int id, Produto produto)
    {
        if (id != produto.Id) return BadRequest();

        _context.Entry(produto).State = EntityState.Modified;
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
