using RowingApp.Common.Helpers;
using RowingApp.Common.Requests;
using RowingApp.Common.Responses;
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
    public class ObrasReparosController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IFilesHelper _filesHelper;

        public ObrasReparosController(DataContext dataContext,IFilesHelper filesHelper)
        {
            _dataContext = dataContext;
            _filesHelper = filesHelper;
        }

        [HttpGet]
        [Route("GetNroRegistroMax")]
        public async Task<IActionResult> GetNroRegistroMax()
        {
            int query = _dataContext.ObrasReparos.Max(c => c.NROREGISTRO);

            return Ok(query);
        }
        
        [HttpPost]
        [Route("GetObrasReparos/{nroobra}")]
        public async Task<IActionResult> GetObrasReparos(int nroobra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var obrasReparos = await _dataContext.ObrasReparos
           .Where(o => (o.NROOBRA == nroobra)
           //&& (o.FECHACUMPLIMENTO == null)
          
           )
           .OrderBy(o => o.NROREGISTRO)
           .ToListAsync();


            if (obrasReparos == null)
            {
                return BadRequest("No hay Obras Reparos.");
            }
            return Ok(obrasReparos);
        }

        [HttpPost]
        [Route("GetObrasReparosByCodigo/{codigocausante}")]
        public async Task<IActionResult> GetObrasReparosByCodigo(String codigocausante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var obrasReparos = await _dataContext.ObrasReparos
           .Where(o => (o.CODCAUSANTE == codigocausante) && (o.FotoFin == null || o.FotoFin=="" ) && (o.FECHACUMPLIMENTO==null)

           )
           .OrderBy(o => o.NROREGISTRO)
           .ToListAsync();


            if (obrasReparos == null)
            {
                return BadRequest("No hay Obras Reparos.");
            }
            return Ok(obrasReparos);
        }

        [HttpPost]
        [Route("GetObrasReparosTodas")]
        public async Task<IActionResult> GetObrasReparosTodas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var obrasReparos = await _dataContext.ObrasReparos
           .Where(o => (o.FECHACUMPLIMENTO == null && o.LATITUD!="" && o.LONGITUD != "" && (o.FotoFin == null || o.FotoFin == ""))

           )
           .OrderBy(o => o.NROREGISTRO)
           .ToListAsync();


            if (obrasReparos == null)
            {
                return BadRequest("No hay Obras Reparos.");
            }
            return Ok(obrasReparos);
        }

        [HttpPost]
        [Route("PostObrasReparos")]
        public async Task<IActionResult> PostObrasReparos([FromBody] ObrasReparosRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Foto
            string imageUrl = string.Empty;
            if (request.ImageArray != null && request.ImageArray.Length > 0)
            {
                var stream1 = new MemoryStream(request.ImageArray);
                var guid1 = Guid.NewGuid().ToString();
                var file1 = $"{guid1}.jpg";
                var folder1 = "wwwroot\\images\\ObrasReparos";
                var fullPath1 = $"~/images/ObrasReparos/{file1}";
                var response1 = _filesHelper.UploadPhoto(stream1, folder1, file1);

                if (response1)
                {
                    imageUrl = fullPath1;
                }
            }

            //Foto Inicio
            string imageUrlInicio = string.Empty;
            if (request.FotoInicioArray != null && request.FotoInicioArray.Length > 0)
            {
                var stream2 = new MemoryStream(request.FotoInicioArray);
                var guid2 = Guid.NewGuid().ToString();
                var file2 = $"{guid2}.jpg";
                var folder2 = "wwwroot\\images\\ObrasReparos";
                var fullPath2 = $"~/images/ObrasReparos/{file2}";
                var response2 = _filesHelper.UploadPhoto(stream2, folder2, file2);

                if (response2)
                {
                    imageUrlInicio = fullPath2;
                }
            }

            //Foto Fin
            string imageUrlFin = string.Empty;
            if (request.FotoFinArray != null && request.FotoFinArray.Length > 0)
            {
                var stream3 = new MemoryStream(request.FotoFinArray);
                var guid3 = Guid.NewGuid().ToString();
                var file3 = $"{guid3}.jpg";
                var folder3 = "wwwroot\\images\\ObrasReparos";
                var fullPath3 = $"~/images/ObrasReparos/{file3}";
                var response3 = _filesHelper.UploadPhoto(stream3, folder3, file3);

                if (response3)
                {
                    imageUrlFin = fullPath3;
                }
            }


            var obrasReparo = new ObrasReparo
            {
                NROREGISTRO = request.NROREGISTRO,
                NROOBRA=request.NROOBRA,
                FECHAALTA= DateTime.Now,
                FECHAINICIO=null,
                FECHACUMPLIMENTO=null,
                REQUERIDOPOR="PorJO",
                SUBCONTRATISTA="",
                SUBCONTRATISTAREPARO = "",
                CODCAUSANTE = "",
                NROCTOC = "",
                DIRECCION=request.DIRECCION,
                ALTURA=request.ALTURA,
                LATITUD = request.LATITUD,
                LONGITUD = request.LONGITUD,
                CODTIPOSTDRPARO=request.CODTIPOSTDRPARO,
                ESTADOSUBCON="SinDatos",
                RECURSOS = "",
                MONTODISPONIBLE=null,
                GRUA = "",
                IDUsuario=request.IDUsuario,
                Terminal=null,
                Observaciones=request.Observaciones,
                Foto1 = imageUrl,
                FotoInicio=imageUrlInicio,
                FotoFin=imageUrlFin,
                TipoVereda=request.TipoVereda,
                CantidadMTL=request.CantidadMTL,
                Ancho=request.Ancho,
                Profundidad=request.Profundidad,
                FechaCierreElectrico=request.FechaCierreElectrico,
                ObservacionesFotoInicio=request.ObservacionesFotoInicio,
                ObservacionesFotoFin = request.ObservacionesFotoFin,
                Modulo = request.Modulo,
                Largo2=request.Largo2,
                Ancho2=request.Ancho2
            };
            _dataContext.ObrasReparos.Add(obrasReparo);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
                
        [HttpPut("{id}")]        
        public async Task<IActionResult> PutObrasReparo([FromRoute] int id, [FromBody] ObraReparoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.NROREGISTRO)
            {
                return BadRequest();
            }

            //Foto Inicio
            string imageUrlInicio = null;
            if (request.FotoInicioArray != null && request.FotoInicioArray.Length > 0)
            {
                var stream2 = new MemoryStream(request.FotoInicioArray);
                var guid2 = Guid.NewGuid().ToString();
                var file2 = $"{guid2}.jpg";
                var folder2 = "wwwroot\\images\\ObrasReparos";
                var fullPath2 = $"~/images/ObrasReparos/{file2}";
                var response2 = _filesHelper.UploadPhoto(stream2, folder2, file2);

                if (response2)
                {
                    imageUrlInicio = fullPath2;
                }
            }

            //Foto Fin
            string imageUrlFin = null;
            DateTime? fec = null;
            if (request.FotoFinArray != null && request.FotoFinArray.Length > 0)
            {

                fec = DateTime.Now;
                var stream3 = new MemoryStream(request.FotoFinArray);
                var guid3 = Guid.NewGuid().ToString();
                var file3 = $"{guid3}.jpg";
                var folder3 = "wwwroot\\images\\ObrasReparos";
                var fullPath3 = $"~/images/ObrasReparos/{file3}";
                var response3 = _filesHelper.UploadPhoto(stream3, folder3, file3);

                if (response3)
                {
                    imageUrlFin = fullPath3;
                }
            }

            var oldObraReparo = await _dataContext.ObrasReparos.FindAsync(request.NROREGISTRO);
            if (oldObraReparo == null)
            {
                return BadRequest("La Obra Reparo no existe.");
            }

            string obsInicio = null;
            string obsFin = null;

            if (request.ObservacionesFotoInicio.Length > 0)
            {
                obsInicio = request.ObservacionesFotoInicio;
            }

            if (request.ObservacionesFotoFin.Length > 0)
            {
                obsFin = request.ObservacionesFotoFin;
            }


            oldObraReparo.FECHACUMPLIMENTO = request.FECHACUMPLIMENTO;
            oldObraReparo.FotoInicio = imageUrlInicio;
            oldObraReparo.FotoFin = imageUrlFin;
            oldObraReparo.ObservacionesFotoInicio = obsInicio;
            oldObraReparo.ObservacionesFotoFin = obsFin;
            oldObraReparo.Ancho2 = request.Ancho2;
            oldObraReparo.Largo2 =request.Largo2;


            _dataContext.ObrasReparos.Update(oldObraReparo);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}