CREATE TABLE [identity].[Products] (
    [ProductId]   INT             IDENTITY (1, 1) NOT NULL,
    [UserId]      NVARCHAR (128)  NOT NULL,
    [Name]        NVARCHAR (MAX)  NOT NULL,
    [Description] NVARCHAR (MAX)  NOT NULL,
    [Price]       DECIMAL (18, 2) NOT NULL,
    [Category]    INT             NOT NULL,
    CONSTRAINT [PK_identity.Products] PRIMARY KEY CLUSTERED ([ProductId] ASC), 
    CONSTRAINT [FK_Products_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id]) 
);

