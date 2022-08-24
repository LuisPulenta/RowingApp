using GenericApp.Common.Requests;
using GenericApp.Common.Responses;
using GenericApp.Web.Data;
using GenericApp.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CausantesController : ControllerBase
    {
        private readonly DataContext2 _dataContext;
        private readonly IImageHelper _imageHelper;

        public CausantesController(DataContext2 dataContext,IImageHelper imageHelper)
        {
            _dataContext = dataContext;
            _imageHelper = imageHelper;
        }

        [HttpPost]
        [Route("GetCausanteByCodigo")]
        public async Task<IActionResult> GetCausante(CausanteRequest codigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Data.Entities.Causante user = await _dataContext.Causantes.FirstOrDefaultAsync
                (o => (o.codigo.ToLower() == codigo.Codigo.ToLower() || o.NroSAP.ToLower() == codigo.Codigo.ToLower()) && (o.grupo == "PPR" || o.grupo == "PPC"));

            if (user == null)
            {
                return BadRequest("El Empleado no existe.");
            }

            CausanteResponse response = new CausanteResponse
            {
                codigo = user.codigo,
                nombre = user.nombre,
                encargado = user.encargado,
                NroCausante = user.NroCausante,
                telefono = user.telefono,
                NroSAP=user.NroSAP,
                grupo=user.grupo,
                estado=user.estado,
                LinkFoto=user.LinkFoto,
                direccion = user.direccion,
                Numero = user.Numero,
                TelefonoContacto1 = user.TelefonoContacto1,
                TelefonoContacto2 = user.TelefonoContacto2,
                TelefonoContacto3 = user.TelefonoContacto3,
                fecha = user.fecha,
                NotasCausantes = user.NotasCausantes,
                ciudad=user.ciudad,
                Provincia = user.Provincia,
            };

            return Ok(response);
        }

        // GET: api/Users/5
        [HttpGet("GetCausanteByCodigo2/{codigo}")]
        public async Task<ActionResult<Data.Entities.Causante>> GetCausante2(string codigo)
        {
            Data.Entities.Causante causante = await _dataContext.Causantes
                .FirstOrDefaultAsync(o => (o.codigo.ToLower() == codigo.ToLower() || o.NroSAP.ToLower() == codigo.ToLower()) && (o.grupo == "PPR" || o.grupo == "PPC"));

            if (causante == null)
            {
                return NotFound();
            }
            return causante;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCausante([FromRoute] int id, [FromBody] CausanteRequest2 request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.Id)
            {
                return BadRequest();
            }

            

            var oldCausante = await _dataContext.Causantes.FindAsync(request.Id);
            if (oldCausante == null)
            {
                return BadRequest("El Causante no existe.");
            }

            string imageId = oldCausante.LinkFoto;
            if (request.Image != null && request.Image.Length > 0)
            {
                imageId = _imageHelper.UploadImage(request.Image, "Causantes");
            }

            oldCausante.telefono = request.telefono;
            oldCausante.LinkFoto = imageId;
            oldCausante.direccion = request.direccion;
            oldCausante.Numero = request.Numero;
            oldCausante.TelefonoContacto1 = request.TelefonoContacto1;
            oldCausante.TelefonoContacto2 = request.TelefonoContacto2;
            oldCausante.TelefonoContacto3 = request.TelefonoContacto3;
            oldCausante.fecha = request.fecha;
            oldCausante.NotasCausantes = request.NotasCausantes;
            oldCausante.ciudad = request.ciudad;
            oldCausante.Provincia = request.Provincia;

            _dataContext.Causantes.Update(oldCausante);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }


        [HttpPost]
        [Route("GetCausantesByGrupo/{Grupo}")]
        public IActionResult GetCausantesByProyectoModulo(string Grupo)
        {
            return Ok(_dataContext.Causantes
                .Where(o => o.grupo == Grupo && o.estado==true)
                .OrderBy(o => o.nombre)
                );
        }

    }
}