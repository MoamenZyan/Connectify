using Connectify.Domain.Entities;
using Connectify.Infrastructure.Configurations.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<OTP> OTPs { get; set; }
        public DbSet<UserBlocks> UserBlocks { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
        public DbSet<UserSeenMessage> UserSeenMessages { get; set; }
        public DbSet<InfoNotification> InfoNotifications { get; set; }
        public DbSet<AssociatedInfoNotification> AssociatedInfoNotifications { get; set; }
        public DbSet<UserInfoNotification> UserInfoNotifications { get; set; }
        public DbSet<UserAssociatedInfoNotification> UserAssociatedInfoNotifications { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }
        public DbSet<UserPrivateChat> UserPrivateChats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new OTPConfiguration());
            modelBuilder.ApplyConfiguration(new InfoNotificationConfiguration());
            modelBuilder.ApplyConfiguration(new AssociatedInfoNotificationConfiguration());

            modelBuilder.ApplyConfiguration(new UserBlocksConfiguration());
            modelBuilder.ApplyConfiguration(new UserChatConfiguration());
            modelBuilder.ApplyConfiguration(new UserSeenMessageConfiguration());
            modelBuilder.ApplyConfiguration(new UserAssociatedInfoNotificationConfiguration());
            modelBuilder.ApplyConfiguration(new UserInfoNotificationConfiguration());
            modelBuilder.ApplyConfiguration(new FriendRequestConfiguration());
            modelBuilder.ApplyConfiguration(new UserFriendConfiguration());
            modelBuilder.ApplyConfiguration(new UserPrivateChatConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
