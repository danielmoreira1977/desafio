using Questao5.Behaviors;
using Questao5.Extensions;
using Questao5.Infrastructure.Sqlite;

var builder = WebApplication.CreateBuilder(args);


//builder
//    .Services
//    .Scan(
//        selector => selector
//            .FromAssemblies(typeof(Program).Assembly)
//            .AddClasses(false)
//            .AsImplementedInterfaces()
//            .WithScopedLifetime());

// Add services to the container.

builder
    .Services
    .AddControllers()
    .AddApplicationPart(typeof(Program).Assembly);

builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(IdempotenciaCommandPipelineBehavior<,>));
});


builder.Services.RegisterDependenciesroup();

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

app.Run();

// Informações úteis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html


