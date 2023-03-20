using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Models;
using PokemonAPI.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokemonAPI.Controllers
{
    [Route("api/[controller]")]
    //URI: api/pokemons
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private IPokemonsRepository _repository;

        public PokemonsController(IPokemonsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<PokemonsController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<IEnumerable<Pokemon>> Get([FromQuery] string? nameFilter,
            [FromQuery] int? minLevel, 
            [FromQuery] int? maxLevel)
        {
            List<Pokemon> result = _repository.GetAll(nameFilter, minLevel, maxLevel);
            if (result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("{nameFilter}")]
        public ActionResult<IEnumerable<Pokemon>> GetNameFilterRoute(string? nameFilter)
        {
            List<Pokemon> result = _repository.GetAll(nameFilter, null,null);
            if (result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        // GET api/<PokemonsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Pokemon> Get(int id)
        {
            Pokemon? foundPokemon = _repository.GetByID(id);
            if (foundPokemon == null)
            {
                return NotFound();
            }
            return Ok(foundPokemon);
        }

        // POST api/<PokemonsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Pokemon> Post([FromBody] Pokemon newPokemon)
        {
            try
            {
                Pokemon createdPokemon = _repository.Add(newPokemon);
                return Created("api/pokemons/" + createdPokemon.Id, createdPokemon);
            }
            catch (Exception ex) when (ex is ArgumentNullException ||
                                       ex is ArgumentOutOfRangeException ||
                                       ex is ArgumentException)
            {
                return BadRequest(ex.InnerException);
            }
        }

        // PUT api/<PokemonsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<Pokemon> Put(int id, [FromBody] Pokemon updates)
        {
            try
            {
                Pokemon? updatedPokemon = _repository.Update(id, updates);
                if (updatedPokemon == null)
                {
                    return NotFound();
                }
                return Ok(updatedPokemon);
            }
            catch (Exception ex) when (ex is ArgumentNullException ||
                                       ex is ArgumentOutOfRangeException ||
                                       ex is ArgumentException)
            {
                return BadRequest(ex.InnerException);
            }
        }

        // DELETE api/<PokemonsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Pokemon> Delete(int id)
        {
            Pokemon? deletedPokemon = _repository.Delete(id);
            if (deletedPokemon == null)
            {
                return NotFound();
            }
            return Ok(deletedPokemon);
        }
    }
}
