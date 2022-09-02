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
        private readonly DataContext2 _dataContext2;

        public AccountController(DataContext dataContext, DataContext2 dataContext2)
        {
            _dataContext = dataContext;
            _dataContext2 = dataContext2;
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

                var user2 = await _dataContext2.Causantes.FirstOrDefaultAsync(o => o.codigo.ToLower() == userRequest.Email.ToLower() && o.NroSAP.ToLower() == userRequest.Password.ToLower());
                if (user2 == null)
                {
                    return BadRequest("El Usuario no existe.");
                }
                var response2 = new UsuarioAppResponse
                {
                    IDUsuario = user2.NroCausante,
                    CodigoCausante = user2.codigo,
                    Login = user2.codigo,
                    Contrasena = user2.NroSAP,
                    Nombre = user2.nombre,
                    Apellido = user2.nombre,
                    AutorWOM = 0,
                    Estado = 1,
                    HabilitaAPP = 1,
                    HabilitaFotos = 0,
                    HabilitaReclamos = 0,
                    HabilitaSSHH = 0,
                    HabilitaRRHH = 0,
                    Modulo = user2.RazonSocial,
                    HabilitaMedidores = 0,
                    HabilitaFlotas = "NO",
                    ReHabilitaUsuarios = 0,
                    CODIGOGRUPO = user2.codigo,
                    FechaCaduca=0,
                    IntentosInvDiario=0,
                    OpeAutorizo=0
                };

                return Ok(response2);
            }

            var response = new UsuarioAppResponse
            {
                IDUsuario = user.IDUsuario,
                CodigoCausante=user.CodigoCausante,
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
                HabilitaRRHH = user.HabilitaRRHH,
                Modulo = user.Modulo,
                HabilitaMedidores=user.HabilitaMedidores,
                HabilitaFlotas=user.HabilitaFlotas,
                ReHabilitaUsuarios = user.ReHabilitaUsuarios,
                CODIGOGRUPO = user.CODIGOGRUPO,
                FechaCaduca = user.FechaCaduca,
                IntentosInvDiario = user.IntentosInvDiario,
                OpeAutorizo = user.OpeAutorizo
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("GetUserByLogin")]
        public async Task<IActionResult> GetUserByLogin(UsuarioRequest userRequest)
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
                CodigoCausante = user.CodigoCausante,
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
                HabilitaRRHH = user.HabilitaRRHH,
                Modulo = user.Modulo,
                HabilitaMedidores = user.HabilitaMedidores,
                HabilitaFlotas = user.HabilitaFlotas,
                ReHabilitaUsuarios = user.ReHabilitaUsuarios,
                CODIGOGRUPO = user.CODIGOGRUPO,
                FechaCaduca = user.FechaCaduca,
                IntentosInvDiario = user.IntentosInvDiario,
                OpeAutorizo = user.OpeAutorizo
            };
            return Ok(response);
        }


        [HttpPut("{login}")]
        [Route("ReactivaUsuario")]
        public async Task<IActionResult> ReactivaUsuario([FromRoute] string login, [FromBody] UsuarioAutorizaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (login != request.Login)
            {
                return BadRequest();
            }

            var oldUsuario= await _dataContext.Usuarios.FirstOrDefaultAsync(x => x.Login == request.Login);

            if (oldUsuario == null)
            {
                return BadRequest("El Usuario no existe.");
            }

            oldUsuario.Estado=1;
            oldUsuario.FechaCaduca = request.FechaCaduca;
            oldUsuario.IntentosInvDiario = 0;
            oldUsuario.OpeAutorizo = request.IdUsuarioAutoriza;

            _dataContext.Usuarios.Update(oldUsuario);
            await _dataContext.SaveChangesAsync();
            return Ok();
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

        [HttpPost]
        [Route("GetObras/{ProyectoModulo}")]
        public async Task<IActionResult> GetObras(string ProyectoModulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var obras = await _dataContext.Obras
            .Include(p => p.ObrasDocumentos)
           .Where(o => (o.Finalizada == 0)
           && (o.Modulo == ProyectoModulo))
           .OrderBy(o => o.NroObra)
           .ToListAsync();
            if (obras == null)
            {
                return BadRequest("No hay Obras.");
            }
            return Ok(obras);
        }

        [HttpPost]
        [Route("GetObrasTodas")]
        public async Task<IActionResult> GetObrasTodas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var obras = await _dataContext.Obras
            .Include(p => p.ObrasDocumentos)
           .Where(o => (o.Finalizada == 0))
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

        [HttpPost]
        [Route("GetObrasReclamos/{ProyectoModulo}")]
        public IActionResult GetObrasReclamos(string ProyectoModulo)
        {
            return Ok(_dataContext.Obras
                .Where(o => o.HabilitaReclamosAPP == 1 && o.Modulo == ProyectoModulo)
                );
        }

        [HttpPost]
        [Route("GetObrasReclamosTodas")]
        public IActionResult GetObrasReclamosTodas()
        {
            return Ok(_dataContext.Obras
                .Where(o => o.HabilitaReclamosAPP == 1)
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

        [HttpPost]
        [Route("GetObrasEPP/{ProyectoModulo}")]
        public IActionResult GetObrasEPP(string ProyectoModulo)
        {
            return Ok(_dataContext.Obras
                .Where(o => o.CORRESPONDEABONADOS == 1 && o.Modulo == ProyectoModulo)
                );
        }
    }
}