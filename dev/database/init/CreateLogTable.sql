use MclarenApp

/*==============================================================*/
/* Table: LogLogin                                              */
/*==============================================================*/
create table LogLogin (
   Id                   int                  identity,
   LogLevel             varchar(50)          not null default '',
   UserId               int                  not null default 0,
   MemberId             int                  not null default 0,
   Url                  nvarchar(500)        not null default '',
   IP                   nvarchar(20)         not null default '',
   LoginTime            datetime             not null default getdate(),
   ExitTime             datetime             null,
   SourceId             int                  not null default 0,
   SessionId            nvarchar(50)         not null default '',
   UniqueCookie         varchar(200)         not null default '',
   IsSuccess            bit                  not null default 0,
   SystemName           nvarchar(50)         not null default '',
   BrowseName           nvarchar(50)         not null default '',
   Remark               nvarchar(200)        not null default '',
   constraint PK_LOGLOGIN primary key (Id)
)
on Log
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Զ����',
   'user', @CurrentUser, 'table', 'LogLogin', 'column', 'Id'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��־����',
   'user', @CurrentUser, 'table', 'LogLogin', 'column', 'LogLevel'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�û����',
   'user', @CurrentUser, 'table', 'LogLogin', 'column', 'UserId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��Ա���',
   'user', @CurrentUser, 'table', 'LogLogin', 'column', 'MemberId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��¼��ַ',
   'user', @CurrentUser, 'table', 'LogLogin', 'column', 'Url'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��¼IP',
   'user', @CurrentUser, 'table', 'LogLogin', 'column', 'IP'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��¼ʱ��',
   'user', @CurrentUser, 'table', 'LogLogin', 'column', 'LoginTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�˳�ʱ��',
   'user', @CurrentUser, 'table', 'LogLogin', 'column', 'ExitTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��Դ��ʶ',
   'user', @CurrentUser, 'table', 'LogLogin', 'column', 'SourceId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Ự���',
   'user', @CurrentUser, 'table', 'LogLogin', 'column', 'SessionId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ΨһCookie��ʶ',
   'user', @CurrentUser, 'table', 'LogLogin', 'column', 'UniqueCookie'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Ƿ�ɹ���¼',
   'user', @CurrentUser, 'table', 'LogLogin', 'column', 'IsSuccess'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ϵͳ����',
   'user', @CurrentUser, 'table', 'LogLogin', 'column', 'SystemName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���������',
   'user', @CurrentUser, 'table', 'LogLogin', 'column', 'BrowseName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', @CurrentUser, 'table', 'LogLogin', 'column', 'Remark'
go

/*==============================================================*/
/* Index: Index_1                                               */
/*==============================================================*/
create index Index_1 on LogLogin (
UserId ASC
)
go

/*==============================================================*/
/* Index: Index_2                                               */
/*==============================================================*/
create index Index_2 on LogLogin (
MemberId ASC
)
go

/*==============================================================*/
/* Index: Index_3                                               */
/*==============================================================*/
create index Index_3 on LogLogin (
LoginTime ASC
)
go

/*==============================================================*/
/* Index: Index_4                                               */
/*==============================================================*/
create index Index_4 on LogLogin (
LogLevel ASC
)
go



/*==============================================================*/
/* Table: LogException                                          */
/*==============================================================*/
create table LogException (
   Id                   int                  identity,
   Name                 nvarchar(50)         not null default '',
   LogLevel             varchar(50)          null default '1',
   MethodName           nvarchar(200)        not null default '',
   Message              nvarchar(500)        not null default '',
   StackTrace           nvarchar(max)        not null default '',
   Description          nvarchar(500)        not null default '',
   CreateTime           datetime             not null default getdate(),
   UserId               int                  not null default 0,
   DataId               int                  not null default 0,
   Remark               nvarchar(200)        not null default '',
   constraint PK_LOGEXCEPTION primary key (Id)
)
on Log
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Զ����',
   'user', @CurrentUser, 'table', 'LogException', 'column', 'Id'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��־����',
   'user', @CurrentUser, 'table', 'LogException', 'column', 'Name'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��־����',
   'user', @CurrentUser, 'table', 'LogException', 'column', 'LogLevel'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�༰��������',
   'user', @CurrentUser, 'table', 'LogException', 'column', 'MethodName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�쳣��Ϣ',
   'user', @CurrentUser, 'table', 'LogException', 'column', 'Message'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�쳣��ջ',
   'user', @CurrentUser, 'table', 'LogException', 'column', 'StackTrace'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�쳣����',
   'user', @CurrentUser, 'table', 'LogException', 'column', 'Description'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', @CurrentUser, 'table', 'LogException', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�û����',
   'user', @CurrentUser, 'table', 'LogException', 'column', 'UserId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���ݱ��',
   'user', @CurrentUser, 'table', 'LogException', 'column', 'DataId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', @CurrentUser, 'table', 'LogException', 'column', 'Remark'
