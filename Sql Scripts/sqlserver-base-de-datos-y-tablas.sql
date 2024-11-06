-- Create a new database called 'CursosOnlineDB'
-- Connect to the 'master' database to run this snippet
USE master
GO

-- Create the new database if it does not exist already
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'CursosOnlineDB')
	CREATE DATABASE CursosOnlineDB
GO

USE CursosOnlineDB
GO

-- Create the table 'cursos' if it does not exist already
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='cursos')
    BEGIN
        CREATE TABLE cursos (
            curso_id INT PRIMARY KEY IDENTITY (1, 1),
            titulo VARCHAR(80) NOT NULL,
            descripcion VARCHAR(150),
            fecha_publicacion DATETIME NOT NULL,
            foto_portada VARBINARY(MAX)
        )
		
		-- Datos de prueba
        INSERT INTO cursos (titulo, descripcion, fecha_publicacion)
        VALUES ('React Hooks Firebase y Material Design', 'Curso de Programación de React', '2020-02-05'),
            ('ASP.NET Core y React Hooks', 'Curso de .NET y JavaScript', '2020-11-05')
    END
GO

-- Create the table 'precios' if it does not exist already
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='precios')
    BEGIN
        CREATE TABLE precios (
            precio_id INT PRIMARY KEY IDENTITY (1, 1),
            precio_actual MONEY NOT NULL,
            promocion MONEY,
            curso_id INT NOT NULL
        )

        ALTER TABLE precios ADD CONSTRAINT fk_precios_curso FOREIGN KEY(curso_id) REFERENCES cursos(curso_id)

        -- Datos de prueba
        INSERT INTO precios (precio_actual, promocion, curso_id)
        VALUES (900, 9.99, 1), (650, 15, 2)
    END
GO

-- Create the table 'comentarios' if it does not exist already
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='comentarios')
	BEGIN
		CREATE TABLE comentarios (
			comentario_id INT PRIMARY KEY IDENTITY (1, 1),
			alumno VARCHAR(75) NOT NULL,
			puntuacion_curso INT,
			texto VARCHAR(MAX) NOT NULL,
			curso_id INT NOT NULL
		)
			
		ALTER TABLE comentarios ADD CONSTRAINT fk_comentatios_curso FOREIGN KEY(curso_id) REFERENCES cursos(curso_id)
		
		-- Datos de prueba
        INSERT INTO comentarios (alumno, puntuacion_curso, texto, curso_id) VALUES ('Alberto Rosales', 5, 'Es el mejor curso de React', 1)
        INSERT INTO comentarios (alumno, texto, curso_id) VALUES ('Román Albeiro', 'Buen curso de programación', 1)
		INSERT INTO comentarios (alumno, puntuacion_curso, texto, curso_id) VALUES ('Ángela Árias', 4, 'Laboratorios muy prácticos', 1)
		INSERT INTO comentarios (alumno, puntuacion_curso, texto, curso_id) VALUES ('John Doe', 5, 'Buen curso de ASP.NET Core', 2)
		INSERT INTO comentarios (alumno, texto, curso_id) VALUES ('Felipe Benegas', 'Sql Server al máximo', 2)
	END
GO

-- Create the table 'instructores' if it does not exist already
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='instructores')
	BEGIN
		CREATE TABLE instructores (
			instructor_id INT PRIMARY KEY IDENTITY (1, 1),
			nombre VARCHAR(60) NOT NULL,
			apellido VARCHAR(80),
			grado VARCHAR(100),
			foto_perfil VARBINARY(MAX)
		)
		
		-- Datos de prueba
        INSERT INTO instructores (nombre, apellido, grado) VALUES ('John', 'Doe', 'Master')
        INSERT INTO instructores (nombre, apellido, grado) VALUES ('Nestor', 'García', 'Ingeniero')
		INSERT INTO instructores (nombre, apellido, grado) VALUES ('John', 'Ortíz', 'Técnico')
		INSERT INTO instructores (nombre, apellido, grado) VALUES ('Ángela', 'Árias', 'Ingeniero')
	END
GO

-- Create the table 'cursos_instructores' if it does not exist already
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='cursos_instructores')
	BEGIN
		CREATE TABLE cursos_instructores (
			curso_id INT NOT NULL,
			instructor_id INT NOT NULL,
			PRIMARY KEY (curso_id, instructor_id),
			FOREIGN KEY (curso_id) REFERENCES cursos(curso_id),
			FOREIGN KEY (instructor_id) REFERENCES instructores(instructor_id)
		)
		
		-- Datos de prueba
        INSERT INTO cursos_instructores VALUES (1, 1), (1, 2), (1, 3), (2, 4), (2, 1)
	END
