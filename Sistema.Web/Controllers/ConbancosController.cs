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
using Sistema.Web.Models.Maestros.Conbancos;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConbancosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ConbancosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Conbancos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ConbancoViewModel>> Listar()
        {
            var conbanco = await _context.Conbancos
                .Include(a => a.empresa)
                .Include(a => a.grpconcepto)
                .Include(a => a.banco)
                .ToListAsync();

            return conbanco.Select(a => new ConbancoViewModel
            {
                Id = a.Id,
                empresaId = a.empresaId,
                empresa = a.empresa.nombre,
                orden = a.orden,
                nombre = a.nombre,
                bancoId = a.bancoId,
                banco = a.banco.nombre,
                grpconceptoId = a.grpconceptoId,
                grpconcepto = a.grpconcepto.nombre,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Conbancos/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<ConbancoSelectModel>> Select()
        {
            var conbanco = await _context.Conbancos
                .Where(a => a.activo == true)
                .OrderBy(a => a.nombre)
                .AsNoTracking()
                .ToListAsync();

            return conbanco.Select(a => new ConbancoSelectModel
            {
                Id = a.Id,
                orden = a.orden,
                nombre = a.nombre
            });
        }

        // GET: api/Conbancos/SelectConbancoDeEmpresa/1
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<ConbancoSelectModel>> SelectConbancoDeEmpresa([FromRoute] int id)
        {
            var conbanco = await _context.Conbancos
                .Where(a => a.activo == true && a.empresaId == id)
                .OrderBy(a => a.orden)
                .AsNoTracking()
                .ToListAsync();

            return conbanco.Select(a => new ConbancoSelectModel
            {
                Id = a.Id,
                orden = a.orden,
                nombre = a.nombre
            });
        }

        // GET: api/Conbancos/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var conbanco = await _context.Conbancos
                .SingleOrDefaultAsync(a => a.Id == id);

            if (conbanco == null)
            {
                return NotFound();
            }

            return Ok(new ConbancoViewModel
            {
                Id = conbanco.Id,
                empresaId = conbanco.empresaId,
                orden = conbanco.orden,
                nombre = conbanco.nombre,
                bancoId = conbanco.bancoId,
                grpconceptoId = conbanco.grpconceptoId,
                iduseralta = conbanco.iduseralta,
                fecalta = conbanco.fecalta,
                iduserumod = conbanco.iduserumod,
                fecumod = conbanco.fecumod,
                activo = conbanco.activo
            });
        }

        // PUT: api/Conbancos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ConbancoUpdateModel model)
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
            var conbanco = await _context.Conbancos
                .FirstOrDefaultAsync(a => a.Id == model.Id);

            if (conbanco == null)
            {
                return NotFound();
            }

            conbanco.empresaId = model.empresaId;
            conbanco.orden = model.orden;
            conbanco.nombre = model.nombre;
            conbanco.iduseralta = model.iduseralta;
            conbanco.fecalta = model.fecalta;
            conbanco.iduserumod = model.iduserumod;
            conbanco.fecumod = fechaHora;
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

        // POST: api/Conbancos/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] ConbancoCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Conbanco conbanco = new Conbanco
            {
                empresaId = model.empresaId,
                orden = model.orden,
                nombre = model.nombre,
                grpconceptoId = model.grpconceptoId,
                bancoId=model.bancoId,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Conbancos.Add(conbanco);
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

        // DELETE: api/Conbancos/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var conbanco = await _context.Conbancos
                .FindAsync(id);

            if (conbanco == null)
            {
                return NotFound();
            }

            _context.Conbancos.Remove(conbanco);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(conbanco);
        }

        // PUT: api/Conbancos/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var conbanco = await _context
                .Conbancos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (conbanco == null)
            {
                return NotFound();
            }

            conbanco.activo = false;

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

        // PUT: api/Conbancos/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var conbanco = await _context
                .Conbancos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (conbanco == null)
            {
                return NotFound();
            }

            conbanco.activo = true;

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

        private bool ConbancoExists(int id)
        {
            return _context.Conbancos.Any(e => e.Id == id);
        }
    }
}
