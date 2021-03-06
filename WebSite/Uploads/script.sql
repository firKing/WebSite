USE [WebSite]
GO
/****** Object:  Table [dbo].[admins]    Script Date: 2015/2/1 18:12:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[admins](
	[adminId] [int] IDENTITY(1,1) NOT NULL,
	[admin_name] [varchar](30) NOT NULL,
	[admin_pwd] [varchar](20) NOT NULL,
	[time] [datetime] NOT NULL,
 CONSTRAINT [PK_admins] PRIMARY KEY CLUSTERED 
(
	[adminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[audits]    Script Date: 2015/2/1 18:12:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[audits](
	[bidId] [int] IDENTITY(1,1) NOT NULL,
	[expertId] [int] NOT NULL,
	[auditId] [int] NOT NULL,
	[audit_content] [varchar](500) NULL,
	[audit_time] [datetime] NOT NULL,
 CONSTRAINT [PK_audits] PRIMARY KEY CLUSTERED 
(
	[auditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[bidders]    Script Date: 2015/2/1 18:12:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bidders](
	[bidderId] [int] IDENTITY(1,1) NOT NULL,
	[bidder_is_team] [bit] NOT NULL,
	[tendererId] [int] NOT NULL,
 CONSTRAINT [PK_bidders] PRIMARY KEY CLUSTERED 
(
	[bidderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bids]    Script Date: 2015/2/1 18:12:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[bids](
	[bidId] [int] IDENTITY(1,1) NOT NULL,
	[purchaseId] [int] NOT NULL,
	[bidderId] [int] NOT NULL,
	[bid_title] [varchar](50) NOT NULL,
	[bid_device_name] [varchar](50) NOT NULL,
	[bid_time] [datetime] NOT NULL,
	[bid_number] [int] NOT NULL,
	[bid_introduction] [varchar](1500) NOT NULL,
	[bid_content] [varchar](50) NOT NULL,
 CONSTRAINT [PK_bids] PRIMARY KEY CLUSTERED 
(
	[bidId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[companies]    Script Date: 2015/2/1 18:12:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[companies](
	[companyId] [int] IDENTITY(1,1) NOT NULL,
	[user_userId] [int] NOT NULL,
 CONSTRAINT [PK_companies] PRIMARY KEY CLUSTERED 
(
	[companyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[experts]    Script Date: 2015/2/1 18:12:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[experts](
	[expertId] [int] IDENTITY(1,1) NOT NULL,
	[expert_image] [nvarchar](max) NULL,
	[expert_accept_count] [int] NOT NULL,
	[user_userId] [int] NOT NULL,
 CONSTRAINT [PK_experts] PRIMARY KEY CLUSTERED 
(
	[expertId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[invitations]    Script Date: 2015/2/1 18:12:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[invitations](
	[purchaseId] [int] NOT NULL,
	[expertId] [int] NOT NULL,
	[invitationId] [int] IDENTITY(1,1) NOT NULL,
	[invitation_content] [varchar](500) NOT NULL,
	[invitation_time] [datetime] NOT NULL,
 CONSTRAINT [PK_invitations] PRIMARY KEY CLUSTERED 
(
	[invitationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[members]    Script Date: 2015/2/1 18:12:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[members](
	[vendorId] [int] NOT NULL,
	[teamId] [int] NOT NULL,
	[memberId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_members] PRIMARY KEY CLUSTERED 
(
	[memberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[news]    Script Date: 2015/2/1 18:12:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[news](
	[newsId] [int] IDENTITY(1,1) NOT NULL,
	[companyId] [int] NOT NULL,
	[news_title] [varchar](20) NOT NULL,
	[news_content] [varchar](max) NOT NULL,
	[news_time] [datetime] NOT NULL,
 CONSTRAINT [PK_news] PRIMARY KEY CLUSTERED 
(
	[newsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[purchases]    Script Date: 2015/2/1 18:12:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[purchases](
	[purchaseId] [int] IDENTITY(1,1) NOT NULL,
	[companyId] [int] NOT NULL,
	[purchase_title] [varchar](20) NOT NULL,
	[purchase_content] [varchar](max) NOT NULL,
	[purchase_time] [datetime] NOT NULL,
	[hitId] [int] NOT NULL,
 CONSTRAINT [PK_purchases] PRIMARY KEY CLUSTERED 
(
	[purchaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[teams]    Script Date: 2015/2/1 18:12:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[teams](
	[teamId] [int] IDENTITY(1,1) NOT NULL,
	[team_name] [varchar](30) NOT NULL,
	[team_introduction] [varchar](300) NOT NULL,
	[purchaseId] [int] NOT NULL,
	[createId] [int] NOT NULL,
	[team_time] [datetime] NOT NULL,
 CONSTRAINT [PK_teams] PRIMARY KEY CLUSTERED 
(
	[teamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users]    Script Date: 2015/2/1 18:12:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[user_type] [nvarchar](max) NOT NULL,
	[user_telephone] [nvarchar](max) NOT NULL,
	[user_mail] [nvarchar](max) NOT NULL,
	[user_name] [nvarchar](max) NOT NULL,
	[user_address] [nvarchar](max) NOT NULL,
	[user_introduction] [nvarchar](max) NOT NULL,
	[user_password] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vendors]    Script Date: 2015/2/1 18:12:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vendors](
	[vendorId] [int] IDENTITY(1,1) NOT NULL,
	[user_userId] [int] NOT NULL,
 CONSTRAINT [PK_vendors] PRIMARY KEY CLUSTERED 
(
	[vendorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[bidders] ON 

INSERT [dbo].[bidders] ([bidderId], [bidder_is_team], [tendererId]) VALUES (1, 1, 1)
INSERT [dbo].[bidders] ([bidderId], [bidder_is_team], [tendererId]) VALUES (2, 1, 2)
INSERT [dbo].[bidders] ([bidderId], [bidder_is_team], [tendererId]) VALUES (3, 0, 1)
INSERT [dbo].[bidders] ([bidderId], [bidder_is_team], [tendererId]) VALUES (4, 0, 2)
SET IDENTITY_INSERT [dbo].[bidders] OFF
SET IDENTITY_INSERT [dbo].[bids] ON 

INSERT [dbo].[bids] ([bidId], [purchaseId], [bidderId], [bid_title], [bid_device_name], [bid_time], [bid_number], [bid_introduction], [bid_content]) VALUES (2, 4, 1, N'投标4号标书', N'1000', CAST(N'2015-02-01 00:00:00.000' AS DateTime), 1000, N'212341231321321', N'no add url')
INSERT [dbo].[bids] ([bidId], [purchaseId], [bidderId], [bid_title], [bid_device_name], [bid_time], [bid_number], [bid_introduction], [bid_content]) VALUES (5, 4, 2, N'投标4号标书', N'500', CAST(N'2015-02-01 01:00:00.000' AS DateTime), 900, N'2131', N'no add url2')
INSERT [dbo].[bids] ([bidId], [purchaseId], [bidderId], [bid_title], [bid_device_name], [bid_time], [bid_number], [bid_introduction], [bid_content]) VALUES (8, 4, 4, N'投标啊啊啊', N'100', CAST(N'2014-12-31 00:00:00.000' AS DateTime), 700, N'123123', N'no add url3')
SET IDENTITY_INSERT [dbo].[bids] OFF
SET IDENTITY_INSERT [dbo].[companies] ON 

INSERT [dbo].[companies] ([companyId], [user_userId]) VALUES (2, 2)
INSERT [dbo].[companies] ([companyId], [user_userId]) VALUES (1, 3)
SET IDENTITY_INSERT [dbo].[companies] OFF
SET IDENTITY_INSERT [dbo].[experts] ON 

INSERT [dbo].[experts] ([expertId], [expert_image], [expert_accept_count], [user_userId]) VALUES (1, N'Protrait/4.jpg', 0, 1)
INSERT [dbo].[experts] ([expertId], [expert_image], [expert_accept_count], [user_userId]) VALUES (2, N'Protrait/4.jpg', 0, 4)
INSERT [dbo].[experts] ([expertId], [expert_image], [expert_accept_count], [user_userId]) VALUES (3, N'Protrait/0.jpg', 0, 6)
SET IDENTITY_INSERT [dbo].[experts] OFF
SET IDENTITY_INSERT [dbo].[invitations] ON 

INSERT [dbo].[invitations] ([purchaseId], [expertId], [invitationId], [invitation_content], [invitation_time]) VALUES (4, 1, 1, N'221321', CAST(N'2015-02-01 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[invitations] OFF
SET IDENTITY_INSERT [dbo].[members] ON 

INSERT [dbo].[members] ([vendorId], [teamId], [memberId]) VALUES (1, 1, 1)
INSERT [dbo].[members] ([vendorId], [teamId], [memberId]) VALUES (2, 1, 3)
INSERT [dbo].[members] ([vendorId], [teamId], [memberId]) VALUES (1, 2, 4)
INSERT [dbo].[members] ([vendorId], [teamId], [memberId]) VALUES (2, 2, 6)
SET IDENTITY_INSERT [dbo].[members] OFF
SET IDENTITY_INSERT [dbo].[news] ON 

INSERT [dbo].[news] ([newsId], [companyId], [news_title], [news_content], [news_time]) VALUES (1, 1, N'测试新闻标题1', N'测试新闻1', CAST(N'2015-02-01 00:00:00.000' AS DateTime))
INSERT [dbo].[news] ([newsId], [companyId], [news_title], [news_content], [news_time]) VALUES (2, 1, N'测试新闻标题2', N'测试新闻2', CAST(N'2015-02-01 07:00:00.000' AS DateTime))
INSERT [dbo].[news] ([newsId], [companyId], [news_title], [news_content], [news_time]) VALUES (3, 1, N'测试新闻标题3', N'测试新闻3', CAST(N'2015-02-01 09:00:00.000' AS DateTime))
INSERT [dbo].[news] ([newsId], [companyId], [news_title], [news_content], [news_time]) VALUES (4, 2, N'单位2测试新闻1', N'测试新闻1', CAST(N'2015-02-01 06:00:00.000' AS DateTime))
INSERT [dbo].[news] ([newsId], [companyId], [news_title], [news_content], [news_time]) VALUES (5, 2, N'单位2测试新闻2', N'测试新闻2', CAST(N'2015-02-01 06:00:12.000' AS DateTime))
INSERT [dbo].[news] ([newsId], [companyId], [news_title], [news_content], [news_time]) VALUES (8, 2, N'单位2测试新闻3', N'单位2测试新闻3', CAST(N'2014-02-01 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[news] OFF
SET IDENTITY_INSERT [dbo].[purchases] ON 

INSERT [dbo].[purchases] ([purchaseId], [companyId], [purchase_title], [purchase_content], [purchase_time], [hitId]) VALUES (2, 2, N'采购信息1', N'采购信息1采购信息1', CAST(N'2015-02-01 00:00:00.000' AS DateTime), 0)
INSERT [dbo].[purchases] ([purchaseId], [companyId], [purchase_title], [purchase_content], [purchase_time], [hitId]) VALUES (3, 2, N'采购信息2', N'采购信息2采购信息2', CAST(N'2015-01-30 00:00:00.000' AS DateTime), 0)
INSERT [dbo].[purchases] ([purchaseId], [companyId], [purchase_title], [purchase_content], [purchase_time], [hitId]) VALUES (4, 1, N'来一车dong1234', N'来一打董少侠啊', CAST(N'2015-01-29 00:00:00.000' AS DateTime), 0)
INSERT [dbo].[purchases] ([purchaseId], [companyId], [purchase_title], [purchase_content], [purchase_time], [hitId]) VALUES (7, 1, N'来一打女神牌花露水', N'来一车啊来一车', CAST(N'2015-01-28 00:00:00.000' AS DateTime), 0)
INSERT [dbo].[purchases] ([purchaseId], [companyId], [purchase_title], [purchase_content], [purchase_time], [hitId]) VALUES (8, 1, N'来一堆老李', N'来一群', CAST(N'2015-01-27 00:00:00.000' AS DateTime), 0)
INSERT [dbo].[purchases] ([purchaseId], [companyId], [purchase_title], [purchase_content], [purchase_time], [hitId]) VALUES (9, 2, N'来一堆啊来一堆', N'agin', CAST(N'2015-01-26 00:00:00.000' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[purchases] OFF
SET IDENTITY_INSERT [dbo].[teams] ON 

INSERT [dbo].[teams] ([teamId], [team_name], [team_introduction], [purchaseId], [createId], [team_time]) VALUES (1, N'和谐社会啊啊啊', N'啦啦啦', 4, 1, CAST(N'2015-02-01 00:00:00.000' AS DateTime))
INSERT [dbo].[teams] ([teamId], [team_name], [team_introduction], [purchaseId], [createId], [team_time]) VALUES (2, N'和谐社会亚无搜', N'啦啦啦', 4, 1, CAST(N'2015-02-01 00:00:00.000' AS DateTime))
INSERT [dbo].[teams] ([teamId], [team_name], [team_introduction], [purchaseId], [createId], [team_time]) VALUES (3, N'中国梦之声', N'啦啦啦啦啦', 4, 1, CAST(N'2015-02-01 00:00:00.000' AS DateTime))
INSERT [dbo].[teams] ([teamId], [team_name], [team_introduction], [purchaseId], [createId], [team_time]) VALUES (4, N'额额额额额', N'你妹你峨眉你峨眉', 2, 2, CAST(N'2015-01-29 00:00:00.000' AS DateTime))
INSERT [dbo].[teams] ([teamId], [team_name], [team_introduction], [purchaseId], [createId], [team_time]) VALUES (5, N'AEIOU', N'噶法律', 2, 2, CAST(N'2014-02-01 00:00:00.000' AS DateTime))
INSERT [dbo].[teams] ([teamId], [team_name], [team_introduction], [purchaseId], [createId], [team_time]) VALUES (7, N'55555', N'123123123', 4, 2, CAST(N'2014-05-01 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[teams] OFF
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([userId], [user_type], [user_telephone], [user_mail], [user_name], [user_address], [user_introduction], [user_password]) VALUES (1, N'Expert', N'15310610260', N'601519305@qq.com', N'zhongguofu11', N'zhongjlfaf', N'1233', N'123456')
INSERT [dbo].[users] ([userId], [user_type], [user_telephone], [user_mail], [user_name], [user_address], [user_introduction], [user_password]) VALUES (2, N'Company', N'15310610260', N'601519305@qq.com', N'钟郭福啊啊啊', N'zhongjlfaf', N'sdasda', N'123456')
INSERT [dbo].[users] ([userId], [user_type], [user_telephone], [user_mail], [user_name], [user_address], [user_introduction], [user_password]) VALUES (3, N'Company', N'15310610260', N'601519305@qq.com', N'钟郭福单位', N'zhongjlfaf', N'123', N'123456')
INSERT [dbo].[users] ([userId], [user_type], [user_telephone], [user_mail], [user_name], [user_address], [user_introduction], [user_password]) VALUES (4, N'Vendor', N'15310610260', N'601519305@qq.com', N'钟郭福供应商', N'zhongjlfaf', N'123', N'123456')
INSERT [dbo].[users] ([userId], [user_type], [user_telephone], [user_mail], [user_name], [user_address], [user_introduction], [user_password]) VALUES (5, N'Vendor', N'15310610260', N'601519305@qq.com', N'钟郭福供应商2', N'zhongjlfaf', N'123', N'123456')
INSERT [dbo].[users] ([userId], [user_type], [user_telephone], [user_mail], [user_name], [user_address], [user_introduction], [user_password]) VALUES (6, N'Expert', N'15310610260', N'601519305@qq.com', N'钟郭福', N'zhongjlfaf', N'123', N'123456')
SET IDENTITY_INSERT [dbo].[users] OFF
SET IDENTITY_INSERT [dbo].[vendors] ON 

INSERT [dbo].[vendors] ([vendorId], [user_userId]) VALUES (2, 4)
INSERT [dbo].[vendors] ([vendorId], [user_userId]) VALUES (1, 5)
SET IDENTITY_INSERT [dbo].[vendors] OFF
ALTER TABLE [dbo].[audits]  WITH CHECK ADD  CONSTRAINT [FK_AUDIT_AUDIT_BID] FOREIGN KEY([bidId])
REFERENCES [dbo].[bids] ([bidId])
GO
ALTER TABLE [dbo].[audits] CHECK CONSTRAINT [FK_AUDIT_AUDIT_BID]
GO
ALTER TABLE [dbo].[audits]  WITH CHECK ADD  CONSTRAINT [FK_AUDIT_HAVE_EXPERT] FOREIGN KEY([expertId])
REFERENCES [dbo].[experts] ([expertId])
GO
ALTER TABLE [dbo].[audits] CHECK CONSTRAINT [FK_AUDIT_HAVE_EXPERT]
GO
ALTER TABLE [dbo].[bids]  WITH CHECK ADD  CONSTRAINT [FK_BID_HAVE_BID_PURCHASE] FOREIGN KEY([purchaseId])
REFERENCES [dbo].[purchases] ([purchaseId])
GO
ALTER TABLE [dbo].[bids] CHECK CONSTRAINT [FK_BID_HAVE_BID_PURCHASE]
GO
ALTER TABLE [dbo].[bids]  WITH CHECK ADD  CONSTRAINT [FK_BID_PUBLISH_B_BIDDER] FOREIGN KEY([bidderId])
REFERENCES [dbo].[bidders] ([bidderId])
GO
ALTER TABLE [dbo].[bids] CHECK CONSTRAINT [FK_BID_PUBLISH_B_BIDDER]
GO
ALTER TABLE [dbo].[companies]  WITH CHECK ADD  CONSTRAINT [FK_usercompany] FOREIGN KEY([user_userId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[companies] CHECK CONSTRAINT [FK_usercompany]
GO
ALTER TABLE [dbo].[experts]  WITH CHECK ADD  CONSTRAINT [FK_userexpert] FOREIGN KEY([user_userId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[experts] CHECK CONSTRAINT [FK_userexpert]
GO
ALTER TABLE [dbo].[invitations]  WITH CHECK ADD  CONSTRAINT [FK_INVITATI_INVITE_EXPERT] FOREIGN KEY([expertId])
REFERENCES [dbo].[experts] ([expertId])
GO
ALTER TABLE [dbo].[invitations] CHECK CONSTRAINT [FK_INVITATI_INVITE_EXPERT]
GO
ALTER TABLE [dbo].[invitations]  WITH CHECK ADD  CONSTRAINT [FK_INVITATI_RESPOND_PURCHASE] FOREIGN KEY([purchaseId])
REFERENCES [dbo].[purchases] ([purchaseId])
GO
ALTER TABLE [dbo].[invitations] CHECK CONSTRAINT [FK_INVITATI_RESPOND_PURCHASE]
GO
ALTER TABLE [dbo].[members]  WITH CHECK ADD  CONSTRAINT [FK_MEMBER_COMPONENT_TEAM] FOREIGN KEY([teamId])
REFERENCES [dbo].[teams] ([teamId])
GO
ALTER TABLE [dbo].[members] CHECK CONSTRAINT [FK_MEMBER_COMPONENT_TEAM]
GO
ALTER TABLE [dbo].[members]  WITH CHECK ADD  CONSTRAINT [FK_MEMBER_INCLUDE_VENDOR] FOREIGN KEY([vendorId])
REFERENCES [dbo].[vendors] ([vendorId])
GO
ALTER TABLE [dbo].[members] CHECK CONSTRAINT [FK_MEMBER_INCLUDE_VENDOR]
GO
ALTER TABLE [dbo].[news]  WITH CHECK ADD  CONSTRAINT [FK_NEWS_PUBLISH_N_COMPANY] FOREIGN KEY([companyId])
REFERENCES [dbo].[companies] ([companyId])
GO
ALTER TABLE [dbo].[news] CHECK CONSTRAINT [FK_NEWS_PUBLISH_N_COMPANY]
GO
ALTER TABLE [dbo].[purchases]  WITH CHECK ADD  CONSTRAINT [FK_PURCHASE_PUBLISH_P_COMPANY] FOREIGN KEY([companyId])
REFERENCES [dbo].[companies] ([companyId])
GO
ALTER TABLE [dbo].[purchases] CHECK CONSTRAINT [FK_PURCHASE_PUBLISH_P_COMPANY]
GO
ALTER TABLE [dbo].[teams]  WITH CHECK ADD  CONSTRAINT [FK_purchaseteam] FOREIGN KEY([purchaseId])
REFERENCES [dbo].[purchases] ([purchaseId])
GO
ALTER TABLE [dbo].[teams] CHECK CONSTRAINT [FK_purchaseteam]
GO
ALTER TABLE [dbo].[vendors]  WITH CHECK ADD  CONSTRAINT [FK_uservendor] FOREIGN KEY([user_userId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[vendors] CHECK CONSTRAINT [FK_uservendor]
GO
