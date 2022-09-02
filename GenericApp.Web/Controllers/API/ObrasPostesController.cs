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
                (o => (o.ASTICKET.ToLower() == ticket.ASTICKET.ToLower()) && (o.TipoImput == "Medidores"));

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
                IDActaCertif=obraPoste.IDActaCertif,
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

        // GET: api/Users/5
        [HttpGet("GetTicket2/{codigo}")]
        public async Task<ActionResult<Data.Entities.ObrasPoste>> GetTicket2(string codigo)
        {
            Data.Entities.ObrasPoste obraPoste = await _context.ObrasPostes
                .FirstOrDefaultAsync
                (o => (o.ASTICKET.ToLower() == codigo.ToLower()) && (o.TipoImput == "Medidores") && (o.CERTIFICADO != "SI"));

            if (obraPoste == null)
            {
                return NotFound();
            }
            return obraPoste;
        }



        [HttpGet]
        [Route("GetNroRegistroMax")]
        public async Task<IActionResult> GetNroRegistroMax()
        {
            var query = _context.ObrasPostes.Max(c => c.NROREGISTRO);

            return Ok(query);
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

        [HttpPost]
        [Route("PostReclamo")]
        public async Task<IActionResult> PostReclamo([FromBody] ObrasPosteCajasAppRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var Reclamo = new ObrasPosteCajasAPP
            {
                NROREGISTRO= request.NROREGISTRO,
                ASTICKET=request.ASTICKET,
                NUMERACION=request.NUMERACION,
                NROOBRA = request.NROOBRA,
                Subcontratista = request.Subcontratista,
                ZONA = request.ZONA,
                TERMINAL = request.TERMINAL,
                CausanteC = request.CausanteC,
                DIRECCION = request.DIRECCION,
                TipoImput = request.TipoImput,
                GRXX = request.GRXX,
                GRYY = request.GRYY,
                IDUsrIn = request.IDUsrIn,
                ObservacionAdicional = request.ObservacionAdicional,
                FechaCarga=request.FechaCarga,
                RiesgoElectrico = request.RiesgoElectrico,
                CERTIFICADO = request.CERTIFICADO,
                FECHAASIGNACION = request.FECHAASIGNACION,
                MES = request.MES,
                OBSERVACIONES = request.OBSERVACIONES,
                CajaDAE = request.CajaDAE,
                IDActaCertif=request.IDActaCertif,
                Cliente = request.Cliente,
                Lindero1 = request.Lindero1,
                Lindero2 = request.Lindero2,
                Localidad = request.Localidad,
                Precinto = request.Precinto,
                SerieMedidorColocado = request.SerieMedidorColocado,
                Telefono = request.Telefono,
            };

            _context.ObrasPostesCajasAPP.Add(Reclamo);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("GetReclamos/{ObraID}")]
        public async Task<IActionResult> GetReclamosEnergia(int ObraID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var reclamos = await _context.ObrasPostesCajasAPP
           .Where(o => (o.NROOBRA == ObraID) && ((o.TipoImput == "Reclamos")))
           .OrderBy(o => o.NROREGISTRO)
           .ToListAsync();


            if (reclamos == null)
            {
                return BadRequest("No hay Reclamos para esta Obra.");
            }

            return Ok(reclamos);
        }

        [HttpPost]
        [Route("GetReclamosByUser/{UserID}")]
        public async Task<IActionResult> GetReclamosByUser(int UserID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var reclamos = await _context.ObrasPostesCajasAPP
           .Where(o => (o.IDUsrIn == UserID) && (o.TipoImput == "Reclamos") && (o.CERTIFICADO == "No"))
           .OrderBy(o => o.NROREGISTRO)
           .ToListAsync();


            if (reclamos == null)
            {
                return BadRequest("No hay Reclamos para esta Obra.");
            }

            return Ok(reclamos);
        }
    }
}