go

/*==============================================================*/
/* Index: Index_1                                               */
/*==============================================================*/
create index Index_1 on LogException (
Name ASC
)
go

/*==============================================================*/
/* Index: Index_2                                               */
/*==============================================================*/
create index Index_2 on LogException (
CreateTime ASC
)
go

/*==============================================================*/
/* Index: Index_3                                               */
/*==============================================================*/
create index Index_3 on LogException (
LogLevel ASC
)
go


/*==============================================================*/
/* Table: LogRun                                                */
/*==============================================================*/
create table LogRun (
   Id                   int                  identity,
   MethodName           nvarchar(200)        not null default '',
   LogLevel             varchar(50)          not null default '',
   Name                 nvarchar(50)         not null default '',
   UserId               int                  not null default 0,
   MemberId             int                  not null default 0,
   DataId               int                  not null default 0,
   Description          varchar(8000)        not null default '',
   Result               varchar(1000)        not null default '',
   StartTime            datetime             not null default getdate(),
   EndTime              datetime             not null default getdate(),
   Timestamp            int                  not null default 0,
   IP                   nvarchar(20)         not null default '',
   Url                  varchar(500)         not null default '',
   Remark               varchar(1000)        not null default '',
   constraint PK_LOGRUN primary key (Id)
)
on Log
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Զ����',
   'user', @CurrentUser, 'table', 'LogRun', 'column', 'Id'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����������',
   'user', @CurrentUser, 'table', 'LogRun', 'column', 'MethodName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��־����',
   'user', @CurrentUser, 'table', 'LogRun', 'column', 'LogLevel'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��־����',
   'user', @CurrentUser, 'table', 'LogRun', 'column', 'Name'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�û����',
   'user', @CurrentUser, 'table', 'LogRun', 'column', 'UserId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��Ա���',
   'user', @CurrentUser, 'table', 'LogRun', 'column', 'MemberId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���ݱ��',
   'user', @CurrentUser, 'table', 'LogRun', 'column', 'DataId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', @CurrentUser, 'table', 'LogRun', 'column', 'Description'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���',
   'user', @CurrentUser, 'table', 'LogRun', 'column', 'Result'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ʼʱ��',
   'user', @CurrentUser, 'table', 'LogRun', 'column', 'StartTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', @CurrentUser, 'table', 'LogRun', 'column', 'EndTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ʱ���(����),���ݔ���ĳ�ʽ���ӵ����Iӛ��¼������ĕr�g,�Ժ���ӛ.',
   'user', @CurrentUser, 'table', 'LogRun', 'column', 'Timestamp'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'IP',
   'user', @CurrentUser, 'table', 'LogRun', 'column', 'IP'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����·��',
   'user', @CurrentUser, 'table', 'LogRun', 'column', 'Url'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', @CurrentUser, 'table', 'LogRun', 'column', 'Remark'
go

/*==============================================================*/
/* Index: Index_1                                               */
/*==============================================================*/
create index Index_1 on LogRun (
Name ASC
)
go

/*==============================================================*/
/* Index: Index_2                                               */
/*==============================================================*/
create index Index_2 on LogRun (
StartTime ASC
)
go

/*==============================================================*/
/* Index: Index_3                                               */
/*==============================================================*/
create index Index_3 on LogRun (
LogLevel ASC
)
go


