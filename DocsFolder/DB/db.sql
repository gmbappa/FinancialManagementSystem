USE [master]
GO
/****** Object:  Database [FinancialManagementSystem]    Script Date: 11/3/2024 11:31:43 PM ******/
CREATE DATABASE [FinancialManagementSystem]

GO
USE [FinancialManagementSystem]
GO
/****** Object:  UserDefinedTableType [dbo].[JournalEntryLineType]    Script Date: 11/3/2024 11:31:43 PM ******/
CREATE TYPE [dbo].[JournalEntryLineType] AS TABLE(
	[AccountName] [nvarchar](255) NULL,
	[Amount] [decimal](18, 2) NULL,
	[IsDebit] [bit] NULL
)
GO
/****** Object:  Table [dbo].[ChartOfAccounts]    Script Date: 11/3/2024 11:31:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChartOfAccounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountName] [nvarchar](max) NULL,
	[AccountType] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [date] NOT NULL,
 CONSTRAINT [PK_dbo.ChartOfAccounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JournalEntries]    Script Date: 11/3/2024 11:31:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JournalEntries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EntryDate] [datetime] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Reference] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.JournalEntries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JournalEntryLines]    Script Date: 11/3/2024 11:31:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JournalEntryLines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JournalEntryId] [int] NOT NULL,
	[AccountName] [nvarchar](max) NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[IsDebit] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.JournalEntryLines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/3/2024 11:31:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Password] [nvarchar](max) NULL,
	[Role] [varchar](20) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [date] NOT NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ChartOfAccounts] ON 
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (1, N'Cash', N'Asset', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (2, N'Accounts Receivable', N'Asset', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (3, N'Inventory', N'Asset', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (4, N'Prepaid Expenses', N'Asset', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (5, N'Property, Plant & Equipment', N'Asset', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (6, N'Accounts Payable', N'Liability', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (7, N'Accrued Liabilities', N'Liability', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (8, N'Notes Payable', N'Liability', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (9, N'Long-term Debt', N'Liability', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (10, N'Common Stock', N'Equity', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (11, N'Retained Earnings', N'Equity', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (12, N'Sales Revenue', N'Revenue', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (13, N'Service Revenue', N'Revenue', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (14, N'Cost of Goods Sold', N'Expense', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (15, N'Salaries Expense', N'Expense', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (16, N'Rent Expense', N'Expense', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (17, N'Utilities Expense', N'Expense', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (18, N'Advertising Expense', N'Expense', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (19, N'Interest Expense', N'Expense', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (20, N'Income Tax Expense', N'Expense', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (21, N'Test1', N'Asset', 0, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (22, N'Test', N'Equity', 0, CAST(N'2024-11-03' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (23, N'Test', N'Asset', 0, CAST(N'2024-11-03' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (24, N'Test', N'Liability', 0, CAST(N'2024-11-03' AS Date))
GO
INSERT [dbo].[ChartOfAccounts] ([Id], [AccountName], [AccountType], [IsActive], [CreatedDate]) VALUES (25, N'Test', N'Asset', 0, CAST(N'2024-11-03' AS Date))
GO
SET IDENTITY_INSERT [dbo].[ChartOfAccounts] OFF
GO
SET IDENTITY_INSERT [dbo].[JournalEntries] ON 
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (1, CAST(N'2024-11-01T18:52:16.237' AS DateTime), N'Initial Entry', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (2, CAST(N'2024-10-31T18:52:16.237' AS DateTime), N'Daily Transactions', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (3, CAST(N'2024-10-30T18:52:16.237' AS DateTime), N'Weekly Summary', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (4, CAST(N'2024-10-29T18:52:16.237' AS DateTime), N'Monthly Review', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (5, CAST(N'2024-10-28T18:52:16.237' AS DateTime), N'Year-End Closing', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (6, CAST(N'2024-10-27T18:52:16.237' AS DateTime), N'Purchase of Supplies', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (7, CAST(N'2024-10-26T18:52:16.237' AS DateTime), N'Payment of Rent', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (8, CAST(N'2024-10-25T18:52:16.237' AS DateTime), N'Sale of Goods', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (9, CAST(N'2024-10-24T18:52:16.237' AS DateTime), N'Utilities Payment', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (10, CAST(N'2024-10-23T18:52:16.237' AS DateTime), N'Advertising Expense', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (11, CAST(N'2024-10-22T18:52:16.237' AS DateTime), N'Service Revenue', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (12, CAST(N'2024-10-21T18:52:16.237' AS DateTime), N'Interest Expense', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (13, CAST(N'2024-10-20T18:52:16.237' AS DateTime), N'Tax Payment', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (14, CAST(N'2024-10-19T18:52:16.237' AS DateTime), N'Insurance Expense', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (15, CAST(N'2024-10-18T18:52:16.237' AS DateTime), N'Salary Payment', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (16, CAST(N'2024-10-17T18:52:16.237' AS DateTime), N'Equipment Purchase', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (17, CAST(N'2024-10-16T18:52:16.237' AS DateTime), N'Repair and Maintenance', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (18, CAST(N'2024-10-15T18:52:16.237' AS DateTime), N'Loan Repayment', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (19, CAST(N'2024-10-14T18:52:16.237' AS DateTime), N'Dividend Payment', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (20, CAST(N'2024-10-13T18:52:16.237' AS DateTime), N'Miscellaneous Expense', NULL)
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (21, CAST(N'2024-11-03T00:00:00.000' AS DateTime), N'test', N'1')
GO
INSERT [dbo].[JournalEntries] ([Id], [EntryDate], [Description], [Reference]) VALUES (22, CAST(N'2024-11-01T00:00:00.000' AS DateTime), N'rpov2', NULL)
GO
SET IDENTITY_INSERT [dbo].[JournalEntries] OFF
GO
SET IDENTITY_INSERT [dbo].[JournalEntryLines] ON 
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (1, 1, N'Cash', CAST(1000.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (2, 1, N'Sales Revenue', CAST(1000.00 AS Decimal(18, 2)), 0)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (3, 2, N'Accounts Receivable', CAST(500.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (4, 2, N'Sales Revenue', CAST(500.00 AS Decimal(18, 2)), 0)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (5, 3, N'Inventory', CAST(200.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (6, 3, N'Accounts Payable', CAST(200.00 AS Decimal(18, 2)), 0)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (7, 4, N'Utilities Expense', CAST(150.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (8, 4, N'Cash', CAST(150.00 AS Decimal(18, 2)), 0)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (9, 5, N'Salaries Expense', CAST(3000.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (10, 5, N'Cash', CAST(3000.00 AS Decimal(18, 2)), 0)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (11, 6, N'Office Supplies', CAST(500.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (12, 6, N'Cash', CAST(500.00 AS Decimal(18, 2)), 0)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (13, 7, N'Rent Expense', CAST(1200.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (14, 7, N'Cash', CAST(1200.00 AS Decimal(18, 2)), 0)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (15, 8, N'Sales Revenue', CAST(800.00 AS Decimal(18, 2)), 0)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (16, 8, N'Cash', CAST(800.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (17, 9, N'Utilities Payable', CAST(100.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (18, 9, N'Cash', CAST(100.00 AS Decimal(18, 2)), 0)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (19, 10, N'Advertising Expense', CAST(250.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (20, 10, N'Accounts Payable', CAST(250.00 AS Decimal(18, 2)), 0)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (37, 22, N'a', CAST(1.00 AS Decimal(18, 2)), 0)
GO
INSERT [dbo].[JournalEntryLines] ([Id], [JournalEntryId], [AccountName], [Amount], [IsDebit]) VALUES (38, 22, N'b', CAST(1.00 AS Decimal(18, 2)), 0)
GO
SET IDENTITY_INSERT [dbo].[JournalEntryLines] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [Name], [Password], [Role], [IsActive], [CreatedDate]) VALUES (1, N'admin', N'123', N'admin', 1, CAST(N'2024-11-02' AS Date))
GO
INSERT [dbo].[Users] ([Id], [Name], [Password], [Role], [IsActive], [CreatedDate]) VALUES (2, N'user', N'123', N'user', 1, CAST(N'2024-11-02' AS Date))
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_JournalEntryId]    Script Date: 11/3/2024 11:31:43 PM ******/
CREATE NONCLUSTERED INDEX [IX_JournalEntryId] ON [dbo].[JournalEntryLines]
(
	[JournalEntryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChartOfAccounts] ADD  CONSTRAINT [DF_ChartOfAccounts_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[JournalEntryLines]  WITH CHECK ADD  CONSTRAINT [FK_dbo.JournalEntryLines_dbo.JournalEntries_JournalEntryId] FOREIGN KEY([JournalEntryId])
REFERENCES [dbo].[JournalEntries] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[JournalEntryLines] CHECK CONSTRAINT [FK_dbo.JournalEntryLines_dbo.JournalEntries_JournalEntryId]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteChartOfAccounts]    Script Date: 11/3/2024 11:31:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_DeleteChartOfAccounts]
    @Id INT   
