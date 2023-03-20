using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonAPI.Models;
using PokemonAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonAPI.Repositories.Tests
{
    [TestClass()]
    public class PokemonsRepositoryTests
    {
        [TestMethod()]
        public void GetAllTest()
        {
            int expectedCount = 3;

            PokemonsRepository repos = new PokemonsRepository();

            List<Pokemon> list = repos.GetAll();

            Assert.IsNotNull(list);
            Assert.AreEqual(typeof(List<Pokemon>), list.GetType());
            Assert.AreEqual(expectedCount, list.Count);

            foreach (var pokemon in list)
            {
                int foundIds = list.FindAll(x => x.Id == pokemon.Id).Count;
                if (foundIds > 1)
                {
                    Assert.Fail($"ID: {pokemon.Id} exists {foundIds} times in the list");
                }
            }
        }

        [TestMethod()]
        public void GetByIDTest()
        {
            int id = 1;
            int wrongID = 99;
            string name = "Pikachu";
            int level = 99;
            int pokedex = 25;

            PokemonsRepository repos = new PokemonsRepository();

            Pokemon? foundPokemon = repos.GetByID(id);
            Pokemon? notFoundPokemon = repos.GetByID(wrongID);

            Assert.IsNotNull(foundPokemon);
            Assert.AreEqual(id, foundPokemon.Id);
            Assert.AreEqual(name, foundPokemon.Name);
            Assert.AreEqual(level, foundPokemon.Level);
            Assert.AreEqual(pokedex, foundPokemon.PokeDex);

            Assert.IsNull(notFoundPokemon);
        }

        [TestMethod()]
        public void AddTest()
        {
            int wrongId = 33;
            int correctID = 4;
            string name = "Squirtle";
            int level = 1;
            int pokedex = 10;

            PokemonsRepository repos = new PokemonsRepository();
            int beforeAdd = repos.GetAll().Count();

            Pokemon newPokemon = new Pokemon { Id = wrongId, Name = name, Level = level, PokeDex = pokedex };

            Pokemon addedPokemon = repos.Add(newPokemon);

            Assert.AreEqual(typeof(Pokemon), addedPokemon.GetType());

            Assert.AreEqual(correctID, addedPokemon.Id);
            Assert.AreEqual(name, addedPokemon.Name);
            Assert.AreEqual(level, addedPokemon.Level);
            Assert.AreEqual(pokedex, addedPokemon.PokeDex);

            Assert.AreEqual(beforeAdd + 1, repos.GetAll().Count);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            int id = 1;
            int wrongID = 99;

            PokemonsRepository repos = new PokemonsRepository();
            int beforeDelete = repos.GetAll().Count();

            Pokemon? deleted = repos.Delete(id);
            Pokemon? triedDeleted = repos.Delete(wrongID);

            Assert.IsNull(repos.GetByID(id));
            Assert.AreEqual(beforeDelete - 1, repos.GetAll().Count);
            Assert.IsNull(triedDeleted);

        }

        [TestMethod()]
        public void UpdateTest()
        {
            int id = 1;
            int wrongId = 99;
            string name = "Squirtle";
            int level = 1;
            int pokedex = 10;

            PokemonsRepository repos = new PokemonsRepository();
            Pokemon updates = new Pokemon { Id = id, Name = name, Level = level, PokeDex = pokedex };

            Pokemon? updated = repos.Update(id, updates);
            Pokemon? wrongUpdated = repos.Update(wrongId, updates);

            Assert.AreEqual(id, updated.Id);
            Assert.AreEqual(name, updated.Name);
            Assert.AreEqual(level, updated.Level);
            Assert.AreEqual(pokedex, updated.PokeDex);

            Assert.IsNull(wrongUpdated);
        }
    }
}