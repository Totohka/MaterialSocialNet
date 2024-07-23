IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ChatRooms] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [Description] nvarchar(100) NOT NULL,
    [LastMessage] nvarchar(500) NOT NULL,
    CONSTRAINT [PK_ChatRooms] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SettingNotifications] (
    [Id] int NOT NULL IDENTITY,
    [NewMessage] bit NOT NULL,
    [NewSubscibe] bit NOT NULL,
    [NewPosts] bit NOT NULL,
    CONSTRAINT [PK_SettingNotifications] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SettingPrivacies] (
    [Id] int NOT NULL IDENTITY,
    [ShowPost] int NOT NULL,
    [InvateChats] int NOT NULL,
    [ShowDateBirthday] int NOT NULL,
    CONSTRAINT [PK_SettingPrivacies] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [SettingAccountId] int NOT NULL,
    [FirstName] nvarchar(25) NOT NULL,
    [LastName] nvarchar(25) NOT NULL,
    [DateBirthday] datetime2 NOT NULL,
    [City] nvarchar(25) NOT NULL,
    [Country] nvarchar(25) NOT NULL,
    [Phone] nvarchar(11) NOT NULL,
    [Status] nvarchar(100) NOT NULL,
    [Avatar] nvarchar(100) NOT NULL,
    [SettingNotificationId] int NOT NULL,
    [SettingPrivacyId] int NOT NULL,
    [Email] nvarchar(182) NOT NULL,
    [HashPassword] nvarchar(182) NOT NULL,
    [PathGallery] nvarchar(182) NOT NULL,
    [AccessToken] nvarchar(200) NOT NULL,
    [RefreshToken] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Users_SettingNotifications_SettingNotificationId] FOREIGN KEY ([SettingNotificationId]) REFERENCES [SettingNotifications] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Users_SettingPrivacies_SettingPrivacyId] FOREIGN KEY ([SettingPrivacyId]) REFERENCES [SettingPrivacies] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ChatRoomUsers] (
    [Id] int NOT NULL IDENTITY,
    [ChatRoomId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_ChatRoomUsers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ChatRoomUsers_ChatRooms_ChatRoomId] FOREIGN KEY ([ChatRoomId]) REFERENCES [ChatRooms] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ChatRoomUsers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Posts] (
    [Id] int NOT NULL IDENTITY,
    [Image] nvarchar(30) NOT NULL,
    [Title] nvarchar(100) NOT NULL,
    [DateCreate] datetime2 NOT NULL,
    [UserId] int NOT NULL,
    [Text] nvarchar(max) NOT NULL,
    [Tags] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Posts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserFriends] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [UserFriendId] int NOT NULL,
    CONSTRAINT [PK_UserFriends] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserFriends_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Messages] (
    [Id] int NOT NULL IDENTITY,
    [Msg] nvarchar(500) NOT NULL,
    [DateSend] datetime2 NOT NULL,
    [ChatRoomUserId] int NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Messages_ChatRoomUsers_ChatRoomUserId] FOREIGN KEY ([ChatRoomUserId]) REFERENCES [ChatRoomUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_ChatRoomUsers_ChatRoomId] ON [ChatRoomUsers] ([ChatRoomId]);
GO

CREATE INDEX [IX_ChatRoomUsers_UserId] ON [ChatRoomUsers] ([UserId]);
GO

CREATE INDEX [IX_Messages_ChatRoomUserId] ON [Messages] ([ChatRoomUserId]);
GO

CREATE INDEX [IX_Posts_UserId] ON [Posts] ([UserId]);
GO

CREATE INDEX [IX_UserFriends_UserId] ON [UserFriends] ([UserId]);
GO

CREATE UNIQUE INDEX [IX_Users_SettingNotificationId] ON [Users] ([SettingNotificationId]);
GO

