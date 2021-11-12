using GenericApp.Common.Helpers;
using GenericApp.Common.Requests;
using GenericApp.Common.Responses;
using GenericApp.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetCausante(TicketRequest ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Data.Entities.ObrasPoste obraPoste = await _context.ObrasPostes.FirstOrDefaultAsync
                (o => (o.ASTICKET.ToLower() == ticket.ASTICKET.ToLower()) && (o.TipoImput == "Medidores") && (o.CERTIFICADO=="SI"));

            if (obraPoste == null)
            {
                return BadRequest("El Ticket no existe.");
            }

            ObrasPosteResponse response = new ObrasPosteResponse
            {
                ASTICKET = obraPoste.ASTICKET,
                CERTIFICADO = obraPoste.CERTIFICADO,
                NROOBRA = obraPoste.NROOBRA,
                NUMERACION = obraPoste.NUMERACION,
                Cliente = obraPoste.Cliente,
                DIRECCION = obraPoste.DIRECCION,
                Localidad = obraPoste.Localidad,
                NROREGISTRO = obraPoste.NROREGISTRO,
                Telefono = obraPoste.Telefono,
                TipoImput = obraPoste.TipoImput,
            };
            return Ok(response);
        }
    }
}