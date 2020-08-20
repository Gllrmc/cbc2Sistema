using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Maestros;
using Sistema.Web.Models.Maestros.Concuentas;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConcuentasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ConcuentasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Concuentas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ConcuentaViewModel>> Listar()
        {
            var concuenta = await _context.Concuentas
                .Include(a => a.empresa)
                .ToListAsync();

            return concuenta.Select(a => new ConcuentaViewModel
            {
                Id = a.Id,
                empresaId = a.empresaId,
                empresa = a.empresa.nombre,
                apporigen = a.apporigen,
                moneda = a.moneda,
                numcuenta = a.numcuenta,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Concuentas/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<ConcuentaSelectModel>> Select()
        {
            var concuenta = await _context.Concuentas
                .Include(a => a.empresa)
                .Where(a => a.activo == true)
                .OrderBy(a => a.numcuenta)
                .AsNoTracking()
                .ToListAsync();

            return concuenta.Select(a => new ConcuentaSelectModel
            {
                Id = a.Id,
                empresaId = a.empresaId,
                empresa = a.empresa.nombre,
                apporigen = a.apporigen,
                moneda = a.moneda,
                numcuenta = a.numcuenta
            });
        }

        // GET: api/Concuentas/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var concuenta = await _context.Concuentas
                .SingleOrDefaultAsync(a => a.Id == id);

            if (concuenta == null)
            {
                return NotFound();
            }

            return Ok(new ConcuentaViewModel
            {
                Id = concuenta.Id,
                empresaId = concuenta.empresaId,
                apporigen = concuenta.apporigen,
                moneda = concuenta.moneda,
                numcuenta = concuenta.numcuenta,
                iduseralta = concuenta.iduseralta,
                fecalta = concuenta.fecalta,
                iduserumod = concuenta.iduserumod,
                fecumod = concuenta.fecumod,
                activo = concuenta.activo
            });
        }

        // PUT: api/Concuentas/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ConcuentaUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id <= 0)
            {
                return BadRequest();
            }

            var fechaHora = DateTime.Now;
            var concuenta = await _context.Concuentas
                .FirstOrDefaultAsync(a => a.Id == model.Id);

            if (concuenta == null)
            {
                return NotFound();
            }
            concuenta.empresaId = model.empresaId;
            concuenta.apporigen = model.apporigen;
            concuenta.moneda = model.moneda;
            concuenta.numcuenta = model.numcuenta;
            concuenta.iduseralta = model.iduseralta;
            concuenta.fecalta = model.fecalta;
            concuenta.iduserumod = model.iduserumod;
            concuenta.fecumod = fechaHora;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/Concuentas/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] ConcuentaCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Concuenta concuenta = new Concuenta
            {
                empresaId = model.empresaId,
                apporigen = model.apporigen,
                moneda = model.moneda,
                numcuenta = model.numcuenta,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Concuentas.Add(concuenta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/Concuentas/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var concuenta = await _context.Concuentas
                .FindAsync(id);

            if (concuenta == null)
            {
                return NotFound();
            }

            _context.Concuentas.Remove(concuenta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(concuenta);
        }

        // PUT: api/Concuentas/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var concuenta = await _context
                .Concuentas
                .FirstOrDefaultAsync(c => c.Id == id);

            if (concuenta == null)
            {
                return NotFound();
            }

            concuenta.activo = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Concuentas/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var concuenta = await _context
                .Concuentas
                .FirstOrDefaultAsync(c => c.Id == id);

            if (concuenta == null)
            {
                return NotFound();
            }

            concuenta.activo = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        private bool ConcuentaExists(int id)
        {
            return _context.Concuentas.Any(e => e.Id == id);
        }
    }
}
