using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication2.Authorization;
using WebApplication2.Data;
using WebApplication2.Filters;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize]
    // [LogSensitiveAction] this attribute is for action fillter debug on this contoller just not for others
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ProductController> _logger;

        public ProductController( ApplicationDbContext dbContext,
            ILogger<ProductController> logger)
        {
            this._dbContext = dbContext;
            this._logger = logger;
        }


        [HttpGet]
        [Route("GetAll")]
        [CheckPermission(Permission.ReadProducts)]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProduct()
        {
            var Username = User.Identity.Name;
            var userid = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;// return Id of User
            var Recodes = await this._dbContext.Set<Product>().ToListAsync();
            return Ok(Recodes);
        }

        [HttpGet]
        [Route("GetById/{Id}")]
        //[Route("{key}")]                    //[FromQuery (Name = "key")]
        [LogSensitiveAction]                 //[FromRoute(Name = "Key")]
        public async Task<ActionResult<Product>> GetById(int Id)
        {
            _logger.LogDebug("Getting product #" + Id);
            //_logger.LogDebug("Getting product #{Id}" + Id);
            var Recode = await this._dbContext.Set<Product>().FindAsync(Id);
            if (Recode == null)
                _logger.LogWarning("Product #{x} was not found -- time :{y}", Id,DateTime.Now);
            return Recode == null ? NotFound(): Ok(Recode);
        }




        [HttpPost]
        [Route("Create")]
        [AllowAnonymous] // put the action for everyone no constrains
                                        //[FromQuery] Product product,[FromQuery(Name = "p2")] Product product2
        public async Task<ActionResult<int>> CreateProduct(Product product ,
            [FromHeader (Name = "Accept-Language")] string Language)
        {
            this._dbContext.Set<Product>().Add(product);
            await this._dbContext.SaveChangesAsync();
            return Ok(product.Id);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> UpdateProduct(Product product)
        {
            var ExistingProduct = this._dbContext.Set<Product>().Find(product.Id);
            ExistingProduct.Name = product.Name;
            ExistingProduct.Sku = product.Sku;
            this._dbContext.Update(ExistingProduct);
            await this._dbContext.SaveChangesAsync();
            return Ok();

        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult>DeleteProduct(int Id)
        {
            var ExistingProduct = await this._dbContext.Set<Product>().FindAsync(Id);
            this._dbContext.Set<Product>().Remove(ExistingProduct);
            await this._dbContext.SaveChangesAsync();
            return Ok();
        }







    }
}
