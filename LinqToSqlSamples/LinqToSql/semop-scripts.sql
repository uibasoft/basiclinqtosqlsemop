Create database [AlcaldiaInfra]
GO
USE [AlcaldiaInfra]
GO
/****** Object:  Table [dbo].[Personas]    Script Date: 2016-05-18 3:31:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_dbo.Personas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Responsables]    Script Date: 2016-05-18 3:31:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Responsables](
	[Id] [int] NOT NULL,
	[FechaAsignacion] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Responsables] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SubAlcaldias]    Script Date: 2016-05-18 3:31:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubAlcaldias](
	[IdSubAlcaldia] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Direccion] [nvarchar](max) NOT NULL,
	[Zona] [nvarchar](50) NOT NULL,
	[Telefono] [nvarchar](30) NULL,
	[NombreSubAlcalde] [nvarchar](50) NULL,
 CONSTRAINT [PK_dbo.SubAlcaldias] PRIMARY KEY CLUSTERED 
(
	[IdSubAlcaldia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Responsables]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Responsables_dbo.Personas_Id] FOREIGN KEY([Id])
REFERENCES [dbo].[Personas] ([Id])
GO
ALTER TABLE [dbo].[Responsables] CHECK CONSTRAINT [FK_dbo.Responsables_dbo.Personas_Id]
GO
/****** Object:  StoredProcedure [dbo].[SubAlcaldia_Delete]    Script Date: 2016-05-18 3:31:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SubAlcaldia_Delete]
    @IdSubAlcaldia [int]
AS
BEGIN
    DELETE [dbo].[SubAlcaldias]
    WHERE ([IdSubAlcaldia] = @IdSubAlcaldia)
END
GO
/****** Object:  StoredProcedure [dbo].[SubAlcaldia_Insert]    Script Date: 2016-05-18 3:31:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SubAlcaldia_Insert]
    @Nombre [nvarchar](50),
    @Direccion [nvarchar](max),
    @Zona [nvarchar](50),
    @Telefono [nvarchar](30),
    @NombreSubAlcalde [nvarchar](50)
AS
BEGIN
    INSERT [dbo].[SubAlcaldias]([Nombre], [Direccion], [Zona], [Telefono], [NombreSubAlcalde])
    VALUES (@Nombre, @Direccion, @Zona, @Telefono, @NombreSubAlcalde)
    
    DECLARE @IdSubAlcaldia int
    SELECT @IdSubAlcaldia = [IdSubAlcaldia]
    FROM [dbo].[SubAlcaldias]
    WHERE @@ROWCOUNT > 0 AND [IdSubAlcaldia] = scope_identity()
    
    SELECT t0.[IdSubAlcaldia]
    FROM [dbo].[SubAlcaldias] AS t0
    WHERE @@ROWCOUNT > 0 AND t0.[IdSubAlcaldia] = @IdSubAlcaldia
END
GO
/****** Object:  StoredProcedure [dbo].[SubAlcaldia_Update]    Script Date: 2016-05-18 3:31:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SubAlcaldia_Update]
    @IdSubAlcaldia [int],
    @Nombre [nvarchar](50),
    @Direccion [nvarchar](max),
    @Zona [nvarchar](50),
    @Telefono [nvarchar](30),
    @NombreSubAlcalde [nvarchar](50)
AS
BEGIN
    UPDATE [dbo].[SubAlcaldias]
    SET [Nombre] = @Nombre, [Direccion] = @Direccion, [Zona] = @Zona, [Telefono] = @Telefono, [NombreSubAlcalde] = @NombreSubAlcalde
    WHERE ([IdSubAlcaldia] = @IdSubAlcaldia)
END
GO
