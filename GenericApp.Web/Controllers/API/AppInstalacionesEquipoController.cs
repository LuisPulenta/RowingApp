using GenericApp.Common.Helpers;
using GenericApp.Common.Requests;
using GenericApp.Web.Data;
using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppInstalacionesEquipoController : ControllerBase
    {
        private readonly DataContext2 _dataContext;
        private readonly IFilesHelper _filesHelper;

        public AppInstalacionesEquipoController(DataContext2 dataContext, IFilesHelper filesHelper)
        {
            _dataContext = dataContext;
            _filesHelper = filesHelper;
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPost]
        [Route("GetAppInstalacionesEquipo/{UserId}")]
        public async Task<IActionResult> GetAppInstalacionesEquipo(int UserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var instalaciones = await _dataContext.AppInstalacionesEquipos
           .Where(o => (
           o.IdUsuario == UserId
           && (o.Fecha > DateTime.Now.AddDays(-2)
           )

           ))
           .OrderBy(o => o.IDRegistro)
           .ToListAsync();
            if (instalaciones == null)
            {
                return BadRequest("No hay Instalaciones.");
            }
            return Ok(instalaciones);
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPost]
        [Route("GetAppInstalacionesEquiposDetalles/{IdInstalacionEquipo}")]
        public async Task<IActionResult> GetAppInstalacionesEquiposDetalles(int IdInstalacionEquipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var instalacionesDetalles = await _dataContext.AppInstalacionesEquiposDetalles
           .Where(o => (
           o.IDINSTALACIONEQUIPO == IdInstalacionEquipo
           ))
           .OrderBy(o => o.IDDETALLE)
           .ToListAsync();
            if (instalacionesDetalles == null)
            {
                return BadRequest("No hay InstalacionesDetalles.");
            }
            return Ok(instalacionesDetalles);
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPost]
        [Route("PostAppInstalacionesEquipo")]
        public async Task<IActionResult> PostAppInstalacionesEquipo([FromBody] AppInstalacionesEquipoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //FIRMACLIENTE
            string imageUrl = string.Empty;
            if (request.ImageArrayFIRMACLIENTE != null && request.ImageArrayFIRMACLIENTE.Length > 0)
            {
                var stream = new MemoryStream(request.ImageArrayFIRMACLIENTE);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\Instalaciones";
                var fullPath = $"~/images/Instalaciones/{file}";
                var response = _filesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl = fullPath;
                }
            }

            var appInstalacionesEquipo = new AppInstalacionesEquipo
            {
                ApellidoCliente = request.ApellidoCliente,
                Auditado = request.Auditado,
                EsAveria = request.EsAveria,
                NombreApellidoFirmante = request.NombreApellidoFirmante,
                Causante = request.Causante,
                Documento = request.Documento,
                DomicilioInstalacion = request.DomicilioInstalacion,
                EntreCalles = request.EntreCalles,
                Fecha = request.Fecha,
                FechaInstalacion = request.FechaInstalacion,
                Firmacliente = imageUrl,
                Grupo = request.Grupo,
                IdUsuario = request.IdUsuario,
                Imei = request.Imei,
                Latitud = request.Latitud,
                Longitud = request.Longitud,
                NombreCliente = request.NombreCliente,
                NroObra = request.NroObra,
                Pedido = request.Pedido,
                TipoInstalacion = request.TipoInstalacion
            };

            _dataContext.AppInstalacionesEquipos.Add(appInstalacionesEquipo);
            await _dataContext.SaveChangesAsync();
            return Ok(appInstalacionesEquipo);
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPost]
        [Route("PostAppInstalacionesEquiposDetalle")]
        public async Task<IActionResult> PostAppInstalacionesEquiposDetalle([FromBody] AppInstalacionesEquiposDetalle request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dataContext.AppInstalacionesEquiposDetalles.Add(request);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        //---------------------------------------------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppInstalacionesEquipo([FromRoute] int id, [FromBody] AppInstalacionesEquipoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.IDRegistro)
            {
                return BadRequest();
            }

            var oldInstalacion = await _dataContext.AppInstalacionesEquipos.FindAsync(request.IDRegistro);
            if (oldInstalacion == null)
            {
                return BadRequest("La Instalación no existe.");
            }

            string imageFirmaCliente = oldInstalacion.Firmacliente;

            if (request.ImageArrayFIRMACLIENTE != null && request.ImageArrayFIRMACLIENTE.Length > 0)
            {

                var stream = new MemoryStream(request.ImageArrayFIRMACLIENTE);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\Instalaciones";
                var fullPath = $"~/images/Instalaciones/{file}";
                var response = _filesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageFirmaCliente = fullPath;
                }
            }

            oldInstalacion.ApellidoCliente = request.ApellidoCliente;
            oldInstalacion.Causante = request.Causante;
            oldInstalacion.Documento = request.Documento;
            oldInstalacion.DomicilioInstalacion = request.DomicilioInstalacion;
            oldInstalacion.EntreCalles = request.EntreCalles;
            oldInstalacion.EsAveria = request.EsAveria;
            oldInstalacion.Fecha = request.Fecha;
            oldInstalacion.FechaInstalacion = request.FechaInstalacion;
            oldInstalacion.Firmacliente = imageFirmaCliente;
            oldInstalacion.Grupo = request.Grupo;
            oldInstalacion.Imei = request.Imei;
            oldInstalacion.Latitud = request.Latitud;
            oldInstalacion.Longitud = request.Longitud;
            oldInstalacion.NombreApellidoFirmante = request.NombreApellidoFirmante;
            oldInstalacion.NombreCliente = request.NombreCliente;
            oldInstalacion.NroObra = request.NroObra;
            oldInstalacion.Pedido = request.Pedido;
            oldInstalacion.TipoInstalacion = request.TipoInstalacion;

            _dataContext.AppInstalacionesEquipos.Update(oldInstalacion);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        //---------------------------------------------------------------------------------------------------
        [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppInstalacionesEquipo([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return this.BadRequest(ModelState);
        }

        var instalacion = await _dataContext.AppInstalacionesEquipos
            .FirstOrDefaultAsync(p => p.IDRegistro == id);
        if (instalacion == null)
        {
            return this.NotFound();
        }

            var instalacionesDetalles = await _dataContext.AppInstalacionesEquiposDetalles
             .Where(o => o.IDINSTALACIONEQUIPO == id)
             .ToListAsync();

            foreach(AppInstalacionesEquiposDetalle instalacionDetalle in instalacionesDetalles)
            {
                _dataContext.AppInstalacionesEquiposDetalles.Remove(instalacionDetalle);
                await _dataContext.SaveChangesAsync();
            }

            _dataContext.AppInstalacionesEquipos.Remove(instalacion);
        await _dataContext.SaveChangesAsync();
        return Ok("Instalación borrada");
        }
    }
}