using Microsoft.AspNetCore.Mvc;
using MW.DTO;
using MW.Repositories;

namespace MW.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _repo;

        public ProductController(ILogger<ProductController> logger, IProductRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        => Ok(await _repo.GetPaging());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _repo.GetById(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> GetOrderNumber(SaleOrderFilterDto filter)
        {

            try
            {
                filter.StatusId = 3;
                filter.HasAllPermission = true;
                filter.Refunded = false;
                filter.CreatedBy = "hien.cao";

                var dto = await _repo.GetSaleOrderNumbersAsync(filter, true, "hien.cao");
                return Ok(dto);
            }
            catch (Exception ex)
            {
                //this.LogError(this, ex);
            }
            return NotFound();
        }
    }
}