/*==============================================================*/
/* Table: LogOperation                                          */
/*==============================================================*/
create table LogOperation (
   Id                   int                  identity,
   LogLevel             varchar(50)          not null default '',
   UserId               int                  not null default 0,
   MemberId             int                  not null default 0,
   OperationType        int                  not null default 0,
   Url                  varchar(500)         not null default '',
   Name                 nvarchar(50)         not null default '',
   DataId               int                  not null default 0,
   TableName            nvarchar(200)        not null default '',
   Description          nvarchar(2000)       not null default '',
   Result               nvarchar(200)        not null default '',
   IP                   nvarchar(20)         not null default '',
   CreateTime           datetime             not null default getdate(),
   Remark               nvarchar(200)        not null default '',
   UniqueCookie         varchar(200)         not null default '',
   constraint PK_LOGOPERATION primary key (Id)
)
on Log
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Զ����',
   'user', @CurrentUser, 'table', 'LogOperation', 'column', 'Id'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��־����',
   'user', @CurrentUser, 'table', 'LogOperation', 'column', 'LogLevel'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�û�������',
   'user', @CurrentUser, 'table', 'LogOperation', 'column', 'UserId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��Ա������',
   'user', @CurrentUser, 'table', 'LogOperation', 'column', 'MemberId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������:1��ʾ������2��ʾ�༭��3��ʾ��ѯ��4��ʾɾ����5��ʾ����',
   'user', @CurrentUser, 'table', 'LogOperation', 'column', 'OperationType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����·��',
   'user', @CurrentUser, 'table', 'LogOperation', 'column', 'Url'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��־����',
   'user', @CurrentUser, 'table', 'LogOperation', 'column', 'Name'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���ݱ��',
   'user', @CurrentUser, 'table', 'LogOperation', 'column', 'DataId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���ݿ����',
   'user', @CurrentUser, 'table', 'LogOperation', 'column', 'TableName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', @CurrentUser, 'table', 'LogOperation', 'column', 'Description'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���',
   'user', @CurrentUser, 'table', 'LogOperation', 'column', 'Result'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����IP',
   'user', @CurrentUser, 'table', 'LogOperation', 'column', 'IP'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', @CurrentUser, 'table', 'LogOperation', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', @CurrentUser, 'table', 'LogOperation', 'column', 'Remark'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Ψһcookie',
   'user', @CurrentUser, 'table', 'LogOperation', 'column', 'UniqueCookie'
go

/*==============================================================*/
/* Index: Index_1                                               */
/*==============================================================*/
create index Index_1 on LogOperation (
CreateTime ASC
)
go

/*==============================================================*/
/* Index: Index_2                                               */
/*==============================================================*/
create index Index_2 on LogOperation (
UserId ASC
)
go

/*==============================================================*/
/* Index: Index_3                                               */
/*==============================================================*/
create index Index_3 on LogOperation (
Name ASC
)
go


/*==============================================================*/
/* Table: LogInterface                                          */
/*==============================================================*/
create table LogInterface (
   Id                   int                  identity,
   LogLevel             varchar(50)          not null default '',
   Name                 nvarchar(50)         not null default '',
   MethodName           nvarchar(200)        not null default '',
   IP                   nvarchar(20)         not null default '',
   UserId               int                  not null default 0,
   StartTime            datetime             not null default getdate(),
   EndTime              datetime             not null default getdate(),
   KeyParameter         nvarchar(100)        not null default '',
   ParaDesc             nvarchar(max)        not null default '',
   ReturnCode           nvarchar(50)         not null default '',
   ReturnDescription    nvarchar(200)        not null default '',
   ReturnData           nvarchar(max)        not null default '',
   IsSuccess            bit                  not null default 1,
   AccessToken          varchar(200)         not null default '',
   Timestamp            int                  not null default 0,
   constraint PK_LOGINTERFACE primary key (Id)
)
on Log
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Զ����',
   'user', @CurrentUser, 'table', 'LogInterface', 'column', 'Id'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��־����',
   'user', @CurrentUser, 'table', 'LogInterface', 'column', 'LogLevel'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��־����',
   'user', @CurrentUser, 'table', 'LogInterface', 'column', 'Name'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����������',
   'user', @CurrentUser, 'table', 'LogInterface', 'column', 'MethodName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'IP',
   'user', @CurrentUser, 'table', 'LogInterface', 'column', 'IP'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�����߱��',
   'user', @CurrentUser, 'table', 'LogInterface', 'column', 'UserId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ʼʱ��',
   'user', @CurrentUser, 'table', 'LogInterface', 'column', 'StartTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', @CurrentUser, 'table', 'LogInterface', 'column', 'EndTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�ؼ�����',
   'user', @CurrentUser, 'table', 'LogInterface', 'column', 'KeyParameter'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 'LogInterface', 'column', 'ParaDesc'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���ش���',
   'user', @CurrentUser, 'table', 'LogInterface', 'column', 'ReturnCode'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 'LogInterface', 'column', 'ReturnDescription'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 'LogInterface', 'column', 'ReturnData'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Ƿ�ɹ�����',
   'user', @CurrentUser, 'table', 'LogInterface', 'column', 'IsSuccess'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 'LogInterface', 'column', 'AccessToken'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ִ�п�ʼ��ʹ�õ�ʱ��(����)',
   'user', @CurrentUser, 'table', 'LogInterface', 'column', 'Timestamp'
