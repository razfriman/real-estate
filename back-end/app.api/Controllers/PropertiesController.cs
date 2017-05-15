using System.Threading.Tasks;
using app.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace app.api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class PropertiesController : Controller
    {
        private readonly ILogger _logger;

        private readonly PropertyService _propertyService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="propertyService"></param>
        public PropertiesController(ILogger<PropertiesController> logger, PropertyService propertyService)
        {
            _logger = logger;
            _propertyService = propertyService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var result = await _propertyService.Get();
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var result = await _propertyService.GetByID(id);

            if (result == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, $"GetByID({id}) NOT FOUND");
                return NotFound("Property not found");
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
