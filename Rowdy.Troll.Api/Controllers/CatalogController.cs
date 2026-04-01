using Microsoft.AspNetCore.Mvc;
using Rowdy.Troll.Domain.Catalog;

namespace Rowdy.Troll.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
    [HttpGet]
    public IActionResult GetItems()
    {
        var items = new List<Item>
        {
            new Item("Shoes", "Running shoes", "Nike", 129.99m),
            new Item("Shorts", "Ohio State shorts", "Nike", 49.99m)
        };

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetItem(int id)
    {
        var items = new List<Item>
        {
            new Item("Shoes", "Running shoes", "Nike", 129.99m),
            new Item("Shorts", "Ohio State shorts", "Nike", 49.99m)
        };

        if (id < 1 || id > items.Count)
        {
            return NotFound();
        }

        return Ok(items[id - 1]);
    }

    [HttpPost]
    public IActionResult Post(Item item)
    {
        return Created("/catalog/42", item);
    }

    [HttpPost("{id:int}/ratings")]
    public IActionResult AddRating(int id, Rating rating)
    {
        var items = new List<Item>
        {
            new Item("Shoes", "Running shoes", "Nike", 129.99m),
            new Item("Shorts", "Ohio State shorts", "Nike", 49.99m)
        };

        if (id < 1 || id > items.Count)
        {
            return NotFound();
        }

        items[id - 1].AddRating(rating);

        return Ok(items[id - 1]);
    }
}