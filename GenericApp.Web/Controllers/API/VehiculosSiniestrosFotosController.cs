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
    public class VehiculosSiniestrosFotos : ControllerBase
    {
        private readonly DataContext2 _context;
        private readonly IFilesHelper _filesHelper;

        public VehiculosSiniestrosFotos(DataContext2 context, IFilesHelper filesHelper)
        {
            _context = context;
            _filesHelper = filesHelper;
        }

        // POST: api/ObrasDocuments

        [HttpPost]
        [Route("VehiculosSiniestrosFoto")]
        public async Task<IActionResult> PostVehiculosSiniestrosFoto([FromBody] VehiculosSiniestrosFotoRequest request)
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
            var folder = "wwwroot\\images\\Siniestros";
            var fullPath = $"~/images/Siniestros/{file}";
            var response = _filesHelper.UploadPhoto(stream, folder, file);

            if (response)
            {
                imageUrl1 = fullPath;
            }

            VehiculosSiniestro vehiculosSiniestro = await _context.VehiculosSiniestros
                .FirstOrDefaultAsync(o => o.NROSINIESTRO == request.NROSINIESTROCAB);

            var vehiculosSiniestrosFoto = new VehiculosSiniestrosFoto
            {
                //NROREGISTRO = request.NROREGISTRO,
                LINKFOTO = imageUrl1,
                OBSERVACION=request.OBSERVACION,
                NROSINIESTROCAB=request.NROSINIESTROCAB,
                CORRESPONDEA = request.CORRESPONDEA
            };

            _context.VehiculosSiniestrosFotos.Add(vehiculosSiniestrosFoto);
            await _context.SaveChangesAsync();

            return Ok(vehiculosSiniestrosFoto);
        }

        

        // DELETE: api/ObrasDocumentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiculosSiniestrosFoto([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var vehiculosSiniestrosFoto = await _context.VehiculosSiniestrosFotos
                .FirstOrDefaultAsync(p => p.IDFOTOSINIESTRO == id);
            if (vehiculosSiniestrosFoto == null)
            {
                return this.NotFound();
            }

            _context.VehiculosSiniestrosFotos.Remove(vehiculosSiniestrosFoto);
            await _context.SaveChangesAsync();
            return Ok("VehiculosSiniestrosFoto borrado");
        }

        [HttpPost]
        [Route("GetVehiculosSiniestrosFotos/{NROSINIESTROCAB}")]
        public async Task<IActionResult> GetVehiculosSiniestrosFotos(int NROSINIESTROCAB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vehiculosSiniestrosFotos = await _context.VehiculosSiniestrosFotos
            .Where(o => (o.NROSINIESTROCAB == NROSINIESTROCAB))
           .ToListAsync();

            if (vehiculosSiniestrosFotos == null)
            {
                return BadRequest("No hay Fotos para este Siniestro.");
            }

            return Ok(vehiculosSiniestrosFotos);
        }
    }
}

