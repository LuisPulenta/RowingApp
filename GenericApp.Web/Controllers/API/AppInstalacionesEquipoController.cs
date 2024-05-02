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
           && (o.Fecha > DateTime.Now.AddDays(-5)
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
            return Ok();
        }

        //---------------------------------------------------------------------------------------------------
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCabeceraCertificacion([FromRoute] int id, [FromBody] CabeceraCertificacio request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != request.ID)
        //    {
        //        return BadRequest();
        //    }

        //    var oldCabeceraCertificacio = await _dataContext.CabeceraCertificacion.FindAsync(request.ID);
        //    if (oldCabeceraCertificacio == null)
        //    {
        //        return BadRequest("La CabeceraCertificacion no existe.");
        //    }

        //    oldCabeceraCertificacio.NROOBRA = request.NROOBRA;
        //    oldCabeceraCertificacio.DefProy = request.DefProy;
        //    oldCabeceraCertificacio.NombreObra = request.NombreObra;
        //    oldCabeceraCertificacio.NroOE = request.NroOE;
        //    oldCabeceraCertificacio.subCodigo = request.subCodigo;
        //    oldCabeceraCertificacio.CENTRAL = request.CENTRAL;
        //    oldCabeceraCertificacio.OBSERVACION = request.OBSERVACION;
        //    oldCabeceraCertificacio.FECHACORRESPONDENCIA = request.FECHACORRESPONDENCIA;
        //    oldCabeceraCertificacio.CODIGOPRODUCCION = request.CODIGOPRODUCCION;
        //    oldCabeceraCertificacio.VALORTOTALC = request.VALORTOTALC;
        //    oldCabeceraCertificacio.VALORTOTALT = request.VALORTOTALT;
        //    oldCabeceraCertificacio.CodCausanteC = request.CodCausanteC;
        //    oldCabeceraCertificacio.MesImputacion = request.MesImputacion;
        //    oldCabeceraCertificacio.Objeto = request.Objeto;
        //    oldCabeceraCertificacio.PorcActa = request.PorcActa;
        //    oldCabeceraCertificacio.CENTRAL = request.CENTRAL;

        //    oldCabeceraCertificacio.VALOR90 = request.VALORTOTALC;
        //    oldCabeceraCertificacio.VALORTOTAL = request.VALORTOTALC;
        //    oldCabeceraCertificacio.PRECIO90 = request.VALORTOTALC;

        //    _dataContext.CabeceraCertificacion.Update(oldCabeceraCertificacio);
        //    await _dataContext.SaveChangesAsync();
        //    return Ok();
        //}

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

        _dataContext.AppInstalacionesEquipos.Remove(instalacion);
        await _dataContext.SaveChangesAsync();
        return Ok("Instalación borrada");
        }
    }
}