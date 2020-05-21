using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Operaciones;
using Sistema.Web.Models.Operaciones;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
    [Route("api/[controller]")]
    [ApiController]
    public class LotesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public LotesController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Lotes/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<LoteViewModel>> Listar()
        {
            var lote = await _context.Lotes
                .Include(a => a.empresa)
                .Include(a => a.asocuenta)
                .ThenInclude(a => a.bancuenta)
                .ThenInclude(a => a.banco)
                .Include(a => a.asocuenta)
                .ThenInclude(a => a.concuenta)
                .ToListAsync();

            return lote.Select(a => new LoteViewModel
            {
                Id = a.Id,
                empresaId = a.empresaId,
                empresa = a.empresa.nombre,
                asocuenta = a.asocuenta.bancuenta.banco.nombrecorto + a.asocuenta.bancuenta.tipo + a.asocuenta.bancuenta.moneda + a.asocuenta.bancuenta.numcuenta + "/" + a.asocuenta.concuenta.apporigen + a.asocuenta.concuenta.moneda + a.asocuenta.concuenta.numcuenta,
                anio = a.anio,
                mes = a.mes,
                bansalini = a.bansalini,
                bansalfin = a.bansalfin,
                consalini = a.consalini,
                consalfin = a.consalfin,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // PUT: api/Lotes/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] LoteUpdateModel model)
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
            var lote = await _context.Lotes
                .FirstOrDefaultAsync(a => a.Id == model.Id);

            if (lote == null)
            {
                return NotFound();
            }

            lote.empresaId = model.empresaId;
            lote.asocuentaId = model.asocuentaId;
            lote.anio = model.anio;
            lote.mes = model.mes;
            lote.bansalini = model.bansalini;
            lote.bansalfin = model.bansalfin;
            lote.consalini = model.consalini;
            lote.consalfin = model.consalfin;
            lote.iduserumod = model.iduserumod;
            lote.fecumod = fechaHora;
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

        // POST: api/Lotes/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] LoteCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Lote lote = new Lote
            {
                empresaId = model.empresaId,
                asocuentaId = model.asocuentaId,
                anio = model.anio,
                mes = model.mes,
                bansalini = model.bansalini,
                bansalfin = model.bansalfin,
                consalini = model.consalini,
                consalfin = model.consalfin,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Lotes.Add(lote);
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

        // DELETE: api/Lotes/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lote = await _context.Lotes
                .FindAsync(id);

            if (lote == null)
            {
                return NotFound();
            }

            _context.Lotes.Remove(lote);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(lote);
        }

        // PUT: api/Lotes/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var lote = await _context
                .Lotes
                .FirstOrDefaultAsync(c => c.Id == id);

            if (lote == null)
            {
                return NotFound();
            }

            lote.activo = false;

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

        // PUT: api/Lotes/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var lote = await _context
                .Lotes
                .FirstOrDefaultAsync(c => c.Id == id);

            if (lote == null)
            {
                return NotFound();
            }

            lote.activo = true;

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

        private bool LoteExists(int id)
        {
            return _context.Lotes.Any(e => e.Id == id);
        }
    }
}
