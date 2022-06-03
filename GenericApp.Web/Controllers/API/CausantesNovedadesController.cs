using GenericApp.Common.Helpers;
using GenericApp.Common.Requests;
using GenericApp.Common.Responses;
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
    public class CausantesNovedadesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IFilesHelper _filesHelper;

        public CausantesNovedadesController(DataContext dataContext,IFilesHelper filesHelper)
        {
            _dataContext = dataContext;
            _filesHelper = filesHelper;
        }

        [HttpGet]
        [Route("GetTipoNovedades")]
        public async Task<IActionResult> GetTipoNovedades()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var tipoNovedades = await _dataContext.TipoNovedad
                     .OrderBy(o => o.TIPODENOVEDAD)
           .ToListAsync();
            if (tipoNovedades == null)
            {
                return BadRequest("No hay Tipos Novedades.");
            }
            return Ok(tipoNovedades);
        }


        [HttpPost]
        [Route("GetNovedades/{grupo}/{causante}")]
        public async Task<IActionResult> GetNovedades(string Grupo,string Causante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var novedades = await _dataContext.CausantesNovedades
           .Where(o => ((o.GRUPO == Grupo) && (o.CAUSANTE == Causante) && (o.FECHACARGA.AddDays(30) >= DateTime.Now) && (o.Estado != "Pendiente"))
           || ((o.GRUPO == Grupo) && (o.CAUSANTE == Causante) && (o.Estado == "Pendiente"))
           || ((o.GRUPO == Grupo) && (o.CAUSANTE == Causante) && (o.ConfirmaLeido != 1))
           )

           .OrderBy(o => o.FECHACARGA)
           .ToListAsync();


            if (novedades == null)
            {
                return BadRequest("No hay Novedades.");
            }
            return Ok(novedades);
        }

        [HttpPost]
        [Route("PostNovedades")]
        public async Task<IActionResult> PostNovedades([FromBody] CausantesNovedadeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Foto1
            string imageUrl1 = string.Empty;
            if (request.ImageArray1 != null && request.ImageArray1.Length > 0)
            {
                var stream1 = new MemoryStream(request.ImageArray1);
                var guid1 = Guid.NewGuid().ToString();
                var file1 = $"{guid1}.jpg";
                var folder1 = "wwwroot\\images\\Novedades";
                var fullPath1 = $"~/images/Novedades/{file1}";
                var response1 = _filesHelper.UploadPhoto(stream1, folder1, file1);

                if (response1)
                {
                    imageUrl1 = fullPath1;
                }
            }

            //Foto2
            string imageUrl2 = string.Empty;
            if (request.ImageArray2 != null && request.ImageArray2.Length > 0)
            {
                var stream2 = new MemoryStream(request.ImageArray2);
                var guid2 = Guid.NewGuid().ToString();
                var file2 = $"{guid2}.jpg";
                var folder2 = "wwwroot\\images\\Novedades";
                var fullPath2 = $"~/images/Novedades/{file2}";
                var response2 = _filesHelper.UploadPhoto(stream2, folder2, file2);

                if (response2)
                {
                    imageUrl2 = fullPath2;
                }
            }

            var causantesNovedade = new CausantesNovedade
            {
                //NROREGISTRO = request.NROREGISTRO,
                CAUSANTE=request.CAUSANTE,
                EMPRESA = request.EMPRESA,
                FECHACARGA = request.FECHACARGA,
                FECHAFIN = request.FECHAFIN,
                FECHAINICIO = request.FECHAINICIO,
                FECHANOVEDAD = request.FECHANOVEDAD,
                GRUPO = request.GRUPO,
                LinkAdjunto1= imageUrl1,
                LinkAdjunto2= imageUrl2,
                OBSERVACIONES = request.OBSERVACIONES,
                TIPONOVEDAD = request.TIPONOVEDAD,
                Idusuario = request.Idusuario,
                VistaRRHH = request.VistaRRHH,
                FechaEstado=request.FechaEstado,
                ObservacionEstado = request.ObservacionEstado,
                ConfirmaLeido = request.ConfirmaLeido,
                IDUsrEstado = request.IDUsrEstado,
                Estado = request.Estado
            };
            _dataContext.CausantesNovedades.Add(causantesNovedade);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNovedad([FromRoute] int id, [FromBody] NovedadRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.IDNOVEDAD)
            {
                return BadRequest();
            }

            var oldNovedad = await _dataContext.CausantesNovedades.FindAsync(request.IDNOVEDAD);
            if (oldNovedad == null)
            {
                return BadRequest("La Novedad no existe.");
            }

            oldNovedad.ConfirmaLeido = 1;

            _dataContext.CausantesNovedades.Update(oldNovedad);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}