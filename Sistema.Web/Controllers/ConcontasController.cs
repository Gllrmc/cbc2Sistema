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
using Sistema.Web.Models.Maestros.Concontas;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConcontasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ConcontasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Concontas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ConcontaViewModel>> Listar()
        {
            var conconta = await _context.Concontas
                .Include(a => a.empresa)
                .Include(a => a.grpconcepto)
                .ToListAsync();

            return conconta.Select(a => new ConcontaViewModel
            {
                Id = a.Id,
                empresaId = a.empresaId,
                empresa = a.empresa.nombre,
                orden = a.orden,
                nombre = a.nombre,
                grpconceptoId = a.grpconceptoId,
                grpconcepto = a.grpconcepto.nombre,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Concontas/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<ConcontaSelectModel>> Select()
        {
            var conconta = await _context.Concontas
                .Where(a => a.activo == true)
                .OrderBy(a => a.nombre)
                .AsNoTracking()
                .ToListAsync();

            return conconta.Select(a => new ConcontaSelectModel
            {
                Id = a.Id,
                orden = a.orden,
                nombre = a.nombre
            });
        }

        // GET: api/Concontas/SelectConcontaDeEmpresa/1
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<ConcontaSelectModel>> SelectConcontaDeEmpresa([FromRoute] int id)
        {
            var conconta = await _context.Concontas
                .Where(a => a.activo == true && a.empresaId == id)
                .OrderBy(a => a.orden)
                .AsNoTracking()
                .ToListAsync();

            return conconta.Select(a => new ConcontaSelectModel
            {
                Id = a.Id,
                orden = a.orden,
                nombre = a.nombre
            });
        }

        // GET: api/Concontas/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var conconta = await _context.Concontas
                .SingleOrDefaultAsync(a => a.Id == id);

            if (conconta == null)
            {
                return NotFound();
            }

            return Ok(new ConcontaViewModel
            {
                Id = conconta.Id,
                empresaId = conconta.empresaId,
                orden = conconta.orden,
                nombre = conconta.nombre,
                iduseralta = conconta.iduseralta,
                fecalta = conconta.fecalta,
                iduserumod = conconta.iduserumod,
                fecumod = conconta.fecumod,
                activo = conconta.activo
            });
        }

        // PUT: api/Concontas/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ConcontaUpdateModel model)
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
            var conconta = await _context.Concontas
                .FirstOrDefaultAsync(a => a.Id == model.Id);

            if (conconta == null)
            {
                return NotFound();
            }

            conconta.empresaId = model.empresaId;
            conconta.orden = model.orden;
            conconta.nombre = model.nombre;
            conconta.grpconceptoId = model.grpconceptoId;
            conconta.iduseralta = model.iduseralta;
            conconta.fecalta = model.fecalta;
            conconta.iduserumod = model.iduserumod;
            conconta.fecumod = fechaHora;
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

        // POST: api/Concontas/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] ConcontaCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Conconta conconta = new Conconta
            {
                empresaId = model.empresaId,
                orden = model.orden,
                nombre = model.nombre,
                grpconceptoId = model.grpconceptoId,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Concontas.Add(conconta);
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

        // DELETE: api/Concontas/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var conconta = await _context.Concontas
                .FindAsync(id);

            if (conconta == null)
            {
                return NotFound();
            }

            _context.Concontas.Remove(conconta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(conconta);
        }

        // PUT: api/Concontas/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var conconta = await _context
                .Concontas
                .FirstOrDefaultAsync(c => c.Id == id);

            if (conconta == null)
            {
                return NotFound();
            }

            conconta.activo = false;

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

        // PUT: api/Concontas/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var conconta = await _context
                .Concontas
                .FirstOrDefaultAsync(c => c.Id == id);

            if (conconta == null)
            {
                return NotFound();
            }

            conconta.activo = true;

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

        private bool ConcontaExists(int id)
        {
            return _context.Concontas.Any(e => e.Id == id);
        }
    }
}
