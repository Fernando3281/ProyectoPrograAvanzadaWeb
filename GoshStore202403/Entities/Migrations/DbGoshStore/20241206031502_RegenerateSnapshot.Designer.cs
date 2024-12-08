﻿// <auto-generated />
using System;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Entities.Migrations.DbGoshStore
{
    [DbContext(typeof(DbGoshStoreContext))]
    [Migration("20241206031502_RegenerateSnapshot")]
    partial class RegenerateSnapshot
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Entities.CarritoProducto", b =>
                {
                    b.Property<int>("IdCarrito")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_carrito");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCarrito"));

                    b.Property<int>("IdProducto")
                        .HasColumnType("int")
                        .HasColumnName("id_producto");

                    b.Property<string>("IdUsuario")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UsuarioId");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int")
                        .HasColumnName("cantidad");

                    b.HasKey("IdCarrito", "IdProducto", "IdUsuario")
                        .HasName("PK__Carrito___E01FD2585775A3D6");

                    b.HasIndex("IdProducto");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Carrito_Productos", (string)null);
                });

            modelBuilder.Entity("Entities.Entities.Categoria", b =>
                {
                    b.Property<int>("IdCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_categoria");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCategoria"));

                    b.Property<string>("NombreCategoria")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("nombre_categoria");

                    b.HasKey("IdCategoria")
                        .HasName("PK__Categori__CD54BC5A2C243626");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Entities.Entities.DetallePedido", b =>
                {
                    b.Property<int>("IdDetallePedido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_detalle_pedido");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDetallePedido"));

                    b.Property<int?>("Cantidad")
                        .HasColumnType("int")
                        .HasColumnName("cantidad");

                    b.Property<int?>("IdPedido")
                        .HasColumnType("int")
                        .HasColumnName("id_pedido");

                    b.Property<int?>("IdProducto")
                        .HasColumnType("int")
                        .HasColumnName("id_producto");

                    b.Property<decimal?>("PrecioUnitario")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("precio_unitario");

                    b.HasKey("IdDetallePedido")
                        .HasName("PK__Detalle___C08768CF308756E1");

                    b.HasIndex("IdPedido");

                    b.HasIndex("IdProducto");

                    b.ToTable("Detalle_Pedido", (string)null);
                });

            modelBuilder.Entity("Entities.Entities.Direccione", b =>
                {
                    b.Property<int>("IdDireccion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_direccion");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDireccion"));

                    b.Property<string>("Ciudad")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("ciudad");

                    b.Property<string>("CodigoPostal")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("codigo_postal");

                    b.Property<string>("DireccionEnvio")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("direccion_envio");

                    b.Property<int?>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("id_usuario");

                    b.HasKey("IdDireccion")
                        .HasName("PK__Direccio__25C35D07D4B05CDC");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Direcciones");
                });

            modelBuilder.Entity("Entities.Entities.Pedido", b =>
                {
                    b.Property<int>("IdPedido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_pedido");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPedido"));

                    b.Property<string>("Estado")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("estado");

                    b.Property<DateTime?>("FechaPedido")
                        .HasColumnType("datetime")
                        .HasColumnName("fecha_pedido");

                    b.Property<int?>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("id_usuario");

                    b.HasKey("IdPedido")
                        .HasName("PK__Pedidos__6FF014893E98EDD7");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("Entities.Entities.Producto", b =>
                {
                    b.Property<int>("IdProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_producto");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProducto"));

                    b.Property<int?>("CategoriaId")
                        .HasColumnType("int")
                        .HasColumnName("categoria_id");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("descripcion");

                    b.Property<string>("Imagen")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("imagen");

                    b.Property<string>("NombreProducto")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("nombre_producto");

                    b.Property<decimal?>("Precio")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("precio");

                    b.Property<int?>("Stock")
                        .HasColumnType("int")
                        .HasColumnName("stock");

                    b.Property<string>("Talla")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.HasKey("IdProducto")
                        .HasName("PK__Producto__FF341C0DAFF5321B");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("Entities.Entities.Transaccione", b =>
                {
                    b.Property<int>("IdTransaccion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_transaccion");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTransaccion"));

                    b.Property<string>("Estado")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("estado");

                    b.Property<DateTime?>("FechaTransaccion")
                        .HasColumnType("datetime")
                        .HasColumnName("fecha_transaccion");

                    b.Property<int?>("IdPedido")
                        .HasColumnType("int")
                        .HasColumnName("id_pedido");

                    b.Property<string>("MetodoPago")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("metodo_pago");

                    b.Property<decimal?>("MontoTotal")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("monto_total");

                    b.HasKey("IdTransaccion")
                        .HasName("PK__Transacc__1EDAC099BD0902B3");

                    b.HasIndex("IdPedido");

                    b.ToTable("Transacciones");
                });

            modelBuilder.Entity("Entities.Entities.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_usuario");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"));

                    b.Property<string>("Contraseña")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("contraseña");

                    b.Property<string>("Correo")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("correo");

                    b.Property<string>("Nombre")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("nombre");

                    b.Property<string>("Rol")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("rol");

                    b.Property<string>("Telefono")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("telefono");

                    b.HasKey("IdUsuario")
                        .HasName("PK__Usuarios__4E3E04ADA89DE695");

                    b.HasIndex(new[] { "Correo" }, "UQ__Usuarios__2A586E0B1B2509DA")
                        .IsUnique()
                        .HasFilter("[correo] IS NOT NULL");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("IdentityUser");
                });

            modelBuilder.Entity("Entities.Entities.CarritoProducto", b =>
                {
                    b.HasOne("Entities.Entities.Producto", "IdProductoNavigation")
                        .WithMany("CarritoProductos")
                        .HasForeignKey("IdProducto")
                        .IsRequired()
                        .HasConstraintName("FK__Carrito_P__id_pr__3F466844");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "UsuarioNavigation")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .IsRequired()
                        .HasConstraintName("FK_Carrito_Usuarios");

                    b.Navigation("IdProductoNavigation");

                    b.Navigation("UsuarioNavigation");
                });

            modelBuilder.Entity("Entities.Entities.DetallePedido", b =>
                {
                    b.HasOne("Entities.Entities.Pedido", "IdPedidoNavigation")
                        .WithMany("DetallePedidos")
                        .HasForeignKey("IdPedido")
                        .HasConstraintName("FK__Detalle_P__id_pe__45F365D3");

                    b.HasOne("Entities.Entities.Producto", "IdProductoNavigation")
                        .WithMany("DetallePedidos")
                        .HasForeignKey("IdProducto")
                        .HasConstraintName("FK__Detalle_P__id_pr__46E78A0C");

                    b.Navigation("IdPedidoNavigation");

                    b.Navigation("IdProductoNavigation");
                });

            modelBuilder.Entity("Entities.Entities.Direccione", b =>
                {
                    b.HasOne("Entities.Entities.Usuario", "IdUsuarioNavigation")
                        .WithMany("Direcciones")
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("FK__Direccion__id_us__4CA06362");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("Entities.Entities.Pedido", b =>
                {
                    b.HasOne("Entities.Entities.Usuario", "IdUsuarioNavigation")
                        .WithMany("Pedidos")
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("FK__Pedidos__id_usua__4316F928");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("Entities.Entities.Producto", b =>
                {
                    b.HasOne("Entities.Entities.Categoria", "Categoria")
                        .WithMany("Productos")
                        .HasForeignKey("CategoriaId")
                        .HasConstraintName("FK__Productos__categ__3C69FB99");

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("Entities.Entities.Transaccione", b =>
                {
                    b.HasOne("Entities.Entities.Pedido", "IdPedidoNavigation")
                        .WithMany("Transacciones")
                        .HasForeignKey("IdPedido")
                        .HasConstraintName("FK__Transacci__id_pe__49C3F6B7");

                    b.Navigation("IdPedidoNavigation");
                });

            modelBuilder.Entity("Entities.Entities.Categoria", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("Entities.Entities.Pedido", b =>
                {
                    b.Navigation("DetallePedidos");

                    b.Navigation("Transacciones");
                });

            modelBuilder.Entity("Entities.Entities.Producto", b =>
                {
                    b.Navigation("CarritoProductos");

                    b.Navigation("DetallePedidos");
                });

            modelBuilder.Entity("Entities.Entities.Usuario", b =>
                {
                    b.Navigation("Direcciones");

                    b.Navigation("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
