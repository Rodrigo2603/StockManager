using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EstoqueAPI.Data;
using EstoqueAPI.Models;
using EstoqueAPI.DTOs;

namespace EstoqueAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategorias()
    {
        var categorias = await _context.Categorias
            .Select(c => new CategoriaDTO
            {
                Id = c.Id,
                Nome = c.Nome
            })
            .ToListAsync();

        return Ok(categorias);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaDTO>> GetCategoria(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);

        if (categoria is null)
            return NotFound();

        var categoriaDto = new CategoriaDTO
        {
            Id = categoria.Id,
            Nome = categoria.Nome
        };

        return Ok(categoriaDto);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaDTO>> CreateCategoria(CreateCategoriaDTO dto)
    {
        var categoria = new Categoria
        {
            Nome = dto.Nome
        };

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        var categoriaDto = new CategoriaDTO
        {
            Id = categoria.Id,
            Nome = categoria.Nome
        };

        return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoriaDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategoria(int id, UpdateCategoriaDTO categoriaDto)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria is null)
            return NotFound();

        categoria.Nome = categoriaDto.Nome;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoria(int id)
    {
        var categoria = await _context.Categorias
            .Include(c => c.Produtos)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (categoria is null)
            return NotFound();

        if (categoria.Produtos != null && categoria.Produtos.Any())
            return BadRequest("Não é possível excluir uma categoria que possui produtos vinculados.");

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
