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
           .Where(o => (o.NROOBRA == nroobra) && (o.FECHACUMPLIMENTO == null)
          
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
           .Where(o => (o.FECHACUMPLIMENTO == null && o.LATITUD!="" && o.LONGITUD != "")

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
                CODTIPOSTDRPARO=null,
                ESTADOSUBCON="SinDatos",
                RECURSOS = "",
                MONTODISPONIBLE=null,
                GRUA = "",
                IDUsuario=request.IDUsuario,
                Terminal=null,
                Observaciones=request.Observaciones,
                Foto1 = imageUrl,
                TipoVereda=request.TipoVereda,
                CantidadMTL=request.CantidadMTL,
                Ancho=request.Ancho,
                Profundidad=request.Profundidad,
                FechaCierreElectrico=request.FechaCierreElectrico
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

            var oldObraReparo = await _dataContext.ObrasReparos.FindAsync(request.NROREGISTRO);
            if (oldObraReparo == null)
            {
                return BadRequest("La Obra Reparo no existe.");
            }

            oldObraReparo.FECHACUMPLIMENTO = request.FECHACUMPLIMENTO;

            _dataContext.ObrasReparos.Update(oldObraReparo);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}