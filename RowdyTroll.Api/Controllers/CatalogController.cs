using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RowdyTroll.Domain.Catalog;
using RowdyTroll.Data;

namespace RowdyTroll.Api.Controllers
{
    [ApiController]
    [Route("catalog")]
    public class CatalogController : ControllerBase
    {
        private readonly StoreContext _db;

        public CatalogController(StoreContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_db.Items);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var item = _db.Items.Find(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Item item)
        {
            _db.Items.Add(item);
            _db.SaveChanges();
            return Created($"/catalog/{item.Id}", item);
        }

        [HttpPost("{id:int}/ratings")]
        public IActionResult AddRating(int id, [FromBody] Rating rating)
        {
            var item = _db.Items.Find(id);
            if (item == null) return NotFound();
            item.AddRating(rating);
            _db.SaveChanges();
            return Ok(item);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Item item)
        {
            if (id != item.Id) return BadRequest();
            var existing = _db.Items.Find(id);
            if (existing == null) return NotFound();

            // Update scalar properties
            existing.Name = item.Name;
            existing.Description = item.Description;
            existing.Brand = item.Brand;
            existing.Price = item.Price;

            _db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var item = _db.Items.Find(id);
            if (item == null) return NotFound();

            // Load related ratings and remove them first to avoid FK constraint failures
            _db.Entry(item).Collection(i => i.Ratings).Load();
            if (item.Ratings != null && item.Ratings.Count > 0)
            {
                _db.RemoveRange(item.Ratings);
            }

            _db.Items.Remove(item);
            _db.SaveChanges();
            return Ok();
        }
    }
}
