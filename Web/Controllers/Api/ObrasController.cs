using RowingApp.Common.Helpers;
using RowingApp.Common.Requests;
using Web.Data;
using RowingApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RowingApp.Web.Controllers.API
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

        [HttpPut]
        [Route("PutEstadoSubestado/{id}")]
        public async Task<IActionResult> PutEstadoSubestado([FromRoute] int id, [FromBody] ObraEstadoSubestadoRequest request)
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

            oldObra.CodigoEstado = request.CodigoEstado;
            oldObra.CodigoSubEstado = request.CodigoSubEstado;

            _dataContext.Obras.Update(oldObra);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("GetObrasEstados")]
        public async Task<IActionResult> GetObrasEstados()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var estados = await _dataContext.EstadosPEP
           .OrderBy(o => o.CODIGO)
           .ToListAsync();
            if (estados == null)
            {
                return BadRequest("No hay Estados de Obras.");
            }
            return Ok(estados);
        }

        [HttpGet]
        [Route("GetObrasSubEstados")]
        public async Task<IActionResult> GetObrasSubEstados()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var subestados = await _dataContext.EstadosPEPSub
           .OrderBy(o => o.CODIGOESTADO)
           .ToListAsync();
            if (subestados == null)
            {
                return BadRequest("No hay SubEstados de Obras.");
            }
            return Ok(subestados);
        }

        [HttpPost]
        [Route("GetObrasAsignacion/{ProyectoModulo}/{Causante}")]
        public async Task<IActionResult> GetObrasAsignacion(string ProyectoModulo, string Causante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var obras = await _dataContext.VistaObrasAsignacionSubC
           .Where(o => (
           o.Modulo == ProyectoModulo 
           && o.CAUSANTE == Causante
           && (o.FechaCierre==null || o.FechaCierre > DateTime.Now.AddDays(-1)
           )

           ))
           .OrderBy(o => o.NROOBRA)
           .ToListAsync();
            if (obras == null)
            {
                return BadRequest("No hay Obras.");
            }
            return Ok(obras);
        }

        [HttpPut]
        [Route("PutObrasAsignacion/{id}")]
        public async Task<IActionResult> PutObrasAsignacion([FromRoute] int id, [FromBody] ObrasAsignacionSub request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oldObra = await _dataContext.ObrasAsignacionSubC.FindAsync(request.NROREGISTRO);
            if (oldObra == null)
            {
                return BadRequest("La Obra no existe.");
            }

            oldObra.OBSERVACION = request.OBSERVACION;
            oldObra.FechaCierre = request.FechaCierre;

            _dataContext.ObrasAsignacionSubC.Update(oldObra);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}