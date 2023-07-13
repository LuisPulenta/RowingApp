using GenericApp.Common.Helpers;
using GenericApp.Common.Requests;
using GenericApp.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObrasController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IFilesHelper _filesHelper;

        public ObrasController(DataContext dataContext,IFilesHelper filesHelper)
        {
            _dataContext = dataContext;
            _filesHelper = filesHelper;
        }

        [HttpPut]
        [Route("PutDatosObra/{id}")]
        public async Task<IActionResult> PutDatosObra([FromRoute] int id, [FromBody] ObraDatosRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.NroObra)
            {
                return BadRequest();
            }

            var oldObra = await _dataContext.Obras.FindAsync(request.NroObra);
            if (oldObra == null)
            {
                return BadRequest("La Obra no existe.");
            }

            oldObra.POSX = request.POSX;
            oldObra.POSY = request.POSY;
            oldObra.Direccion = request.Direccion;
            oldObra.TextoLocalizacion = request.TextoLocalizacion;
            oldObra.TextoClase = request.TextoClase;
            oldObra.TextoTipo = request.TextoTipo;
            oldObra.TextoComponente = request.TextoComponente;
            oldObra.CodigoDiametro = request.CodigoDiametro;
            oldObra.Motivo = request.Motivo;
            oldObra.Planos = request.Planos;

            _dataContext.Obras.Update(oldObra);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFechaCierreElectrico([FromRoute] int id, [FromBody] ObraFechaCierreElectricoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.NroObra)
            {
                return BadRequest();
            }

            var oldObra = await _dataContext.Obras.FindAsync(request.NroObra);
            if (oldObra == null)
            {
                return BadRequest("La Obra no existe.");
            }

            oldObra.FechaCierreElectrico = request.FechaCierreElectrico;

            _dataContext.Obras.Update(oldObra);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}