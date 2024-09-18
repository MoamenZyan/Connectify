using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Connectify.Application.Interfaces.RepositoriesInterfaces;
using Connectify.Application.Interfaces.UtilitesInterfaces;
using Connectify.Application.Services.EntitiesApplicationServices;
using Connectify.Domain.Entities;
using Connectify.Domain.Interfaces;
using Connectify.Domain.Services;
using Connectify.Infrastructure.Authentication;
using Connectify.Infrastructure.Configurations.JWTConfigurations;
using Connectify.Infrastructure.Data;
using Connectify.Infrastructure.Repositories;
using Connectify.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.AspNetCore.Mvc.Versioning;
using Connectify.Infrastructure.Configurations.ExternalNotificationsConfigurations;
using Connectify.Application.Interfaces.ExternalNotificationsInterfaces;
using Connectify.Infrastructure.Services.ExternalNotificationsServices;
using Connectify.Application.Interfaces.ExternalNotificationsInterfaces.EmailStrategies;
using Connectify.Infrastructure.Services.ExternalNotificationsServices.EmailStrategies;
using Microsoft.AspNetCore.SignalR;
using Connectify.Application.Services.NotificationServices;
using Connectify.Application.Services.NotificationService;
using Connectify.Infrastructure.Configurations.AWSConfigurations;
using Amazon.Runtime;
using Amazon.S3;
using Amazon;
using Connectify.Application.Interfaces.AWSServicesInterfaces;
using Connectify.Infrastructure.Services.AWSService;
using Connectify.Application.Interfaces.HubInterfaces;
using Connectify.Application.Hubs;
using Hangfire;
using StackExchange.Redis;
using Connectify.Infrastructure.Services.RedisService;

namespace Connectify.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database registeration
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // Redis Registeration
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(configuration["Redis:ConnectionStrings"]!);
            });
            services.AddScoped<Application.Interfaces.RedisInterfaces.IRedis, RedisService>();

            // Entities Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IOTPRepository, OTPRepository>();
            services.AddScoped<IUserPrivateChatRepository, UserPrivateChatRepository>();

            // Notification Repositories
            services.AddScoped<INotificationRepository<AssociatedInfoNotification>, AssociatedInfoNotificationRepository>();
            services.AddScoped<INotificationRepository<InfoNotification>, InfoNotificationRepository>();
            services.AddScoped<IUserNotificationRepository<UserAssociatedInfoNotification>, UserAssosicatedInfoNotificationRepository>();
            services.AddScoped<IUserNotificationRepository<UserInfoNotification>, UserInfoNotificationRepository>();

            // Application Notification Services
            services.AddScoped<IExternalNotificationApplicationService, ExternalNotificationApplicationService>();
            services.AddScoped<IInternalNotificationApplicationService, InternalNotificationApplicationService>();
            services.AddScoped<INotificationApplicationService, NotificationApplicationService>();

            // Junction Entities Repositories
            services.AddScoped<IFriendRequestRepository, FriendRequestRepository>();
            services.AddScoped<IUserBlocksRepository, UserBlocksRepository>();
            services.AddScoped<IUserChatRepository, UserChatRepository>();
            services.AddScoped<IUserSeenMessageRepository, UserSeenMessageRepository>();
            services.AddScoped<IUserFriendRepository, UserFriendRepository>();

            // Domain Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<INotificationService, NotificationService>();

            // Application Services
            services.AddScoped<IChatApplicationService, ChatApplicationService>();
            services.AddScoped<IMessageApplicationService, MessageApplicationService>();
            services.AddScoped<IUserApplicationService, UserApplicationService>();


            // Authentication
            services.Configure<JWTConfigurations>(configuration.GetSection("JWTConfigurations"));
            services.AddSingleton<JWTConfigurations>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    JWTConfigurations jwtConfigs = services.BuildServiceProvider().GetRequiredService<IOptions<JWTConfigurations>>().Value;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtConfigs.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtConfigs.Audience,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigs.SigningKey)),

                        ValidateLifetime = true,
                    };


                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            var path = context.HttpContext.Request.Path;
                            if (path.StartsWithSegments("/chathub") || path.StartsWithSegments("/notificationhub"))
                            {
                                context.Token = context.Request.Query["access_token"];
                            }
                            else
                            {
                                var token = Convert.ToString(context.HttpContext.Request.Headers["Authorization"]);
                                if (token != null)
                                    context.Token = token.Substring("Bearer".Length).Trim();
                            }
                            
                            return Task.CompletedTask;
                        }
                    };
                });


            // API Versioning
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });


            // External Notifications
            services.Configure<EmailServiceConfiguration>(configuration.GetSection("SendGridConfigurations"));
            services.AddSingleton<EmailServiceConfiguration>();
            services.AddScoped<IExternalNotificationContext, ExternalNotificationContext>();


            // Email Strategies
            services.AddScoped<IWelcomeEmailStrategy, WelcomeEmailStrategy>();
            services.AddScoped<IReceivedFriendRequestEmailStrategy, ReceivedFriendRequestEmailStrategy>();
            services.AddScoped<IFriendRequestAcceptedEmailStrategy, FriendRequestAcceptedEmailStrategy>();

            // Add Cors Policies
            services.AddCors(options =>
            {
                options.AddPolicy("AllowLocal", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            // SignalR
            services.AddSignalR();
            services.AddScoped<INotificationHub, NotificationHub>();

            // AWS
            var awsConfigs = configuration.GetSection("AWS").Get<AWSConfigurations>();
            var credentials = new BasicAWSCredentials(awsConfigs!.AccessKey, awsConfigs.SecretAccessKey);
            var configs = new AmazonS3Config()
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(awsConfigs.Region),
            };
            services.Configure<AWSConfigurations>(configuration.GetSection("AWS"));
            services.AddSingleton<AWSConfigurations>();
            services.AddSingleton<IAmazonS3>(sp => new AmazonS3Client(credentials, configs));
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IAttachmentsService, AttachmentsService>();


            // Hangfire
            services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();


            return services;
        }
    }
}
