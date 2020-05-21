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
using Sistema.Web.Models.Maestros.Personas;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,Conciliador,Dataentry")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public PersonasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Personas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaViewModel>> Listar()
        {
            var persona = await _context.Personas
                .Include(a => a.paises)
                .Include(a => a.provincias)
                .OrderBy(a => a.nombre)
                .ToListAsync();

            return persona.Select(a => new PersonaViewModel
            {
                Id = a.Id,
                nombre = a.nombre,
                domicilio = a.domicilio,
                localidad = a.localidad,
                cpostal = a.cpostal,
                paisId = a.paisId,
                pais = a.paises.nombre,
                provinciaId = a.provinciaId,
                provincia = a.provincias.nombre,
                emailpersonal = a.emailpersonal,
                telefonopersonal = a.telefonopersonal,
                tipodocumento = a.tipodocumento,
                numdocumento = a.numdocumento,
                esempleado = a.esempleado,
                esproveedor = a.esproveedor,
                escliente = a.escliente,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });

        }

        // GET: api/Personas/SelectContactoCliente
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaSelectModel>> SelectContactoCliente()
        {
            var persona = await _context.Personas
                .Where(r => r.activo == true && r.escliente == true)
                .OrderBy(a => a.nombre)
                .ToListAsync();

            return persona.Select(r => new PersonaSelectModel
            {
                Id = r.Id,
                nombre = r.nombre
            });
        }

        // GET: api/Personas/SelectContactoProveedor
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaSelectModel>> SelectContactoProveedor()
        {
            var persona = await _context.Personas.Where(r => r.activo == true && r.esproveedor == true).OrderBy(a => a.nombre).ToListAsync();

            return persona.Select(r => new PersonaSelectModel
            {
                Id = r.Id,
                nombre = r.nombre
            });
        }

        // GET: api/Personas/SelectContactoEmpleado
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaSelectModel>> SelectContactoEmpleado()
        {
            var persona = await _context.Personas
                .Where(r => r.activo == true && r.esempleado == true)
                .OrderBy(a => a.nombre)
                .ToListAsync();

            return persona.Select(r => new PersonaSelectModel
            {
                Id = r.Id,
                nombre = r.nombre
            });
        }
        // GET: api/Personas/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaSelectModel>> Select()
        {
            var persona = await _context.Personas.Where(r => r.activo == true).OrderBy(a => a.nombre).ToListAsync();

            return persona.Select(r => new PersonaSelectModel
            {
                Id = r.Id,
                nombre = r.nombre
            });
        }

        // GET: api/Personas/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var persona = await _context.Personas
                .Include(a => a.paises)
                .Include(a => a.provincias)
                .SingleOrDefaultAsync(a => a.Id == id);

            if (persona == null)
            {
                return NotFound();
            }

            return Ok(new PersonaViewModel
            {
                Id = persona.Id,
                paisId = persona.paisId,
                provinciaId = persona.provinciaId,
                pais = persona.paises.nombre,
                provincia = persona.provincias.nombre,
                nombre = persona.nombre,
                domicilio = persona.domicilio,
                localidad = persona.localidad,
                cpostal = persona.cpostal,
                emailpersonal = persona.emailpersonal,
                telefonopersonal = persona.telefonopersonal,
                tipodocumento = persona.tipodocumento,
                numdocumento = persona.numdocumento,
                esempleado = persona.esempleado,
                esproveedor = persona.esproveedor,
                escliente = persona.escliente,
                iduseralta = persona.iduseralta,
                fecalta = persona.fecalta,
                iduserumod = persona.iduserumod,
                fecumod = persona.fecumod,
                activo = persona.activo
            });
        }

        // PUT: api/Personas/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] PersonaUpdateModel model)
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
            var persona = await _context.Personas
                .FirstOrDefaultAsync(a => a.Id == model.Id);

            if (persona == null)
            {
                return NotFound();
            }

            persona.Id = model.Id;
            persona.nombre = model.nombre;
            persona.domicilio = model.domicilio;
            persona.localidad = model.localidad;
            persona.cpostal = model.cpostal;
            persona.provinciaId = model.provinciaId;
            persona.paisId = model.paisId;
            persona.emailpersonal = model.emailpersonal;
            persona.telefonopersonal = model.telefonopersonal;
            persona.tipodocumento = model.tipodocumento;
            persona.numdocumento = model.numdocumento;
            persona.esempleado = model.esempleado;
            persona.esproveedor = model.esproveedor;
            persona.escliente = model.escliente;
            persona.iduseralta = model.iduseralta;
            persona.fecalta = model.fecalta;
            persona.iduserumod = model.iduserumod;
            persona.fecumod = fechaHora;
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

        // POST: api/Personas/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] PersonaCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Persona persona = new Persona
            {
                paisId = model.paisId,
                provinciaId = model.provinciaId,
                nombre = model.nombre,
                domicilio = model.domicilio,
                localidad = model.localidad,
                cpostal = model.cpostal,
                emailpersonal = model.emailpersonal,
                telefonopersonal = model.telefonopersonal,
                tipodocumento = model.tipodocumento,
                numdocumento = model.numdocumento,
                esempleado = model.esempleado,
                esproveedor = model.esproveedor,
                escliente = model.escliente,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Personas.Add(persona);
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

        // PUT: api/Personas/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var persona = await _context.Personas.FirstOrDefaultAsync(a => a.Id == id);

            if (persona == null)
            {
                return NotFound();
            }

            persona.activo = false;

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

        // PUT: api/Personas/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var persona = await _context.Personas.FirstOrDefaultAsync(a => a.Id == id);

            if (persona == null)
            {
                return NotFound();
            }

            persona.activo = true;

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


        private bool PersonaExists(int id)
        {
            return _context.Personas.Any(e => e.Id == id);
        }
    }
}
