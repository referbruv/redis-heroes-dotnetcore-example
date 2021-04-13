using Microsoft.AspNetCore.Mvc;
using RedisHeroesApi.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisHeroesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly IHeroesRepository _repository;

        public HeroesController(IHeroesRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<HeroesController>
        [HttpGet]
        public IEnumerable<Hero> Get(int page = 1, int max = 1000)
        {
            return _repository.All(page, max).ToList();
        }

        // GET api/<HeroesController>/5
        [HttpGet("{id}")]
        public Hero Get(int id)
        {
            return _repository.Single(id);
        }

        // POST api/<HeroesController>
        [HttpPost]
        public Hero Post([FromBody] CreateOrUpdateHeroRequest value)
        {
            return _repository.Create(new Hero
            {
                Category = value.Category,
                HasCape = value.HasCape,
                IsAlive = value.IsAlive,
                Name = value.Name,
                Powers = value.Powers
            });
        }

        // PUT api/<HeroesController>/5
        [HttpPut("{id}")]
        public Hero Put(int id, [FromBody] CreateOrUpdateHeroRequest value)
        {
            return _repository.Update(id, new Hero
            {
                Category = value.Category,
                HasCape = value.HasCape,
                IsAlive = value.IsAlive,
                Name = value.Name,
                Powers = value.Powers
            });
        }

        // DELETE api/<HeroesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }

    public class CreateOrUpdateHeroRequest
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        public string Powers { get; set; }
        public bool HasCape { get; set; }
        public bool IsAlive { get; set; }
        public Category Category { get; set; }
    }
}
