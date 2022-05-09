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
    public class InspeccionesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public InspeccionesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        [Route("GetClientes")]
        public async Task<IActionResult> GetClientes()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var clientes = await _dataContext.Clientes
           .OrderBy(o => o.NOMBRE)
           .ToListAsync();
            if (clientes == null)
            {
                return BadRequest("No hay Clientes.");
            }
            return Ok(clientes);
        }

        [HttpGet]
        [Route("GetTiposTrabajos/{idcliente}")]
        public async Task<IActionResult> GetTiposTrabajos(int idcliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var tiposTrabajos = await _dataContext.SHTiposTrabajos
            .Where(o => (o.IDCLIENTE == idcliente))
            .OrderBy(o => o.DESCRIPCION)
            .ToListAsync();
            if (tiposTrabajos == null)
            {
                return BadRequest("No hay TiposTrabajos.");
            }
            return Ok(tiposTrabajos);
        }

        [HttpGet]
        [Route("GetGruposFormularios/{idcliente}/{idtipotrabajo}")]
        public async Task<IActionResult> GetGruposFormularios(int idcliente,int idtipotrabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var gruposFormularios = await _dataContext.SHGrupoFormularios
           .Where(o => (o.IDCLIENTE == idcliente) && (o.IDTIPOTRABAJO == idtipotrabajo))

           .OrderBy(o => o.IDGRUPOFORMULARIO)
           .ToListAsync();

            if (gruposFormularios == null)
            {
                return BadRequest("No hay GruposFormularios.");
            }
            return Ok(gruposFormularios);
        }

        [HttpGet]
        [Route("GetDetallesFormularios/{idcliente}")]
        public async Task<IActionResult> GetDetallesFormularios(int idcliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var detallesFormularios = await _dataContext.SHGrupoFormPonderados
           .Where(o => (o.IDCLIENTE == idcliente))

           .OrderBy(o => o.IDGRUPOFORMULARIO).ThenBy(o=>o.DETALLEF)
           .ToListAsync();

            if (detallesFormularios == null)
            {
                return BadRequest("No hay GrupoFormPonderados.");
            }
            return Ok(detallesFormularios);
        }
    }
}