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
    public class MovimientosController : ControllerBase
    {
        private readonly DataContext2 _dataContext;
        private readonly IFilesHelper _filesHelper;

        public MovimientosController(DataContext2 dataContext, IFilesHelper filesHelper)
        {
            _dataContext = dataContext;
            _filesHelper = filesHelper;
        }

        //-----------------------------------------------------------------------------------
        [HttpPost]
        [Route("GetMovimientos/{user}")]
        public async Task<IActionResult> GetMovimientos(int user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movimientos = await _dataContext.Movimientos
           .Where(o => 
           o.UsrAlta == user && 
           o.Recibido && 
           (o.CodigoConcepto=="101" || o.CodigoConcepto == "502") && 
           o.FechaCarga.AddDays(10) >= DateTime.Now)

           .OrderBy(o => o.NroMovimiento)
           .ToListAsync();


            if (movimientos == null)
            {
                return BadRequest("No hay Movimientos.");
            }
            return Ok(movimientos);
        }

        //-----------------------------------------------------------------------------------
        [HttpPost]
        [Route("PostMovimiento")]
        public async Task<IActionResult> PostMovimiento([FromBody] MovimientoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Foto
            string imageUrl = string.Empty;
            if (request.ImageArray != null && request.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(request.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\Remitos";
                var fullPath = $"~/images/Remitos/{file}";
                var response = _filesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl = fullPath;
                }
            }

            Movimiento oldMovimiento = await _dataContext.Movimientos.FirstOrDefaultAsync(o => o.NroMovimiento == request.NroMovimiento);

            if (oldMovimiento == null)
            {
                return BadRequest("El Movimiento no existe.");
            }

            oldMovimiento.LinkRemito = imageUrl;

            _dataContext.Movimientos.Update(oldMovimiento);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        //-----------------------------------------------------------------------------------
        [HttpGet("{id}")]
        [Route("GetMovimiento/{id}")]
        public async Task<ActionResult<Movimiento>> GetMovimiento(int id)
        {
            Movimiento movimiento = await _dataContext.Movimientos
                .FirstOrDefaultAsync(x => x.NroMovimiento == id);
            if (movimiento == null)
            {
                return NotFound();
            }
            return movimiento;
        }
    }
}