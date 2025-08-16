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
    public class CausantesJuiciosController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IFilesHelper _filesHelper;

        public CausantesJuiciosController(DataContext dataContext, IFilesHelper filesHelper)
        {
            _dataContext = dataContext;
            _filesHelper = filesHelper;
        }

        [HttpPost]
        [Route("GetJuicios")]
        public async Task<IActionResult> GetJuicios()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var juicios = await _dataContext.CausantesJuicios
           .Where(o => ((o.CERRADO == 0))
           )

           .OrderBy(o => o.ID_CASO)
           .ToListAsync();


            if (juicios == null)
            {
                return BadRequest("No hay Juicios.");
            }
            return Ok(juicios);
        }

        [HttpPost]
        [Route("GetMediaciones/{codigo}")]
        public async Task<IActionResult> GetMediaciones(int codigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var mediaciones = await _dataContext.CausantesJuiciosMediaciones
           .Where(o => ((o.IDCAUSANTEJUICIO == codigo))
           )

           .OrderBy(o => o.IDMEDIACION)
           .ToListAsync();


            if (mediaciones == null)
            {
                return BadRequest("No hay Mediaciones.");
            }
            return Ok(mediaciones);
        }

        [HttpPost]
        [Route("GetNotificaciones/{codigo}")]
        public async Task<IActionResult> GetNotificaciones(int codigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var notificaciones = await _dataContext.CausantesJuiciosNotificaciones
           .Where(o => ((o.IDJUICIO == codigo))
           )

           .OrderBy(o => o.IDNOTIFICACION)
           .ToListAsync();


            if (notificaciones == null)
            {
                return BadRequest("No hay Notificaciones.");
            }
            return Ok(notificaciones);
        }

        [HttpPost]
        [Route("GetContraparte/{id}")]
        public async Task<IActionResult> GetContraparte(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var contraparte = await _dataContext.CausantesJuiciosContraparte
           .Where(o => ((o.IDCONTRAPARTE == id))
           )

           .OrderBy(o => o.IDCONTRAPARTE)
           .ToListAsync();


            if (contraparte == null)
            {
                return BadRequest("No hay Contraparte.");
            }
            return Ok(contraparte);
        }

        [HttpGet]
        [Route("GetNroRegistroMaxNotificaciones")]
        public IActionResult GetNroRegistroMaxNotificaciones()
        {
            int query = _dataContext.CausantesJuiciosNotificaciones.Max(c => c.IDNOTIFICACION);

            return Ok(query);
        }

        [HttpGet]
        [Route("GetNroRegistroMaxMediaciones")]
        public IActionResult GetNroRegistroMaxMediaciones()
        {
            int query = _dataContext.CausantesJuiciosMediaciones.Max(c => c.IDMEDIACION);

            return Ok(query);
        }

        [HttpPost]
        [Route("PostNotificacion")]
        public async Task<IActionResult> PostNotificacion([FromBody] CausantesJuiciosNotificacioneRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Foto
            string imageUrl = string.Empty;
            if (request.ImageArray != null && request.ImageArray.Length > 0)
            {
                var file = "";
                var stream = new MemoryStream(request.ImageArray);
                var guid = Guid.NewGuid().ToString();
                if (request.TIPOARRAY == "jpg") { file = $"{guid}.jpg"; };
                if (request.TIPOARRAY == "pdf") { file = $"{guid}.pdf"; };
                var folder = "wwwroot\\images\\Legales";
                var fullPath = $"~/images/Legales/{file}";
                var response = _filesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl = fullPath;
                }
            }

            var causantesJuiciosNotificacione = new CausantesJuiciosNotificacione
            {
                IDNOTIFICACION = request.IDNOTIFICACION,
                IDJUICIO = request.IDJUICIO,
                FECHACARGA = DateTime.Now,
                TIPO = request.TIPO,
                TITULO = request.TITULO,
                OBSERVACIONES = request.OBSERVACIONES,
                MONEDA = request.MONEDA,
                MONTO = request.MONTO,
                TIPOTRANSACCION = request.TIPOTRANSACCION,
                CONDICIONPAGO = request.CONDICIONPAGO,
                NROFACTURA = request.NROFACTURA,
                LUGAR = request.LUGAR,
                PARTICIPANTES = request.PARTICIPANTES,
                FECHAECHO = request.FECHAECHO,
                FECHAVENCIMIENTO = request.FECHAVENCIMIENTO,
                LINKARCHIVO = imageUrl,
                
            };
            _dataContext.CausantesJuiciosNotificaciones.Add(causantesJuiciosNotificacione);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("PostMediacion")]
        public async Task<IActionResult> PostMediacion([FromBody] CausantesJuiciosMediacioneRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            


            //Foto
            string imageUrl = string.Empty;
            if (request.ImageArray != null && request.ImageArray.Length > 0)
            {
                var file = "";


                var stream = new MemoryStream(request.ImageArray);
                var guid = Guid.NewGuid().ToString();
                if (request.TIPOARRAY == "jpg") { file = $"{guid}.jpg"; };
                if (request.TIPOARRAY == "pdf") { file = $"{guid}.pdf"; };

                var folder = "wwwroot\\images\\Legales";
                var fullPath = $"~/images/Legales/{file}";
                var response = _filesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl = fullPath;
                }
            }

            var causantesJuiciosMediacione = new CausantesJuiciosMediacione
            {
                IDMEDIACION = request.IDMEDIACION,
                IDCAUSANTEJUICIO = request.IDCAUSANTEJUICIO,
                MEDIADORES = request.MEDIADORES,
                FECHA = DateTime.Now,
                ABOGADO = request.ABOGADO,
                IDCONTRAPARTE = request.IDCONTRAPARTE,
                MONEDA = request.MONEDA,
                OFRECIMIENTO = request.OFRECIMIENTO,
                TIPOTRANSACCION = request.TIPOTRANSACCION,
                CONDICIONPAGO = request.CONDICIONPAGO,
                VENCIMIENTOOFERTA = request.VENCIMIENTOOFERTA,
                RESULTADOOFERTA = request.RESULTADOOFERTA,
                MONTOCONTRAOFERTA = request.MONTOCONTRAOFERTA,
                ACEPTACIONCONTRAOFERTA = request.ACEPTACIONCONTRAOFERTA,
                LINKARCHIVOMEDIACION=imageUrl,

            };
            _dataContext.CausantesJuiciosMediaciones.Add(causantesJuiciosMediacione);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}