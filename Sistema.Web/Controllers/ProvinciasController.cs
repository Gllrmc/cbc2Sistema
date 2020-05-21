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
using Sistema.Web.Models.Maestros.Provincias;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ProvinciasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Provincias/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ProvinciaViewModel>> Listar()
        {
            var provincia = await _context.Provincias
                .Include(a => a.pais)
                .ToListAsync();

            return provincia.Select(a => new ProvinciaViewModel
            {
                Id = a.Id,
                nombre = a.nombre,
                paisId = a.pais.Id,
                pais = a.pais.nombre,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Privincias/SelectProvinciasDePais/1
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<ProvinciaSelectModel>> SelectProvinciasDePais([FromRoute] int id)

        {
            var proyecto = await _context.Provincias
                .Where(a => a.paisId == id)
                .OrderBy(a => a.nombre)
                .ToListAsync();

            return proyecto.Select(a => new ProvinciaSelectModel
            {
                Id = a.Id,
                nombre = a.nombre
            });

        }

        // GET: api/Provincias/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<ProvinciaSelectModel>> Select()
        {
            var provincia = await _context.Provincias
                .Where(a => a.activo == true)
                .OrderBy(a => a.nombre)
                .ToListAsync();

            return provincia.Select(a => new ProvinciaSelectModel
            {
                Id = a.Id,
                nombre = a.nombre,
                paisId = a.paisId
            });
        }

        // GET: api/Provincias/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var provincia = await _context.Provincias
                .Include(a => a.pais)
                .SingleOrDefaultAsync(a => a.Id == id);

            if (provincia == null)
            {
                return NotFound();
            }

            return Ok(new ProvinciaViewModel
            {
                Id = provincia.Id,
                nombre = provincia.nombre,
                paisId = provincia.paisId,
                pais = provincia.pais.nombre,
                iduseralta = provincia.iduseralta,
                fecalta = provincia.fecalta,
                iduserumod = provincia.iduserumod,
                fecumod = provincia.fecumod,
                activo = provincia.activo
            });
        }

        // PUT: api/Provincias/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ProvinciaUpdateModel model)
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
            var provincia = await _context.Provincias
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (provincia == null)
            {
                return NotFound();
            }

            provincia.nombre = model.nombre;
            provincia.iduseralta = model.iduseralta;
            provincia.fecalta = model.fecalta;
            provincia.iduserumod = model.iduserumod;
            provincia.fecumod = fechaHora;

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

        // POST: api/Provicnias/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] ProvinciaCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Provincia provincia = new Provincia
            {
                nombre = model.nombre,
                paisId = model.paisId,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Provincias.Add(provincia);
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

        // DELETE: api/Provincias/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var provincia = await _context.Provincias.FindAsync(id);
            if (provincia == null)
            {
                return NotFound();
            }

            _context.Provincias.Remove(provincia);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(provincia);
        }

        // PUT: api/Provincias/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var provincia = await _context.Provincias.FirstOrDefaultAsync(c => c.Id == id);

            if (provincia == null)
            {
                return NotFound();
            }

            provincia.activo = false;

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

        // PUT: api/Provincias/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var provincia = await _context.Provincias.FirstOrDefaultAsync(c => c.Id == id);

            if (provincia == null)
            {
                return NotFound();
            }

            provincia.activo = true;

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


        private bool ProvinciaExists(int id)
        {
            return _context.Provincias.Any(e => e.Id == id);
        }
    }
}
