
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/23/2015 11:42:32
-- Generated from EDMX file: F:\MyProjects\WebSite\WebSite\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [WebSite];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AUDIT_AUDIT_BID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[audit] DROP CONSTRAINT [FK_AUDIT_AUDIT_BID];
GO
IF OBJECT_ID(N'[dbo].[FK_AUDIT_HAVE_EXPERT]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[audit] DROP CONSTRAINT [FK_AUDIT_HAVE_EXPERT];
GO
IF OBJECT_ID(N'[dbo].[FK_BID_HAVE_BID_PURCHASE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[bid] DROP CONSTRAINT [FK_BID_HAVE_BID_PURCHASE];
GO
IF OBJECT_ID(N'[dbo].[FK_BID_PUBLISH_B_BIDDER]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[bid] DROP CONSTRAINT [FK_BID_PUBLISH_B_BIDDER];
GO
IF OBJECT_ID(N'[dbo].[FK_INVITATI_INVITE_EXPERT]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[invitation] DROP CONSTRAINT [FK_INVITATI_INVITE_EXPERT];
GO
IF OBJECT_ID(N'[dbo].[FK_INVITATI_RESPOND_PURCHASE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[invitation] DROP CONSTRAINT [FK_INVITATI_RESPOND_PURCHASE];
GO
IF OBJECT_ID(N'[dbo].[FK_MEMBER_COMPONENT_TEAM]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[member] DROP CONSTRAINT [FK_MEMBER_COMPONENT_TEAM];
GO
IF OBJECT_ID(N'[dbo].[FK_MEMBER_INCLUDE_VENDOR]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[member] DROP CONSTRAINT [FK_MEMBER_INCLUDE_VENDOR];
GO
IF OBJECT_ID(N'[dbo].[FK_NEWS_PUBLISH_N_COMPANY]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[news] DROP CONSTRAINT [FK_NEWS_PUBLISH_N_COMPANY];
GO
IF OBJECT_ID(N'[dbo].[FK_PURCHASE_PUBLISH_P_COMPANY]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[purchase] DROP CONSTRAINT [FK_PURCHASE_PUBLISH_P_COMPANY];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[admin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[admin];
GO
IF OBJECT_ID(N'[dbo].[audit]', 'U') IS NOT NULL
    DROP TABLE [dbo].[audit];
GO
IF OBJECT_ID(N'[dbo].[bid]', 'U') IS NOT NULL
    DROP TABLE [dbo].[bid];
GO
IF OBJECT_ID(N'[dbo].[bidder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[bidder];
GO
IF OBJECT_ID(N'[dbo].[company]', 'U') IS NOT NULL
    DROP TABLE [dbo].[company];
GO
IF OBJECT_ID(N'[dbo].[expert]', 'U') IS NOT NULL
    DROP TABLE [dbo].[expert];
GO
IF OBJECT_ID(N'[dbo].[invitation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[invitation];
GO
IF OBJECT_ID(N'[dbo].[member]', 'U') IS NOT NULL
    DROP TABLE [dbo].[member];
GO
IF OBJECT_ID(N'[dbo].[news]', 'U') IS NOT NULL
    DROP TABLE [dbo].[news];
GO
IF OBJECT_ID(N'[dbo].[purchase]', 'U') IS NOT NULL
    DROP TABLE [dbo].[purchase];
GO
IF OBJECT_ID(N'[dbo].[team]', 'U') IS NOT NULL
    DROP TABLE [dbo].[team];
GO
IF OBJECT_ID(N'[dbo].[vendor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[vendor];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'admins'
CREATE TABLE [dbo].[admins] (
    [adminId] int  NOT NULL,
    [admin_name] varchar(30)  NOT NULL,
    [admin_pwd] varchar(20)  NOT NULL,
    [time] datetime  NOT NULL
);
GO

-- Creating table 'audits'
CREATE TABLE [dbo].[audits] (
    [bidId] int  NOT NULL,
    [expertId] int  NOT NULL,
    [auditId] int  NOT NULL,
    [audit_content] varchar(500)  NULL
);
GO

-- Creating table 'bids'
CREATE TABLE [dbo].[bids] (
    [bidId] int  NOT NULL,
    [purchaseId] int  NOT NULL,
    [bidderId] int  NOT NULL,
    [bid_title] varchar(50)  NOT NULL,
    [bid_device_name] varchar(50)  NOT NULL,
    [bid_time] datetime  NOT NULL,
    [bid_number] int  NOT NULL,
    [bid_introduction] varchar(1500)  NOT NULL,
    [bid_content] varbinary(max)  NOT NULL
);
GO

-- Creating table 'bidders'
CREATE TABLE [dbo].[bidders] (
    [bidderId] int  NOT NULL,
    [bidder_is_team] bit  NOT NULL,
    [tendererId] int  NOT NULL
);
GO

-- Creating table 'companies'
CREATE TABLE [dbo].[companies] (
    [companyId] int  NOT NULL,
    [company_name] varchar(20)  NOT NULL,
    [company_telephone] varchar(20)  NOT NULL,
    [company_mail] varchar(50)  NOT NULL,
    [company_address] varchar(300)  NOT NULL,
    [company_introduction] varchar(500)  NOT NULL,
    [company_password] varchar(20)  NOT NULL
);
GO

-- Creating table 'experts'
CREATE TABLE [dbo].[experts] (
    [expertId] int  NOT NULL,
    [expert_name] varchar(20)  NOT NULL,
    [expert_telephone] varchar(20)  NOT NULL,
    [expert_mail] varchar(50)  NOT NULL,
    [expert_address] varchar(300)  NOT NULL,
    [expert_introduce] varchar(500)  NOT NULL,
    [expert_password] varchar(20)  NOT NULL,
    [expert_image] varbinary(max)  NULL
);
GO

-- Creating table 'invitations'
CREATE TABLE [dbo].[invitations] (
    [purchaseId] int  NOT NULL,
    [expertId] int  NOT NULL,
    [invitationId] int  NOT NULL,
    [invitation_content] varchar(500)  NOT NULL
);
GO

-- Creating table 'members'
CREATE TABLE [dbo].[members] (
    [vendorId] int  NOT NULL,
    [teamId] int  NOT NULL,
    [memberId] int  NOT NULL
);
GO

-- Creating table 'news'
CREATE TABLE [dbo].[news] (
    [newsId] int  NOT NULL,
    [companyId] int  NOT NULL,
    [news_title] varchar(20)  NOT NULL,
    [news_content] varchar(max)  NOT NULL,
    [news_time] datetime  NOT NULL
);
GO

-- Creating table 'purchases'
CREATE TABLE [dbo].[purchases] (
    [purchaseId] int  NOT NULL,
    [companyId] int  NOT NULL,
    [purchase_title] varchar(20)  NOT NULL,
    [purchase_content] varchar(max)  NOT NULL,
    [purchase_time] datetime  NOT NULL,
    [hitId] int  NOT NULL
);
GO

-- Creating table 'teams'
CREATE TABLE [dbo].[teams] (
    [teamId] int  NOT NULL,
    [team_name] varchar(30)  NOT NULL,
    [team_introduction] varchar(300)  NOT NULL
);
GO

-- Creating table 'vendors'
CREATE TABLE [dbo].[vendors] (
    [vendorId] int  NOT NULL,
    [vendor_name] varchar(20)  NOT NULL,
    [vendor_telephone] varchar(20)  NOT NULL,
    [vendor_mail] varchar(50)  NOT NULL,
    [vendor_address] varchar(300)  NULL,
    [vendor_introduction] varchar(500)  NULL,
    [vendor_password] varchar(20)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [adminId] in table 'admins'
ALTER TABLE [dbo].[admins]
ADD CONSTRAINT [PK_admins]
    PRIMARY KEY CLUSTERED ([adminId] ASC);
GO

-- Creating primary key on [auditId] in table 'audits'
ALTER TABLE [dbo].[audits]
ADD CONSTRAINT [PK_audits]
    PRIMARY KEY CLUSTERED ([auditId] ASC);
GO

-- Creating primary key on [bidId] in table 'bids'
ALTER TABLE [dbo].[bids]
ADD CONSTRAINT [PK_bids]
    PRIMARY KEY CLUSTERED ([bidId] ASC);
GO

-- Creating primary key on [bidderId] in table 'bidders'
ALTER TABLE [dbo].[bidders]
ADD CONSTRAINT [PK_bidders]
    PRIMARY KEY CLUSTERED ([bidderId] ASC);
GO

-- Creating primary key on [companyId] in table 'companies'
ALTER TABLE [dbo].[companies]
ADD CONSTRAINT [PK_companies]
    PRIMARY KEY CLUSTERED ([companyId] ASC);
GO

-- Creating primary key on [expertId] in table 'experts'
ALTER TABLE [dbo].[experts]
ADD CONSTRAINT [PK_experts]
    PRIMARY KEY CLUSTERED ([expertId] ASC);
GO

-- Creating primary key on [invitationId] in table 'invitations'
ALTER TABLE [dbo].[invitations]
ADD CONSTRAINT [PK_invitations]
    PRIMARY KEY CLUSTERED ([invitationId] ASC);
GO

-- Creating primary key on [memberId] in table 'members'
ALTER TABLE [dbo].[members]
ADD CONSTRAINT [PK_members]
    PRIMARY KEY CLUSTERED ([memberId] ASC);
GO

-- Creating primary key on [newsId] in table 'news'
ALTER TABLE [dbo].[news]
ADD CONSTRAINT [PK_news]
    PRIMARY KEY CLUSTERED ([newsId] ASC);
GO

-- Creating primary key on [purchaseId] in table 'purchases'
ALTER TABLE [dbo].[purchases]
ADD CONSTRAINT [PK_purchases]
    PRIMARY KEY CLUSTERED ([purchaseId] ASC);
GO

-- Creating primary key on [teamId] in table 'teams'
ALTER TABLE [dbo].[teams]
ADD CONSTRAINT [PK_teams]
    PRIMARY KEY CLUSTERED ([teamId] ASC);
GO

-- Creating primary key on [vendorId] in table 'vendors'
ALTER TABLE [dbo].[vendors]
ADD CONSTRAINT [PK_vendors]
    PRIMARY KEY CLUSTERED ([vendorId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [bidId] in table 'audits'
ALTER TABLE [dbo].[audits]
ADD CONSTRAINT [FK_AUDIT_AUDIT_BID]
    FOREIGN KEY ([bidId])
    REFERENCES [dbo].[bids]
        ([bidId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AUDIT_AUDIT_BID'
CREATE INDEX [IX_FK_AUDIT_AUDIT_BID]
ON [dbo].[audits]
    ([bidId]);
GO

-- Creating foreign key on [expertId] in table 'audits'
ALTER TABLE [dbo].[audits]
ADD CONSTRAINT [FK_AUDIT_HAVE_EXPERT]
    FOREIGN KEY ([expertId])
    REFERENCES [dbo].[experts]
        ([expertId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AUDIT_HAVE_EXPERT'
CREATE INDEX [IX_FK_AUDIT_HAVE_EXPERT]
ON [dbo].[audits]
    ([expertId]);
GO

-- Creating foreign key on [purchaseId] in table 'bids'
ALTER TABLE [dbo].[bids]
ADD CONSTRAINT [FK_BID_HAVE_BID_PURCHASE]
    FOREIGN KEY ([purchaseId])
    REFERENCES [dbo].[purchases]
        ([purchaseId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BID_HAVE_BID_PURCHASE'
CREATE INDEX [IX_FK_BID_HAVE_BID_PURCHASE]
ON [dbo].[bids]
    ([purchaseId]);
GO

-- Creating foreign key on [bidderId] in table 'bids'
ALTER TABLE [dbo].[bids]
ADD CONSTRAINT [FK_BID_PUBLISH_B_BIDDER]
    FOREIGN KEY ([bidderId])
    REFERENCES [dbo].[bidders]
        ([bidderId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BID_PUBLISH_B_BIDDER'
CREATE INDEX [IX_FK_BID_PUBLISH_B_BIDDER]
ON [dbo].[bids]
    ([bidderId]);
GO

-- Creating foreign key on [companyId] in table 'news'
ALTER TABLE [dbo].[news]
ADD CONSTRAINT [FK_NEWS_PUBLISH_N_COMPANY]
    FOREIGN KEY ([companyId])
    REFERENCES [dbo].[companies]
        ([companyId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NEWS_PUBLISH_N_COMPANY'
CREATE INDEX [IX_FK_NEWS_PUBLISH_N_COMPANY]
ON [dbo].[news]
    ([companyId]);
GO

-- Creating foreign key on [companyId] in table 'purchases'
ALTER TABLE [dbo].[purchases]
ADD CONSTRAINT [FK_PURCHASE_PUBLISH_P_COMPANY]
    FOREIGN KEY ([companyId])
    REFERENCES [dbo].[companies]
        ([companyId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PURCHASE_PUBLISH_P_COMPANY'
CREATE INDEX [IX_FK_PURCHASE_PUBLISH_P_COMPANY]
ON [dbo].[purchases]
    ([companyId]);
GO

-- Creating foreign key on [expertId] in table 'invitations'
ALTER TABLE [dbo].[invitations]
ADD CONSTRAINT [FK_INVITATI_INVITE_EXPERT]
    FOREIGN KEY ([expertId])
    REFERENCES [dbo].[experts]
        ([expertId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_INVITATI_INVITE_EXPERT'
CREATE INDEX [IX_FK_INVITATI_INVITE_EXPERT]
ON [dbo].[invitations]
    ([expertId]);
GO

-- Creating foreign key on [purchaseId] in table 'invitations'
ALTER TABLE [dbo].[invitations]
ADD CONSTRAINT [FK_INVITATI_RESPOND_PURCHASE]
    FOREIGN KEY ([purchaseId])
    REFERENCES [dbo].[purchases]
        ([purchaseId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_INVITATI_RESPOND_PURCHASE'
CREATE INDEX [IX_FK_INVITATI_RESPOND_PURCHASE]
ON [dbo].[invitations]
    ([purchaseId]);
GO

-- Creating foreign key on [teamId] in table 'members'
ALTER TABLE [dbo].[members]
ADD CONSTRAINT [FK_MEMBER_COMPONENT_TEAM]
    FOREIGN KEY ([teamId])
    REFERENCES [dbo].[teams]
        ([teamId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MEMBER_COMPONENT_TEAM'
CREATE INDEX [IX_FK_MEMBER_COMPONENT_TEAM]
ON [dbo].[members]
    ([teamId]);
GO

-- Creating foreign key on [vendorId] in table 'members'
ALTER TABLE [dbo].[members]
ADD CONSTRAINT [FK_MEMBER_INCLUDE_VENDOR]
    FOREIGN KEY ([vendorId])
    REFERENCES [dbo].[vendors]
        ([vendorId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MEMBER_INCLUDE_VENDOR'
CREATE INDEX [IX_FK_MEMBER_INCLUDE_VENDOR]
ON [dbo].[members]
    ([vendorId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------