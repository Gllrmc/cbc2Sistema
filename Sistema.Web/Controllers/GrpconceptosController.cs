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
using Sistema.Web.Models.Maestros.Grpconceptos;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
    [Route("api/[controller]")]
    [ApiController]
    public class GrpconceptosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public GrpconceptosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Grpconceptos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<GrpconceptoViewModel>> Listar()
        {
            var grpconcepto = await _context.Grpconceptos
                .Include(a => a.empresa)
                .ToListAsync();

            return grpconcepto.Select(a => new GrpconceptoViewModel
            {
                Id = a.Id,
                empresaId = a.empresaId,
                empresa = a.empresa.nombre,
                orden = a.orden,
                nombre = a.nombre,
                esajuape = a.esajuape,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Grpconceptos/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<GrpconceptoSelectModel>> Select()
        {
            var grpconcepto = await _context.Grpconceptos
                .Where(a => a.activo == true)
                .OrderBy(a => a.nombre)
                .AsNoTracking()
                .ToListAsync();

            return grpconcepto.Select(a => new GrpconceptoSelectModel
            {
                Id = a.Id,
                orden = a.orden,
                nombre = a.nombre,
                esajuape = a.esajuape
            });
        }

        // GET: api/Grpconceptos/SelectGrpconceptoDeEmpresa/1
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<GrpconceptoSelectModel>> SelectGrpconceptoDeEmpresa([FromRoute] int id)
        {
            var grpconcepto = await _context.Grpconceptos
                .Where(a => a.activo == true && a.empresaId == id)
                .OrderBy(a => a.orden)
                .AsNoTracking()
                .ToListAsync();

            return grpconcepto.Select(a => new GrpconceptoSelectModel
            {
                Id = a.Id,
                orden = a.orden,
                nombre = a.nombre,
                esajuape = a.esajuape
            });
        }

        // GET: api/Grpconceptos/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var grpconcepto = await _context.Grpconceptos
                .SingleOrDefaultAsync(a => a.Id == id);

            if (grpconcepto == null)
            {
                return NotFound();
            }

            return Ok(new GrpconceptoViewModel
            {
                Id = grpconcepto.Id,
                empresaId = grpconcepto.empresaId,
                orden = grpconcepto.orden,
                nombre = grpconcepto.nombre,
                esajuape = grpconcepto.esajuape,
                iduseralta = grpconcepto.iduseralta,
                fecalta = grpconcepto.fecalta,
                iduserumod = grpconcepto.iduserumod,
                fecumod = grpconcepto.fecumod,
                activo = grpconcepto.activo
            });
        }

        // PUT: api/Grpconceptos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] GrpconceptoUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id <= 0)
            {
                return BadRequest();
            }

            if (model.esajuape)
            {
                var grpconceptoajuste = await _context.Grpconceptos
                .FirstOrDefaultAsync(a => a.esajuape == true);
                if (grpconceptoajuste != null) 
                {
                    grpconceptoajuste.esajuape = false;
                }
            }

            var fechaHora = DateTime.Now;
            var grpconcepto = await _context.Grpconceptos
                .FirstOrDefaultAsync(a => a.Id == model.Id);

            if (grpconcepto == null)
            {
                return NotFound();
            }

            grpconcepto.empresaId = model.empresaId;
            grpconcepto.orden = model.orden;
            grpconcepto.nombre = model.nombre;
            grpconcepto.esajuape = model.esajuape;
            grpconcepto.iduseralta = model.iduseralta;
            grpconcepto.fecalta = model.fecalta;
            grpconcepto.iduserumod = model.iduserumod;
            grpconcepto.fecumod = fechaHora;

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

        // POST: api/Grpconceptos/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] GrpconceptoCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.esajuape)
            {
                var grpconceptoajuste = await _context.Grpconceptos
                .FirstOrDefaultAsync(a => a.esajuape == true);
                if (grpconceptoajuste != null)
                {
                    grpconceptoajuste.esajuape = false;
                }
            }

            var fechaHora = DateTime.Now;
            Grpconcepto grpconcepto = new Grpconcepto
            {
                empresaId = model.empresaId,
                orden = model.orden,
                nombre = model.nombre,
                esajuape = model.esajuape,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Grpconceptos.Add(grpconcepto);
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

        // DELETE: api/Grpconceptos/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var grpconcepto = await _context.Grpconceptos
                .FindAsync(id);

            if (grpconcepto == null)
            {
                return NotFound();
            }

            _context.Grpconceptos.Remove(grpconcepto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(grpconcepto);
        }

        // PUT: api/Grpconceptos/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var grpconcepto = await _context
                .Grpconceptos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (grpconcepto == null)
            {
                return NotFound();
            }

            grpconcepto.activo = false;

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

        // PUT: api/Grpconceptos/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var grpconcepto = await _context
                .Grpconceptos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (grpconcepto == null)
            {
                return NotFound();
            }

            grpconcepto.activo = true;

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



        private bool GrpconceptoExists(int id)
        {
            return _context.Grpconceptos.Any(e => e.Id == id);
        }
    }
}
