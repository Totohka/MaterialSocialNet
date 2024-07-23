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

CREATE TABLE [TypeReaction] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(20) NOT NULL,
    [Description] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_TypeReaction] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(25) NOT NULL,
    [LastName] nvarchar(25) NOT NULL,
    [Password] nvarchar(100) NOT NULL,
    [DateBirthday] datetime2 NOT NULL,
    [City] nvarchar(25) NOT NULL,
    [Country] nvarchar(25) NOT NULL,
    [Phone] nvarchar(11) NOT NULL,
    [Status] nvarchar(100) NOT NULL,
    [SettingNotificationId] int NOT NULL,
    [SettingPrivacyId] int NOT NULL,
    [Email] nvarchar(182) NOT NULL,
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

CREATE TABLE [Messages] (
    [Id] int NOT NULL IDENTITY,
    [Msg] nvarchar(500) NOT NULL,
    [DateSend] datetime2 NOT NULL,
    [ChatRoomId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Messages_ChatRooms_ChatRoomId] FOREIGN KEY ([ChatRoomId]) REFERENCES [ChatRooms] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Messages_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
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

CREATE TABLE [Reactions] (
    [Id] int NOT NULL IDENTITY,
    [TypeReactionId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Reactions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Reactions_TypeReaction_TypeReactionId] FOREIGN KEY ([TypeReactionId]) REFERENCES [TypeReaction] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reactions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
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

CREATE TABLE [Comments] (
    [Id] int NOT NULL IDENTITY,
    [Text] nvarchar(300) NOT NULL,
    [UserId] int NOT NULL,
    [PostId] int NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comments_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ReactionMessages] (
    [Id] int NOT NULL IDENTITY,
    [ReactionId] int NOT NULL,
    [MessageId] int NOT NULL,
    CONSTRAINT [PK_ReactionMessages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ReactionMessages_Messages_MessageId] FOREIGN KEY ([MessageId]) REFERENCES [Messages] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ReactionMessages_Reactions_ReactionId] FOREIGN KEY ([ReactionId]) REFERENCES [Reactions] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ReactionPosts] (
    [Id] int NOT NULL IDENTITY,
    [ReactionId] int NOT NULL,
    [PostId] int NOT NULL,
    CONSTRAINT [PK_ReactionPosts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ReactionPosts_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ReactionPosts_Reactions_ReactionId] FOREIGN KEY ([ReactionId]) REFERENCES [Reactions] ([Id]) ON DELETE NO ACTION
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

CREATE INDEX [IX_Messages_ChatRoomId] ON [Messages] ([ChatRoomId]);
GO

CREATE INDEX [IX_Messages_UserId] ON [Messages] ([UserId]);
GO

CREATE INDEX [IX_Posts_UserId] ON [Posts] ([UserId]);
GO

CREATE INDEX [IX_ReactionMessages_MessageId] ON [ReactionMessages] ([MessageId]);
GO

CREATE INDEX [IX_ReactionMessages_ReactionId] ON [ReactionMessages] ([ReactionId]);
GO

CREATE INDEX [IX_ReactionPosts_PostId] ON [ReactionPosts] ([PostId]);
GO

CREATE INDEX [IX_ReactionPosts_ReactionId] ON [ReactionPosts] ([ReactionId]);
GO

CREATE INDEX [IX_Reactions_TypeReactionId] ON [Reactions] ([TypeReactionId]);
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
VALUES (N'20240322041809_CreateDBWithReaction', N'7.0.13');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Users] ADD [Avatar] nvarchar(20) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240407161836_AddAvatar', N'7.0.13');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Users] ADD [Background] nvarchar(20) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240409033040_AddFieldBackgroundInUser', N'7.0.13');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Notifications] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Notifications] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Notifications_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [NotificationChatRooms] (
    [Id] int NOT NULL IDENTITY,
    [NotificationId] int NOT NULL,
    [ChatRoomId] int NOT NULL,
    CONSTRAINT [PK_NotificationChatRooms] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_NotificationChatRooms_ChatRooms_ChatRoomId] FOREIGN KEY ([ChatRoomId]) REFERENCES [ChatRooms] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_NotificationChatRooms_Notifications_NotificationId] FOREIGN KEY ([NotificationId]) REFERENCES [Notifications] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [NotificationPosts] (
    [Id] int NOT NULL IDENTITY,
    [NotificationId] int NOT NULL,
    [UserId] int NULL,
    [PostId] int NULL,
    CONSTRAINT [PK_NotificationPosts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_NotificationPosts_Notifications_NotificationId] FOREIGN KEY ([NotificationId]) REFERENCES [Notifications] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_NotificationPosts_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_NotificationPosts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_NotificationChatRooms_ChatRoomId] ON [NotificationChatRooms] ([ChatRoomId]);
GO

CREATE INDEX [IX_NotificationChatRooms_NotificationId] ON [NotificationChatRooms] ([NotificationId]);
GO

CREATE INDEX [IX_NotificationPosts_NotificationId] ON [NotificationPosts] ([NotificationId]);
GO

CREATE INDEX [IX_NotificationPosts_PostId] ON [NotificationPosts] ([PostId]);
GO

CREATE INDEX [IX_NotificationPosts_UserId] ON [NotificationPosts] ([UserId]);
GO

CREATE INDEX [IX_Notifications_UserId] ON [Notifications] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240603150959_AddNotificationModels', N'7.0.13');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Notifications] ADD [IsCheck] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240603174022_UpdateNotificationModel', N'7.0.13');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notifications]') AND [c].[name] = N'IsCheck');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Notifications] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Notifications] DROP COLUMN [IsCheck];
GO

ALTER TABLE [NotificationPosts] ADD [IsCheck] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [NotificationChatRooms] ADD [IsCheck] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240603204927_UpdateNotificationModels', N'7.0.13');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Notifications] ADD [NotificationConnectionId] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240606110042_AddConnectionIdInNotification', N'7.0.13');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Comments] DROP CONSTRAINT [FK_Comments_Posts_PostId];
GO

ALTER TABLE [Comments] DROP CONSTRAINT [FK_Comments_Users_UserId];
GO

DROP INDEX [IX_Comments_PostId] ON [Comments];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Comments]') AND [c].[name] = N'PostId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Comments] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Comments] DROP COLUMN [PostId];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Comments]') AND [c].[name] = N'Text');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Comments] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Comments] DROP COLUMN [Text];
GO

CREATE TABLE [CommentPosts] (
    [Id] int NOT NULL IDENTITY,
    [Text] nvarchar(300) NOT NULL,
    [CommentId] int NOT NULL,
    [PostId] int NOT NULL,
    CONSTRAINT [PK_CommentPosts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CommentPosts_Comments_CommentId] FOREIGN KEY ([CommentId]) REFERENCES [Comments] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CommentPosts_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CommentPosts_CommentId] ON [CommentPosts] ([CommentId]);
GO

CREATE INDEX [IX_CommentPosts_PostId] ON [CommentPosts] ([PostId]);
GO

ALTER TABLE [Comments] ADD CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240607054425_UpdateCommentModels', N'7.0.13');
GO

COMMIT;
GO

