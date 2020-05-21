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
using Sistema.Web.Models.Maestros.Empresas;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public EmpresasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Empresas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<EmpresaViewModel>> Listar()
        {
            var empresa = await _context.Empresas
                .Include(a => a.provincia)
                .Include(a => a.pais)
                .ToListAsync();

            return empresa.Select(a => new EmpresaViewModel
            {
                Id = a.Id,
                nombre = a.nombre,
                cuit = a.cuit,
                direccion = a.direccion,
                localidad = a.localidad,
                cpostal = a.cpostal,
                provinciaId = a.provinciaId,
                provincia = a.provincia.nombre,
                paisId = a.paisId,
                pais = a.pais.nombre,
                telefono = a.telefono,
                email = a.email,
                webpage = a.webpage,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Empresas/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<EmpresaSelectModel>> Select()
        {
            var empresa = await _context.Empresas
                .Where(a => a.activo == true)
                .OrderBy(a => a.nombre)
                .AsNoTracking()
                .ToListAsync();

            return empresa.Select(a => new EmpresaSelectModel
            {
                Id = a.Id,
                nombre = a.nombre
            });
        }

        // GET: api/Empresas/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var empresa = await _context.Empresas
                .Include(a => a.pais)
                .Include(a => a.provincia)
                .SingleOrDefaultAsync(a => a.Id == id);

            if (empresa == null)
            {
                return NotFound();
            }

            return Ok(new EmpresaViewModel
            {
                Id = empresa.Id,
                nombre = empresa.nombre,
                cuit = empresa.cuit,
                direccion = empresa.direccion,
                localidad = empresa.localidad,
                cpostal = empresa.cpostal,
                provinciaId = empresa.provinciaId,
                provincia = empresa.provincia.nombre,
                paisId = empresa.paisId,
                pais = empresa.pais.nombre,
                telefono = empresa.telefono,
                email = empresa.email,
                webpage = empresa.webpage,
                iduseralta = empresa.iduseralta,
                fecalta = empresa.fecalta,
                iduserumod = empresa.iduserumod,
                fecumod = empresa.fecumod,
                activo = empresa.activo
            });
        }

        // PUT: api/Empresas/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] EmpresaUpdateModel model)
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
            var empresa = await _context.Empresas
                .FirstOrDefaultAsync(a => a.Id == model.Id);

            if (empresa == null)
            {
                return NotFound();
            }

            empresa.nombre = model.nombre;
            empresa.cuit = model.cuit;
            empresa.direccion = model.direccion;
            empresa.localidad = model.localidad;
            empresa.cpostal = model.cpostal;
            empresa.provinciaId = model.provinciaId;
            empresa.paisId = model.paisId;
            empresa.telefono = model.telefono;
            empresa.email = model.email;
            empresa.webpage = model.webpage;
            empresa.iduseralta = model.iduseralta;
            empresa.fecalta = model.fecalta;
            empresa.iduserumod = model.iduserumod;
            empresa.fecumod = fechaHora;
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

        // POST: api/Empresas/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] EmpresaCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Empresa empresa = new Empresa
            {
                nombre = model.nombre,
                cuit = model.cuit,
                direccion = model.direccion,
                localidad = model.localidad,
                cpostal = model.cpostal,
                provinciaId = model.provinciaId,
                paisId = model.paisId,
                telefono = model.telefono,
                email = model.email,
                webpage = model.webpage,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Empresas.Add(empresa);
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

        // DELETE: api/Empresas/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empresa = await _context.Empresas
                .FindAsync(id);

            if (empresa == null)
            {
                return NotFound();
            }

            _context.Empresas.Remove(empresa);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(empresa);
        }

        // PUT: api/Empresas/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var empresa = await _context
                .Empresas
                .FirstOrDefaultAsync(c => c.Id == id);

            if (empresa == null)
            {
                return NotFound();
            }

            empresa.activo = false;

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

        // PUT: api/Empresas/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var empresa = await _context
                .Empresas
                .FirstOrDefaultAsync(c => c.Id == id);

            if (empresa == null)
            {
                return NotFound();
            }

            empresa.activo = true;

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


        private bool EmpresaExists(int id)
        {
            return _context.Empresas.Any(e => e.Id == id);
        }
    }
}
