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
using Sistema.Web.Models.Maestros.Appconfig;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,JefeAdministracion")]
    [Route("api/[controller]")]
    [ApiController]
    public class AppconfigsController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public AppconfigsController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Appconfigs/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<AppconfigViewModel>> Listar()
        {
            var appconfig = await _context.Appconfigs.ToListAsync();

            return appconfig.Select(a => new AppconfigViewModel
            {
                id = a.id,
                parametro = a.parametro,
                vstring = a.vstring,
                vint = a.vint,
                vdecimal = a.vdecimal,
                vdatetime = a.vdatetime,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // PUT: api/Appconfigs/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] AppconfigUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.id <= 0)
            {
                return BadRequest();
            }

            var fechaHora = DateTime.Now;
            var appconfig = await _context.Appconfigs.FirstOrDefaultAsync(c => c.id == model.id);

            if (appconfig == null)
            {
                return NotFound();
            }

            appconfig.parametro = model.parametro;
            appconfig.vstring = model.vstring;
            appconfig.vint = model.vint;
            appconfig.vdecimal = model.vdecimal;
            appconfig.vdatetime = model.vdatetime;
            appconfig.iduseralta = model.iduseralta;
            appconfig.fecalta = model.fecalta;
            appconfig.iduserumod = model.iduserumod;
            appconfig.fecumod = fechaHora;

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

        // POST: api/Appconfigs/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] AppconfigCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Appconfig appconfig = new Appconfig
            {
                parametro = model.parametro,
                vstring = model.vstring,
                vint = model.vint,
                vdecimal = model.vdecimal,
                vdatetime = model.vdatetime,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Appconfigs.Add(appconfig);
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

        // DELETE: api/Appconfigs/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appconfig = await _context.Appconfigs.FindAsync(id);
            if (appconfig == null)
            {
                return NotFound();
            }

            _context.Appconfigs.Remove(appconfig);
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

        private bool AppconfigExists(int id)
        {
            return _context.Appconfigs.Any(e => e.id == id);
        }
    }
}
