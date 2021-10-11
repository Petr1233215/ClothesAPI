using ClothesAPI.Helpers;
using ClothesAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClothesController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<ClothesController> _logger;
        private readonly ManageFile manageFile;
        private const string fileName = "Data\\clothes.csv";

        public ClothesController(ILogger<ClothesController> logger, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            manageFile = ManageFile.GetInstance(Path.Combine(_hostingEnvironment.ContentRootPath, fileName));
        }

        /// <summary>
        /// Получение продуктов, по Id
        /// </summary>
        /// <param name="clothesId">id одежды(продукта)</param>
        /// <returns></returns>
        [HttpGet("{clothesId}")]
        public async Task<IActionResult> GetProductById([FromRoute] int clothesId)
        {
            var product = manageFile
                .GetProducts()
                .FirstOrDefault(c => c.Id == clothesId);

            return Ok(product);
        }

        /// <summary>
        /// Получение продуктов, по фильтру тайтл
        /// </summary>
        /// <param name="title">название продукта</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery(Name = "title")] string title)
        {
            var products = manageFile.GetProducts();

            if (title != null)
            {
                title = title.ToUpper();
                products = products.Where(c => c.Title.ToUpper().Contains(title));
            }

            return Ok(products);
        }


        /// <summary>
        /// Добавление новой сущности Продукт
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            var error = manageFile.AddProduct(product);
            if (!string.IsNullOrEmpty(error))
                return BadRequest(error);

            return Ok(product);
        }
    }
}
