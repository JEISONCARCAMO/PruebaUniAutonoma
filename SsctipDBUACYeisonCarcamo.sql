USE [master]
GO
/****** Object:  Database [DBUAC]    Script Date: 09/07/2024 18:00:14 ******/
CREATE DATABASE [DBUAC]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBUAC', FILENAME = N'C:\Users\User\DBUAC.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DBUAC_log', FILENAME = N'C:\Users\User\DBUAC_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DBUAC] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBUAC].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBUAC] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBUAC] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBUAC] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBUAC] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBUAC] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBUAC] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DBUAC] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBUAC] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBUAC] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBUAC] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBUAC] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBUAC] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBUAC] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBUAC] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBUAC] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DBUAC] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBUAC] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBUAC] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBUAC] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBUAC] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBUAC] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBUAC] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBUAC] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DBUAC] SET  MULTI_USER 
GO
ALTER DATABASE [DBUAC] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBUAC] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBUAC] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBUAC] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBUAC] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DBUAC] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DBUAC] SET QUERY_STORE = OFF
GO
USE [DBUAC]
GO
/****** Object:  Table [dbo].[ESTADOS]    Script Date: 09/07/2024 18:00:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ESTADOS](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIOS]    Script Date: 09/07/2024 18:00:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIOS](
	[identificacion] [int] NOT NULL,
	[nombres] [varchar](50) NOT NULL,
	[apellidos] [varchar](50) NOT NULL,
	[fecha_nacimiento] [date] NOT NULL,
	[telefono] [varchar](10) NOT NULL,
	[usuario] [varchar](50) NOT NULL,
	[contrasena] [varchar](150) NOT NULL,
	[estado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_usuarios]    Script Date: 09/07/2024 18:00:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create function [dbo].[fn_usuarios]()
returns table
as
return 
	select u.Identificacion, u.Nombres, u.Apellidos, u.telefono, u.Usuario, u.Contrasena,
	e.Codigo, e.Nombre
	from USUARIOS u
	inner join ESTADOS e on  u.Estado = e.Codigo
GO
/****** Object:  UserDefinedFunction [dbo].[fn_usuario]    Script Date: 09/07/2024 18:00:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create function [dbo].[fn_usuario](@identificacion int)
returns table
as
return 
	select u.Identificacion, u.Nombres, u.Apellidos, u.telefono, u.Usuario, u.Contrasena,
	e.Codigo, e.Nombre,
	CONVERT(char(10), u.Fecha_nacimiento,103)[fechanaciemiento]
	from USUARIOS u
	inner join ESTADOS e on  u.Estado = e.Codigo
	where u.Identificacion = @identificacion
GO
/****** Object:  UserDefinedFunction [dbo].[fn_usuariosActivos]    Script Date: 09/07/2024 18:00:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create function [dbo].[fn_usuariosActivos]()
returns table
as
return 
	select u.Identificacion, u.Nombres, u.Apellidos, u.telefono, u.Usuario, u.Contrasena,
	e.Codigo, e.Nombre,
	CONVERT(char(10), u.Fecha_nacimiento,103)[fechanaciemiento]
	from USUARIOS u
	inner join ESTADOS e on  u.Estado = e.Codigo
	where u.Estado = 1
GO
SET IDENTITY_INSERT [dbo].[ESTADOS] ON 
GO
INSERT [dbo].[ESTADOS] ([Codigo], [Nombre]) VALUES (1, N'Activo')
GO
INSERT [dbo].[ESTADOS] ([Codigo], [Nombre]) VALUES (2, N'Inactivo')
GO
SET IDENTITY_INSERT [dbo].[ESTADOS] OFF
GO
INSERT [dbo].[USUARIOS] ([identificacion], [nombres], [apellidos], [fecha_nacimiento], [telefono], [usuario], [contrasena], [estado]) VALUES (1002496170, N'Yeison De Jesus', N'Carcamo Morales', CAST(N'1900-01-01' AS Date), N'3045663505', N'YeiCar', N'2xeu3bFKMXtAODeNUXN/2Cz2JwcBiwkufCithomvy6I=', 1)
GO
ALTER TABLE [dbo].[USUARIOS]  WITH CHECK ADD FOREIGN KEY([estado])
REFERENCES [dbo].[ESTADOS] ([Codigo])
GO
/****** Object:  StoredProcedure [dbo].[sp_CrearUsuario]    Script Date: 09/07/2024 18:00:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[sp_CrearUsuario] (
@Identificacion int,
@Nombre varchar (50),
@Apellidos varchar (50),
@Fecha_nacimiento date,
@telefono varchar(10),
@NUsuario varchar (50),
@Contrasena varchar (150)
)
as 
begin 
	set dateformat dmy
	insert into USUARIOS (Identificacion,Nombres, Apellidos, Fecha_nacimiento, telefono, Usuario, contrasena, Estado)
	values (@Identificacion,@Nombre, @Apellidos,CONVERT(date,@Fecha_nacimiento), @telefono, @NUsuario, @Contrasena, 1)
end
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteUsuario]    Script Date: 09/07/2024 18:00:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_DeleteUsuario] 
    @Identificacion INT
AS
BEGIN
    -- Establecer el formato de fecha
    SET DATEFORMAT dmy;
    
    -- Actualizar el campo Estado a 0 para eliminación lógica
    UPDATE USUARIOS
    SET Estado = 2
    WHERE Identificacion = @Identificacion;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_EditarUsuario]    Script Date: 09/07/2024 18:00:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_EditarUsuario] (
@Identificacion int,
@Nombre varchar (50),
@Apellidos varchar (50),
@Fecha_nacimiento date,
@telefono varchar(10),
@Usuario varchar (50),
@Contrasena varchar (150)
)
as 
begin 
	set dateformat dmy
	update USUARIOS set
	Nombres = @Nombre,
	Apellidos = @Apellidos,
	Fecha_nacimiento = Convert(date, @Fecha_nacimiento),
	telefono = @telefono,
	Usuario = @Usuario,
	contrasena = @Contrasena

	where Identificacion =@Identificacion
end
GO
USE [master]
GO
ALTER DATABASE [DBUAC] SET  READ_WRITE 
GO