AS
BEGIN   
    UPDATE [dbo].[ChartOfAccounts]
    SET        
        IsActive = 0
    WHERE 
        Id = @Id;    
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteJournalEntry]    Script Date: 11/3/2024 11:31:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteJournalEntry]
    @Id INT 
AS
BEGIN
    DELETE FROM JournalEntryLines WHERE JournalEntryId = @Id;
	 DELETE FROM JournalEntryLines WHERE JournalEntryId = @Id;
  
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllChartOfAccounts]    Script Date: 11/3/2024 11:31:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_GetAllChartOfAccounts]
As
Select * from [dbo].[ChartOfAccounts] Where IsActive=1;
GO
/****** Object:  StoredProcedure [dbo].[sp_GetChartOfAccountById]    Script Date: 11/3/2024 11:31:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_GetChartOfAccountById]
     @Id INT
AS
BEGIN
    SELECT 
       *
    FROM 
        [dbo].[ChartOfAccounts]
    WHERE 
        Id =  @Id ;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetChartOfAccountsReports]    Script Date: 11/3/2024 11:31:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetChartOfAccountsReports]
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL,
    @AccountType NVARCHAR(50) = NULL
AS
BEGIN
    SELECT Id, AccountName, AccountType, IsActive, CreatedDate
    FROM [dbo].[ChartOfAccounts]
    WHERE 
        (@StartDate IS NULL OR FORMAT(CreatedDate,'yyyy-MM-dd') >=FORMAT(@StartDate,'yyyy-MM-dd') )
        AND (@EndDate IS NULL OR FORMAT(CreatedDate,'yyyy-MM-dd')  <=FORMAT(@EndDate,'yyyy-MM-dd') )
        AND (@AccountType IS NULL OR AccountType = @AccountType);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetJournalEntriesReport]    Script Date: 11/3/2024 11:31:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_GetJournalEntriesReport]
 @StartDate DATETIME = NULL,
 @EndDate DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        je.Id AS JournalEntryId,
        je.EntryDate,
        je.Description,
        je.Reference,
        jel.Id AS LineId,
        jel.AccountName,
        CASE WHEN jel.IsDebit = 1 THEN jel.Amount ELSE 0 END AS DebitAmount,
        CASE WHEN jel.IsDebit = 0 THEN jel.Amount ELSE 0 END AS CreditAmount
    FROM 
        dbo.JournalEntries je
    LEFT JOIN 
        dbo.JournalEntryLines jel ON je.Id = jel.JournalEntryId

		Where jel.id is not null
		AND  (@StartDate IS NULL OR FORMAT(je.EntryDate,'yyyy-MM-dd') >=FORMAT(@StartDate,'yyyy-MM-dd') )
        AND (@EndDate IS NULL OR FORMAT(je.EntryDate,'yyyy-MM-dd')  <=FORMAT(@EndDate,'yyyy-MM-dd') )
    ORDER BY 
        je.EntryDate, je.Id, jel.Id;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_GetJournalEntriesWithLines]    Script Date: 11/3/2024 11:31:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetJournalEntriesWithLines]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        je.Id AS JournalEntryId,
        je.EntryDate,
        je.Description,
        je.Reference,
        jel.Id AS LineId,
        jel.AccountName,
        CASE WHEN jel.IsDebit = 1 THEN jel.Amount ELSE 0 END AS DebitAmount,
        CASE WHEN jel.IsDebit = 0 THEN jel.Amount ELSE 0 END AS CreditAmount
    FROM 
        dbo.JournalEntries je
    LEFT JOIN 
        dbo.JournalEntryLines jel ON je.Id = jel.JournalEntryId

		Where jel.id is not null
    ORDER BY 
        je.EntryDate, je.Id, jel.Id;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_GetJournalEntryById]    Script Date: 11/3/2024 11:31:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetJournalEntryById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Retrieve the journal entry and its associated lines
   SELECT 
        je.Id AS JournalEntryId,
        je.EntryDate,
        je.Description,
        je.Reference,
        jel.Id AS LineId,
        jel.AccountName,
        CASE WHEN jel.IsDebit = 1 THEN jel.Amount ELSE 0 END AS DebitAmount,
        CASE WHEN jel.IsDebit = 0 THEN jel.Amount ELSE 0 END AS CreditAmount
    FROM 
        dbo.JournalEntries je
    LEFT JOIN 
        dbo.JournalEntryLines jel ON je.Id = jel.JournalEntryId
    WHERE  jel.Id  is not null and
        je.Id = @Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertChartOfAccounts]    Script Date: 11/3/2024 11:31:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[sp_InsertChartOfAccounts](
@AccountName nvarchar(max),
@AccountType nvarchar(max),
@IsActive bit
)
AS
BEGIN
INSERT INTO [dbo].[ChartOfAccounts]
           ([AccountName]
           ,[AccountType]
           ,[IsActive])
     VALUES
           (@AccountName,@AccountType,@IsActive);

END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertJournalEntry]    Script Date: 11/3/2024 11:31:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertJournalEntry]
    @EntryDate DATE,
    @Description NVARCHAR(255),
    @Reference NVARCHAR(255) =null,
    @Lines dbo.JournalEntryLineType READONLY  -- Ensure this is defined correctly
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @JournalEntryId INT;

    -- Insert the main journal entry into the JournalEntries table
    INSERT INTO JournalEntries (EntryDate, Description, Reference)
    VALUES (@EntryDate, @Description, @Reference);

    -- Get the generated JournalEntryId
    SET @JournalEntryId = SCOPE_IDENTITY();

    -- Insert the lines from the @Lines table parameter
    INSERT INTO JournalEntryLines (JournalEntryId, AccountName, Amount, IsDebit)
    SELECT @JournalEntryId, AccountName, Amount, IsDebit
    FROM @Lines;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateChartOfAccounts]    Script Date: 11/3/2024 11:31:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateChartOfAccounts]
    @Id INT,                -- The ID of the account to update
    @AccountName NVARCHAR(MAX),    -- New account name
    @AccountType NVARCHAR(MAX),    -- New account type
    @IsActive BIT                  -- New active status
AS
BEGIN
    -- Update the ChartOfAccounts table
    UPDATE [dbo].[ChartOfAccounts]
    SET 
        AccountName = @AccountName,
        AccountType = @AccountType,
        IsActive = @IsActive
    WHERE 
        Id = @Id;     -- Identify the record to update based on AccountId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateJournalEntry]    Script Date: 11/3/2024 11:31:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateJournalEntry]
    @JournalEntryId INT,
    @EntryDate DATE,
    @Description NVARCHAR(MAX),
    @Reference NVARCHAR(MAX)=null,
    @Lines dbo.JournalEntryLineType READONLY -- Assuming you have a structured type defined
AS
BEGIN
    -- Update the main journal entry
    UPDATE JournalEntries
    SET 
        EntryDate = @EntryDate,
        Description = @Description,
        Reference = @Reference
    WHERE Id = @JournalEntryId;

    -- Clear existing lines for the journal entry if needed
    DELETE FROM JournalEntryLines WHERE JournalEntryId = @JournalEntryId;

    -- Insert new lines from the @Lines parameter
    INSERT INTO JournalEntryLines (JournalEntryId, AccountName, Amount, IsDebit)
    SELECT 
        @JournalEntryId,
        AccountName,
        Amount,
        IsDebit
    FROM @Lines;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_User]    Script Date: 11/3/2024 11:31:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_User]
    @Name NVARCHAR(50),  
    @Password NVARCHAR(50)
AS
BEGIN
    SELECT * FROM [dbo].[Users]   
    WHERE [Name]=@Name and [Password]=@Password and [IsActive]=1;    	           
END
GO
USE [master]
GO
ALTER DATABASE [FinancialManagementSystem] SET  READ_WRITE 
GO
