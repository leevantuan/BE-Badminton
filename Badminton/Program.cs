using Badminton.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//CORS
builder.Services.AddCors(p => p.AddPolicy("DemoBadminton", buil =>
{
    buil.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/Demo_Log.txt", rollingInterval: RollingInterval.Minute)
    .MinimumLevel.Information()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationService(builder.Configuration);

builder.Services.AddSwaggerGenService(builder.Configuration);

builder.Services.AddAuthenticationService(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

//CORS
app.UseCors("DemoBadminton");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
//dix
//app.UseHttpsRedirection();

app.Run();
