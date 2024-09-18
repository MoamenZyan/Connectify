using Connectify.Application.Hubs;
using Connectify.Infrastructure.IoC;
using Hangfire;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddServices(builder.Configuration);


var app = builder.Build();

app.UseCors("AllowLocal");

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chathub");
app.MapHub<NotificationHub>("/notificationhub");

app.MapControllers();

app.Run();
