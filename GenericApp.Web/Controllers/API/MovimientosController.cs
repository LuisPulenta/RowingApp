using GenericApp.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly DataContext2 _dataContext;

        public MovimientosController(DataContext2 dataContext)
        {
            _dataContext = dataContext;
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
    }
}