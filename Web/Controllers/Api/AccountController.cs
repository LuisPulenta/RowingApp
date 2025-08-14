using Common.Enums;
using RowingApp.Common.Requests;
using RowingApp.Common.Responses;
using Web.Data;
using RowingApp.Web.Data.Entities;
using RowingApp.Web.Helpers;
using RowingApp.Web.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GenericApp.Common.Requests;
namespace RowingApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly DataContext2 _dataContext2;
        private readonly IImageHelper _imageHelper;
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;

        public AccountController(DataContext dataContext, DataContext2 dataContext2, IImageHelper imageHelper,IUserHelper userHelper,
            IConfiguration configuration)
        {
            _dataContext = dataContext;
            _dataContext2 = dataContext2;
            _imageHelper = imageHelper;
            _userHelper = userHelper;
            _configuration = configuration;
        }

        //-----------------------------------------------------------------------------------
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

                var user2 = await _dataContext2.VistaCausantesApp.FirstOrDefaultAsync(
                    o => o.codigo.ToLower() == userRequest.Email.ToLower()
                    && o.NroSAP.ToLower() == userRequest.Password.ToLower()
                    && o.estado == true
                    );
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
                    CODIGOGRUPO = user2.grupo,
                    FechaCaduca = 0,
                    IntentosInvDiario = 0,
                    OpeAutorizo = 0,
                    HabilitaNuevoSuministro = 0,
                    HabilitaVeredas = 0,
                    HabilitaJuicios = 0,
                    HabilitaPresentismo = 0,
                    HabilitaSeguimientoUsuarios = 0,
                    HabilitaVerObrasCerradas = 0,
                    HabilitaElementosCalle=0,
                    HabilitaCertificacion=0,
                    CONCEPTOMOVA = 0,
                    LimitarGrupo = 0,
                    RUBRO = 0,
                    CONCEPTOMOV=0,
                    HabilitaInstalacionesAPP=user2.HabilitaInstalacionesAPP,
                    grupo=user2.grupo,
                    FirmaUsuario=user2.FirmaDigitalAPP,
                };

                return Ok(response2);
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
                OpeAutorizo = user.OpeAutorizo,
                HabilitaNuevoSuministro = user.HabilitaNuevoSuministro,
                HabilitaVeredas = user.HabilitaVeredas,
                HabilitaJuicios = user.HabilitaJuicios,
                HabilitaPresentismo = user.HabilitaPresentismo,
                HabilitaSeguimientoUsuarios = user.HabilitaSeguimientoUsuarios,
                HabilitaVerObrasCerradas=user.HabilitaVerObrasCerradas,
                HabilitaElementosCalle=user.HabilitaElementosCalle,
                HabilitaCertificacion=user.HabilitaCertificacion,
                CONCEPTOMOVA = user.CONCEPTOMOVA,
                LimitarGrupo = user.LimitarGrupo,
                FirmaUsuario = user.FirmaUsuario,
                RUBRO = user.RUBRO,
                CONCEPTOMOV=user.CONCEPTOMOV,
                AppIMEI=user.AppIMEI,
                HabilitaInstalacionesAPP=0
            };

            return Ok(response);
        }

        //-----------------------------------------------------------------------------------
        [HttpPost]
        [Route("CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserAsync(model.Username);
                if (user != null)
                {
                    var result = await _userHelper.ValidatePasswordAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(15),
                            signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return Created(string.Empty, results);
                    }
                }
            }

            return BadRequest();
        }

        //-----------------------------------------------------------------------------------
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("GetUserByEmail2")]
        public async Task<IActionResult> GetUserByEmail2([FromBody] EmailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            User userEntity = await _userHelper.GetUserAsync(request.Email);
            if (userEntity == null)
            {
                return NotFound("Este Usuario no existe.");
            }

            return Ok(userEntity);
        }

        //-----------------------------------------------------------------------------------
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
                OpeAutorizo = user.OpeAutorizo,
                HabilitaNuevoSuministro = user.HabilitaNuevoSuministro,
                HabilitaVeredas = user.HabilitaVeredas,
                HabilitaJuicios = user.HabilitaJuicios,
                HabilitaPresentismo = user.HabilitaPresentismo,
                HabilitaSeguimientoUsuarios = user.HabilitaSeguimientoUsuarios,
                HabilitaVerObrasCerradas=user.HabilitaVerObrasCerradas,
                HabilitaElementosCalle=user.HabilitaElementosCalle,
                HabilitaCertificacion=user.HabilitaCertificacion,
                CONCEPTOMOVA = user.CONCEPTOMOVA,
                LimitarGrupo = user.LimitarGrupo,
                RUBRO = user.RUBRO,
                CONCEPTOMOV=user.CONCEPTOMOV,
                AppIMEI=user.AppIMEI,
                HabilitaInstalacionesAPP=0
            };
            return Ok(response);
        }

        //-----------------------------------------------------------------------------------
        [HttpPost("{login}")]
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

            var oldUsuario = await _dataContext.Usuarios.FirstOrDefaultAsync(x => x.Login == request.Login);

            if (oldUsuario == null)
            {
                return BadRequest("El Usuario no existe.");
            }

            if (oldUsuario.Estado == 0)
            {
                oldUsuario.Estado = 1;
            }
            else
            {
                oldUsuario.Estado = 0;
            }

            oldUsuario.FechaCaduca = request.FechaCaduca;
            oldUsuario.IntentosInvDiario = 0;
            oldUsuario.OpeAutorizo = request.IdUsuarioAutoriza;

            _dataContext.Usuarios.Update(oldUsuario);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        //-----------------------------------------------------------------------------------
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

        //-----------------------------------------------------------------------------------
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

        //-----------------------------------------------------------------------------------
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

        //-----------------------------------------------------------------------------------
        [HttpGet]
        [Route("GetObras2/{ProyectoModulo}")]
        public async Task<IActionResult> GetObras2(string ProyectoModulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var obras = await _dataContext.Obras
            .Include(p => p.ObrasDocumentos)
           .Where(o => (o.Finalizada == 0 && o.ULTIMAACTA == 0)
           && (o.Modulo == ProyectoModulo))
           .OrderBy(o => o.NroObra)
           .ToListAsync();
            if (obras == null)
            {
                return BadRequest("No hay Obras.");
            }
            return Ok(obras);
        }

        //-----------------------------------------------------------------------------------
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
           .Where(o => (o.Finalizada == 0 && o.ULTIMAACTA == 0)
           && (o.Modulo == ProyectoModulo))
           .OrderBy(o => o.NroObra)
           .ToListAsync();
            if (obras == null)
            {
                return BadRequest("No hay Obras.");
            }
            return Ok(obras);
        }

        //-----------------------------------------------------------------------------------
        [HttpPost]
        [Route("GetObrasTodas/{ProyectoModulo}")]
        public async Task<IActionResult> GetObrasTodas(string ProyectoModulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var obras = await _dataContext.Obras
            .Include(p => p.ObrasDocumentos)
             .Where(o => (o.Modulo == ProyectoModulo)
             &&
             (
             (o.Finalizada == 0 && o.ULTIMAACTA == 0)
             ||
             (o.Finalizada == 1
             //&& o.FECHAFINALIZADA> DateTime.Now.AddDays(-180.0)
             )
             )
           )
           .OrderBy(o => o.NroObra)
           .ToListAsync();
            if (obras == null)
            {
                return BadRequest("No hay Obras.");
            }
            return Ok(obras);
        }

        //-----------------------------------------------------------------------------------
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

        //-----------------------------------------------------------------------------------
        [HttpGet]
        [Route("GetObrasReclamosRowing")]
        public IActionResult GetObrasReclamosRowing()
        {
            return Ok(_dataContext.Obras
                .Where(o => o.HabilitaReclamosAPP == 1 && o.Modulo == "Rowing")
                );
        }

        //-----------------------------------------------------------------------------------
        [HttpGet]
        [Route("GetObrasReclamosEnergia")]
        public IActionResult GetObrasReclamosEnergia()
        {
            return Ok(_dataContext.Obras
                .Where(o => o.HabilitaReclamosAPP == 1 && o.Modulo == "Energia")
                );
        }

        //-----------------------------------------------------------------------------------
        [HttpGet]
        [Route("GetObrasReclamosObrasTasa")]
        public IActionResult GetObrasReclamosObrasTasa()
        {
            return Ok(_dataContext.Obras
                .Where(o => o.HabilitaReclamosAPP == 1 && o.Modulo == "ObrasTasa")
                );
        }

        //-----------------------------------------------------------------------------------
        [HttpPost]
        [Route("GetObrasReclamos/{ProyectoModulo}")]
        public IActionResult GetObrasReclamos(string ProyectoModulo)
        {
            return Ok(_dataContext.Obras
                .Where(o => o.HabilitaReclamosAPP == 1 && o.Modulo == ProyectoModulo)
                );
        }

        //-----------------------------------------------------------------------------------
        [HttpPost]
        [Route("GetObrasReclamosTodas")]
        public IActionResult GetObrasReclamosTodas()
        {
            return Ok(_dataContext.Obras
                .Where(o => o.HabilitaReclamosAPP == 1)
                );
        }

        //-----------------------------------------------------------------------------------
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

        //-----------------------------------------------------------------------------------
        [HttpPost]
        [Route("GetObrasEPP/{ProyectoModulo}")]
        public IActionResult GetObrasEPP(string ProyectoModulo)
        {
            return Ok(_dataContext.Obras
                .Where(o => o.CORRESPONDEABONADOS == 1 && o.Modulo == ProyectoModulo)
                );
        }

        //-----------------------------------------------------------------------------------
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest3 request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Models.Response
                {
                    IsSuccess = false,
                    Message = "Bad request",
                    Result = ModelState
                });
            }


            User user = await _userHelper.GetUserAsync(request.Email);
            if (user == null)
            {
                return BadRequest(new Models.Response
                {
                    IsSuccess = false,
                    Message = "Usuario no encontrado"
                });
            }

            IdentityResult result = await _userHelper.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(new Models.Response
                {
                    IsSuccess = false,
                    Message = result.Errors.FirstOrDefault().Description
                });
            }

            return Ok(new Models.Response
            {
                IsSuccess = true,
                Message = "El password fue cambiado con éxito."
            });
        }

        //-----------------------------------------------------------------------------------
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("UpdateLoginDate")]
        public async Task<IActionResult> UpdateLoginDate([FromBody] string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            User userEntity = await _userHelper.GetUserAsync(email);
            if (userEntity == null)
            {
                return BadRequest("Este Usuario no existe.");
            }
            userEntity.LastLogin = DateTime.Now;
            
            IdentityResult respose = await _userHelper.UpdateUserAsync(userEntity);
            if (!respose.Succeeded)
            {
                return BadRequest(respose.Errors.FirstOrDefault().Description);
            }
            return NoContent();
        }

        //-----------------------------------------------------------------------------------
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("UpdateChangePasswordDate")]
        public async Task<IActionResult> UpdateChangePasswordDate([FromBody] string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User userEntity = await _userHelper.GetUserAsync(email);
            if (userEntity == null)
            {
                return BadRequest("Este Usuario no existe.");
            }
            userEntity.ChangePassword = DateTime.Now;

            IdentityResult respose = await _userHelper.UpdateUserAsync(userEntity);
            if (!respose.Succeeded)
            {
                return BadRequest(respose.Errors.FirstOrDefault().Description);
            }
            return NoContent();
        }

        //-----------------------------------------------------------------------------------
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(_dataContext2.Users.Where(o => o.UserType == UserType.User));
        }

        //-----------------------------------------------------------------------------------
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("GetCausantesRecibos")]
        public async Task<IActionResult> GetCausantesRecibos()
        {
            var causantes = _dataContext2.VistaCausantesAppRecibos
                .OrderBy(o => o.nombre).ToList();

            foreach (var causante in causantes)
            {
                if (causante.telefono == null)
                {
                    causante.telefono = "";
                }

                causante.telefono = causante.telefono.Trim();
                causante.nombre = causante.nombre.Trim();
                causante.email = causante.email.Trim();

                await CheckUserAsync(causante.NroCausante, causante.NroSAP, causante.nombre, causante.nombre, causante.email, causante.telefono, causante.codigo, causante.grupo, UserType.User);
            }

            var users = _dataContext2.Users
                .OrderBy(o => o.codigo).ToList();
            foreach (var user in users)
            {
                if (user.UserType != UserType.Admin)
                {
                    var causante = await _dataContext2.VistaCausantesAppRecibos.FirstOrDefaultAsync(o => o.NroCausante == user.NroCausante);

                    if (causante == null)
                    {
                        await _userHelper.DeleteUserAsync(user.Email);
                    }
                }
            }
            return Ok();
        }

        //----------------------------------------------------------------------------------------------------
        private async Task<User> CheckUserAsync(
            int nrocausante,
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string codigo,
            string grupo,
            UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                User newuser = new User
                {
                    NroCausante = nrocausante,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Document = document,
                    UserType = userType,
                    codigo = codigo,
                    grupo = grupo
                };

                if (email != "gaos@keypress.com.ar")
                {
                    try
                    {
                        await _userHelper.AddUserAsync(newuser, "123456");
                    }
                    catch (System.Exception e)
                    {
                        throw;
                    }

                }
                else
                {
                    await _userHelper.AddUserAsync(newuser, "keyroot");
                }
                await _userHelper.AddUserToRoleAsync(newuser, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(newuser);
                await _userHelper.ConfirmEmailAsync(newuser, token);
                user = newuser;
            }

            return user;
        }

        //-----------------------------------------------------------------------------------
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] EmailRequest email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Models.Response
                {
                    IsSuccess = false,
                    Message = "Bad request",
                    Result = ModelState
                });
            }

            User user = await _userHelper.GetUserAsync(email.Email);
            var causante = await _dataContext2.VistaCausantesAppRecibos.FirstOrDefaultAsync(o => o.NroCausante == user.NroCausante);
            if (causante.telefono == null)
            {
                causante.telefono = "";
            }

            causante.telefono = causante.telefono.Trim();
            causante.nombre = causante.nombre.Trim();
            causante.email = causante.email.Trim();
            await _userHelper.DeleteUserAsync(email.Email);
            await CheckUserAsync(causante.NroCausante, causante.NroSAP, causante.nombre, causante.nombre, causante.email, causante.telefono, causante.codigo, causante.grupo, UserType.User);

            return Ok();
        }
    }
}