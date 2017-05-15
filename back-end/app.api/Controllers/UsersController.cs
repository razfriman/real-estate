using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using app;
using app.Services;
using Microsoft.Extensions.Logging;

namespace app.api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ILogger _logger;

        private readonly UserService _userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userService"></param>
        public UsersController(ILogger<UsersController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.Get();
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
            var result = await _userService.GetByID(id);

            if (result == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, $"GetByID({id}) NOT FOUND");
                return NotFound("User not found");
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
