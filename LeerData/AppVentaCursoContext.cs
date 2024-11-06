using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using MySql.Data.EntityFrameworkCore.Extensions;
using LeerData.Entities;

namespace LeerData
{
    public class AppVentaCursoContext: DbContext
    {
        private const string _cadenaConexion = "Data Source=.; Initial Catalog=CursosOnlineDB; User Id=sa; Password=sa@12345";
        private const string _cadenaConexionMySQL = "Server=localhost; Port=3306; Database=CursosOnlineDB; User=root; Password=root";

        public DbSet<Curso> Curso { get; set; }
        public DbSet<Precio> Precio { get; set; }
        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<CursoInstructor> CursoInstructor { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_cadenaConexion);

            // Usamos una base de datos MySQL para probar las migraciones.
            optionsBuilder.UseMySQL(_cadenaConexionMySQL);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Indicamos que la entidad CursoInstructor tiene una clave primaria formada por dos propiedades.
            modelBuilder.Entity<CursoInstructor>().HasKey(ci => new { ci.CursoId, ci.InstructorId });
        }
    }
}