using GenericApp.Common.Helpers;
using GenericApp.Common.Requests;
using GenericApp.Common.Responses;
using GenericApp.Web.Data;
using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ObrasPostesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IFilesHelper _filesHelper;

        public ObrasPostesController(DataContext context, IFilesHelper filesHelper)
        {
            _context = context;
            _filesHelper = filesHelper;
        }

        [HttpPost]
        [Route("GetTicket")]
        public async Task<IActionResult> GetTicket(TicketRequest ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Data.Entities.ObrasPoste obraPoste = await _context.ObrasPostes.FirstOrDefaultAsync
                (o => (o.ASTICKET.ToLower() == ticket.ASTICKET.ToLower()) && (o.TipoImput == "Medidores") && (o.CERTIFICADO != "SI"));

            if (obraPoste == null)
            {
                return BadRequest("El Ticket no existe.");
            }

            ObrasPosteResponse response = new ObrasPosteResponse
            {
                ASTICKET= obraPoste.ASTICKET,
                CERTIFICADO = obraPoste.CERTIFICADO,
                NROOBRA = obraPoste.NROOBRA,
                NUMERACION = obraPoste.NUMERACION,
                OBSERVACIONES = obraPoste.OBSERVACIONES,
                CajaDAE = obraPoste.CajaDAE,
                Cliente = obraPoste.Cliente,
                DIRECCION = obraPoste.DIRECCION,
                Lindero1 = obraPoste.Lindero1,
                Lindero2 = obraPoste.Lindero2,
                Localidad = obraPoste.Localidad,
                NROREGISTRO = obraPoste.NROREGISTRO,
                Precinto = obraPoste.Precinto,
                SerieMedidorColocado = obraPoste.SerieMedidorColocado,
                Telefono = obraPoste.Telefono,
                TipoImput = obraPoste.TipoImput,
            };
                return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutObrasPoste(int id, ObrasPoste obrasPoste)
        {
            if (id != obrasPoste.NROREGISTRO)
            {
                return BadRequest();
            }

            _context.Entry(obrasPoste).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe esta marca.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.InnerException.Message);
            }
        }
    }
}