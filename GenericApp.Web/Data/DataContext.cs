using GenericApp.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GenericApp.Web.Data
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
        public DbSet<ObrasDocumento> ObrasDocumentos { get; set; }
        public DbSet<Catalogo> Catalogos { get; set; }
        public DbSet<ObrasPostesCajaDetalle> ObrasPostesCajasDetalle { get; set; }
        public DbSet<ObrasPosteCajaDetalleAPP> ObrasPostesCajasDetalleAPP { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductImageEntity> ProductImages { get; set; }
        public DbSet<StateEntity> States { get; set; }
        public DbSet<TeamEntity> Teams { get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CategoryEntity>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<CountryEntity>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<DepartmentEntity>(dep =>
            {
                dep.HasIndex("Name", "CountryId").IsUnique();
                dep.HasOne(d => d.Country).WithMany(c => c.Departments).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProductEntity>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<CityEntity>(cit =>
            {
                cit.HasIndex("Name", "DepartmentId").IsUnique();
                cit.HasOne(c => c.Department).WithMany(d => d.Cities).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TeamEntity>(dep =>
            {
                dep.HasIndex("Name", "CountryId").IsUnique();
                dep.HasOne(d => d.Country).WithMany(c => c.Teams).OnDelete(DeleteBehavior.Cascade);
            });

            
            modelBuilder.Entity<SHGrupoFormulario>()
                .HasKey(c => new { c.IDCLIENTE, c.IDTIPOTRABAJO,c.IDGRUPOFORMULARIO });

            modelBuilder.Entity<SHGrupoFormPonderado>()
               .HasKey(c => new { c.IDCLIENTE, c.IDGRUPOFORMULARIO, c.DETALLEF });




        }

    }
}