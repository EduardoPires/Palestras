using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Infra.Data.Context;
using Domain.Models;

namespace DevXDemoASP2.Controllers
{
  public class ClientesController : Controller
  {
    private readonly IClienteService _clienteService;
    private readonly DemoContext _context;

    public ClientesController(DemoContext context, IClienteService clienteService)
    {
      _context = context;
      _clienteService = clienteService;
    }

    public IActionResult Index()
    {
      return View(_clienteService.ObterTodos());
    }

    public async Task<IActionResult> Details(Guid? id)
    {
      if (id == null)
        return NotFound();

      var cliente = await _context.Clientes.SingleOrDefaultAsync(m => m.Id == id);
      if (cliente == null)
        return NotFound();

      return View(cliente);
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Id,Nome,Email")] Cliente cliente)
    {
      if (ModelState.IsValid)
      {
        _clienteService.Adicionar(cliente);
        return RedirectToAction(nameof(Index));
      }
      return View(cliente);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
      if (id == null)
        return NotFound();

      var cliente = await _context.Clientes.SingleOrDefaultAsync(m => m.Id == id);
      if (cliente == null)
        return NotFound();
      return View(cliente);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Email")] Cliente cliente)
    {
      if (id != cliente.Id)
        return NotFound();

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(cliente);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ClienteExists(cliente.Id))
            return NotFound();
          throw;
        }
        return RedirectToAction(nameof(Index));
      }
      return View(cliente);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
      if (id == null)
        return NotFound();

      var cliente = await _context.Clientes
        .SingleOrDefaultAsync(m => m.Id == id);
      if (cliente == null)
        return NotFound();

      return View(cliente);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
      var cliente = await _context.Clientes.SingleOrDefaultAsync(m => m.Id == id);
      _context.Clientes.Remove(cliente);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool ClienteExists(Guid id)
    {
      return _context.Clientes.Any(e => e.Id == id);
    }
  }
}