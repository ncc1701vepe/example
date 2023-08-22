using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Km56.MyStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private Km56.MyStore.Application.Service.IItemService _itemService;

        public ItemsController(Km56.MyStore.Application.Service.IItemService itemService) 
        { 
            _itemService = itemService;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<IEnumerable<Km56.MyStore.Application.Dto.ItemDto>> Get()
        {
            var items = await _itemService.GetAllAsync();
            return items;
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ItemsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
