using System;
using Microsoft.EntityFrameworkCore;
using LeerData.Entities;
using System.Linq;

namespace LeerData
{
    class Program
    {
        static void Main(string[] args)
        {
            // Crea una sesión con la base de datos.
            // Todas las acciones que haya entre los paréntesis, se ejecutan en la sesión.
            using(var db = new AppVentaCursoContext())
            {
                // *** Pruebas usando SQL Server. ***

                // Obtención de cursos.
                //ObtenerCursos(db);

                // Obtención de cursos y sus precios (Relación uno-a-uno entre Curso y Precio).
                //ObtenerCursosConPrecios(db);

                // Obtención de cursos y sus comentarios (Relación uno-a-muchos entre Curso y Comentario).
                //ObtenerCursosConComentarios(db);

                // Obtención de cursos y sus instructores (Relación muchos-a-muchos entre Curso e Instructor).
                //ObtenerCursosConInstructores(db);

                // Inserción de datos.
                //InsertarUnInstructor(db);
                //InsertarDosInstructores(db);

                // Actualización de datos.
                //ActualizarUnInstructor(db);

                // Eliminación de datos.
                //EliminarUnInstructor(db, 7);

                // *** Pruebas usando MySQL. ***

                //RealizaMigracion(db);

                //InsertarDosCursos(db);
            }
        }

        private static void ObtenerCursos(AppVentaCursoContext db)
        {
            // Este método "AsNoTracking" es para que Entity Framework obtenga los datos de la base de datos sin cachearlos en memoria.
            // Devuelve un array de tipo IQueryable.
            var cursos = db.Curso.AsNoTracking();

            foreach (var curso in cursos)
                Console.WriteLine(curso.Titulo + " ---- " + curso.Descripcion);
        }

        private static void ObtenerCursosConPrecios(AppVentaCursoContext db)
        {
            // Relación uno-a-uno entre Curso y Precio.
            var cursosConPrecio = db.Curso.Include(curso => curso.Precio).AsNoTracking();

            foreach (var cursoConPrecio in cursosConPrecio)
                Console.WriteLine(cursoConPrecio.Titulo + " ---- " + cursoConPrecio.Precio.PrecioActual);
        }

        private static void ObtenerCursosConComentarios(AppVentaCursoContext db)
        {
            // Relación uno-a-muchos entre Curso y Comentario.
            var cursosConComentarios = db.Curso.Include(curso => curso.Comentarios).AsNoTracking();

            foreach (var cursoConComentarios in cursosConComentarios)
            {
                Console.WriteLine(cursoConComentarios.Titulo);
                foreach (var comentario in cursoConComentarios.Comentarios)
                    Console.WriteLine("**************" + comentario.Texto);
            }
        }

        private static void ObtenerCursosConInstructores(AppVentaCursoContext db)
        {
            // Relación muchos-a-muchos entre Curso e Instructor.
            var cursosConInstructores = db.Curso.Include(curso => curso.CursoInstructores)
                .ThenInclude(ci => ci.Instructor).AsNoTracking();

            foreach (var cursoConInstructores in cursosConInstructores)
            {
                Console.WriteLine(cursoConInstructores.Titulo);
                foreach (var cursoInstructor in cursoConInstructores.CursoInstructores)
                    Console.WriteLine("**************" + cursoInstructor.Instructor.Nombre);
            }
        }

        private static void InsertarUnInstructor(AppVentaCursoContext db)
        {
            var nuevoInstructor = new Instructor
                {
                    Nombre = "Lorenzo",
                    Apellido = "López",
                    Grado = "Máster en Computación"
                };

            // Agrega la instancia anterior al contexto de la base de datos.
            db.Add(nuevoInstructor);

            // Persiste los datos en la base de datos.
            var estadoTransaccion = db.SaveChanges();

            // En este caso, muestra el número de inserciones (1).
            Console.WriteLine("Estado de la transacción: " + estadoTransaccion);
        }

        private static void InsertarDosInstructores(AppVentaCursoContext db)
        {
            var nuevoInstructor1 = new Instructor
                {
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    Grado = "Máster en Computación"
                };

            // Agrega la instancia anterior al contexto de la base de datos.
            db.Add(nuevoInstructor1);

            var nuevoInstructor2 = new Instructor
                {
                    Nombre = "José",
                    Apellido = "Mariano",
                    Grado = "Máster en Computación"
                };

            // Agrega la instancia anterior al contexto de la base de datos.
            db.Add(nuevoInstructor2);

            // Persiste los datos en la base de datos.
            var estadoTransaccion = db.SaveChanges();

            // En este caso, muestra el número de inserciones (2).
            Console.WriteLine("Estado de la transacción: " + estadoTransaccion);
        }

        private static void InsertarDosCursos(AppVentaCursoContext db)
        {
            var cursoAlgebra = new Curso
            {
                Titulo = "Curso de Algebra",
                Descripcion = "Curso básico de Algebra"
            };

            // Agrega la instancia anterior al contexto de la base de datos.
            db.Add(cursoAlgebra);

            var cursoLenguaje = new Curso
            {
                Titulo = "Curso de Lenguaje",
                Descripcion = "Lengua Española"
            };

            // Agrega la instancia anterior al contexto de la base de datos.
            db.Add(cursoLenguaje);

            // Persiste los datos en la base de datos.
            var estadoTransaccion = db.SaveChanges();

            // En este caso, muestra el número de inserciones (2).
            Console.WriteLine("Estado de la transacción: " + estadoTransaccion);
        }

        private static void ActualizarUnInstructor(AppVentaCursoContext db)
        {
            // Obtenemos de la base de datos al instructor con nombre "Lorenzo".
            // Nota: El método "Single" pertenece a Linq.
            var instructor = db.Instructor.Single(instructor => instructor.Nombre == "Lorenzo");

            if (instructor != null)
            {
                instructor.Apellido = "Castro";
                instructor.Grado = "Biólogo";

                // Actualizamos los datos en la base de datos.
                var estadoTransaccion = db.SaveChanges();

                // En este caso, muestra el número de registros actualizados (1).
                Console.WriteLine("Estado de la transacción: " + estadoTransaccion);
            }
        }

        private static void EliminarUnInstructor(AppVentaCursoContext db, int instructorId)
        {
            // Obtenemos de la base de datos al instructor con id "instructorId".
            // Nota: El método "Single" pertenece a Linq.
            var instructor = db.Instructor.Single(instructor => instructor.InstructorId == instructorId);

            if (instructor != null)
            {
                db.Remove(instructor);

                // Realiza la eliminación en la base de datos.
                var estadoTransaccion = db.SaveChanges();

                // En este caso, muestra el número de registros eliminados (1).
                Console.WriteLine("Estado de la transacción: " + estadoTransaccion);
            }
        }

        private static void RealizaMigracion(AppVentaCursoContext db)
        {
            Console.WriteLine("Inicia la migración.");
            db.Database.Migrate();
            Console.WriteLine("Migración finalizada.");
        }

    }
}
