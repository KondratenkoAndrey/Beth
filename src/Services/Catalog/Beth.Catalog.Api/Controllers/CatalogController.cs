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
[Route("api/v1/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ILogger<CatalogController> _logger;
    private readonly CatalogContext _catalogContext;

    public CatalogController(ILogger<CatalogController> logger, CatalogContext catalogContext)
    {
        _logger = logger;
        _catalogContext = catalogContext;
    }
    
    // GET api/v1/[controller]/items[?pageSize=3&pageIndex=10]
    [HttpGet]
    [Route("items")]
    [ProducesResponseType(typeof(PaginatedItemsViewModel<CatalogItem>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> ItemsAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        var totalItems = await _catalogContext.CatalogItems.LongCountAsync();

        var itemsOnPage = await _catalogContext.CatalogItems
            .Include(c => c.CatalogBrand)
            .Include(c => c.CatalogType)
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        var model = new PaginatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);

        return Ok(model);
    }
}