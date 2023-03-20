using PokemonAPI.Models;
using PokemonAPI.Repositories;
using PokeRESTe6.Contexts;

namespace PokeRESTe6.Repositories
{
    public class PokemonsRepositoryDB : IPokemonsRepository
    {
        private PokemonContext _context;

        public PokemonsRepositoryDB(PokemonContext context)
        {
            _context = context;
        }

        public Pokemon Add(Pokemon newPokemon)
        {
            newPokemon.Validate();
            newPokemon.Id = 0;
            _context.pokemons.Add(newPokemon);
            _context.SaveChanges();
            return newPokemon;
        }

        public Pokemon? Delete(int id)
        {
            Pokemon foundPokemon = GetByID(id);
            if (foundPokemon == null)
            {
                return null;
            }
            _context.pokemons.Remove(foundPokemon);
            _context.SaveChanges();
            return foundPokemon;
        }

        public List<Pokemon> GetAll(string? namefilter, int? minLevel, int? maxLevel)
        {
            return _context.pokemons.Where(poke =>
            (namefilter == null || poke.Name.Contains(namefilter, StringComparison.InvariantCultureIgnoreCase)) &&
            (minLevel == null || poke.Level >= minLevel) &&
            (maxLevel == null || poke.Level <= maxLevel)
            ).ToList();
        }

        public Pokemon? GetByID(int id)
        {
            return _context.pokemons.Find(id);
        }

        public Pokemon? Update(int id, Pokemon updates)
        {
            updates.Validate();
            Pokemon foundPokemon = GetByID(id);
            if (foundPokemon == null)
            {
                return null;
            }
            foundPokemon.Name = updates.Name;
            foundPokemon.Level = updates.Level;
            foundPokemon.PokeDex = updates.PokeDex;
            _context.SaveChanges();
            return foundPokemon;
        }
    }
}
