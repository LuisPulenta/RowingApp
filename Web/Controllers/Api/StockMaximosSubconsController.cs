using RowingApp.Common.Requests;
using Web.Data;
using RowingApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace RowingApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockMaximosSubconsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        
        public StockMaximosSubconsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //-----------------------------------------------------------------------------------
        [HttpPut]
        [Route("UpdateMaximo/{Grupo}/{Causante}/{Catalogo}")]
        public async Task<IActionResult> UpdateMaximo([FromRoute] string Grupo,string Causante, string Catalogo, [FromBody] MaximoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StockMaximosSubcon oldStockMaximosSubcon = await _dataContext.StockMaximosSubcons.FirstOrDefaultAsync
                (o => o.GRUPOC.ToLower() == request.Grupo.ToLower() && o.CAUSANTE.ToLower() == request.Causante.ToLower() && o.CODIGOSIAG == request.Catalogo);


            if (oldStockMaximosSubcon == null)
            {
                return BadRequest("El Catálogo no existe.");
            }
            oldStockMaximosSubcon.MAXIMO = request.Cantidad;
            _dataContext.StockMaximosSubcons.Update(oldStockMaximosSubcon);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}