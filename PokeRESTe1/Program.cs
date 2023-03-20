using Microsoft.EntityFrameworkCore;
using PokemonAPI.Repositories;
using PokeRESTe6;
using PokeRESTe6.Contexts;
using PokeRESTe6.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                              policy =>
                              {
                                  policy.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                              });
});


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

bool useSql = true;
if (useSql)
{
    var optionsBuilder =
        new DbContextOptionsBuilder<PokemonContext>();
    optionsBuilder.UseSqlServer(Secrets.ConnectionString);
    PokemonContext context =
        new PokemonContext(optionsBuilder.Options);
    builder.Services.AddSingleton<IPokemonsRepository>(
        new PokemonsRepositoryDB(context));
}
else
{
    builder.Services.AddSingleton<IPokemonsRepository>
        (new PokemonsRepository());
}

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
