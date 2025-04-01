using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BakeryApp_v1.Models;

public partial class BakeryAppContext : DbContext
{
    public BakeryAppContext()
    {
    }

    public BakeryAppContext(DbContextOptions<BakeryAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Boletin> Boletins { get; set; }

    public virtual DbSet<Boletinnoticia> Boletinnoticias { get; set; }

    public virtual DbSet<Cantone> Cantones { get; set; }

    public virtual DbSet<Carritocompra> Carritocompras { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Detallefactura> Detallefacturas { get; set; }

    public virtual DbSet<Direccionesusuario> Direccionesusuarios { get; set; }

    public virtual DbSet<Distrito> Distritos { get; set; }

    public virtual DbSet<Estadospedido> Estadospedidos { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Ingrediente> Ingredientes { get; set; }

    public virtual DbSet<Marketing> Marketings { get; set; }

    public virtual DbSet<Mensajesboletin> Mensajesboletins { get; set; }

    public virtual DbSet<Notascredito> Notascreditos { get; set; }

    public virtual DbSet<Pagossinpe> Pagossinpes { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Pedidoproducto> Pedidoproductos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Provincia> Provincias { get; set; }

    public virtual DbSet<Receta> Recetas { get; set; }

    public virtual DbSet<Recuperarcontra> Recuperarcontras { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tiposenvio> Tiposenvios { get; set; }

    public virtual DbSet<Tipospago> Tipospagos { get; set; }

    public virtual DbSet<Unidadesmedidum> Unidadesmedida { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;database=BakeryApp;user=Bakery;password=BakeryApp.*");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Boletin>(entity =>
        {
            entity.HasKey(e => e.IdBoletin).HasName("PRIMARY");

            entity.ToTable("boletin");

            entity.HasIndex(e => e.IdUsuario, "fk_id_usuario_boletin");

            entity.HasIndex(e => e.IdBoletinNoticias, "pk_id_boletin_noticias");

            entity.HasOne(d => d.IdBoletinNoticiasNavigation).WithMany(p => p.Boletins)
                .HasForeignKey(d => d.IdBoletinNoticias)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pk_id_boletin_noticias");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Boletins)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_usuario_boletin");
        });

        modelBuilder.Entity<Boletinnoticia>(entity =>
        {
            entity.HasKey(e => e.IdBoletinNoticias).HasName("PRIMARY");

            entity.ToTable("boletinnoticias");

            entity.Property(e => e.NombreBoletin).HasMaxLength(20);
        });

        modelBuilder.Entity<Cantone>(entity =>
        {
            entity.HasKey(e => e.IdCanton).HasName("PRIMARY");

            entity.ToTable("cantones");

            entity.HasIndex(e => e.IdProvincia, "fk_canton_provincia");

            entity.Property(e => e.NombreCanton).HasMaxLength(25);

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Cantones)
                .HasForeignKey(d => d.IdProvincia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_canton_provincia");
        });

        modelBuilder.Entity<Carritocompra>(entity =>
        {
            entity.HasKey(e => e.IdCarrito).HasName("PRIMARY");

            entity.ToTable("carritocompras");

            entity.HasIndex(e => e.IdPersona, "fk_id_persona_carrito");

            entity.HasIndex(e => e.IdProducto, "fk_id_producto");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Carritocompras)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("fk_id_persona_carrito");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Carritocompras)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("fk_id_producto");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PRIMARY");

            entity.ToTable("categorias");

            entity.HasIndex(e => e.NombreCategoria, "uq_nombre_categoria").IsUnique();

            entity.Property(e => e.DescripcionCategoria).HasMaxLength(255);
            entity.Property(e => e.ImagenCategoria).HasMaxLength(80);
            entity.Property(e => e.NombreCategoria).HasMaxLength(40);
        });

        modelBuilder.Entity<Detallefactura>(entity =>
        {
            entity.HasKey(e => new { e.Linea, e.IdFactura }).HasName("PRIMARY");

            entity.ToTable("detallefactura");

            entity.HasIndex(e => e.IdFactura, "fk_id_factura");

            entity.HasIndex(e => e.IdPedidoProducto, "fk_producto_pedido_factura");

            entity.Property(e => e.TotalLinea).HasPrecision(10);

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.Detallefacturas)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("fk_id_factura");

            entity.HasOne(d => d.IdPedidoProductoNavigation).WithMany(p => p.Detallefacturas)
                .HasForeignKey(d => d.IdPedidoProducto)
                .HasConstraintName("fk_producto_pedido_factura");
        });

        modelBuilder.Entity<Direccionesusuario>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PRIMARY");

            entity.ToTable("direccionesusuario");

            entity.HasIndex(e => e.IdCanton, "fk_id_canton_direccion");

            entity.HasIndex(e => e.IdDistrito, "fk_id_distrito_direccion");

            entity.HasIndex(e => e.IdProvincia, "fk_id_provincia_direccion");

            entity.HasIndex(e => e.IdPersona, "fk_id_usuario_direccion");

            entity.Property(e => e.DireccionExacta).HasMaxLength(1000);
            entity.Property(e => e.NombreDireccion).HasMaxLength(50);

            entity.HasOne(d => d.IdCantonNavigation).WithMany(p => p.Direccionesusuarios)
                .HasForeignKey(d => d.IdCanton)
                .HasConstraintName("fk_id_canton_direccion");

            entity.HasOne(d => d.IdDistritoNavigation).WithMany(p => p.Direccionesusuarios)
                .HasForeignKey(d => d.IdDistrito)
                .HasConstraintName("fk_id_distrito_direccion");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Direccionesusuarios)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("fk_id_usuario_direccion");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Direccionesusuarios)
                .HasForeignKey(d => d.IdProvincia)
                .HasConstraintName("fk_id_provincia_direccion");
        });

        modelBuilder.Entity<Distrito>(entity =>
        {
            entity.HasKey(e => e.IdDistrito).HasName("PRIMARY");

            entity.ToTable("distritos");

            entity.HasIndex(e => e.IdCanton, "fk_distrito_canton");

            entity.Property(e => e.NombreDistrito).HasMaxLength(50);

            entity.HasOne(d => d.IdCantonNavigation).WithMany(p => p.Distritos)
                .HasForeignKey(d => d.IdCanton)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_distrito_canton");
        });

        modelBuilder.Entity<Estadospedido>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPedido).HasName("PRIMARY");

            entity.ToTable("estadospedido");

            entity.Property(e => e.NombreEstado).HasMaxLength(40);
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PRIMARY");

            entity.ToTable("facturas");

            entity.HasIndex(e => e.IdPedido, "fk_id_pedido_factura");

            entity.Property(e => e.Envio).HasPrecision(10);
            entity.Property(e => e.FechaFactura).HasColumnType("datetime");
            entity.Property(e => e.Iva)
                .HasPrecision(10)
                .HasColumnName("IVA");
            entity.Property(e => e.TotalPagar).HasPrecision(10);

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_pedido_factura");
        });

        modelBuilder.Entity<Ingrediente>(entity =>
        {
            entity.HasKey(e => e.IdIngrediente).HasName("PRIMARY");

            entity.ToTable("ingredientes");

            entity.HasIndex(e => e.UnidadMedidaIngrediente, "fk_unidad_ingrediente");

            entity.HasIndex(e => e.NombreIngrediente, "uk_nombre_ingrediente").IsUnique();

            entity.Property(e => e.DescripcionIngrediente).HasMaxLength(100);
            entity.Property(e => e.FechaCaducidadIngrediente).HasColumnType("date");
            entity.Property(e => e.NombreIngrediente).HasMaxLength(50);
            entity.Property(e => e.PrecioUnidadIngrediente).HasPrecision(10);

            entity.HasOne(d => d.UnidadMedidaIngredienteNavigation).WithMany(p => p.Ingredientes)
                .HasForeignKey(d => d.UnidadMedidaIngrediente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_unidad_ingrediente");

            entity.HasMany(d => d.IdReceta).WithMany(p => p.IdIngredientes)
                .UsingEntity<Dictionary<string, object>>(
                    "Ingredientesreceta",
                    r => r.HasOne<Receta>().WithMany()
                        .HasForeignKey("IdReceta")
                        .HasConstraintName("fk_id_receta_rec"),
                    l => l.HasOne<Ingrediente>().WithMany()
                        .HasForeignKey("IdIngrediente")
                        .HasConstraintName("fk_id_ingrediente_rec"),
                    j =>
                    {
                        j.HasKey("IdIngrediente", "IdReceta").HasName("PRIMARY");
                        j.ToTable("ingredientesrecetas");
                        j.HasIndex(new[] { "IdReceta" }, "fk_id_receta_rec");
                    });
        });

        modelBuilder.Entity<Marketing>(entity =>
        {
            entity.HasKey(e => e.IdMarketing).HasName("PRIMARY");

            entity.ToTable("marketing");

            entity.Property(e => e.Correo).HasMaxLength(80);
            entity.Property(e => e.Nombre).HasMaxLength(25);
        });

        modelBuilder.Entity<Mensajesboletin>(entity =>
        {
            entity.HasKey(e => e.IdMensajeBoletin).HasName("PRIMARY");

            entity.ToTable("mensajesboletin");

            entity.HasIndex(e => e.IdBoletin, "fk_id_boletin_mensaje");

            entity.Property(e => e.Asunto).HasMaxLength(80);
            entity.Property(e => e.Mensaje).HasMaxLength(2500);

            entity.HasOne(d => d.IdBoletinNavigation).WithMany(p => p.Mensajesboletins)
                .HasForeignKey(d => d.IdBoletin)
                .HasConstraintName("fk_id_boletin_mensaje");
        });

        modelBuilder.Entity<Notascredito>(entity =>
        {
            entity.HasKey(e => e.IdNotaCredito).HasName("PRIMARY");

            entity.ToTable("notascredito");

            entity.HasIndex(e => e.IdFactura, "fk_id_factura_nota");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.Notascreditos)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_factura_nota");
        });

        modelBuilder.Entity<Pagossinpe>(entity =>
        {
            entity.HasKey(e => e.IdPagoSinpe).HasName("PRIMARY");

            entity.ToTable("pagossinpe");

            entity.HasIndex(e => e.IdPedido, "fk_id_pedido_sinpe");

            entity.Property(e => e.RutaImagenSinpe)
                .HasMaxLength(80)
                .HasColumnName("rutaImagenSinpe");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Pagossinpes)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("fk_id_pedido_sinpe");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PRIMARY");

            entity.ToTable("pedidos");

            entity.HasIndex(e => e.IdPersona, "fk_id_cliente_pedido");

            entity.HasIndex(e => e.IdDireccion, "fk_id_direccion_pedido");

            entity.HasIndex(e => e.IdEstadoPedido, "fk_id_estado_pedido");

            entity.HasIndex(e => e.IdTipoEnvio, "fk_id_tipo_envio");

            entity.HasIndex(e => e.IdTipoPago, "fk_id_tipo_pago_pedido");

            entity.Property(e => e.FechaPedido).HasColumnType("datetime");

            entity.HasOne(d => d.IdDireccionNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdDireccion)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_id_direccion_pedido");

            entity.HasOne(d => d.IdEstadoPedidoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdEstadoPedido)
                .HasConstraintName("fk_id_estado_pedido");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("fk_id_cliente_pedido");

            entity.HasOne(d => d.IdTipoEnvioNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdTipoEnvio)
                .HasConstraintName("fk_id_tipo_envio");

            entity.HasOne(d => d.IdTipoPagoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdTipoPago)
                .HasConstraintName("fk_id_tipo_pago_pedido");
        });

        modelBuilder.Entity<Pedidoproducto>(entity =>
        {
            entity.HasKey(e => e.IdPedidoProducto).HasName("PRIMARY");

            entity.ToTable("pedidoproducto");

            entity.HasIndex(e => e.IdPedido, "fk_pedido_producto");

            entity.HasIndex(e => e.IdProducto, "fk_producto_pedido");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Pedidoproductos)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("fk_pedido_producto");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Pedidoproductos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("fk_producto_pedido");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PRIMARY");

            entity.ToTable("personas");

            entity.HasIndex(e => e.IdRol, "per_fk_id_rol");

            entity.HasIndex(e => e.Correo, "uk_correo_per").IsUnique();

            entity.HasIndex(e => e.Telefono, "uk_telefono_per").IsUnique();

            entity.Property(e => e.Contra).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(80);
            entity.Property(e => e.Nombre).HasMaxLength(25);
            entity.Property(e => e.PrimerApellido).HasMaxLength(25);
            entity.Property(e => e.SegundoApellido).HasMaxLength(25);
            entity.Property(e => e.Telefono).HasMaxLength(13);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("per_fk_id_rol");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PRIMARY");

            entity.ToTable("productos");

            entity.HasIndex(e => e.IdCategoria, "fk_id_Categoria");

            entity.HasIndex(e => e.IdReceta, "fk_id_receta");

            entity.HasIndex(e => e.NombreProducto, "uq_nombre_Producto").IsUnique();

            entity.Property(e => e.DescripcionProducto).HasMaxLength(255);
            entity.Property(e => e.ImagenProducto).HasMaxLength(80);
            entity.Property(e => e.NombreProducto).HasMaxLength(40);
            entity.Property(e => e.PrecioProducto).HasPrecision(10);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("fk_id_Categoria");

            entity.HasOne(d => d.IdRecetaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdReceta)
                .HasConstraintName("fk_id_receta");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.IdProvincia).HasName("PRIMARY");

            entity.ToTable("provincias");

            entity.Property(e => e.NombreProvincia).HasMaxLength(20);
        });

        modelBuilder.Entity<Receta>(entity =>
        {
            entity.HasKey(e => e.IdReceta).HasName("PRIMARY");

            entity.ToTable("recetas");

            entity.HasIndex(e => e.NombreReceta, "uk_nombre_receta").IsUnique();

            entity.Property(e => e.NombreReceta).HasMaxLength(50);
        });

        modelBuilder.Entity<Recuperarcontra>(entity =>
        {
            entity.HasKey(e => e.IdRecuperacion).HasName("PRIMARY");

            entity.ToTable("recuperarcontra");

            entity.HasIndex(e => e.IdPersona, "fk_id_usuario_contra");

            entity.Property(e => e.CodigoRecuperacion).HasMaxLength(80);
            entity.Property(e => e.FechaExpiracion).HasColumnType("datetime");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Recuperarcontras)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("fk_id_usuario_contra");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.NombreRol).HasMaxLength(20);
        });

        modelBuilder.Entity<Tiposenvio>(entity =>
        {
            entity.HasKey(e => e.IdTipoEnvio).HasName("PRIMARY");

            entity.ToTable("tiposenvio");

            entity.Property(e => e.NombreTipo).HasMaxLength(40);
        });

        modelBuilder.Entity<Tipospago>(entity =>
        {
            entity.HasKey(e => e.IdTipoPago).HasName("PRIMARY");

            entity.ToTable("tipospago");

            entity.Property(e => e.NombreTipo).HasMaxLength(30);
        });

        modelBuilder.Entity<Unidadesmedidum>(entity =>
        {
            entity.HasKey(e => e.IdUnidad).HasName("PRIMARY");

            entity.ToTable("unidadesmedida");

            entity.Property(e => e.NombreUnidad).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
