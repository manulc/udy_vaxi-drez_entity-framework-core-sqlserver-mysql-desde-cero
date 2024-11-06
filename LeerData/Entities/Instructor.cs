using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeerData.Entities
{
    [Table("instructores")]
    public class Instructor
    {
        [Column("instructor_id")]
        public int InstructorId { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Grado { get; set; }

        [Column("foto_perfil")]
        public byte[] FotoPerfil { get; set; }

        // Relación uno-a-muchos bidireccional.
        public ICollection<CursoInstructor> CursoInstructores { get; set; }
    }
}