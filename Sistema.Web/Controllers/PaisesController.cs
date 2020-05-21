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
using Sistema.Web.Models.Maestros.Paises;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public PaisesController(DbContextSistema context)
        {
            _context = context;
        }
        // GET: api/Paises/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<PaisViewModel>> Listar()
        {
            var pais = await _context
                .Paises.ToListAsync();

            return pais.Select(a => new PaisViewModel
            {
                
                Id = a.Id,
                nombre = a.nombre,
                cuit = a.cuit,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Paises/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<PaisSelectModel>> Select()
        {
            var pais = await _context.Paises
                .Where(r => r.activo == true)
                .OrderBy(r => r.nombre)
                .ToListAsync();

            return pais.Select(r => new PaisSelectModel
            {
                Id = r.Id,
                nombre = r.nombre,
                cuit = r.cuit
            });
        }

        // GET: api/Paises/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var pais = await _context.Paises.FindAsync(id);

            if (pais == null)
            {
                return NotFound();
            }

            return Ok(new PaisViewModel
            {
                Id = pais.Id,
                nombre = pais.nombre,
                cuit = pais.cuit,
                iduseralta = pais.iduseralta,
                fecalta = pais.fecalta,
                iduserumod = pais.iduserumod,
                fecumod = pais.fecumod,
                activo = pais.activo
            });
        }

        // PUT: api/Paises/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] PaisUpdateModel model)
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
            var pais = await _context.Paises
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (pais == null)
            {
                return NotFound();
            }

            pais.nombre = model.nombre;
            pais.cuit = model.cuit;
            pais.iduseralta = model.iduseralta;
            pais.fecalta = model.fecalta;
            pais.iduserumod = model.iduserumod;
            pais.fecumod = fechaHora;

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

        // POST: api/Paises/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] PaisCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Pais pais = new Pais
            {
                nombre = model.nombre,
                cuit = model.cuit,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Paises.Add(pais);
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

        // DELETE: api/Paises/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pais = await _context.Paises
                .FindAsync(id);

            if (pais == null)
            {
                return NotFound();
            }

            _context.Paises.Remove(pais);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(pais);
        }

        // PUT: api/Paises/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var pais = await _context.Paises
                .FirstOrDefaultAsync(c => c.Id == id);

            if (pais == null)
            {
                return NotFound();
            }

            pais.activo = false;

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

        // PUT: api/Paises/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var pais = await _context.Paises
                .FirstOrDefaultAsync(c => c.Id == id);

            if (pais == null)
            {
                return NotFound();
            }

            pais.activo = true;

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


        private bool PaisExists(int id)
        {
            return _context.Paises.Any(e => e.Id == id);
        }
    }
}
