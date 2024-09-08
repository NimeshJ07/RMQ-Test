using Microsoft.AspNetCore.Mvc;
using RMQ.API.Migrations;
using RMQ.IServices;
using RMQ.Model;
using RMQ.RabbitMQ;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RMQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices userservices;
        private readonly IRabitMQProducer rabbitMQProducer;
        private readonly RabbitMQConsumer rabitMQConsumer;

        public UserController(IUserServices _userservices,IRabitMQProducer _rabitMQProducer, RabbitMQConsumer _rabbitMQConsumer)
        {
            userservices = _userservices;
            rabbitMQProducer = _rabitMQProducer;
            rabitMQConsumer = _rabbitMQConsumer;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            var data =  await userservices.GetUsersAsync();
            rabitMQConsumer.Consume();
            return data;    

        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            return await userservices.GetUserByIdAsync(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<User> Post([FromBody] User users)
        {
            var data =  await userservices.CreateUserAsync(users);
            rabbitMQProducer.SendProductMessage(data);
            return data;
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<User> Put(int id, [FromBody] User users)
        {
            return await userservices.UpdateUserAsync(id, users);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public Task<bool> Delete(int id)
        {
            return userservices.DeleteUserAsync(id);
        }
    }
}
