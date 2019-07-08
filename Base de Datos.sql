USE tempdb
go
--Base de Datos Danny
--Programacion de Negocios
--Examen Segundo Parcial

CREATE DATABASE ERP
GO

USE ERP
GO

CREATE SCHEMA Usuarios
go

CREATE TABLE Usuarios.usuario
(
	id INT NOT NULL IDENTITY,
	nombre VARCHAR(50) NOT NULL,
	apellido VARCHAR(50) NOT NULL,
	nombreUsuario VARCHAR(15) NOT NULL,
	contrasenia VARCHAR(11) NOT NULL,
	correo VARCHAR(30) NOT NULL,
	fechaCreacion Datetime Default GetDate(),
	ultimaConexion Datetime Default GetDate(),
	tipoUsuario VARCHAR(20)NOT NULL,
	estado bit,
)
GO

SELECT * FROM Usuarios.usuario
GO