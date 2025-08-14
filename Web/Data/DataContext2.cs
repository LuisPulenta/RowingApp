using RowingApp.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    public class DataContext2 : IdentityDbContext<User>
    {
        public DataContext2(DbContextOptions<DataContext2> options) : base(options)
        {
        }
        public DbSet<Causante> VistaCausantesApp { get; set; }
        public DbSet<Causante2> Causantes { get; set; }
        public DbSet<CausanteInstalacione> VistaCausantesAppInstalaciones { get; set; }
        public DbSet<Entrega> ProductosStock { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<VehiculosKilometraje> VehiculosKilometrajes { get; set; }
        public DbSet<VehiculosProgramaPrev> VehiculosProgramasPrev { get; set; }
        public DbSet<VFlotaApp> VFlotaApps { get; set; }
        public DbSet<VFlotaPreventivo> VFlotaPreventivos { get; set; }
        public DbSet<VehiculosSiniestro> VehiculosSiniestros { get; set; }
        public DbSet<VehiculosSiniestrosFoto> VehiculosSiniestrosFotos { get; set; }
        public DbSet<VehiculosCheckList> VehiculosCheckLists { get; set; }
        public DbSet<VistaFlotasChecklistAPP> VistaFlotasChecklistAPP { get; set; }
        public DbSet<VehiculosCheckListsFoto> VehiculosCheckListsFotos { get; set; }
        public DbSet<ConteoCiclicoCa> VistaConteoCiclicoCab { get; set; }
        public DbSet<ConteoCiclicoDe> ConteoCiclicoDet { get; set; }
        public DbSet<VehiculosPartesTurno> VehiculosPartesTurnos { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<AppInstalacionesEquipo> AppInstalacionesEquipos { get; set; }
        public DbSet<VistaSeriesSinUsarLotesDetalle> VistaSeriesSinUsarLotesDetalles { get; set; }
        public DbSet<AppInstalacionesEquiposDetalle> AppInstalacionesEquiposDetalles { get; set; }
        public DbSet<LotesDetall> LotesDetalle { get; set; }
        public DbSet<AppInstalacionesMateriale> AppInstalacionesMateriales { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Causante3> VistaCausantesAppRecibos { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<CausanteRecibo> CausantesRec { get; set; }
        public DbSet<VistaAppInstalacionesEquipo> VistaAppInstalacionesEquipos { get; set; }
    }
}