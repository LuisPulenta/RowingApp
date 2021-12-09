using GenericApp.Common.Requests;
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
    public class EntregasController : ControllerBase
    {
        private readonly DataContext2 _dataContext;

        public EntregasController(DataContext2 dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetEntregas/{codigo}")]
        public async Task<IActionResult> GetEntregas(string Codigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var entregas = await _dataContext.ProductosStock
           .Where(o => (o.causante == Codigo) && (o.grupo == "PPR" || o.grupo == "PPC") && (o.stock_act > 0))

           .OrderBy(o => o.fecha)
           .GroupBy(r => new
           {
               r.fecha
           })
           .Select(g => new
           {
               fecha = g.Key.fecha,
               CantItems = g.Count()
           })
           .ToListAsync();


            if (entregas == null)
            {
                return BadRequest("No hay Entregas.");
            }
            return Ok(entregas);
        }

        [HttpPost]
        [Route("GetEntregas2/{codigo}")]
        public async Task<IActionResult> GetEntregas2(string Codigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var entregas = await _dataContext.ProductosStock
           .Where(o => (o.causante == Codigo) && (o.grupo == "PPR") && (o.stock_act > 0) && (o.fecha != null))

           .OrderBy(o => o.fecha)
           .ToListAsync();


            if (entregas == null)
            {
                return BadRequest("No hay Entregas.");
            }
            return Ok(entregas);
        }

        [HttpPost]
        [Route("GetEntregaDetalles")]
        public async Task<IActionResult> GetTrabajos(EntregaDetallesRequest entregaDetallesRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var entregaDetalles = await _dataContext.ProductosStock
           .Where(o => (o.fecha == entregaDetallesRequest.fecha) && (o.causante == entregaDetallesRequest.causante) && (o.grupo == "PPR") && (o.stock_act >0) && (o.fecha != null))
           .OrderBy(o => o.Denominacion)
           .ToListAsync();


            if (entregaDetalles == null)
            {
                return BadRequest("No hay Detalles para esta Entrega.");
            }

            return Ok(entregaDetalles);
        }
    }
}