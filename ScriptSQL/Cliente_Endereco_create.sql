CREATE DATABASE Teste_Tecnico
GO

USE [Teste_Tecnico]
GO
 
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cliente](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](300) NULL,
	[Email] [varchar](100) NULL,
	[DataNascimento] [datetime2] NULL,
	[CEP] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO




CREATE TABLE [dbo].[Endereco](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Cep] [varchar](10) NULL,	
	[Logradouro] [varchar](100) NUll,
	[Complemento][varchar](100) NUll,
	[Bairro] [varchar](100) NUll,
	[Localidade] [varchar](100)NUll,
	[UF] [varchar](100) NUll,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO