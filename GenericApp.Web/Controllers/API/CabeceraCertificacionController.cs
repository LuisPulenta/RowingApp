using GenericApp.Web.Data;
using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
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
        public async Task<IActionResult> GetNroRegistroMaxCertificaciones()
        {
            int query = _dataContext.CabeceraCertificacion.Max(c => c.ID);

            return Ok(query);
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPost]
        [Route("PostCabeceraCertificacion")]
        public async Task<IActionResult> PostCabeceraCertificacion([FromBody] CabeceraCertificacio request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dataContext.CabeceraCertificacion.Add(request);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        //---------------------------------------------------------------------------------------------------
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

            _dataContext.CabeceraCertificacion.Update(request);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}