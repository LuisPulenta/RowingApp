using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using RowingApp.Common.Helpers;
using RowingApp.Common.Requests;
using Web.Data;
using RowingApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace RowingApp.Web.Controllers.API
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

        //------------------------------------------------------------------------------------------------------------------------
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
        //------------------------------------------------------------------------------------------------------------------------
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

        //------------------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("UploadAudioFile")]
        public async Task<IActionResult> UploadAudioFile(IFormFile file)
        {

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file provided.");
            }

            var guid = Guid.NewGuid().ToString();
            //var fileName = $"{guid}.wav";
            var fileName = $"{file.FileName}.wav"; 
            var filePath = Path.Combine("C:/inetpub/wwwroot/RowingAppApi/wwwroot/images/", "Multimedia/", fileName);
            //var filePath = Path.Combine("D:/Xamarin/RowingApp/RowingApp.Web/wwwroot/images/", "Multimedia/", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(fileName);
        }
        //------------------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("UploadVideoFile")]
        public async Task<IActionResult> UploadVideoFile(IFormFile file)
        {

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file provided.");
            }

            var guid = Guid.NewGuid().ToString();
            var fileName = $"{file.FileName}.mp4";
            var filePath = Path.Combine("C:/inetpub/wwwroot/RowingAppApi/wwwroot/images/", "Multimedia/", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(fileName);
        }
        //------------------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("ObrasDocumentMultimediaAudio")]
        public async Task<IActionResult> PostObrasDocumentMultimediaAudio([FromBody] ObrasDocumentoRequest3 request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Obra obra = await _context.Obras
                .FirstOrDefaultAsync(o => o.NroObra == request.Obra.NroObra);

            var obraDocumento = new ObrasDocumento
            {
                //NROREGISTRO = request.NROREGISTRO,
                LINK = request.LINK,
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
        //------------------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("ObrasDocumentMultimediaVideo")]
        public async Task<IActionResult> PostObrasDocumentMultimediaVideo([FromBody] ObrasDocumentoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Obra obra = await _context.Obras
                .FirstOrDefaultAsync(o => o.NroObra == request.Obra.NroObra);

            var obraDocumento = new ObrasDocumento
            {
                //NROREGISTRO = request.NROREGISTRO,
                LINK = request.LINK,
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

        //------------------------------------------------------------------------------------------------------------------------
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
        //------------------------------------------------------------------------------------------------------------------------
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
        //------------------------------------------------------------------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> PutObrasDocumentos([FromRoute] int id, [FromBody] ObrasDocumentoRequest2 request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.NROREGISTRO)
            {
                return BadRequest();
            }

            var oldObraDocumento = await _context.ObrasDocumentos.FindAsync(request.NROREGISTRO);
            if (oldObraDocumento == null)
            {
                return BadRequest("ObraDocumento no existe.");
            }

            oldObraDocumento.LINK = request.nombrearchivo;

            _context.ObrasDocumentos.Update(oldObraDocumento);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

