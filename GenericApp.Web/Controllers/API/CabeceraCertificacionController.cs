using GenericApp.Common.Helpers;
using GenericApp.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabeceraCertificacion : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IFilesHelper _filesHelper;

        public CabeceraCertificacion(DataContext dataContext,IFilesHelper filesHelper)
        {
            _dataContext = dataContext;
            _filesHelper = filesHelper;
        }

        //---------------------------------------------------------------------------------------------------
        [HttpPut]
        [Route("PutDatosObra/{id}")]

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
        public async Task<IActionResult> GetNroRegistroMaxCertificaciones()
        {
            int query = _dataContext.CabeceraCertificacion.Max(c => c.ID);

            return Ok(query);
        }

        //---------------------------------------------------------------------------------------------------

        //public async Task<IActionResult> PutDatosObra([FromRoute] int id, [FromBody] ObraDatosRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != request.NroObra)
        //    {
        //        return BadRequest();
        //    }

        //    var oldObra = await _dataContext.Obras.FindAsync(request.NroObra);
        //    if (oldObra == null)
        //    {
        //        return BadRequest("La Obra no existe.");
        //    }

        //    oldObra.POSX = request.POSX;
        //    oldObra.POSY = request.POSY;
        //    oldObra.Direccion = request.Direccion;
        //    oldObra.TextoLocalizacion = request.TextoLocalizacion;
        //    oldObra.TextoClase = request.TextoClase;
        //    oldObra.TextoTipo = request.TextoTipo;
        //    oldObra.TextoComponente = request.TextoComponente;
        //    oldObra.CodigoDiametro = request.CodigoDiametro;
        //    oldObra.Motivo = request.Motivo;
        //    oldObra.Planos = request.Planos;

        //    _dataContext.Obras.Update(oldObra);
        //    await _dataContext.SaveChangesAsync();
        //    return Ok();
        //}
    }
}