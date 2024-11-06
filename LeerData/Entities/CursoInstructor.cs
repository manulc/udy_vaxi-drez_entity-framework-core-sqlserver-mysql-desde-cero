using System.ComponentModel.DataAnnotations.Schema;

namespace LeerData.Entities
{
    // Representa la tabla intermedia de la relación muchos-a-muchos entre Curso e Instructor.

    [Table("cursos_instructores")]
    public class CursoInstructor
    {
        [Column("curso_id")]
        public int CursoId { get; set; }

        [Column("instructor_id")]
        public int InstructorId { get; set; }

        // Relación uno-a-muchos bidireccional.
        // CursoId es la clave fornánea.
        public Curso Curso { get; set; }

        // Relación uno-a-muchos bidireccional.
        // InstuctorId es la clave fornánea.
        public Instructor Instructor { get; set; }
    }
}