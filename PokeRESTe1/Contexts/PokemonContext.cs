using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;

namespace PokeRESTe6.Contexts
{
    public class PokemonContext : DbContext
    {
        public PokemonContext(DbContextOptions<PokemonContext>
            options) : base(options) { }

        public DbSet<Pokemon> pokemons { get; set; }
    }
}
