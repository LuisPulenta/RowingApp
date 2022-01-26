using GenericApp.Common.Requests;
using GenericApp.Common.Responses;
using GenericApp.Web.Data;
using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public AccountController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetUserByEmail")]
        public async Task<IActionResult> GetUser(UsuarioRequest userRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _dataContext.Usuarios.FirstOrDefaultAsync(o => o.Login.ToLower() == userRequest.Email.ToLower());

            if (user == null)
            {
                return BadRequest("El Usuario no existe.");
            }

            var response = new UsuarioAppResponse
            {
                IDUsuario = user.IDUsuario,
                Login = user.Login,
                Contrasena = user.Contrasena,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                AutorWOM = user.AutorWOM,
                Estado = user.Estado,
                HabilitaAPP = user.HabilitaAPP,
                HabilitaFotos = user.HabilitaFotos,
                HabilitaReclamos = user.HabilitaReclamos,
                HabilitaSSHH = user.HabilitaSSHH,
                Modulo = user.Modulo,
                HabilitaMedidores=user.HabilitaMedidores,
                CODIGOCAUSANTE = user.CODIGOCAUSANTE,
                CODIGOGRUPO = user.CODIGOGRUPO
            };

            return Ok(response);
        }


        [HttpGet]
        [Route("GetObrasEnergia")]
        public async Task<IActionResult> GetObrasEnergia()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var obras = await _dataContext.Obras
            .Include(p => p.ObrasDocumentos)
           .Where(o => (o.Finalizada == 0) 
           && (o.Modulo == "Energia"))
           .OrderBy(o => o.NroObra)
           .ToListAsync();
            if (obras == null)
            {
                return BadRequest("No hay Obras.");
            }
            return Ok(obras);
        }

        [HttpGet]
        [Route("GetObrasObrasTasa")]
        public async Task<IActionResult> GetObrasObrasTasa()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var obras = await _dataContext.Obras
            .Include(p => p.ObrasDocumentos)
           .Where(o => (o.Finalizada == 0)
           && (o.Modulo == "ObrasTasa"))
           .OrderBy(o => o.NroObra)
           .ToListAsync();
            if (obras == null)
            {
                return BadRequest("No hay Obras.");
            }
            return Ok(obras);
        }

        [HttpGet]
        [Route("GetObrasRowing")]
        public async Task<IActionResult> GetObrasRowing()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var obras = await _dataContext.Obras
            .Include(p => p.ObrasDocumentos)
           .Where(o => (o.Finalizada == 0)
           && (o.Modulo == "Rowing"))
           .OrderBy(o => o.NroObra)
           .ToListAsync();
            if (obras == null)
            {
                return BadRequest("No hay Obras.");
            }
            return Ok(obras);
        }

        [HttpGet]
        [Route("GetObrasReclamosRowing")]
        public IActionResult GetObrasReclamosRowing()
        {
            return Ok(_dataContext.Obras
                .Where(o=> o.HabilitaReclamosAPP==1 && o.Modulo=="Rowing")
                );
        }

        [HttpGet]
        [Route("GetObrasReclamosEnergia")]
        public IActionResult GetObrasReclamosEnergia()
        {
            return Ok(_dataContext.Obras
                .Where(o => o.HabilitaReclamosAPP == 1 && o.Modulo == "Energia")
                );
        }

        [HttpGet]
        [Route("GetObrasReclamosObrasTasa")]
        public IActionResult GetObrasReclamosObrasTasa()
        {
            return Ok(_dataContext.Obras
                .Where(o => o.HabilitaReclamosAPP == 1 && o.Modulo == "ObrasTasa")
                );
        }

        [HttpGet("{id}")]
        [Route("GetObra/{id}")]
        public async Task<ActionResult<Obra>> GetObra(int id)
        {
            Obra obra = await _dataContext.Obras
                .Include(x => x.ObrasDocumentos)
                .FirstOrDefaultAsync(x => x.NroObra == id);
            if (obra == null)
            {
                return NotFound();
            }
            return obra;
        }
    }
}