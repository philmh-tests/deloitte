CREATE TABLE [dbo].[City] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR (100) NOT NULL,
    [State]               NVARCHAR (100) NOT NULL,
    [Country]             NVARCHAR (100) NOT NULL,
    [TouristRating]       TINYINT        NULL,
    [DateEstablishedOn]   DATETIME2 (7)  NULL,
    [EstimatedPopulation] BIGINT         NULL,
    CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'i.e. Geographic sub-region', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'City', @level2type = N'COLUMN', @level2name = N'State';

