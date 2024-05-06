using GenericApp.Common.Helpers;
using GenericApp.Common.Requests;
using GenericApp.Web.Data;
using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppReclamosController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IFilesHelper _filesHelper;

        public AppReclamosController(DataContext dataContext, IFilesHelper filesHelper)
        {
            _dataContext = dataContext;
            _filesHelper = filesHelper;
        }

        //---------------------------------------------------------------------------------------------------

        [HttpPost]
        [Route("PostAppReclamos")]
        public async Task<IActionResult> PostAppReclamos([FromBody] AppReclamoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //FOTO
            string imageUrl = string.Empty;
            if (request.ArrayFoto != null && request.ArrayFoto.Length > 0)
            {
                var stream = new MemoryStream(request.ArrayFoto);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\AppReclamos";
                var fullPath = $"~/images/AppReclamos/{file}";
                var response = _filesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl = fullPath;
                }
            }

            var appReclamo = new AppReclamo
            {
                Apellido=request.Apellido,
                ApellidoRepresentante = request.ApellidoRepresentante,
                CodPostal = request.CodPostal,
                CodPostalContacto = request.CodPostalContacto,
                CoincideDireccion = request.CoincideDireccion,
                Contacto = request.Contacto,
                Correo = request.Correo,
                Direccion = request.Direccion,
                DireccionContacto = request.DireccionContacto,
                DNI = request.DNI,
                DNIRepresentante = request.DNIRepresentante,
                FechaCarga=DateTime.Now,
                Foto=imageUrl,
                Localidad = request.Localidad,
                LocalidadContacto = request.LocalidadContacto,
                Nis = request.Nis,
                Nombre = request.Nombre,
                NombrePropio = request.NombrePropio,
                NombreRepresentante = request.NombreRepresentante,
                NroCuenta = request.NroCuenta,
                Reclamo = request.Reclamo,
                Telefono = request.Telefono,
                TipoReclamo = request.TipoReclamo,
            };

            _dataContext.AppReclamos.Add(appReclamo);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}