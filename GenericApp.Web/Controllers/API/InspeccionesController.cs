using GenericApp.Common.Helpers;
using GenericApp.Common.Requests;
using GenericApp.Web.Data;
using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspeccionesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IFilesHelper _filesHelper;

        public InspeccionesController(DataContext dataContext, IFilesHelper filesHelper)
        {
            _dataContext = dataContext;
            _filesHelper = filesHelper;
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

        [HttpPost]
        [Route("PostInspeccion")]
        public async Task<IActionResult> PostInspeccion([FromBody] SHInspeccio request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dataContext.SHInspeccion.Add(request);
            await _dataContext.SaveChangesAsync();
            return Ok(request);
        }

        [HttpPost]
        [Route("PostInspeccionDetalle")]
        public async Task<IActionResult> PostInspeccionDetalle([FromBody] SHInspeccionDetallRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Foto
            var imageUrl1 = string.Empty;
            if (request.ImageArray != null && request.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(request.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\Inspecciones";
                var fullPath = $"~/images/Inspecciones/{file}";
                var response = _filesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl1 = fullPath;
                }
            }

            var inspeccionDetalle = new SHInspeccionDetall
            {
                //NROREGISTRO = request.NROREGISTRO,
                Cumple= request.Cumple,
                Descripcion = request.Descripcion,
                DetalleF = request.DetalleF,
                IdCliente = request.IdCliente,
                IDGrupoFormulario = request.IDGrupoFormulario,
                IDRegistro = request.IDRegistro,
                InspeccionCab = request.InspeccionCab,
                LinkFoto=imageUrl1,
                PonderacionPuntos = request.PonderacionPuntos,
            };

            _dataContext.SHInspeccionDetalle.Add(inspeccionDetalle);
            await _dataContext.SaveChangesAsync();

            return Ok(inspeccionDetalle);
        }
    }
}