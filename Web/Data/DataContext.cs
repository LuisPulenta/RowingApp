using RowingApp.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Obra> Obras { get; set; }

        public DbSet<ObrasPoste> ObrasPostes { get; set; }
        public DbSet<ObrasPosteCajasAPP> ObrasPostesCajasAPP { get; set; }
        public DbSet<ObrasPostesCajaDetalle> ObrasPostesCajasDetalle { get; set; }
        public DbSet<ObrasPosteCajaDetalleAPP> ObrasPostesCajasDetalleAPP { get; set; }

        public DbSet<ObrasDocumento> ObrasDocumentos { get; set; }
        public DbSet<Catalogo> Catalogos { get; set; }
        public DbSet<StateEntity> States { get; set; }
        public DbSet<CausantesNovedade> CausantesNovedades { get; set; }
        public DbSet<TipoNoveda> TipoNovedad { get; set; }

        public DbSet<SHInspeccio> SHInspeccion { get; set; }
        public DbSet<SHInspeccionDetall> SHInspeccionDetalle { get; set; }
        public DbSet<SHGrupoFormPonderado> SHGrupoFormPonderados { get; set; }
        public DbSet<SHGrupoFormulario> SHGrupoFormularios { get; set; }
        public DbSet<SHTiposTrabajo> SHTiposTrabajos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<VistaInspeccione> VistaInspecciones { get; set; }
        public DbSet<WebSesio> WebSesion { get; set; }
        public DbSet<UsuariosGeo> UsuariosGeos { get; set; }
        public DbSet<VistaInspeccionesFoto> VistaInspeccionesFotos { get; set; }
        public DbSet<WRemitosCab> WRemitosCab { get; set; }
        public DbSet<WRemitosDet> WRemitosDet { get; set; }
        public DbSet<Subcontratista> Subcontratistas { get; set; }
        public DbSet<ObrasNuevoSuministro> ObrasNuevoSuministros { get; set; }
        public DbSet<ObrasNuevoSuministroDe> ObrasNuevoSuministrosDet { get; set; }
        public DbSet<ObrasReparo> ObrasReparos { get; set; }
        public DbSet<StandardReparo> StandardReparos { get; set; }
        public DbSet<CausantesJuicio> CausantesJuicios { get; set; }
        public DbSet<CausantesJuiciosMediacione> CausantesJuiciosMediaciones { get; set; }
        public DbSet<CausantesJuiciosNotificacione> CausantesJuiciosNotificaciones { get; set; }
        public DbSet<CausantesJuiciosContrapart> CausantesJuiciosContraparte { get; set; }
        public DbSet<CausantesPresentismo> CausantesPresentismos { get; set; }
        public DbSet<CausantesEstado> CausantesEstados { get; set; }
        public DbSet<CausantesZonasZona> CausantesZonasZonas { get; set; }
        public DbSet<CausantesActividade> CausantesActividades { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<TurnosNoche> VistaTurnosNoches { get; set; }
        public DbSet<ObraEstado> EstadosPEP { get; set; }
        public DbSet<ObraSubEstado> EstadosPEPSub { get; set; }
        public DbSet<ElementosEnCalleCab> ElementosEnCalle { get; set; }
        public DbSet<ElementosEnCalleDet> ElementosEnCalleD { get; set; }
        public DbSet<ElemenEnCalleVist> ElemenEnCalleVista { get; set; }
        public DbSet<ObrasAsignacionSub> ObrasAsignacionSubC { get; set; }
        public DbSet<VistaObrasAsignacionSub> VistaObrasAsignacionSubC { get; set; }
        public DbSet<CabeceraCertificacio> CabeceraCertificacion { get; set; }
        public DbSet<CabeceraCertificacionObjeto> CabeceraCertificacionObjetos { get; set; }
        public DbSet<CodigosProduccio> CodigosProduccion { get; set; }
        public DbSet<CausantesObra> CausantesObras { get; set; }
        public DbSet<AppReclamo> AppReclamos { get; set; }
        public DbSet<VistaStocksMaximo> VistaStocksMaximos { get; set; }
        public DbSet<StockMaximosSubcon> StockMaximosSubcons { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Obra>()
       .HasMany(o => o.ObrasDocumentos);  // Obra tiene muchos ObrasDocumento;

            modelBuilder.Entity<SHGrupoFormulario>()
                .HasKey(c => new { c.IDCLIENTE, c.IDTIPOTRABAJO, c.IDGRUPOFORMULARIO });

            modelBuilder.Entity<SHGrupoFormPonderado>()
               .HasKey(c => new { c.IDCLIENTE, c.IDGRUPOFORMULARIO, c.DETALLEF });

            modelBuilder.Entity<StockMaximosSubcon>()
               .HasKey(c => new { c.GRUPOC, c.CAUSANTE, c.CODIGOSIAG });

            modelBuilder.Entity<ObrasDocumento>()
    .HasOne(od => od.Obra)
    .WithMany(o => o.ObrasDocumentos)
    .HasForeignKey(od => od.NROOBRA)
    .HasPrincipalKey(o => o.NroObra); // <- clave principal personalizada

        }
    }
}