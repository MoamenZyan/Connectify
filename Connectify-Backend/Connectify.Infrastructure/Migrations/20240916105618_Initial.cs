using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(4000)", maxLength: 4000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InfoNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Content = table.Column<string>(type: "VARCHAR(1024)", maxLength: 1024, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OTPs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Number = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    Deadline = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Fname = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    Lname = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(528)", maxLength: 528, nullable: false),
                    Phone = table.Column<string>(type: "VARCHAR(528)", maxLength: 528, nullable: false),
                    Photo = table.Column<string>(type: "VARCHAR(528)", maxLength: 528, nullable: false),
                    IsVerified = table.Column<bool>(type: "BIT", nullable: false),
                    IsOnline = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssociatedInfoNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Content = table.Column<string>(type: "VARCHAR(1024)", maxLength: 1024, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    AssoicatedUserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociatedInfoNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssociatedInfoNotifications_Users_AssoicatedUserId",
                        column: x => x.AssoicatedUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FriendRequests",
                columns: table => new
                {
                    SenderId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequests", x => new { x.SenderId, x.ReceiverId });
                    table.ForeignKey(
                        name: "FK_FriendRequests_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FriendRequests_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    SenderId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ChatId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Content = table.Column<string>(type: "NVARCHAR(4000)", maxLength: 4000, nullable: false),
                    AttachmentPath = table.Column<string>(type: "VARCHAR(4000)", maxLength: 4000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserBlocks",
                columns: table => new
                {
                    BlockerId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    BlockedId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBlocks", x => new { x.BlockerId, x.BlockedId });
                    table.ForeignKey(
                        name: "FK_UserBlocks_Users_BlockedId",
                        column: x => x.BlockedId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserBlocks_Users_BlockerId",
                        column: x => x.BlockerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserChats",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ChatId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChats", x => new { x.UserId, x.ChatId });
                    table.ForeignKey(
                        name: "FK_UserChats_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserFriends",
                columns: table => new
                {
                    UserId1 = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    UserId2 = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFriends", x => new { x.UserId1, x.UserId2 });
                    table.ForeignKey(
                        name: "FK_UserFriends_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserFriends_Users_UserId2",
                        column: x => x.UserId2,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserInfoNotifications",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    NotificationId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfoNotifications", x => new { x.UserId, x.NotificationId });
                    table.ForeignKey(
                        name: "FK_UserInfoNotifications_InfoNotifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "InfoNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInfoNotifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserPrivateChats",
                columns: table => new
                {
                    User1Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    User2Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ChatId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPrivateChats", x => new { x.User1Id, x.User2Id });
                    table.ForeignKey(
                        name: "FK_UserPrivateChats_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPrivateChats_Users_User1Id",
                        column: x => x.User1Id,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserPrivateChats_Users_User2Id",
                        column: x => x.User2Id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserAssociatedNotifications",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    NotificationId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssociatedNotifications", x => new { x.UserId, x.NotificationId });
                    table.ForeignKey(
                        name: "FK_UserAssociatedNotifications_AssociatedInfoNotifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "AssociatedInfoNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAssociatedNotifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserSeenMessages",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    MessageId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    SeenAt = table.Column<DateTime>(type: "DATETIME2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSeenMessages", x => new { x.UserId, x.MessageId });
                    table.ForeignKey(
                        name: "FK_UserSeenMessages_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSeenMessages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssociatedInfoNotifications_AssoicatedUserId",
                table: "AssociatedInfoNotifications",
                column: "AssoicatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_ReceiverId",
                table: "FriendRequests",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssociatedNotifications_NotificationId",
                table: "UserAssociatedNotifications",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlocks_BlockedId",
                table: "UserBlocks",
                column: "BlockedId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_ChatId",
                table: "UserChats",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_UserId2",
                table: "UserFriends",
                column: "UserId2");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfoNotifications_NotificationId",
                table: "UserInfoNotifications",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPrivateChats_ChatId",
                table: "UserPrivateChats",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPrivateChats_User2Id",
                table: "UserPrivateChats",
                column: "User2Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserSeenMessages_MessageId",
                table: "UserSeenMessages",
                column: "MessageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendRequests");

            migrationBuilder.DropTable(
                name: "OTPs");

            migrationBuilder.DropTable(
                name: "UserAssociatedNotifications");

            migrationBuilder.DropTable(
                name: "UserBlocks");

            migrationBuilder.DropTable(
                name: "UserChats");

            migrationBuilder.DropTable(
                name: "UserFriends");

            migrationBuilder.DropTable(
                name: "UserInfoNotifications");

            migrationBuilder.DropTable(
                name: "UserPrivateChats");

            migrationBuilder.DropTable(
                name: "UserSeenMessages");

            migrationBuilder.DropTable(
                name: "AssociatedInfoNotifications");

            migrationBuilder.DropTable(
                name: "InfoNotifications");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
