using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GenericApp.Web.Data;
using GenericApp.Common.Responses;
using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Linq;
using GenericApp.Common.Helpers;
using GenericApp.Common.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ObrasDocumentsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IFilesHelper _filesHelper;

        public ObrasDocumentsController(DataContext context, IFilesHelper filesHelper)
        {
            _context = context;
            _filesHelper = filesHelper;
        }

        // POST: api/ObrasDocuments

        [HttpPost]
        [Route("ObrasDocument")]
        public async Task<IActionResult> PostObrasDocument([FromBody] ObrasDocumentoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Foto
            var imageUrl1 = string.Empty;
            var stream = new MemoryStream(request.ImageArray);
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";
            var folder = "wwwroot\\images\\Obras";
            var fullPath = $"~/images/Obras/{file}";
            var response = _filesHelper.UploadPhoto(stream, folder, file);

            if (response)
            {
                imageUrl1 = fullPath;
            }

            Obra obra = await _context.Obras
                .FirstOrDefaultAsync(o => o.NroObra == request.Obra.NroObra);

            var obraDocumento = new ObrasDocumento
            {
                //NROREGISTRO = request.NROREGISTRO,
                LINK= imageUrl1,
                FECHA=request.FECHA,
                NROOBRA=request.NROOBRA,
                IDObrasPostes = request.IDObrasPostes,
                OBSERVACION=request.OBSERVACION,
                Estante=request.Estante,
                GeneradoPor=request.GeneradoPor,
                MODULO=request.MODULO,
                NroLote=request.NroLote,
                Sector=request.Sector,
                Latitud = request.Latitud,
                Longitud = request.Longitud,
                FechaHsFoto = request.FechaHsFoto,
                TipoDeFoto = request.TipoDeFoto,
                DireccionFoto = request.DireccionFoto
    };

            _context.ObrasDocumentos.Add(obraDocumento);
            await _context.SaveChangesAsync();

            return Ok(obraDocumento);
        }

        [HttpPost]
        [Route("ObrasDocument2")]
        public async Task<IActionResult> PostObrasDocument2([FromBody] ObrasDocumentoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Foto
            var imageUrl1 = string.Empty;
            var stream = new MemoryStream(request.ImageArray);
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";
            var folder = "wwwroot\\images\\Medidores";
            var fullPath = $"~/images/Medidores/{file}";
            var response = _filesHelper.UploadPhoto(stream, folder, file);

            if (response)
            {
                imageUrl1 = fullPath;
            }

            Obra obra = await _context.Obras
                .FirstOrDefaultAsync(o => o.NroObra == request.Obra.NroObra);

            var obraDocumento = new ObrasDocumento
            {
                //NROREGISTRO = request.NROREGISTRO,
                LINK = imageUrl1,
                FECHA = request.FECHA,
                NROOBRA = request.NROOBRA,
                IDObrasPostes = request.IDObrasPostes,
                OBSERVACION = request.OBSERVACION,
                Estante = request.Estante,
                GeneradoPor = request.GeneradoPor,
                MODULO = request.MODULO,
                NroLote = request.NroLote,
                Sector = request.Sector,
                Latitud = request.Latitud,
                Longitud = request.Longitud,
                FechaHsFoto = request.FechaHsFoto,
                TipoDeFoto = request.TipoDeFoto,
                DireccionFoto = request.DireccionFoto
            };

            _context.ObrasDocumentos.Add(obraDocumento);
            await _context.SaveChangesAsync();

            return Ok(obraDocumento);
        }

        // DELETE: api/ObrasDocumentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteObrasDocumento([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var ObrasDocumento = await _context.ObrasDocumentos
                .FirstOrDefaultAsync(p => p.NROREGISTRO == id);
            if (ObrasDocumento == null)
            {
                return this.NotFound();
            }

            _context.ObrasDocumentos.Remove(ObrasDocumento);
            await _context.SaveChangesAsync();
            return Ok("ObrasDocumento borrado");
        }

        [HttpPost]
        [Route("GetObrasDocumentos/{IDObrasPostes}")]
        public async Task<IActionResult> GetObrasDocumentos(int IDObrasPostes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var obrasDocumentos = await _context.ObrasDocumentos
            .Where(o => (o.IDObrasPostes == IDObrasPostes))
           .OrderBy(o => o.TipoDeFoto)
           .ToListAsync();

            if (obrasDocumentos == null)
            {
                return BadRequest("No hay Fotos para este Ticket.");
            }

            return Ok(obrasDocumentos);
        }
    }
}

