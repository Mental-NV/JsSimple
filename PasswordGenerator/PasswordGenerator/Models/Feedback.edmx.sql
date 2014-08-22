
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/23/2014 03:34:57
-- Generated from EDMX file: C:\Users\Mental\GitHub\JsSimple\PasswordGenerator\PasswordGenerator\Models\Feedback.edmx
-- --------------------------------------------------

/*SET QUOTED_IDENTIFIER OFF;
GO
USE [shortener_db];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO*/

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'FeedbackSet'
CREATE TABLE [dbo].[FeedbackSet] (
    [FeedId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Message] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [FeedId] in table 'FeedbackSet'
ALTER TABLE [dbo].[FeedbackSet]
ADD CONSTRAINT [PK_FeedbackSet]
    PRIMARY KEY CLUSTERED ([FeedId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------