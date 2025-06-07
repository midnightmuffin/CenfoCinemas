﻿USE [cenfocinemas-db]
GO

/****** Object:  Table [dbo].[TBL_Movie]    Script Date: 07/06/2025 11:01:17 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TBL_Movie](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NULL,
	[Title] [nvarchar](75) NOT NULL,
	[Description] [nvarchar](250) NOT NULL,
	[ReleaseDate] [datetime] NOT NULL,
	[Genre] [nvarchar](20) NOT NULL,
	[Director] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_TBL_Movie] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO