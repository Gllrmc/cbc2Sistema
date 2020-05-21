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
using Sistema.Web.Models.Maestros.Bancuentas;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
    [Route("api/[controller]")]
    [ApiController]
    public class BancuentasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public BancuentasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Bancuentas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<BancuentaViewModel>> Listar()
        {
            var bancuenta = await _context.Bancuentas
                .Include(a => a.empresa)
                .Include(a => a.banco)
                .ToListAsync();

            return bancuenta.Select(a => new BancuentaViewModel
            {
                Id = a.Id,
                empresaId = a.empresaId,
                empresa = a.empresa.nombre,
                bancoId = a.bancoId,
                banco = a.banco.nombrecorto,
                numcuenta = a.numcuenta,
                tipo = a.tipo,
                moneda = a.moneda,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Bancuentas/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<BancuentaSelectModel>> Select()
        {
            var bancuenta = await _context.Bancuentas
                .Include(a => a.empresa)
                .Include(b => b.banco)
                .Where(a => a.activo == true)
                .OrderBy(a => a.numcuenta)
                .AsNoTracking()
                .ToListAsync();

            return bancuenta.Select(a => new BancuentaSelectModel
            {
                Id = a.Id,
                empresaId = a.empresaId,
                empresa = a.empresa.nombre,
                banco = a.banco.nombrecorto,
                tipo = a.tipo,
                moneda = a.moneda,
                numcuenta = a.numcuenta
            });
        }

        // GET: api/Bancuentas/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var bancuenta = await _context.Bancuentas
                .SingleOrDefaultAsync(a => a.Id == id);

            if (bancuenta == null)
            {
                return NotFound();
            }

            return Ok(new BancuentaViewModel
            {
                Id = bancuenta.Id,
                empresaId = bancuenta.empresaId,
                bancoId = bancuenta.bancoId,
                numcuenta = bancuenta.numcuenta,
                tipo = bancuenta.tipo,
                moneda = bancuenta.moneda,
                iduseralta = bancuenta.iduseralta,
                fecalta = bancuenta.fecalta,
                iduserumod = bancuenta.iduserumod,
                fecumod = bancuenta.fecumod,
                activo = bancuenta.activo
            });
        }

        // PUT: api/Bancuentas/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] BancuentaUpdateModel model)
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
            var bancuenta = await _context.Bancuentas
                .FirstOrDefaultAsync(a => a.Id == model.Id);

            if (bancuenta == null)
            {
                return NotFound();
            }
            bancuenta.empresaId = model.empresaId;
            bancuenta.bancoId = model.bancoId;
            bancuenta.numcuenta = model.numcuenta;
            bancuenta.tipo = model.tipo;
            bancuenta.moneda = model.moneda;
            bancuenta.iduseralta = model.iduseralta;
            bancuenta.fecalta = model.fecalta;
            bancuenta.iduserumod = model.iduserumod;
            bancuenta.fecumod = model.fecumod;
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

        // POST: api/Bancuentas/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] BancuentaCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Bancuenta bancuenta = new Bancuenta
            {
                empresaId = model.empresaId,
                bancoId = model.bancoId,
                numcuenta = model.numcuenta,
                tipo = model.tipo,
                moneda = model.moneda,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Bancuentas.Add(bancuenta);
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

        // DELETE: api/Bancuentas/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bancuenta = await _context.Bancuentas
                .FindAsync(id);

            if (bancuenta == null)
            {
                return NotFound();
            }

            _context.Bancuentas.Remove(bancuenta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(bancuenta);
        }

        // PUT: api/Bancuentas/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var bancuenta = await _context
                .Bancuentas
                .FirstOrDefaultAsync(c => c.Id == id);

            if (bancuenta == null)
            {
                return NotFound();
            }

            bancuenta.activo = false;

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

        // PUT: api/Bancuentas/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var bancuenta = await _context
                .Bancuentas
                .FirstOrDefaultAsync(c => c.Id == id);

            if (bancuenta == null)
            {
                return NotFound();
            }

            bancuenta.activo = true;

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

        private bool BancuentaExists(int id)
        {
            return _context.Bancuentas.Any(e => e.Id == id);
        }
    }
}
