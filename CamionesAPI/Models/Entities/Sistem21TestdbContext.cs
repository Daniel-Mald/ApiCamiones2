using Microsoft.EntityFrameworkCore;

namespace CamionesAPI.Models.Entities;

public partial class Sistem21TestdbContext : DbContext
{
    public Sistem21TestdbContext()
    {
    }

    public Sistem21TestdbContext(DbContextOptions<Sistem21TestdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bitacora> Bitacora { get; set; }

    public virtual DbSet<Chofer> Chofer { get; set; }

    public virtual DbSet<Disponibilidad> Disponibilidad { get; set; }

    public virtual DbSet<Estatusfactura> Estatusfactura { get; set; }

    public virtual DbSet<Factura> Factura { get; set; }

    public virtual DbSet<Gasto> Gasto { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Tipodeviaje> Tipodeviaje { get; set; }

    public virtual DbSet<Unidad> Unidad { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<Viaje> Viaje { get; set; }

    public virtual DbSet<Viajegasto> Viajegasto { get; set; }

    public virtual DbSet<Vwvistageneral> Vwvistageneral { get; set; }

    private static readonly int[] value = [0, 0];

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Bitacora>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.UsuarioId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", value);

            entity.ToTable("bitacora");

            entity.HasIndex(e => e.UsuarioId, "fk_Bitacora_Usuario_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("int(11)");
            entity.Property(e => e.UsuarioId)
                .HasColumnType("int(11)")
                .HasColumnName("Usuario_Id");
            entity.Property(e => e.IdUsuario).HasColumnType("int(11)");
            entity.Property(e => e.Movimiento).HasMaxLength(45);
            entity.Property(e => e.Tabla).HasMaxLength(45);

            entity.HasOne(d => d.Usuario).WithMany(p => p.Bitacora)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bitacora_Usuario");
        });

        modelBuilder.Entity<Chofer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("chofer");

            entity.HasIndex(e => e.IdentificadorChofer, "IdenitificadorDeChofer_UNIQUE").IsUnique();

            entity.HasIndex(e => e.IdDisponibilidad, "fk_usuario_disponibilidad_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdDisponibilidad).HasColumnType("int(11)");
            entity.Property(e => e.IdentificadorChofer).HasMaxLength(45);
            entity.Property(e => e.Nombre).HasMaxLength(45);
            entity.Property(e => e.Sueldo).HasPrecision(10);
            entity.Property(e => e.Telefono).HasMaxLength(10);

            entity.HasOne(d => d.IdDisponibilidadNavigation).WithMany(p => p.Chofer)
                .HasForeignKey(d => d.IdDisponibilidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_chofer_disponibilidad");
        });

        modelBuilder.Entity<Disponibilidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("disponibilidad");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Estado).HasMaxLength(8);
        });

        modelBuilder.Entity<Estatusfactura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estatusfactura");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Estatus).HasMaxLength(50);
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("factura");

            entity.HasIndex(e => e.IdEstatus, "fk_factura_EstatusFactura");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdEstatus).HasColumnType("int(11)");
            entity.Property(e => e.Monto).HasPrecision(10);
            entity.Property(e => e.NumeroFactura).HasMaxLength(9);

            entity.HasOne(d => d.IdEstatusNavigation).WithMany(p => p.Factura)
                .HasForeignKey(d => d.IdEstatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_factura_EstatusFactura");
        });

        modelBuilder.Entity<Gasto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("gasto");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Monto).HasPrecision(10);
            entity.Property(e => e.Nombre).HasMaxLength(45);
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rol");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(13);
        });

        modelBuilder.Entity<Tipodeviaje>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tipodeviaje");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Tipo).HasMaxLength(8);
        });

        modelBuilder.Entity<Unidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("unidad");

            entity.HasIndex(e => e.IdDisponibilidad, "fk_Usuario_disponibilidad_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Capacidad).HasMaxLength(10);
            entity.Property(e => e.IdDisponibilidad).HasColumnType("int(11)");
            entity.Property(e => e.Modelo).HasMaxLength(45);
            entity.Property(e => e.Placa).HasMaxLength(8);

            entity.HasOne(d => d.IdDisponibilidadNavigation).WithMany(p => p.Unidad)
                .HasForeignKey(d => d.IdDisponibilidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Unidad_Disponibilidad");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.IdDisponibilidad, "fk_Usuario_Disponibilidad_idx");

            entity.HasIndex(e => e.IdRol, "fk_Usuario_Rol_idx");

            entity.HasIndex(e => e.IdViajes, "fk_Usuario_Viajes_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Contraseña).HasMaxLength(128);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.IdDisponibilidad).HasColumnType("int(11)");
            entity.Property(e => e.IdRol).HasColumnType("int(11)");
            entity.Property(e => e.IdViajes).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(45);

            entity.HasOne(d => d.IdDisponibilidadNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdDisponibilidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Usuario_Disponibilidad");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Usuario_Rol");

            entity.HasOne(d => d.IdViajesNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdViajes)
                .HasConstraintName("fk_Usuario_Viajes");
        });

        modelBuilder.Entity<Viaje>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("viaje");

            entity.HasIndex(e => e.ChoferId, "fk_Viaje_Chofer1");

            entity.HasIndex(e => e.IdDisponibilidad, "fk_Viaje_Disponibilidad_idx");

            entity.HasIndex(e => e.IdFactura, "fk_Viaje_Factura_idx");

            entity.HasIndex(e => e.IdTipoViaje, "fk_Viaje_TipoDeViaje");

            entity.HasIndex(e => e.UnidadId, "fk_Viaje_Unidad1");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.ChoferId)
                .HasColumnType("int(11)")
                .HasColumnName("Chofer_Id");
            entity.Property(e => e.Destino).HasMaxLength(50);
            entity.Property(e => e.FechaApagar).HasColumnName("FechaAPagar");
            entity.Property(e => e.GananciaMonetaria).HasPrecision(10, 2);
            entity.Property(e => e.IdDisponibilidad).HasColumnType("int(11)");
            entity.Property(e => e.IdFactura).HasColumnType("int(11)");
            entity.Property(e => e.IdTipoViaje).HasColumnType("int(11)");
            entity.Property(e => e.NombreCliente).HasMaxLength(50);
            entity.Property(e => e.NumeroViaje).HasMaxLength(30);
            entity.Property(e => e.Observaciones).HasColumnType("text");
            entity.Property(e => e.Origen).HasMaxLength(50);
            entity.Property(e => e.Semana).HasColumnType("tinyint(3)");
            entity.Property(e => e.UnidadId)
                .HasColumnType("int(11)")
                .HasColumnName("Unidad_Id");

            entity.HasOne(d => d.Chofer).WithMany(p => p.Viaje)
                .HasForeignKey(d => d.ChoferId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Viaje_Chofer1");

            entity.HasOne(d => d.IdDisponibilidadNavigation).WithMany(p => p.Viaje)
                .HasForeignKey(d => d.IdDisponibilidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Viaje_Disponibilidad");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.Viaje)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Viaje_Factura");

            entity.HasOne(d => d.IdTipoViajeNavigation).WithMany(p => p.Viaje)
                .HasForeignKey(d => d.IdTipoViaje)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Viaje_TipoDeViaje");

            entity.HasOne(d => d.Unidad).WithMany(p => p.Viaje)
                .HasForeignKey(d => d.UnidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Viaje_Unidad1");
        });

        modelBuilder.Entity<Viajegasto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("viajegasto");

            entity.HasIndex(e => e.IdGasto, "fk_ViajeGasto_Gasto_idx");

            entity.HasIndex(e => e.IdViaje, "fk_ViajeGasto_Viaje_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.IdGasto).HasColumnType("int(11)");
            entity.Property(e => e.IdViaje).HasColumnType("int(11)");

            entity.HasOne(d => d.IdGastoNavigation).WithMany(p => p.Viajegasto)
                .HasForeignKey(d => d.IdGasto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ViajeGasto_Gasto");

            entity.HasOne(d => d.IdViajeNavigation).WithMany(p => p.Viajegasto)
                .HasForeignKey(d => d.IdViaje)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ViajeGasto_Viaje");
        });

        modelBuilder.Entity<Vwvistageneral>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwvistageneral");

            entity.Property(e => e.Cliente).HasMaxLength(50);
            entity.Property(e => e.Destino).HasMaxLength(50);
            entity.Property(e => e.Estatus).HasMaxLength(50);
            entity.Property(e => e.FechaApagar).HasColumnName("FechaAPagar");
            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Monto).HasPrecision(10, 2);
            entity.Property(e => e.NumeroFactura).HasMaxLength(9);
            entity.Property(e => e.NumeroViaje).HasMaxLength(30);
            entity.Property(e => e.Observaciones).HasColumnType("text");
            entity.Property(e => e.Origen).HasMaxLength(50);
            entity.Property(e => e.Semana).HasColumnType("tinyint(3)");
            entity.Property(e => e.Tipo).HasMaxLength(8);
            entity.Property(e => e.Unidad).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
