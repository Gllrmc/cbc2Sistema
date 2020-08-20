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
using Sistema.Web.Models.Maestros.Asocuentas;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
    [Route("api/[controller]")]
    [ApiController]
    public class AsocuentasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public AsocuentasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Asocuentas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<AsocuentaViewModel>> Listar()
        {
            var asocuenta = await _context.Asocuentas
                .Include(a => a.empresa)
                .Include(a => a.bancuenta)
                .ThenInclude(a => a.banco)
                .Include(a => a.concuenta)
                .OrderBy(a => a.orden)
                .ToListAsync();

            return asocuenta.Select(a => new AsocuentaViewModel
            {
                Id = a.Id,
                orden = a.orden,
                descripcion = a.descripcion,
                empresaId = a.empresaId,
                empresa = a.empresa.nombre,
                bancuentaId = a.bancuentaId,
                bancuenta = a.bancuenta.banco.nombrecorto+a.bancuenta.tipo+a.bancuenta.moneda+a.bancuenta.numcuenta,
                concuentaId = a.concuentaId,
                concuenta = a.concuenta.apporigen+a.concuenta.moneda+a.concuenta.numcuenta,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Asocuentas/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<AsocuentaSelectModel>> Select()
        {
            var asocuenta = await _context.Asocuentas
                .Include(b => b.bancuenta)
                .Include(b => b.concuenta)
                .Where(a => a.activo == true)
                .OrderBy(a => a.orden)
                .AsNoTracking()
                .ToListAsync();

            return asocuenta.Select(a => new AsocuentaSelectModel
            {
                Id = a.Id,
                orden = a.orden,
                descripcion = a.descripcion,
                empresaId = a.empresaId,
                empresa = a.empresa.nombre,
                bancuentaId = a.bancuentaId,
                bancuenta = a.bancuenta.banco.nombrecorto + a.bancuenta.tipo + a.bancuenta.moneda + a.bancuenta.numcuenta,
                concuentaId = a.concuentaId,
                concuenta = a.concuenta.apporigen + a.concuenta.moneda + a.concuenta.numcuenta,
            });
        }

        // GET: api/Asocuentas/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var asocuenta = await _context.Asocuentas
                .SingleOrDefaultAsync(a => a.Id == id);

            if (asocuenta == null)
            {
                return NotFound();
            }

            return Ok(new AsocuentaViewModel
            {
                Id = asocuenta.Id,
                empresaId = asocuenta.empresaId,
                orden = asocuenta.orden,
                descripcion = asocuenta.descripcion,
                bancuentaId = asocuenta.bancuentaId,
                concuentaId = asocuenta.concuentaId,
                iduseralta = asocuenta.iduseralta,
                fecalta = asocuenta.fecalta,
                iduserumod = asocuenta.iduserumod,
                fecumod = asocuenta.fecumod,
                activo = asocuenta.activo
            });
        }

        // PUT: api/Asocuentas/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] AsocuentaUpdateModel model)
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
            var asocuenta = await _context.Asocuentas
                .FirstOrDefaultAsync(a => a.Id == model.Id);

            if (asocuenta == null)
            {
                return NotFound();
            }
            asocuenta.empresaId = model.empresaId;
            asocuenta.orden = model.orden;
            asocuenta.descripcion = model.descripcion;
            asocuenta.bancuentaId = model.bancuentaId;
            asocuenta.concuentaId = model.concuentaId;
            asocuenta.iduseralta = model.iduseralta;
            asocuenta.fecalta = model.fecalta;
            asocuenta.iduserumod = model.iduserumod;
            asocuenta.fecumod = fechaHora;
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

        // POST: api/Asocuentas/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] AsocuentaCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Asocuenta asocuenta = new Asocuenta
            {
                empresaId = model.empresaId,
                orden = model.orden,
                descripcion = model.descripcion,
                bancuentaId = model.bancuentaId,
                concuentaId = model.concuentaId,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Asocuentas.Add(asocuenta);
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

        // DELETE: api/Asocuentas/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var asocuenta = await _context.Asocuentas
                .FindAsync(id);

            if (asocuenta == null)
            {
                return NotFound();
            }

            _context.Asocuentas.Remove(asocuenta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(asocuenta);
        }

        // PUT: api/Asocuentas/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var asocuenta = await _context
                .Asocuentas
                .FirstOrDefaultAsync(c => c.Id == id);

            if (asocuenta == null)
            {
                return NotFound();
            }

            asocuenta.activo = false;

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

        // PUT: api/Asocuentas/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var asocuenta = await _context
                .Asocuentas
                .FirstOrDefaultAsync(c => c.Id == id);

            if (asocuenta == null)
            {
                return NotFound();
            }

            asocuenta.activo = true;

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

        private bool AsocuentaExists(int id)
        {
            return _context.Asocuentas.Any(e => e.Id == id);
        }
    }
}
