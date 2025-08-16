using Web.Data;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace RowingApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfVentasTempController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public InfVentasTempController(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpDelete()]
        [Route("DeleteVentas")]
        public IActionResult DeleteVentas()
        {
            BorrarTabla();
            return Ok("Borrado");
        }

        public void BorrarTabla()
        {

            // Crear la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Crear el comando para ejecutar el procedimiento almacenado
                               

                //using (SqlCommand command = new SqlCommand("LimpiaHombreConCasco", connection))
                using (SqlCommand command = new SqlCommand("exec [dbo].[LimpiaHombreConCasco]", connection))
                {
                    //command.CommandType = CommandType.StoredProcedure;
                    command.CommandType = CommandType.Text;
                    
                    // Abrir la conexión y ejecutar el comando
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {   
                    }
                }
            }            
        }
    }
}