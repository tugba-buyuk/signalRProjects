using SinalRServerExample.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
var app = builder.Build();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MyHub>("/myhub");
});
app.Run();
