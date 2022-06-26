using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GenericApp.Web.Data
{
    public class DataContext2 : IdentityDbContext<User>
    {
        public DataContext2(DbContextOptions<DataContext2> options) : base(options)
        {
        }
        public DbSet<Causante> Causantes { get; set; }
        public DbSet<Entrega> ProductosStock { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<VehiculosKilometraje> VehiculosKilometrajes { get; set; }
        public DbSet<VehiculosProgramaPrev> VehiculosProgramasPrev { get; set; }
        public DbSet<VFlotaApp> VFlotaApps { get; set; }
        public DbSet<VFlotaPreventivo> VFlotaPreventivos { get; set; }
        public DbSet<VehiculosSiniestro> VehiculosSiniestros { get; set; }
        public DbSet<VehiculosSiniestrosFoto> VehiculosSiniestrosFotos { get; set; }

    }
}