CREATE UNIQUE INDEX [IX_Users_SettingPrivacyId] ON [Users] ([SettingPrivacyId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231114060010_Init', N'7.0.13');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'SettingAccountId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Users] DROP COLUMN [SettingAccountId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231114075209_DropSettingAccountId', N'7.0.13');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP INDEX [IX_Users_SettingNotificationId] ON [Users];
GO

DROP INDEX [IX_Users_SettingPrivacyId] ON [Users];
GO

CREATE INDEX [IX_Users_SettingNotificationId] ON [Users] ([SettingNotificationId]);
GO

CREATE INDEX [IX_Users_SettingPrivacyId] ON [Users] ([SettingPrivacyId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231115022616_ChangeLinkTables', N'7.0.13');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'HashPassword');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Users] DROP COLUMN [HashPassword];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'RefreshToken');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Users] DROP COLUMN [RefreshToken];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231118120908_UpdateUserModel', N'7.0.13');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ChatRooms] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [Description] nvarchar(100) NOT NULL,
    [LastMessage] nvarchar(500) NOT NULL,
    CONSTRAINT [PK_ChatRooms] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SettingNotifications] (
    [Id] int NOT NULL IDENTITY,
    [NewMessage] bit NOT NULL,
    [NewSubscibe] bit NOT NULL,
    [NewPosts] bit NOT NULL,
    CONSTRAINT [PK_SettingNotifications] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SettingPrivacies] (
    [Id] int NOT NULL IDENTITY,
    [ShowPost] int NOT NULL,
    [InvateChats] int NOT NULL,
    [ShowDateBirthday] int NOT NULL,
    CONSTRAINT [PK_SettingPrivacies] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(25) NOT NULL,
    [LastName] nvarchar(25) NOT NULL,
    [DateBirthday] datetime2 NOT NULL,
    [City] nvarchar(25) NOT NULL,
    [Country] nvarchar(25) NOT NULL,
    [Phone] nvarchar(11) NOT NULL,
    [Status] nvarchar(100) NOT NULL,
    [Avatar] nvarchar(100) NOT NULL,
    [SettingNotificationId] int NOT NULL,
    [SettingPrivacyId] int NOT NULL,
    [Email] nvarchar(182) NOT NULL,
    [PathGallery] nvarchar(182) NOT NULL,
    [AccessToken] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Users_SettingNotifications_SettingNotificationId] FOREIGN KEY ([SettingNotificationId]) REFERENCES [SettingNotifications] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Users_SettingPrivacies_SettingPrivacyId] FOREIGN KEY ([SettingPrivacyId]) REFERENCES [SettingPrivacies] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ChatRoomUsers] (
    [Id] int NOT NULL IDENTITY,
    [ChatRoomId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_ChatRoomUsers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ChatRoomUsers_ChatRooms_ChatRoomId] FOREIGN KEY ([ChatRoomId]) REFERENCES [ChatRooms] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ChatRoomUsers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Posts] (
    [Id] int NOT NULL IDENTITY,
    [Image] nvarchar(30) NOT NULL,
    [Title] nvarchar(100) NOT NULL,
    [DateCreate] datetime2 NOT NULL,
    [UserId] int NOT NULL,
    [Text] nvarchar(max) NOT NULL,
    [Tags] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Posts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserFriends] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [UserFriendId] int NOT NULL,
    CONSTRAINT [PK_UserFriends] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserFriends_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Messages] (
    [Id] int NOT NULL IDENTITY,
    [Msg] nvarchar(500) NOT NULL,
    [DateSend] datetime2 NOT NULL,
    [ChatRoomUserId] int NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Messages_ChatRoomUsers_ChatRoomUserId] FOREIGN KEY ([ChatRoomUserId]) REFERENCES [ChatRoomUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Comments] (
    [Id] int NOT NULL IDENTITY,
    [Text] nvarchar(max) NOT NULL,
    [UserId] int NOT NULL,
    [PostId] int NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comments_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Reactions] (
    [Id] int NOT NULL IDENTITY,
    [Like] bit NOT NULL,
    [UserId] int NOT NULL,
    [PostId] int NOT NULL,
    CONSTRAINT [PK_Reactions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Reactions_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reactions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_ChatRoomUsers_ChatRoomId] ON [ChatRoomUsers] ([ChatRoomId]);
GO

CREATE INDEX [IX_ChatRoomUsers_UserId] ON [ChatRoomUsers] ([UserId]);
GO

CREATE INDEX [IX_Comments_PostId] ON [Comments] ([PostId]);
GO

CREATE INDEX [IX_Comments_UserId] ON [Comments] ([UserId]);
GO

CREATE INDEX [IX_Messages_ChatRoomUserId] ON [Messages] ([ChatRoomUserId]);
GO

CREATE INDEX [IX_Posts_UserId] ON [Posts] ([UserId]);
GO

CREATE INDEX [IX_Reactions_PostId] ON [Reactions] ([PostId]);
GO

CREATE INDEX [IX_Reactions_UserId] ON [Reactions] ([UserId]);
GO

CREATE INDEX [IX_UserFriends_UserId] ON [UserFriends] ([UserId]);
GO

CREATE INDEX [IX_Users_SettingNotificationId] ON [Users] ([SettingNotificationId]);
GO

CREATE INDEX [IX_Users_SettingPrivacyId] ON [Users] ([SettingPrivacyId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240213174753_addComAndLike', N'7.0.13');
GO

COMMIT;
GO

