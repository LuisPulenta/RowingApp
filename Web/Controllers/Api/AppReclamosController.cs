using RowingApp.Common.Helpers;
using RowingApp.Common.Requests;
using Web.Data;
using RowingApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RowingApp.Web.Controllers.API
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

            //FOTO1
            string imageUrl1 = string.Empty;
            if (request.ArrayFoto1 != null && request.ArrayFoto1.Length > 0)
            {
                var stream1= new MemoryStream(request.ArrayFoto1);
                var guid1 = Guid.NewGuid().ToString();
                var file1 = $"{guid1}.jpg";
                var folder1 = "wwwroot\\images\\AppReclamos";
                var fullPath1 = $"~/images/AppReclamos/{file1}";
                var response1 = _filesHelper.UploadPhoto(stream1, folder1, file1);

                if (response1)
                {
                    imageUrl1 = fullPath1;
                }
            }

            //FOTO2
            string imageUrl2 = string.Empty;
            if (request.ArrayFoto2 != null && request.ArrayFoto2.Length > 0)
            {
                var stream2 = new MemoryStream(request.ArrayFoto2);
                var guid2 = Guid.NewGuid().ToString();
                var file2 = $"{guid2}.jpg";
                var folder2 = "wwwroot\\images\\AppReclamos";
                var fullPath2 = $"~/images/AppReclamos/{file2}";
                var response2 = _filesHelper.UploadPhoto(stream2, folder2, file2);

                if (response2)
                {
                    imageUrl2 = fullPath2;
                }
            }

            //FOTO3
            string imageUrl3 = string.Empty;
            if (request.ArrayFoto3 != null && request.ArrayFoto3.Length > 0)
            {
                var stream3 = new MemoryStream(request.ArrayFoto3);
                var guid3 = Guid.NewGuid().ToString();
                var file3 = $"{guid3}.jpg";
                var folder3 = "wwwroot\\images\\AppReclamos";
                var fullPath3 = $"~/images/AppReclamos/{file3}";
                var response3 = _filesHelper.UploadPhoto(stream3, folder3, file3);

                if (response3)
                {
                    imageUrl3 = fullPath3;
                }
            }

            //PDF1
            string imageUrl4 = string.Empty;
            if (request.ArrayPdf1 != null && request.ArrayPdf1.Length > 0)
            {
                var stream4 = new MemoryStream(request.ArrayPdf1);
                var guid4 = Guid.NewGuid().ToString();
                var file4 = $"{guid4}.pdf";
                var folder4 = "wwwroot\\images\\AppReclamos";
                var fullPath4 = $"~/images/AppReclamos/{file4}";
                var response4 = _filesHelper.UploadPhoto(stream4, folder4, file4);

                if (response4)
                {
                    imageUrl4 = fullPath4;
                }
            }

            //PDF2
            string imageUrl5 = string.Empty;
            if (request.ArrayPdf2 != null && request.ArrayPdf2.Length > 0)
            {
                var stream5 = new MemoryStream(request.ArrayPdf2);
                var guid5 = Guid.NewGuid().ToString();
                var file5 = $"{guid5}.pdf";
                var folder5 = "wwwroot\\images\\AppReclamos";
                var fullPath5 = $"~/images/AppReclamos/{file5}";
                var response5 = _filesHelper.UploadPhoto(stream5, folder5, file5);

                if (response5)
                {
                    imageUrl5 = fullPath5;
                }
            }

            //PDF3
            string imageUrl6 = string.Empty;
            if (request.ArrayPdf3 != null && request.ArrayPdf3.Length > 0)
            {
                var stream6 = new MemoryStream(request.ArrayPdf3);
                var guid6 = Guid.NewGuid().ToString();
                var file6 = $"{guid6}.pdf";
                var folder6 = "wwwroot\\images\\AppReclamos";
                var fullPath6 = $"~/images/AppReclamos/{file6}";
                var response6 = _filesHelper.UploadPhoto(stream6, folder6, file6);

                if (response6)
                {
                    imageUrl6 = fullPath6;
                }
            }

            var appReclamo = new AppReclamo
            {
                Apellido=request.Apellido,
                ApellidoRepresentante = request.ApellidoRepresentante,
                CodPostal = request.CodPostal,
                CodPostalContacto = request.CodPostalContacto,
                CoincideDireccion = request.CoincideDireccion,
                Correo = request.Correo,
                Direccion = request.Direccion,
                DireccionContacto = request.DireccionContacto,
                DNI = request.DNI,
                DNIRepresentante = request.DNIRepresentante,
                FechaCarga=DateTime.Now,
                Foto1=imageUrl1,
                Foto2 = imageUrl2,
                Foto3 = imageUrl3,
                Pdf1= imageUrl4,
                Pdf2 = imageUrl5,
                Pdf3 = imageUrl6,
                Localidad = request.Localidad,
                LocalidadContacto = request.LocalidadContacto,
                Nis = request.Nis,
                Nombre = request.Nombre,
                NombrePropio = request.NombrePropio,
                NombreRepresentante = request.NombreRepresentante,
                NroCuenta = request.NroCuenta,
                Reclamo = request.Reclamo,
                Telefono = request.Telefono,
                ErroresEnFacturacion = request.ErroresEnFacturacion,
                ResarcimientoPorDanios = request.ResarcimientoPorDanios,
                SuspensionDeSuministro = request.SuspensionDeSuministro,
                MalaAtencionComercial = request.MalaAtencionComercial,
                NegativaDeConexion = request.NegativaDeConexion,
                InconvenienteDeTension = request.InconvenienteDeTension,
                FacturaFueraDeTerminoNoRecibidas = request.FacturaFueraDeTerminoNoRecibidas 
            };

            _dataContext.AppReclamos.Add(appReclamo);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}
