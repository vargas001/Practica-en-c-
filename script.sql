/****** Object:  Database     Script Date: 14/02/2022 22:55:18 ******/
CREATE DATABASE [InfoToolsSV]
/****** Object:  Table   Script Date: 14/02/2022 22:55:18 ******/
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1000,1) NOT NULL,
	[Nombres] [varchar](50) NULL,
	[Descripción] [varchar](50) NULL,
	[FechaActividad] [date] NULL,
	[Usuario] [varchar](50) NULL,
	[Contrasenia] [varbinary](500) NULL
) ON [PRIMARY]
