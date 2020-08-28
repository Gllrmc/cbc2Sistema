using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Maestros;
using Sistema.Entidades.Operaciones;
using Sistema.Web.Models.Operaciones;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public MovimientosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Movimientos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<MovimientoViewModel>> Listar()
        {
            var movimiento = await _context.Movimientos
                .Include(a => a.empresa)
                .Include(a => a.lote)
                .ThenInclude(a => a.asocuenta)
                .Include(a => a.asiento)
                .Include(a => a.grpconcepto)
                .ToListAsync();

            return movimiento.Select(a => new MovimientoViewModel
            {
                Id = a.Id,
                empresaId= a.empresaId,
                empresa = a.empresa.nombre,
                loteId = a.loteId,
                aniomes = a.lote.anio + "/" + a.lote.mes,
                asocuenta = a.lote.asocuenta.descripcion,
                asientoId = a.asientoId,
                origen = a.origen,
                grpconceptoId = a.grpconceptoId,
                grpconcepto = a.grpconcepto.nombre,
                concepto = a.concepto,
                fecha = a.fecha,
                importe = a.importe,
                unsgimporte = Math.Abs(a.importe),
                ref0 = a.ref0,
                ref1 = a.ref1,
                ref2 = a.ref2,
                ref3 = a.ref3,
                ref4 = a.ref4,
                ref5 = a.ref5,
                ref6 = a.ref6,
                ref7 = a.ref7,
                ref8 = a.ref8,
                ref9 = a.ref9,
                etlId = a.etlId,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });
        }

        // GET: api/Movimientos/Listarnoclote/1
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<MovimientoViewModel>> Listarnoclote([FromRoute] int id)
        {
            var movimiento = await _context.Movimientos
                .Where(a => a.loteId == id && a.asientoId == null)
                .Include(a => a.empresa)
                .Include(a => a.lote)
                .ThenInclude(a => a.asocuenta)
                .Include(a => a.asiento)
                .Include(a => a.grpconcepto)
                .ToListAsync();

            return movimiento.Select(a => new MovimientoViewModel
            {
                Id = a.Id,
                empresaId = a.empresaId,
                empresa = a.empresa.nombre,
                loteId = a.loteId,
                aniomes = a.lote.anio + "/" + a.lote.mes,
                asocuenta = a.lote.asocuenta.descripcion,
                asientoId = a.asientoId,
                origen = a.origen,
                grpconceptoId = a.grpconceptoId,
                grpconcepto = a.grpconcepto.nombre,
                concepto = a.concepto,
                fecha = a.fecha,
                importe = a.importe,
                unsgimporte = Math.Abs(a.importe),
                ref0 = a.ref0,
                ref1 = a.ref1,
                ref2 = a.ref2,
                ref3 = a.ref3,
                ref4 = a.ref4,
                ref5 = a.ref5,
                ref6 = a.ref6,
                ref7 = a.ref7,
                ref8 = a.ref8,
                ref9 = a.ref9,
                etlId = a.etlId,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });
        }

        // GET: api/Movimientos/Listarconlote/1
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<MovimientoViewModel>> Listarconlote([FromRoute] int id)
        {
            var movimiento = await _context.Movimientos
                .Where(a => a.loteId == id && a.asientoId != null)
                .Include(a => a.empresa)
                .Include(a => a.lote)
                .ThenInclude(a => a.asocuenta)
                .Include(a => a.asiento)
                .Include(a => a.grpconcepto)
                .ToListAsync();

            return movimiento.Select(a => new MovimientoViewModel
            {
                Id = a.Id,
                empresaId = a.empresaId,
                empresa = a.empresa.nombre,
                loteId = a.loteId,
                aniomes = a.lote.anio + "/" + a.lote.mes,
                asocuenta = a.lote.asocuenta.descripcion,
                asientoId = a.asientoId,
                origen = a.origen,
                grpconceptoId = a.grpconceptoId,
                grpconcepto = a.grpconcepto.nombre,
                concepto = a.concepto,
                fecha = a.fecha,
                importe = a.importe,
                unsgimporte = Math.Abs(a.importe),
                ref0 = a.ref0,
                ref1 = a.ref1,
                ref2 = a.ref2,
                ref3 = a.ref3,
                ref4 = a.ref4,
                ref5 = a.ref5,
                ref6 = a.ref6,
                ref7 = a.ref7,
                ref8 = a.ref8,
                ref9 = a.ref9,
                etlId = a.etlId,
                iduseralta = a.iduseralta,
                fecalta = a.fecalta,
                iduserumod = a.iduserumod,
                fecumod = a.fecumod,
                activo = a.activo
            });
        }

        // GET: api/Movimientos/Consultacuadrada/2020/8
        [HttpGet("[action]/{anio}/{mes}")]
        public async Task<IActionResult> Consultacuadrada([FromRoute] string stranio, string strmes)
        {
            int anio = Int32.Parse(stranio);
            int mes = Int32.Parse(strmes);
            int aniomesencurso = anio * 100 + mes ;
            decimal[,] cuadro = new decimal[3, 3];
            cuadro[0, 0] = 0;
            cuadro[0, 1] = 0; 
            cuadro[0, 2] = 0;
            cuadro[1, 0] = 0;
            cuadro[1, 1] = 0;
            cuadro[1, 2] = 0;
            cuadro[2, 0] = 0;
            cuadro[2, 1] = 0;
            cuadro[2, 2] = 0;


            // buscar el registro de lote.
            var lote = await _context.Lotes
                .FirstOrDefaultAsync(a => (int.Parse(a.anio) * 100 + int.Parse(a.mes)) == aniomesencurso);

            // Saldo inicial contable correspondiente al lote seleccionado
            cuadro[0, 0] = lote.consalini;

            // movimientos contables del mes en curso  ver como se manejaran los ajustes que modifican saldo.
            cuadro[0, 1] = await _context.Movimientos
                .Include(a => a.lote)
                .Where(a => (int.Parse(a.lote.anio) * 100 + int.Parse(a.lote.mes)) == aniomesencurso && ( a.origen == "CON" || a.origen == "AJU" ) ) 
                .SumAsync(a => a.importe);

            // Saldo final contable correspondiente al lote seleccionado
            cuadro[0, 2] = lote.consalfin;

            //movimientos contables pendientes al inicio
            cuadro[1, 0] = await _context.Movimientos
                .Include(a => a.lote)
                .Where(a => (int.Parse(a.lote.anio) * 100 + int.Parse(a.lote.mes)) < aniomesencurso && a.asientoId == null)
                .SumAsync(a => a.importe);

            // todos los movimientos (contables, banco, aperturas, ajustes)
            cuadro[1, 1] = await _context.Movimientos
                .Include(a => a.lote)
                .Where(a => (int.Parse(a.lote.anio) * 100 + int.Parse(a.lote.mes)) == aniomesencurso)
                .SumAsync(a => a.importe);

            //movimientos contables pendientes al fin
            cuadro[1, 2] = await _context.Movimientos
                .Include(a => a.lote)
                .Where(a => (int.Parse(a.lote.anio) * 100 + int.Parse(a.lote.mes)) <= aniomesencurso && a.asientoId == null)
                .SumAsync(a => a.importe);

            // Saldo inicial banco correspondiente al lote seleccionado
            cuadro[2, 1] = lote.bansalini;

            //movimientos de banco del mes en curso
            cuadro[2, 1] = await _context.Movimientos
                .Include(a => a.lote)
                .Where(a => (int.Parse(a.lote.anio) * 100 + int.Parse(a.lote.mes)) == aniomesencurso && a.origen == "BAN")
                .SumAsync(a => a.importe);

            // Saldo final banco correspondiente al lote seleccionado
            cuadro[2, 2] = lote.bansalfin;

            return Ok(new CuadroViewModel
            {
                anio = anio,
                mes = mes,
                contaSI = cuadro[0,0],
                contaMO = cuadro[0,1],
                contaSF = cuadro[0,2],
                partiSI = cuadro[1,0],
                partiMO = cuadro[1,1],
                partiSF = cuadro[1,2],
                bancoSI = cuadro[2,0],
                bancoMO = cuadro[2,1],
                bancoSF = cuadro[2,2]
            });
        }


        // GET: api/Movimientos/Listarheader
        [HttpGet("[action]")]
        public async Task<IEnumerable<HeadermovViewModel>> Listarheader()
        {
            var movimiento = await _context.Movimientos
                .Include(a => a.empresa)
                .Include(a => a.lote)
                .ThenInclude(a => a.asocuenta)
                .GroupBy(a => new { a.empresaId, a.empresa.nombre, a.loteId, a.lote.asocuenta.descripcion, a.lote.anio, a.lote.mes})
                .Select(a => new { a.Key.empresaId, a.Key.nombre, a.Key.loteId, a.Key.descripcion, a.Key.anio, a.Key.mes, Count = a.Count() })
                .ToListAsync();

            return movimiento.Select(a => new HeadermovViewModel
            {
                empresaId = a.empresaId,
                empresa = a.nombre,
                loteId = a.loteId,
                asocuenta = a.descripcion,
                aniomes = a.anio + "/" + a.mes,
                cantidad = a.Count
            });

        }


        // PUT: api/Movimientos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] MovimientoUpdateModel model)
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
            var movimiento = await _context.Movimientos
                .FirstOrDefaultAsync(a => a.Id == model.Id);

            if (movimiento == null)
            {
                return NotFound();
            }

            movimiento.empresaId = model.empresaId;
            movimiento.loteId = model.loteId;
            movimiento.asientoId = model.asientoId;
            movimiento.origen = model.origen;
            movimiento.grpconceptoId = model.grpconceptoId;
            movimiento.concepto = model.concepto;
            movimiento.fecha = model.fecha;
            movimiento.importe = model.importe;
            movimiento.ref0 = model.ref0;
            movimiento.ref1 = model.ref1;
            movimiento.ref2 = model.ref2;
            movimiento.ref3 = model.ref3;
            movimiento.ref4 = model.ref4;
            movimiento.ref5 = model.ref5;
            movimiento.ref6 = model.ref6;
            movimiento.ref7 = model.ref7;
            movimiento.ref8 = model.ref8;
            movimiento.ref9 = model.ref9;
            movimiento.etlId = model.etlId;
            movimiento.iduseralta = model.iduseralta;
            movimiento.fecalta = model.fecalta;
            movimiento.iduserumod = model.iduserumod;
            movimiento.fecumod = fechaHora;
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

        // PUT: api/Movimientos/Actualizarasiento
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizarasiento([FromBody] MovimientoMassiveUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id.Length == 0)
            {
                return BadRequest();
            }

            var fechaHora = DateTime.Now;
            var numasiento = _context.Movimientos
                .Select(p => p.asientoId)
                .Max();
            numasiento = numasiento.HasValue ? numasiento + 1 : 1;

            var movimiento = await _context.Movimientos.Where(f=>model.Id.Contains(f.Id)).ToListAsync();
            movimiento.ForEach(a => { a.asientoId = numasiento; a.iduserumod = model.iduserumod; a.fecumod = fechaHora; });
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

        // PUT: api/Movimientos/Crearajuste
        [HttpPut("[action]")]
        public async Task<IActionResult> Crearajuste([FromBody] MovimientoAjusteModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id.Length == 0)
            {
                return BadRequest();
            }

            var fechaHora = DateTime.Now;
            var numasiento = _context.Movimientos
                .Select(p => p.asientoId)
                .Max();
            numasiento = numasiento.HasValue ? numasiento + 1 : 1;

            var movimiento = await _context.Movimientos.Where(f => model.Id.Contains(f.Id)).ToListAsync();
            movimiento.ForEach(a => { a.asientoId = numasiento; a.iduserumod = model.iduseralta; a.fecumod = fechaHora; });

            Movimiento alta = new Movimiento
            {
                empresaId = model.empresaId,
                loteId = model.loteId,
                asientoId = numasiento,
                origen = model.origen,
                grpconceptoId = model.grpconceptoId,
                concepto = model.concepto,
                fecha = model.fecha,
                importe = model.importe,
                etlId = model.etlId,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Movimientos.Add(alta);
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

        // PUT: api/Movimientos/Borrarasiento
        [HttpPut("[action]")]
        public async Task<IActionResult> Borrarasiento([FromBody] MovimientoMassiveBorrarModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id.Length == 0)
            {
                return BadRequest();
            }

            var fechaHora = DateTime.Now;
            var baja = await _context.Movimientos.Where(f => model.Id.Contains(f.asientoId) && f.origen == "AJU" ).ToListAsync();
            baja.ForEach( a => _context.Movimientos.Remove(a));

            var movimiento = await _context.Movimientos.Where(f => model.Id.Contains(f.asientoId)).ToListAsync();
            movimiento.ForEach(a => { a.asientoId = null; a.iduserumod = model.iduserumod; a.fecumod = fechaHora; });
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

        // POST: api/Movimientos/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] MovimientoCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Movimiento movimiento = new Movimiento
            {
                empresaId = model.empresaId,
                loteId = model.loteId,
                asientoId = model.asientoId,
                origen = model.origen,
                grpconceptoId = model.grpconceptoId,
                concepto = model.concepto,
                fecha = model.fecha,
                importe = model.importe,
                ref0 = model.ref0,
                ref1 = model.ref1,
                ref2 = model.ref2,
                ref3 = model.ref3,
                ref4 = model.ref4,
                ref5 = model.ref5,
                ref6 = model.ref6,
                ref7 = model.ref7,
                ref8 = model.ref8,
                ref9 = model.ref9,
                etlId = model.etlId,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Movimientos.Add(movimiento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(movimiento);
        }

        // POST: api/Movimientos/Crearapertura
        [HttpPost("[action]")]
        public async Task<IActionResult> Crearapertura([FromBody] MovimientoMassiveCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;

            for (var i=0; i<model.origen.Length; i++)
                {
                Movimiento movimiento = new Movimiento
                {
                    empresaId = model.empresaId[i],
                    loteId = model.loteId[i],
                    asientoId = model.asientoId[i],
                    origen = model.origen[i],
                    grpconceptoId = model.grpconceptoId[i],
                    concepto = model.concepto[i],
                    fecha = model.fecha[i],
                    importe = model.importe[i],
                    ref0 = model.ref0[i],
                    ref1 = model.ref1[i],
                    ref2 = model.ref2[i],
                    ref3 = model.ref3[i],
                    ref4 = model.ref4[i],
                    ref5 = model.ref5[i],
                    ref6 = model.ref6[i],
                    ref7 = model.ref7[i],
                    ref8 = model.ref8[i],
                    ref9 = model.ref9[i],
                    etlId = model.etlId,
                    iduseralta = model.iduseralta,
                    fecalta = fechaHora,
                    iduserumod = model.iduseralta,
                    fecumod = fechaHora,
                    activo = true
                };

                _context.Movimientos.Add(movimiento);
            }

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


        // DELETE: api/Movimientos/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movimiento = await _context.Movimientos
                .FindAsync(id);

            if (movimiento == null)
            {
                return NotFound();
            }

            _context.Movimientos.Remove(movimiento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(movimiento);
        }

        // PUT: api/Movimientos/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var movimiento = await _context
                .Movimientos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (movimiento == null)
            {
                return NotFound();
            }

            movimiento.activo = false;

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

        // PUT: api/Movimientos/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var movimiento = await _context
                .Movimientos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (movimiento == null)
            {
                return NotFound();
            }

            movimiento.activo = true;

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

        private bool MovimientoExists(int id)
        {
            return _context.Movimientos.Any(e => e.Id == id);
        }
    }
}
