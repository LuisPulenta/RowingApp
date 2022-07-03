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

        [HttpPost]
        [Route("PostInspeccionDetalleConFotoExistente")]
        public async Task<IActionResult> PostInspeccionDetalleConFotoExistente([FromBody] SHInspeccionDetallRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inspeccionDetalle = new SHInspeccionDetall
            {
                //NROREGISTRO = request.NROREGISTRO,
                Cumple = request.Cumple,
                Descripcion = request.Descripcion,
                DetalleF = request.DetalleF,
                IdCliente = request.IdCliente,
                IDGrupoFormulario = request.IDGrupoFormulario,
                IDRegistro = request.IDRegistro,
                InspeccionCab = request.InspeccionCab,
                LinkFoto = request.LinkFoto,
                PonderacionPuntos = request.PonderacionPuntos,
            };

            _dataContext.SHInspeccionDetalle.Add(inspeccionDetalle);
            await _dataContext.SaveChangesAsync();

            return Ok(inspeccionDetalle);
        }

        [HttpPost]
        [Route("GetInspecciones/{UserID}")]
        public async Task<IActionResult> GetInspecciones(int UserID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var inspecciones = await _dataContext.VistaInspecciones
           .Where(o => (o.UsuarioAlta == UserID) && (o.Fecha.AddDays(3)>= DateTime.Now))
           .OrderBy(o => o.Fecha)
           .ToListAsync();


            if (inspecciones == null)
            {
                return BadRequest("No hay Inspecciones para este Usuario.");
            }

            return Ok(inspecciones);
        }

        // GET: api/Users/5
        [HttpGet("GetInspeccion/{codigo}")]
        public async Task<ActionResult<Data.Entities.SHInspeccio>> GetInspeccion(int codigo)
        {
            Data.Entities.SHInspeccio inspeccion = await _dataContext.SHInspeccion
                .FirstOrDefaultAsync
                (o => (o.IDInspeccion == codigo));

            if (inspeccion == null)
            {
                return NotFound();
            }
            return inspeccion;
        }

        [HttpGet]
        [Route("GetDetallesInspecciones/{idinspeccion}")]
        public async Task<IActionResult> GetDetallesInspecciones(int idinspeccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var detallesInspeccion = await _dataContext.SHInspeccionDetalle
           .Where(o => (o.InspeccionCab == idinspeccion))

           .OrderBy(o => o.IDRegistro)
           .ToListAsync();

            if (detallesInspeccion == null)
            {
                return BadRequest("No hay DetalleInspeccion.");
            }
            return Ok(detallesInspeccion);
        }

        // GET: api/Users/5
        [HttpGet("GetObra/{codigo}")]
        public async Task<ActionResult<Data.Entities.Obra>> GetObra(int codigo)
        {
            Data.Entities.Obra obra = await _dataContext.Obras
                .FirstOrDefaultAsync
                (o => (o.NroObra == codigo));

            if (obra == null)
            {
                return NotFound();
            }
            return obra;
        }

        [HttpGet]
        [Route("GetVistaInspeccionesFotos/{codigo}")]
        public async Task<IActionResult> GetVistaInspeccionesFotos(int codigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vistaInspeccionesFotos = await _dataContext.VistaInspeccionesFotos
           .Where (o => o.Fecha >= DateTime.Now.AddDays(-codigo))
                .OrderBy(o => o.IDRegistro)

           .ToListAsync();
            if (vistaInspeccionesFotos == null)
            {
                return BadRequest("No hay Fotos.");
            }
            return Ok(vistaInspeccionesFotos);
        }
    }
}