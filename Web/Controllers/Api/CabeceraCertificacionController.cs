using Web.Data;
using RowingApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Request;

namespace RowingApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabeceraCertificacionController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CabeceraCertificacionController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //---------------------------------------------------------------------------------------------------
       
        [HttpPost]
        [Route("GetCabeceraCertificacion/{ProyectoModulo}/{UserId}")]
        public async Task<IActionResult> GetCabeceraCertificacion(string ProyectoModulo, int UserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var cabcert = await _dataContext.CabeceraCertificacion
           .Where(o => (
           o.Modulo == ProyectoModulo 
           && o.IdUsuario == UserId
           && (o.FECHACARGA > DateTime.Now.AddDays(-30)
           )

           ))
           .OrderBy(o => o.ID)
           .ToListAsync();
            if (cabcert == null)
            {
                return BadRequest("No hay CabecerasCertificacion.");
            }
            return Ok(cabcert);
        }

        //---------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("GetNroRegistroMaxCertificaciones")]
        public IActionResult GetNroRegistroMaxCertificaciones()
        {
            int query = _dataContext.CabeceraCertificacion.Max(c => c.ID);

            return Ok(query);
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPost]
        [Route("PostCabeceraCertificacion")]
        public async Task<IActionResult> PostCabeceraCertificacion([FromBody] CabeceraCertificacioRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CabeceraCertificacio newCertificado = new CabeceraCertificacio
            {
                ID = request.ID,
                NROOBRA = request.NROOBRA,
                DefProy = request.DefProy,
                FECHACARGA = request.FECHACARGA,
                FECHADESPACHO = request.FECHADESPACHO,
                FECHAEJECUCION = request.FECHAEJECUCION,
                NombreObra = request.NombreObra,
                NroOE = request.NroOE,
                FINALIZADA = 1,
                MATERIALESDESCONTADOS = 1,
                subCodigo = request.subCodigo,
                CENTRAL = request.CENTRAL,
                PREADICIONAL = 0,
                NroPre = request.NroPre,
                SIPA = "1",
                OBSERVACION = request.OBSERVACION,
                TIPIFICACION = "Normal",
                FECHACORRESPONDENCIA = request.FECHACORRESPONDENCIA,
                MARCADEVENTA = 0,
                NRO103 = "",
                NRO105 = "",
                IDUSUARIOP = 1,
                FECHALIBERACION = request.FECHALIBERACION,
                IDUSUARIOL = 1,
                NROORDENPAGO = 0,
                VALORTOTAL = request.VALORTOTAL,
                PAGAR90 = "N",
                VALOR90 = request.VALOR90,
                PRECIO90 = request.PRECIO90,
                MONTO90 = request.MONTO90,
                PAGAR10 = "N",
                VALOR10 = 0,
                PRECIO10 = 0,
                MONTO10 = 0,
                IDUSUARIOFR =0,
                FECHAFONDOREPARO = null,
                NROPAGOFR = 0,
                CODIGOPRODUCCION = request.CODIGOPRODUCCION,
                ObservacionO = "App",
                Clase = "Puntos",
                VALORTOTALC = request.VALORTOTALC,
                VALORTOTALT = request.VALORTOTALT,
                PorcAplicado = 100,
                PAGARX = "N",
                VALORX = 0,
                PRECIO10X = 0,
                MONTOX = 0,
                CodCausanteC = request.CodCausanteC,
                Cobrar = 1,
                Presentado = "No",
                Estado = "PEN",
                Modulo = request.Modulo,
                IdUsuario = request.IdUsuario,
                Terminal = request.Terminal,
                Fecha103 = null,
                Fecha105 = null,
                MesImputacion = request.MesImputacion,
                Objeto = request.Objeto,
                PorcActa = request.PorcActa
            };

            _dataContext.CabeceraCertificacion.Add(newCertificado);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        //---------------------------------------------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCabeceraCertificacion([FromRoute] int id, [FromBody] CabeceraCertificacio request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.ID)
            {
                return BadRequest();
            }

            var oldCabeceraCertificacio = await _dataContext.CabeceraCertificacion.FindAsync(request.ID);
            if (oldCabeceraCertificacio == null)
            {
                return BadRequest("La CabeceraCertificacion no existe.");
            }

            oldCabeceraCertificacio.NROOBRA = request.NROOBRA;
            oldCabeceraCertificacio.DefProy = request.DefProy;
            oldCabeceraCertificacio.NombreObra = request.NombreObra;
            oldCabeceraCertificacio.NroOE = request.NroOE;
            oldCabeceraCertificacio.subCodigo = request.subCodigo;
            oldCabeceraCertificacio.CENTRAL = request.CENTRAL;
            oldCabeceraCertificacio.OBSERVACION = request.OBSERVACION;
            oldCabeceraCertificacio.FECHACORRESPONDENCIA = request.FECHACORRESPONDENCIA;
            oldCabeceraCertificacio.CODIGOPRODUCCION = request.CODIGOPRODUCCION;
            oldCabeceraCertificacio.VALORTOTALC = request.VALORTOTALC;
            oldCabeceraCertificacio.VALORTOTALT = request.VALORTOTALT;
            oldCabeceraCertificacio.CodCausanteC = request.CodCausanteC;
            oldCabeceraCertificacio.MesImputacion = request.MesImputacion;
            oldCabeceraCertificacio.Objeto = request.Objeto;
            oldCabeceraCertificacio.PorcActa = request.PorcActa;
            oldCabeceraCertificacio.CENTRAL = request.CENTRAL;

            oldCabeceraCertificacio.VALOR90 = request.VALORTOTALC;
            oldCabeceraCertificacio.VALORTOTAL = request.VALORTOTALC;
            oldCabeceraCertificacio.PRECIO90 = request.VALORTOTALC;

            _dataContext.CabeceraCertificacion.Update(oldCabeceraCertificacio);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        //---------------------------------------------------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCabeceraCertificacion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var cabeceraCertificacion = await _dataContext.CabeceraCertificacion
                .FirstOrDefaultAsync(p => p.ID == id);
            if (cabeceraCertificacion == null)
            {
                return this.NotFound();
            }

            _dataContext.CabeceraCertificacion.Remove(cabeceraCertificacion);
            await _dataContext.SaveChangesAsync();
            return Ok("Cabecera Certificación borrada");
        }
    }
}