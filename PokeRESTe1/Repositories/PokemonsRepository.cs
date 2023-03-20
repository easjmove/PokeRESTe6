using PokemonAPI.Models;


namespace PokemonAPI.Repositories
{
    public class PokemonsRepository : IPokemonsRepository
    {
        private int _nextID;
        private List<Pokemon> _pokemons;

        public PokemonsRepository()
        {
            _nextID = 1;
            _pokemons = new List<Pokemon>()
            {
                new Pokemon() {Id = _nextID++, Name="Pikachu", Level=99, PokeDex=25},
                new Pokemon() {Id = _nextID++, Name="Charmander", Level=10, PokeDex=12},
                new Pokemon() {Id = _nextID++, Name="Arbok", Level=20, PokeDex=80},
            };
        }

        public List<Pokemon> GetAll(string? nameFilter, int? minLevel, int? maxLevel)
        {
            List<Pokemon> result = new List<Pokemon>(_pokemons);

            if (nameFilter != null)
            {
                result = result.FindAll(pokemon => pokemon.Name.Contains(nameFilter,
                    StringComparison.InvariantCultureIgnoreCase));
            }

            if (minLevel != null)
            {
                result = result.FindAll(pokemon => pokemon.Level >= minLevel);
            }

            if (maxLevel != null)
            {
                result = result.FindAll(pokemon => pokemon.Level >= maxLevel);
            }

            return result;
        }

        public Pokemon? GetByID(int id)
        {
            return _pokemons.Find(x => x.Id == id);
        }

        public Pokemon Add(Pokemon newPokemon)
        {
            newPokemon.Validate();
            newPokemon.Id = _nextID++;
            _pokemons.Add(newPokemon);
            return newPokemon;
        }

        public Pokemon? Delete(int id)
        {
            Pokemon? foundPokemon = GetByID(id);
            if (foundPokemon == null)
            {
                return null;
            }
            _pokemons.Remove(foundPokemon);
            return foundPokemon;
        }

        public Pokemon? Update(int id, Pokemon updates)
        {
            updates.Validate();
            Pokemon? foundPokemon = GetByID(id);
            if (foundPokemon == null)
            {
                return null;
            }
            foundPokemon.Name = updates.Name;
            foundPokemon.PokeDex = updates.PokeDex;
            foundPokemon.Level = updates.Level;
            return foundPokemon;
        }
    }
}
