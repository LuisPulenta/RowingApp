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
    public class VehiculosController : ControllerBase
    {
        private readonly DataContext2 _dataContext;

        public VehiculosController(DataContext2 dataContext)
        {
            _dataContext = dataContext;
        }


        [HttpPost("GetVehiculoByChapa/{chapa}")]
        public async Task<ActionResult<Data.Entities.Causante>> GetVehiculoByChapa(string chapa)
        {
            Data.Entities.Vehiculo vehiculo = await _dataContext.Vehiculos
                .FirstOrDefaultAsync(o => o.NUMCHA.ToLower() == chapa.ToLower());

            if (vehiculo == null)
            {
                return BadRequest("El Vehículo no existe.");
            }

            VehiculoResponse response = new VehiculoResponse
            {
                ANIOFA = vehiculo.ANIOFA,
                CAMPOMEMO = vehiculo.CAMPOMEMO,
                CHASIS = vehiculo.CHASIS,
                KMHSACTUAL = vehiculo.KMHSACTUAL,
                NUMCHA = vehiculo.NUMCHA,
                CentroCosto = vehiculo.CentroCosto,
                CodProducto = vehiculo.CodProducto,
                CODVEH = vehiculo.CODVEH,
                Descripcion = vehiculo.Descripcion,
                FechaVencITV = vehiculo.FechaVencITV,
                FechaVencObleaGAS = vehiculo.FechaVencObleaGAS,
                Habilitado = vehiculo.Habilitado,
                Modulo = vehiculo.Modulo,
                NMOTOR = vehiculo.NMOTOR,
                NroPolizaSeguro = vehiculo.NroPolizaSeguro,
                PropiedadDe = vehiculo.PropiedadDe,
                Telepase = vehiculo.Telepase,
                UsaHoras = vehiculo.UsaHoras,
            };
            return Ok(response);
        }
    }
}