go

/*==============================================================*/
/* Index: Index_1                                               */
/*==============================================================*/
create index Index_1 on LogInterface (
Name ASC
)
go

/*==============================================================*/
/* Index: Index_2                                               */
/*==============================================================*/
create index Index_2 on LogInterface (
StartTime ASC
)
go

/*==============================================================*/
/* Index: Index_3                                               */
/*==============================================================*/
create index Index_3 on LogInterface (
UserId ASC
)
go

/*==============================================================*/
/* Index: Index_4                                               */
/*==============================================================*/
create index Index_4 on LogInterface (
LogLevel ASC
)
go


/*==============================================================*/
/* Table: LogTrack                                              */
/*==============================================================*/
create table LogTrack (
   Id                   int                  identity,
   SystemSign           varchar(50)          not null default '',
   HttpMethod           nvarchar(20)         not null default '',
   PagePath             varchar(500)         not null default '',
   PageUrl              varchar(500)         not null default '',
   BeforeUrl            varchar(500)         not null default '',
   IP                   varchar(20)          not null default '',
   CreateTime           datetime             not null default getdate(),
   RefreshTime          datetime             not null default getdate(),
   SourceId             int                  not null default 0,
   UserId               int                  not null default 0,
   MemberId             int                  not null default 0,
   OpenId               varchar(100)         not null default '',
   SessionId            nvarchar(50)         not null default '',
   BrowserName          nvarchar(100)        not null default '',
   OSName               nvarchar(100)        not null default '',
   SearchName           nvarchar(50)         not null default '',
   SearchKey            nvarchar(200)        not null default '',
   CookiesSign          nvarchar(100)        not null default '',
   ModuleName           nvarchar(100)        not null default '',
   DataId               int                  not null default 0,
   DataType             int                  not null default 0,
   ShareSignId          int                  not null default 0,
   DeviceName           varchar(100)         not null default '',
   Remark               nvarchar(200)        not null default '',
   GUID                 varchar(50)          not null default '',
   constraint PK_LOGTRACK primary key (Id)
)
on Track
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Զ����',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'Id'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ϵͳ��ʶ',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'SystemSign'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'HttpMethod'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ҳ��',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'PagePath'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����Url',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'PageUrl'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ǰһUrl',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'BeforeUrl'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'IP',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'IP'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ˢ��ʱ��',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'RefreshTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��Դ��ʶ',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'SourceId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�û����',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'UserId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��Ա���',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'MemberId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��˿���',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'OpenId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Ự���',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'SessionId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�����',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'BrowserName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ϵͳ',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'OSName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '������������',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'SearchName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�����ؼ���',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'SearchKey'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Cookies��ʶ',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'CookiesSign'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ģ������',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'ModuleName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���ݱ��',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'DataId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������:1��ʾ��Ϣ���,2��ʾ����,3��ʾ�ʾ���,4��ʾ��Ʒ���,5��ʾ��Ʒ���',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'DataType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�����ʶ���',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'ShareSignId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�豸����',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'DeviceName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'Remark'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'GUID',
   'user', @CurrentUser, 'table', 'LogTrack', 'column', 'GUID'
go

/*==============================================================*/
/* Index: Index_1                                               */
/*==============================================================*/
create index Index_1 on LogTrack (
CreateTime ASC
)
go

/*==============================================================*/
/* Index: Index_2                                               */
/*==============================================================*/
create index Index_2 on LogTrack (
DataId ASC
)
go

/*==============================================================*/
/* Index: Index_3                                               */
/*==============================================================*/
create index Index_3 on LogTrack (
DataType ASC
)
go

/*==============================================================*/
/* Index: Index_4                                               */
/*==============================================================*/
create index Index_4 on LogTrack (
MemberId ASC
)
go

/*==============================================================*/
/* Index: Index_5                                               */
/*==============================================================*/
create index Index_5 on LogTrack (
UserId ASC
)
go
