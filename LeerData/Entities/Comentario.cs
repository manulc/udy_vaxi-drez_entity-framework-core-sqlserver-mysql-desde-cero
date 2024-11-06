using System.ComponentModel.DataAnnotations.Schema;

namespace LeerData.Entities
{
    [Table("comentarios")]
    public class Comentario
    {
        [Column("comentario_id")]
        public int ComentarioId { get; set; }

        public string Alumno { get; set; }

        [Column("puntuacion_curso")]
        public int PuntuacionCurso { get; set; }
        
        public string Texto { get; set; }

        [Column("curso_id")]
        public int CursoId { get; set; }

        // Relación uno-a-uno bidireccional.
        // CursoId es la clave fornánea.
        public Curso Curso { get; set; }
    }
}