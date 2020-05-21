using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Mapping.Maestros;
using Sistema.Datos.Mapping.Operaciones;
using Sistema.Datos.Mapping.Usuarios;
using Sistema.Entidades.Maestros;
using Sistema.Entidades.Operaciones;
using Sistema.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos
{
    public class DbContextSistema : DbContext
    {
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Appconfig> Appconfigs { get; set; }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Grpconcepto> Grpconceptos { get; set; }
        public DbSet<Conconta> Concontas { get; set; }
        public DbSet<Conbanco> Conbancos { get; set; }
        public DbSet<Bancuenta> Bancuentas { get; set; }
        public DbSet<Concuenta> Concuentas { get; set; }
        public DbSet<Asocuenta> Asocuentas { get; set; }
        public DbSet<Asiento> Asientos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        public DbContextSistema(DbContextOptions<DbContextSistema> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new PersonaMap());
            modelBuilder.ApplyConfiguration(new EmpresaMap());
            modelBuilder.ApplyConfiguration(new PaisMap());
            modelBuilder.ApplyConfiguration(new ProvinciaMap());
            modelBuilder.ApplyConfiguration(new AppconfigMap());
            modelBuilder.ApplyConfiguration(new BancoMap());
            modelBuilder.ApplyConfiguration(new GrpconceptoMap());
            modelBuilder.ApplyConfiguration(new ConcontaMap());
            modelBuilder.ApplyConfiguration(new ConbancoMap());
            modelBuilder.ApplyConfiguration(new BancuentaMap());
            modelBuilder.ApplyConfiguration(new ConcuentaMap());
            modelBuilder.ApplyConfiguration(new AsocuentaMap());
            modelBuilder.ApplyConfiguration(new AsientoMap());
            modelBuilder.ApplyConfiguration(new LoteMap());
            modelBuilder.ApplyConfiguration(new MovimientoMap());
        }
    }
}
