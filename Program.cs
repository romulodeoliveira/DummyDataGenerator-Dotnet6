using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0",
        Title = "Dummy Data Generator API",
        Description = "Projeto destinado a portfólio. Totalmente gratuito, este projeto foi pensado para auxiliar programadores e desenvolvedores de sistemas a realizar testes durante o processo de criação de um software online.",
        Contact = new OpenApiContact
        {
            Name = "Romulo de Oliveira",
            Email = "dev@romulodeoliveira.net",
            Url = new Uri("https://romulodeoliveira.net/"),
        },
        License = new OpenApiLicense
        {
            Name = "Licença",
            Url = new Uri("https://github.com/romulodeoliveira/DummyDataGenerator-Dotnet6/blob/main/LICENSE.md"),
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
