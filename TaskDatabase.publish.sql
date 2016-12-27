
PRINT N'Creating [dbo].[LastReadFeedItem]...';


GO
CREATE TABLE [dbo].[LastReadFeedItem] (
    [Id]              INT              IDENTITY (1, 1) NOT NULL,
    [FeedId]          UNIQUEIDENTIFIER NOT NULL,
    [LastEntryReadId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_LastReadFeedItem] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Messages]...';


GO
CREATE TABLE [dbo].[Messages] (
    [MessageId]   UNIQUEIDENTIFIER NOT NULL,
    [Topic]       NVARCHAR (255)   NULL,
    [MessageType] NVARCHAR (32)    NULL,
    [Timestamp]   DATETIME         NULL,
    [HeaderBag]   NTEXT            NULL,
    [Body]        NTEXT            NULL,
    CONSTRAINT [PK_MessageId] PRIMARY KEY CLUSTERED ([MessageId] ASC)
);


GO
PRINT N'Creating [dbo].[Messages].[UQ_Messages__MessageId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Messages__MessageId]
    ON [dbo].[Messages]([MessageId] ASC);


GO
PRINT N'Creating [dbo].[Orders]...';


GO
CREATE TABLE [dbo].[Orders] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [customername]     NVARCHAR (250) NOT NULL,
    [orderdescription] NVARCHAR (500) NULL,
    [duedate]          DATETIME       NULL,
    [completiondate]   DATETIME       NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Products]...';


GO
CREATE TABLE [dbo].[Products] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [ProductName]        NVARCHAR (100) NOT NULL,
    [ProductDescription] NVARCHAR (250) NULL,
    [ProductPrice]       FLOAT (53)     NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[StoreMessages]...';


GO
CREATE TABLE [dbo].[StoreMessages] (
    [Id]          BIGINT           IDENTITY (1, 1) NOT NULL,
    [MessageId]   UNIQUEIDENTIFIER NOT NULL,
    [Topic]       NVARCHAR (255)   NULL,
    [MessageType] NVARCHAR (32)    NULL,
    [Timestamp]   DATETIME         NULL,
    [HeaderBag]   NTEXT            NULL,
    [Body]        NTEXT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Tasks]...';


GO
CREATE TABLE [dbo].[Tasks] (
    [taskname]        NVARCHAR (250) NOT NULL,
    [taskdescription] NVARCHAR (500) NULL,
    [duedate]         DATETIME       NULL,
    [completiondate]  DATETIME       NULL,
    [id]              INT            IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK__Tasks__0000000000000025] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [UQ__Tasks__0000000000000020] UNIQUE NONCLUSTERED ([id] ASC)
);


GO

PRINT N'Update complete.';


GO
