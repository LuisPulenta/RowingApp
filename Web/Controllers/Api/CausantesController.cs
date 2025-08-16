using RowingApp.Common.Requests;
using RowingApp.Common.Responses;
using Web.Data;
using RowingApp.Web.Data.Entities;
using RowingApp.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace RowingApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CausantesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly DataContext2 _dataContext2;
        private readonly IImageHelper _imageHelper;

        public CausantesController(DataContext dataContext,DataContext2 dataContext2,IImageHelper imageHelper)
        {
            _dataContext = dataContext;
            _dataContext2 = dataContext2;
            _imageHelper = imageHelper;
        }

        [HttpPost]
        [Route("GetCausanteByCodigo")]
        public async Task<IActionResult> GetCausante(CausanteRequest codigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            Data.Entities.Causante user = await _dataContext2.VistaCausantesApp.FirstOrDefaultAsync
                (o => (o.codigo.ToLower() == codigo.Codigo.ToLower() || o.NroSAP.ToLower() == codigo.Codigo.ToLower()));

            if (user == null)
            {
                return BadRequest("El Empleado no existe.");
            }

            CausanteResponse response = new CausanteResponse
            {
                codigo = user.codigo,
                nombre = user.nombre,
                encargado = user.encargado,
                NroCausante = user.NroCausante,
                telefono = user.telefono,
                NroSAP=user.NroSAP,
                grupo=user.grupo,
                estado=user.estado,
                LinkFoto=user.LinkFoto,
                direccion = user.direccion,
                Numero = user.Numero,
                TelefonoContacto1 = user.TelefonoContacto1,
                TelefonoContacto2 = user.TelefonoContacto2,
                TelefonoContacto3 = user.TelefonoContacto3,
                fecha = user.fecha,
                NotasCausantes = user.NotasCausantes,
                ciudad=user.ciudad,
                Provincia = user.Provincia,
                CodigoSupervisorObras=user.CodigoSupervisorObras,
                ZonaTrabajo=user.ZonaTrabajo,
                NombreActividad=user.NombreActividad,
                notas=user.notas,
                PerteneceCuadrilla=user.PerteneceCuadrilla,
                FirmaDigitalAPP=user.FirmaDigitalAPP
            };

            return Ok(response);
        }

        // GET: api/Users/5
        [HttpGet("GetCausanteByCodigo2/{codigo}")]
        public async Task<ActionResult<Data.Entities.Causante>> GetCausante2(string codigo)
        {
            Data.Entities.Causante causante = await _dataContext2.VistaCausantesApp
                .FirstOrDefaultAsync(o => 
                    (o.codigo.ToLower() == codigo.ToLower() || o.NroSAP.ToLower() == codigo.ToLower()) 
                    &&
                    (o.estado == true)
                    );

            if (causante == null)
            {
                return NotFound();
            }
            return causante;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCausante([FromRoute] int id, [FromBody] CausanteRequest2 request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.Id)
            {
                return BadRequest();
            }

            

            var oldCausante = await _dataContext2.VistaCausantesApp.FindAsync(request.Id);
            if (oldCausante == null)
            {
                return BadRequest("El Causante no existe.");
            }

            string imageId = oldCausante.LinkFoto;
            if (request.Image != null && request.Image.Length > 0)
            {
                imageId = _imageHelper.UploadImage(request.Image, "Causantes");
            }

            string firmaId = oldCausante.FirmaDigitalAPP;
            if (request.Firma != null && request.Firma.Length > 0)
            {
                firmaId = _imageHelper.UploadImage(request.Image, "Recibos");
            }

            oldCausante.telefono = request.telefono;
            oldCausante.LinkFoto = imageId;
            oldCausante.direccion = request.direccion;
            oldCausante.Numero = request.Numero;
            oldCausante.TelefonoContacto1 = request.TelefonoContacto1;
            oldCausante.TelefonoContacto2 = request.TelefonoContacto2;
            oldCausante.TelefonoContacto3 = request.TelefonoContacto3;
            oldCausante.fecha = request.fecha;
            oldCausante.NotasCausantes = request.NotasCausantes;
            oldCausante.ciudad = request.ciudad;
            oldCausante.Provincia = request.Provincia;
            oldCausante.ZonaTrabajo = request.ZonaTrabajo;
            oldCausante.NombreActividad = request.ZonaTrabajo;
            oldCausante.FirmaDigitalAPP = firmaId;

            _dataContext2.VistaCausantesApp.Update(oldCausante);
            await _dataContext2.SaveChangesAsync();
            return Ok();
        }


        [HttpPost]
        [Route("GetCausantesByGrupo/{Grupo}")]
        public IActionResult GetCausantesByProyectoModulo(string Grupo)
        {
            return Ok(_dataContext2.VistaCausantesApp
                .Where(o => o.grupo == Grupo && o.estado==true && o.VisualizaSPR==1)
                .OrderBy(o => o.nombre)
                );
        }

        [HttpPost]
        [Route("GetCausantesBySupervisor/{id}")]
        public IActionResult GetCausantesBySupervisor(int id)
        {
            return Ok(_dataContext2.VistaCausantesApp
                .Where(o => (o.CodigoSupervisorObras == id && o.estado == true))
                .OrderBy(o => o.nombre)
                );
        }

        [HttpPost]
        [Route("GetTalleres")]

        public IActionResult GetTalleres()
        {

            var talleres = (_dataContext2.VistaCausantesApp
                .Where(o => o.grupo == "TAL" && o.estado == true)
                .OrderBy(o => o.nombre));

            return Ok(talleres);
        }

        [HttpPost]
        [Route("GetCausantesEstados")]
        public IActionResult GetCausantesEstados()
        {
            return Ok(_dataContext.CausantesEstados
                .Where(o => o.SoloAPP == 1)
                .OrderBy(o => o.NOMENCLADORESTADO)
                );
        }

        [HttpPost]
        [Route("GetCausantesZonas")]
        public IActionResult GetCausantesZonas()
        {
            return Ok(_dataContext.CausantesZonasZonas
                
                .OrderBy(o => o.NOMBREZONA)
                );
        }

        [HttpPost]
        [Route("GetCausantesActividades")]
        public IActionResult GetCausantesActividades()
        {
            return Ok(_dataContext.CausantesActividades

                .OrderBy(o => o.NOMBREACTIVIDAD)
                );
        }

        //-----------------------------------------------------------------------------
        //----------------- METODOS PARA PRESENTISMO ----------------------------------
        //-----------------------------------------------------------------------------

        //------- Trae los empleados a cargo de un Supervisor -------------------------
        [HttpPost]
        [Route("GetPresentismosBySupervisorDay/{id}/{year}/{month}/{day}")]

        public IActionResult GetPresentismosBySupervisorDay(int id, int year, int month, int day)
        {
            return Ok(_dataContext.CausantesPresentismos
                .Where(o => (o.IDSUPERVISOR == id) && (o.FECHA.Year == year) && (o.FECHA.Month == month) && (o.FECHA.Day == day))
                .OrderBy(o => o.CAUSANTEC)
                );
        }


        //------- Trae los presentismos guardados como Turno Noche -------------------------
        [HttpPost]
        [Route("GetPresentismosTurnoNoche/{id}")]

        public IActionResult GetPresentismosTurnoNoche(int id)
        {
            return Ok(_dataContext.VistaTurnosNoches
                 .Where(o => (o.IDSUPERVISOR == id) && (o.ESTADO == "Turno Noche"))
                .OrderBy(o => o.FECHA).ThenBy (o=>o.CAUSANTEC)
                );
        }

        //--------------------------- Graba los Presentismos --------------------------
        [HttpPost]
        [Route("PostPresentismos")]
        public async Task<IActionResult> PostPresentismos([FromBody] CausantesPresentismo request)
        
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Verifica si ya existe el Presentismo

            var presentismo = _dataContext.CausantesPresentismos
                .Where(o => 
                (o.FECHA.Year == request.FECHA.Year) 
                && (o.FECHA.Month == request.FECHA.Month) 
                && (o.FECHA.Day == request.FECHA.Day) 
                && (o.CAUSANTEC == request.CAUSANTEC))
                .OrderBy(o => o.CAUSANTEC)
                .ToList();

            if (presentismo.Count != 0)
            {
                //borra el presentismo existente
                await DeletePresentismo(presentismo[0].IDPRESENTISMO);
            }


            //Graba Presentismo
            _dataContext.CausantesPresentismos.Add(request);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }


        //--------------------------- Put de un Presentismo --------------------------
        [HttpPut]
        [Route("PutPresentismo/{id}")]
        public async Task<IActionResult> PutPresentismo([FromRoute] int id, [FromBody] CausantesPresentismo request)
        {
            if (!ModelState.IsValid)
            {
               return BadRequest(ModelState);
            }

            if (id != request.IDPRESENTISMO)
            {
               return BadRequest();
            }

            var oldPresentismo = await _dataContext.CausantesPresentismos.FindAsync(request.IDPRESENTISMO);
            if (oldPresentismo == null)
            {
                return BadRequest("El Causante no existe.");
            }

            oldPresentismo.ESTADO = request.ESTADO;

            _dataContext.CausantesPresentismos.Update(oldPresentismo);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }


        //---------------------------- Borra un Presentismo -------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePresentismo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var Presentismo = await _dataContext.CausantesPresentismos
                .FirstOrDefaultAsync(p => p.IDPRESENTISMO == id);
            if (Presentismo == null)
            {
                return this.NotFound();
            }

            _dataContext.CausantesPresentismos.Remove(Presentismo);
            await _dataContext.SaveChangesAsync();
            return Ok("Presentismo borrado");
        }

        //---------------------------- FIN DE LOS METODOS DE PRESENTISMO ------------------

        //------------------------------------------------------------------------------------
        [HttpPost]
        [Route("GetCausanteByGrupoAndByCodigo2")]
        public async Task<IActionResult> GetCausantePPRByCodigo2(CausanteRequest3 request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            Causante causante = await _dataContext2.VistaCausantesApp.FirstOrDefaultAsync
                (o => o.codigo.ToLower() == request.Codigo.ToLower() && o.grupo == request.Grupo.ToLower() && o.estado == true);

            if (causante == null)
            {
                return BadRequest("El Empleado no existe.");
            }

            CausanteResponse response = new CausanteResponse
            {
                codigo = causante.codigo,
                nombre = causante.nombre,
                encargado = causante.encargado,
                NroCausante = causante.NroCausante,
                telefono = causante.telefono,
                NroSAP = causante.NroSAP,
                grupo = causante.grupo,
                estado = causante.estado,
                LinkFoto = causante.LinkFoto,
                direccion = causante.direccion,
                Numero = causante.Numero,
                TelefonoContacto1 = causante.TelefonoContacto1,
                TelefonoContacto2 = causante.TelefonoContacto2,
                TelefonoContacto3 = causante.TelefonoContacto3,
                fecha = causante.fecha,
                NotasCausantes = causante.NotasCausantes,
                ciudad = causante.ciudad,
                Provincia = causante.Provincia,
                CodigoSupervisorObras = causante.CodigoSupervisorObras,
                ZonaTrabajo = causante.ZonaTrabajo,
                NombreActividad = causante.NombreActividad,
                notas = causante.notas,
                PerteneceCuadrilla = causante.PerteneceCuadrilla,
                FirmaDigitalAPP = causante.FirmaDigitalAPP
            };

            return Ok(response);
        }

        //------------------------------------------------------------------------------------
        [HttpPost]
        [Route("GetCausanteByGrupoAndByCodigo")]
        public async Task<IActionResult> GetCausantePPRByCodigo(CausanteRequest3 request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            Causante causante = await _dataContext2.VistaCausantesApp.FirstOrDefaultAsync
                (o => o.codigo.ToLower() == request.Codigo.ToLower() && o.grupo== request.Grupo.ToLower() && o.estado==false);

            if (causante == null)
            {
                return BadRequest("El Empleado no existe.");
            }

            CausanteResponse response = new CausanteResponse
            {
                codigo = causante.codigo,
                nombre = causante.nombre,
                encargado = causante.encargado,
                NroCausante = causante.NroCausante,
                telefono = causante.telefono,
                NroSAP = causante.NroSAP,
                grupo = causante.grupo,
                estado = causante.estado,
                LinkFoto = causante.LinkFoto,
                direccion = causante.direccion,
                Numero = causante.Numero,
                TelefonoContacto1 = causante.TelefonoContacto1,
                TelefonoContacto2 = causante.TelefonoContacto2,
                TelefonoContacto3 = causante.TelefonoContacto3,
                fecha = causante.fecha,
                NotasCausantes = causante.NotasCausantes,
                ciudad = causante.ciudad,
                Provincia = causante.Provincia,
                CodigoSupervisorObras = causante.CodigoSupervisorObras,
                ZonaTrabajo = causante.ZonaTrabajo,
                NombreActividad = causante.NombreActividad,
                notas = causante.notas,
                PerteneceCuadrilla = causante.PerteneceCuadrilla,
                FirmaDigitalAPP = causante.FirmaDigitalAPP
            };

            return Ok(response);
        }

        //------------------------------------------------------------------------------------

        [HttpPut]
        [Route("ReactivarLegajo/{id}")]
        public async Task<IActionResult> ReactivarLegajo([FromRoute] int id, [FromBody] CausanteRequest3 request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id.ToString() != request.Codigo)
            {
                return BadRequest();
            }

            Causante2 oldCausante = await _dataContext2.Causantes.FirstOrDefaultAsync
                (o => o.codigo.ToLower() == request.Codigo.ToLower() && o.grupo.ToLower() == request.Grupo.ToLower() && o.estado == false);


            if (oldCausante == null)
            {
                return BadRequest("El Causante no existe.");
            }
            oldCausante.estado = true;
            _dataContext2.Causantes.Update(oldCausante);
            await _dataContext2.SaveChangesAsync();
            return Ok();
        }
    }
}