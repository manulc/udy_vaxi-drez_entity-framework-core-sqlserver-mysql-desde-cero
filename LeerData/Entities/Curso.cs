using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeerData.Entities
{
    [Table("cursos")]
    public class Curso
    {
        [Column("curso_id")]
        public int CursoId { get; set; }

        
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        [Column("fecha_publicacion")]
        public DateTime FechaPublicacion { get; set; }

        // Relación uno-a-uno bidireccional.
        public Precio Precio { get; set; }

        // Relación uno-a-muchos bidireccional.
        public ICollection<Comentario> Comentarios { get; set; }

        // Relación uno-a-muchos bidireccional.
        public ICollection<CursoInstructor> CursoInstructores { get; set; }
    }
}
