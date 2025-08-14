using RowingApp.Common.Helpers;
using RowingApp.Common.Requests;
using Web.Data;
using RowingApp.Web.Data.Entities;
using RowingApp.Web.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GenericApp.Common.Requests;

namespace RowingApp.Web.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CausanteRecibosController : ControllerBase
    {
        private readonly DataContext2 _dataContext2;
        private readonly IFilesHelper _filesHelper;
        private readonly IMailHelper _mailHelper;

        public CausanteRecibosController(DataContext2 dataContext2, IFilesHelper filesHelper, IMailHelper mailHelper)
        {
            _dataContext2 = dataContext2;
            _filesHelper = filesHelper;
            _mailHelper = mailHelper;
        }

        //------------------------------------------------------------------------------
        [HttpPost]
        [Route("GetRecibos")]
        public async Task<IActionResult> GetRecibos(CausanteRequest3 request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var recibos = await _dataContext2.CausantesRec
                    .Where(o => o.CAUSANTE == request.Codigo && o.GRUPO == request.Grupo)
                    .OrderBy(o => o.ANIO + o.MES)                       
          .ToListAsync();
            if (recibos == null)
            {
                return BadRequest("No hay Recibos");
            }
            return Ok(recibos);
        }

        //-----------------------------------------------------------------------------------
        [HttpPut]
        [Route("FirmarRecibo/{id}")]
        public async Task<IActionResult> FirmarRecibo([FromRoute] int id, [FromBody] ReciboRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (request.IdRecibo!=id)
            {
                return BadRequest(ModelState);
            }


            //Foto
            string imageUrl = string.Empty;
            var file = "";
            var stream = new MemoryStream(request.ImageArray);
            file = $"{request.FileName}";
            var folder = "wwwroot\\images\\Recibos";
            var fullPath = $"~/images/Recibos/{file}";
            var response = _filesHelper.UploadPhoto(stream, folder, file);

            CausanteRecibo oldCausanteRecibo = await _dataContext2.CausantesRec.FindAsync(request.IdRecibo);

            if (oldCausanteRecibo == null)
            {
                return BadRequest("El Recibo no existe.");
            }

            if (oldCausanteRecibo.FIRMADO == 1)
            {
                return BadRequest("El Recibo ya fue firmado.");
            }

            var horaActual = DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60;

            oldCausanteRecibo.FIRMADO = 1;
            oldCausanteRecibo.EstadoRecibo = "Firmado";
            oldCausanteRecibo.FECHAESTADO = DateTime.Now;
            oldCausanteRecibo.HSESTADO = horaActual;
            oldCausanteRecibo.FECHAFIRMAELECTRONICA = DateTime.Now;
            oldCausanteRecibo.HSFIRMAELECTRONICA = horaActual;
            oldCausanteRecibo.LATFIRMAELECTRONICA = request.Latitud;
            oldCausanteRecibo.LONGFIRMAELECTRONICA = request.Longitud;
            oldCausanteRecibo.IMEI = request.Imei;

            _dataContext2.CausantesRec.Update(oldCausanteRecibo);
            await _dataContext2.SaveChangesAsync();
            return Ok();
        }

        //-------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("SendMailWithPdf")]
        public void SendMailWithPdf(SendEmailRequest request)
        {
            _mailHelper.SendMailWithPdf(request.to, request.subject, request.body,request.fileUrl,request.fileName);
        }
    }
}