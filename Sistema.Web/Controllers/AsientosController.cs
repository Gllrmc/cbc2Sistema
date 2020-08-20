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
    public class AsientosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public AsientosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Asientos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<AsientoViewModel>> Listar()
        {
            var asiento = await _context.Asientos
                .Include(a => a.empresa)
                .OrderBy(a => a.Id)
                .ToListAsync();

            return asiento.Select(a => new AsientoViewModel
            {
                Id = a.Id,
                empresaId = a.empresaId,
                empresa = a.empresa.nombre,
                comentario = a.comentario,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Asientos/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<AsientoSelectModel>> Select()
        {
            var asiento = await _context.Asientos
                .Include(b => b.empresa)
                .Where(a => a.activo == true)
                .OrderByDescending(a => a.Id)
                .AsNoTracking()
                .ToListAsync();

            return asiento.Select(a => new AsientoSelectModel
            {
                Id = a.Id,
                comentario = a.comentario,
                empresaId = a.empresaId,
                empresa = a.empresa.nombre
            });
        }

        // GET: api/Asientos/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var asiento = await _context.Asientos
                .SingleOrDefaultAsync(a => a.Id == id);

            if (asiento == null)
            {
                return NotFound();
            }

            return Ok(new AsientoViewModel
            {
                Id = asiento.Id,
                empresaId = asiento.empresaId,
                comentario = asiento.comentario,
                iduseralta = asiento.iduseralta,
                fecalta = asiento.fecalta,
                iduserumod = asiento.iduserumod,
                fecumod = asiento.fecumod,
                activo = asiento.activo
            });
        }

        // PUT: api/Asientos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] AsientoUpdateModel model)
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
            var asiento = await _context.Asientos
                .FirstOrDefaultAsync(a => a.Id == model.Id);

            if (asiento == null)
            {
                return NotFound();
            }
            asiento.empresaId = model.empresaId;
            asiento.comentario = model.comentario;
            asiento.iduseralta = model.iduseralta;
            asiento.fecalta = model.fecalta;
            asiento.iduserumod = model.iduserumod;
            asiento.fecumod = fechaHora;
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

        // POST: api/Asientos/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] AsientoCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Asiento asiento = new Asiento
            {
                empresaId = model.empresaId,
                comentario = model.comentario,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Asientos.Add(asiento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(asiento.Id);
        }

        // DELETE: api/Asientos/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var asiento = await _context.Asientos
                .FindAsync(id);

            if (asiento == null)
            {
                return NotFound();
            }

            _context.Asientos.Remove(asiento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(asiento);
        }

        // PUT: api/Asientos/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var asiento = await _context
                .Asientos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (asiento == null)
            {
                return NotFound();
            }

            asiento.activo = false;

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

        // PUT: api/Asientos/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var asiento = await _context
                .Asientos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (asiento == null)
            {
                return NotFound();
            }

            asiento.activo = true;

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

        private bool AsientoExists(int id)
        {
            return _context.Asientos.Any(e => e.Id == id);
        }
    }
}
