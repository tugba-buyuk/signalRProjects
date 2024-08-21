using SinalRServerExample.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
   policy.AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()
           .SetIsOriginAllowed(origin => true)
));
builder.Services.AddControllers();
var app = builder.Build();

app.UseCors();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MyHub>("/myhub");
    endpoints.MapHub<MessageHub>("/messagehub");
    endpoints.MapControllers();
});

app.Run();
