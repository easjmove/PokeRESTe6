using PokemonAPI.Models;

namespace PokemonAPI.Repositories
{
    public interface IPokemonsRepository
    {
        Pokemon Add(Pokemon newPokemon);
        Pokemon? Delete(int id);
        List<Pokemon> GetAll(string? nameFilter, int? minLevel, int? maxLevel);
        Pokemon? GetByID(int id);
        Pokemon? Update(int id, Pokemon updates);
    }
}