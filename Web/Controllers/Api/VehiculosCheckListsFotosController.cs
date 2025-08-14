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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VehiculosCheckListsFotosController : ControllerBase
    {
        private readonly DataContext2 _context;
        private readonly IFilesHelper _filesHelper;

        public VehiculosCheckListsFotosController(DataContext2 context, IFilesHelper filesHelper)
        {
            _context = context;
            _filesHelper = filesHelper;
        }

        //-------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("PostVehiculosCheckListsFoto")]
        public async Task<IActionResult> PostVehiculosCheckListsFoto([FromBody] VehiculosCheckListsFotoRequest request)
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
            var folder = "wwwroot\\images\\CheckList";
            var fullPath = $"~/images/CheckList/{file}";
            var response = _filesHelper.UploadPhoto(stream, folder, file);

            if (response)
            {
                imageUrl1 = fullPath;
            }

            

            var vehiculosCheckListsFoto = new VehiculosCheckListsFoto
            {
                //NROREGISTRO = request.NROREGISTRO,
                LINKFOTO = imageUrl1,
                IDCHECKLISTCAB = request.IDCHECKLISTCAB,
                DESCRIPCION = request.DESCRIPCION,
                
            };

            _context.VehiculosCheckListsFotos.Add(vehiculosCheckListsFoto);
            await _context.SaveChangesAsync();

            return Ok(vehiculosCheckListsFoto);
        }

        //-------------------------------------------------------------------------------------------
        //***** Borra una sola foto (el id es el IDREGISTRO) *****
        [HttpDelete]
        [Route("DeleteVehiculosCheckListsFoto/{id}")]
        public async Task<IActionResult> DeleteVehiculosCheckListsFoto([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var vehiculosCheckListsFoto = await _context.VehiculosCheckListsFotos
                .FirstOrDefaultAsync(p => p.IDREGISTRO == id);
            if (vehiculosCheckListsFoto == null)
            {
                return this.NotFound();
            }

            _context.VehiculosCheckListsFotos.Remove(vehiculosCheckListsFoto);
            await _context.SaveChangesAsync();
            return Ok("VehiculosCheckListsFoto borrado");
        }

        //-------------------------------------------------------------------------------------------
        //***** Borra muchas fotos (el id es el IDCHECKLISTCAB) *****
        [HttpDelete]
        [Route("DeleteVehiculosCheckListsFotos/{id}")]
        public async Task<IActionResult> DeleteVehiculosCheckListsFotos([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var vehiculosCheckListsFotos = await _context.VehiculosCheckListsFotos
           
          .Where(o => (o.IDCHECKLISTCAB == id))
          .ToListAsync();
            if (vehiculosCheckListsFotos == null)
            {
                return BadRequest("No hay Fotos.");
            }

            foreach (VehiculosCheckListsFoto foto in vehiculosCheckListsFotos)
            {
                _context.VehiculosCheckListsFotos.Remove(foto);
                await _context.SaveChangesAsync();
            }
            
            return Ok("VehiculosCheckListsFotos borradas");
        }

        //-------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("GetVehiculosCheckListsFoto/{IDCHECKLISTCAB}")]
        public async Task<IActionResult> GetObrasDocumentos(int IDCHECKLISTCAB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vehiculosCheckListsFotos = await _context.VehiculosCheckListsFotos
            .Where(o => (o.IDCHECKLISTCAB == IDCHECKLISTCAB))
           .OrderBy(o => o.IDCHECKLISTCAB)
           .ToListAsync();

            if (vehiculosCheckListsFotos == null)
            {
                return BadRequest("No hay Fotos para este Check List.");
            }
            return Ok(vehiculosCheckListsFotos);
        }
    }
}

