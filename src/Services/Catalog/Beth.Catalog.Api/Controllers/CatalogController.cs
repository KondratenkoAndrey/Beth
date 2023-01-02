using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Beth.Catalog.Api.Infrastructure;
using Beth.Catalog.Api.Models;
using Beth.Catalog.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Beth.Catalog.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]/items")]
public class CatalogController : ControllerBase
{
    private readonly ILogger<CatalogController> _logger;
    private readonly CatalogContext _catalogContext;

    public CatalogController(ILogger<CatalogController> logger, CatalogContext catalogContext)
    {
        _logger = logger;
        _catalogContext = catalogContext;
    }
    
    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(PaginatedViewModel<CatalogItem>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> ItemsAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        var totalItems = await _catalogContext.CatalogItems.LongCountAsync();

        var itemsOnPage = await _catalogContext.CatalogItems
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        var model = new PaginatedViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);

        return Ok(model);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<CatalogItem>> ItemByIdAsync(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var item = await _catalogContext.CatalogItems.FindAsync(id);
        if (item != null)
        {
            return item;
        }

        return NotFound();
    }
    
    [HttpPost]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateItemAsync([FromBody] CatalogItem item)
    {
        await _catalogContext.CatalogItems.AddAsync(item);
        await _catalogContext.SaveChangesAsync();

        return CreatedAtAction("items", new {id = item.Id}, item);
    }

    [Route("{id:int}")]
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<ActionResult> UpdateItemAsync(int id, CatalogItem itemToUpdate)
    {
        var catalogItem = await _catalogContext.CatalogItems.FindAsync(id);
        if (catalogItem == null)
        {
            return NotFound();
        }

        catalogItem.Name = itemToUpdate.Name;
        catalogItem.Description = itemToUpdate.Description;
        catalogItem.Price = itemToUpdate.Price;
        catalogItem.CatalogBrandId = itemToUpdate.CatalogBrandId;
        catalogItem.CatalogTypeId = itemToUpdate.CatalogTypeId;
        
        await _catalogContext.SaveChangesAsync();
        
        return NoContent();
    }
    
    [Route("{id:int}")]
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> DeleteItemAsync(int id)
    {
        var item = _catalogContext.CatalogItems.SingleOrDefault(x => x.Id == id);
        if (item == null)
        {
            return NotFound();
        }

        _catalogContext.CatalogItems.Remove(item);
        await _catalogContext.SaveChangesAsync();

        return NoContent();
    }
}