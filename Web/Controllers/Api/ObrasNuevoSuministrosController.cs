using RowingApp.Common.Helpers;
using RowingApp.Common.Requests;
using Web.Data;
using RowingApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RowingApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObrasNuevoSuministrosController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IFilesHelper _filesHelper;

        public ObrasNuevoSuministrosController(DataContext dataContext,IFilesHelper filesHelper)
        {
            _dataContext = dataContext;
            _filesHelper = filesHelper;
        }

        [HttpGet]
        [Route("GetObrasNuevoSuministros")]
        public async Task<IActionResult> GetObrasNuevoSuministros()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var obrasNuevoSuministros = await _dataContext.ObrasNuevoSuministros
                     .OrderBy(o => o.NROSUMINISTRO)
           .ToListAsync();
            if (obrasNuevoSuministros == null)
            {
                return BadRequest("No hay ObrasNuevoSuministros.");
            }
            return Ok(obrasNuevoSuministros);
        }


        [HttpPost]
        [Route("PostObrasNuevoSuministros")]
        public async Task<IActionResult> PostObrasNuevoSuministros([FromBody] ObrasNuevoSuministrosRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //FotoAntes1
            string imageUrl1 = string.Empty;
            if (request.ImageArrayANTESFOTO1 != null && request.ImageArrayANTESFOTO1.Length > 0)
            {
                var stream1 = new MemoryStream(request.ImageArrayANTESFOTO1);
                var guid1 = Guid.NewGuid().ToString();
                var file1 = $"{guid1}.jpg";
                var folder1 = "wwwroot\\images\\ObrasSuministros";
                var fullPath1 = $"~/images/ObrasSuministros/{file1}";
                var response1 = _filesHelper.UploadPhoto(stream1, folder1, file1);

                if (response1)
                {
                    imageUrl1 = fullPath1;
                }
            }

            //FotoAntes2
            string imageUrl2 = string.Empty;
            if (request.ImageArrayANTESFOTO2 != null && request.ImageArrayANTESFOTO2.Length > 0)
            {
                var stream2 = new MemoryStream(request.ImageArrayANTESFOTO2);
                var guid2 = Guid.NewGuid().ToString();
                var file2 = $"{guid2}.jpg";
                var folder2 = "wwwroot\\images\\ObrasSuministros";
                var fullPath2 = $"~/images/ObrasSuministros/{file2}";
                var response2 = _filesHelper.UploadPhoto(stream2, folder2, file2);

                if (response2)
                {
                    imageUrl2 = fullPath2;
                }
            }

            //FotoDespues1
            string imageUrl3 = string.Empty;
            if (request.ImageArrayDESPUESFOTO1 != null && request.ImageArrayDESPUESFOTO1.Length > 0)
            {
                var stream3 = new MemoryStream(request.ImageArrayDESPUESFOTO1);
                var guid3 = Guid.NewGuid().ToString();
                var file3 = $"{guid3}.jpg";
                var folder3 = "wwwroot\\images\\ObrasSuministros";
                var fullPath3 = $"~/images/ObrasSuministros/{file3}";
                var response3 = _filesHelper.UploadPhoto(stream3, folder3, file3);

                if (response3)
                {
                    imageUrl3 = fullPath3;
                }
            }

            //FotoDespues2
            string imageUrl4 = string.Empty;
            if (request.ImageArrayDESPUESFOTO2 != null && request.ImageArrayDESPUESFOTO2.Length > 0)
            {
                var stream4 = new MemoryStream(request.ImageArrayDESPUESFOTO2);
                var guid4 = Guid.NewGuid().ToString();
                var file4 = $"{guid4}.jpg";
                var folder4 = "wwwroot\\images\\ObrasSuministros";
                var fullPath4 = $"~/images/ObrasSuministros/{file4}";
                var response4 = _filesHelper.UploadPhoto(stream4, folder4, file4);

                if (response4)
                {
                    imageUrl4 = fullPath4;
                }
            }

            //FotoDNIFrente
            string imageUrl5 = string.Empty;
            if (request.ImageArrayFOTODNIFRENTE != null && request.ImageArrayFOTODNIFRENTE.Length > 0)
            {
                var stream5 = new MemoryStream(request.ImageArrayFOTODNIFRENTE);
                var guid5 = Guid.NewGuid().ToString();
                var file5 = $"{guid5}.jpg";
                var folder5 = "wwwroot\\images\\ObrasSuministros";
                var fullPath5 = $"~/images/ObrasSuministros/{file5}";
                var response5 = _filesHelper.UploadPhoto(stream5, folder5, file5);

                if (response5)
                {
                    imageUrl5 = fullPath5;
                }
            }

            //FotoDNIReverso
            string imageUrl6 = string.Empty;
            if (request.ImageArrayFOTODNIREVERSO != null && request.ImageArrayFOTODNIREVERSO.Length > 0)
            {
                var stream6 = new MemoryStream(request.ImageArrayFOTODNIREVERSO);
                var guid6 = Guid.NewGuid().ToString();
                var file6 = $"{guid6}.jpg";
                var folder6 = "wwwroot\\images\\ObrasSuministros";
                var fullPath6 = $"~/images/ObrasSuministros/{file6}";
                var response6 = _filesHelper.UploadPhoto(stream6, folder6, file6);

                if (response6)
                {
                    imageUrl6 = fullPath6;
                }
            }

            //FIRMACLIENTE
            string imageUrl7 = string.Empty;
            if (request.ImageArrayFIRMACLIENTE != null && request.ImageArrayFIRMACLIENTE.Length > 0)
            {
                var stream7 = new MemoryStream(request.ImageArrayFIRMACLIENTE);
                var guid7 = Guid.NewGuid().ToString();
                var file7 = $"{guid7}.jpg";
                var folder7 = "wwwroot\\images\\ObrasSuministros";
                var fullPath7 = $"~/images/ObrasSuministros/{file7}";
                var response7 = _filesHelper.UploadPhoto(stream7, folder7, file7);

                if (response7)
                {
                    imageUrl7 = fullPath7;
                }
            }

            var obrasNuevoSuministro = new ObrasNuevoSuministro
            {
                ANTESFOTO1=imageUrl1,
                ANTESFOTO2=imageUrl2,
                APELLIDONOMBRE = request.APELLIDONOMBRE,
                BARRIO = request.BARRIO,
                CAUSANTEC = request.CAUSANTEC,
                CONEXIONDIRECTA = request.CONEXIONDIRECTA,
                CORTE = request.CORTE,
                CUADRILLA = request.CUADRILLA,
                DENUNCIA = request.DENUNCIA,
                DESPUESFOTO1 = imageUrl3,
                DESPUESFOTO2 = imageUrl4,
                DIRECTA = request.DIRECTA,
                DNI = request.DNI,
                DOMICILIO = request.DOMICILIO,
                EMAIL = request.EMAIL,
                ENRE = request.ENRE,
                ENTRECALLES1 = request.ENTRECALLES1,
                ENTRECALLES2 = request.ENTRECALLES2,
                FECHA = request.FECHA,
                FIRMACLIENTE=imageUrl7,
                FOTODNIFRENTE = imageUrl5,
                FOTODNIREVERSO = imageUrl6,
                GRUPOC = request.GRUPOC,
                KITNRO = request.KITNRO,
                IDCERTIFBAREMO = request.IDCERTIFBAREMO,
                IDCERTIFMATERIALES = request.IDCERTIFMATERIALES,
                LOCALIDAD = request.LOCALIDAD,
                MEDIDORCOLOCADO = request.MEDIDORCOLOCADO,
                MEDIDORVECINO = request.MEDIDORVECINO,
                MTSCABLERETIRADO = request.MTSCABLERETIRADO,
                NROOBRA = request.NROOBRA,
                //NROSUMINISTRO = request.NROSUMINISTRO,
                OBSERVACIONES = request.OBSERVACIONES,
                OTRO = request.OTRO,
                PARTIDO = request.PARTIDO,
                POSTEPODRIDO = request.POSTEPODRIDO,
                POTENCIACONTRATADA = request.POTENCIACONTRATADA,
                RETIROCONEXION = request.RETIROCONEXION,
                RETIROCRUCECALLE = request.RETIROCRUCECALLE,
                TELEFONO = request.TELEFONO,
                TENSIONCONTRATADA = request.TENSIONCONTRATADA,
                TIPORED = request.TIPORED,
                TRABAJOCONHIDRO = request.TRABAJOCONHIDRO,
                PosX=request.PosX,
                PosY = request.PosY,
                IDUserCarga = request.IDUserCarga,
            };
            _dataContext.ObrasNuevoSuministros.Add(obrasNuevoSuministro);
            await _dataContext.SaveChangesAsync();

            int query = _dataContext.ObrasNuevoSuministros.Max(c => c.NROSUMINISTRO);

            return Ok(query);
        }
    }
}