using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EstoqueAPI.Data;
using EstoqueAPI.Models;
using EstoqueAPI.DTOs; 

namespace EstoqueAPI.Controllers;

[ApiController]
[Route("vendas")]
public class VendasController : ControllerBase
{
    private readonly AppDbContext _context;

    public VendasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<VendaDTO>> RealizarVenda(CreateVendaDTO dto)
    {
        var produto = await _context.Produtos.FindAsync(dto.ProdutoId);

        if (produto == null)
        {
            return NotFound("Produto não encontrado.");
        }

        if (produto.Quantidade < dto.Quantidade)
        {
            return BadRequest("Estoque insuficiente.");
        }

        // Atualiza o estoque
        produto.Quantidade -= dto.Quantidade;

        var venda = new Venda
        {
            ProdutoId = dto.ProdutoId,
            Quantidade = dto.Quantidade,
            DataVenda = DateTime.UtcNow
        };

        _context.Vendas.Add(venda);
        await _context.SaveChangesAsync();

        var result = new VendaDTO
        {
            Id = venda.Id,
            ProdutoId = venda.ProdutoId,
            ProdutoNome = produto.Nome,
            Quantidade = venda.Quantidade,
            DataVenda = venda.DataVenda
        };

        return Ok(result);
    }
}
