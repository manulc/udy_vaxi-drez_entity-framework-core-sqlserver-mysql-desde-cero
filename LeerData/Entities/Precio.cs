using System.ComponentModel.DataAnnotations.Schema;

namespace LeerData.Entities
{
    [Table("precios")]
    public class Precio
    {
        [Column("precio_id")]
        public int PrecioId { get; set; }

        [Column("precio_actual")]
        public decimal PrecioActual { get; set; }

        public decimal Promocion { get; set; }

        [Column("curso_id")]
        public int CursoId { get; set; }

        // Relación uno-a-uno bidireccional.
        // CursoId es la clave fornánea.
        public Curso Curso { get; set; }
    }
}