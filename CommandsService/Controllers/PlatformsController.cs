using System;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers 
{
    /// <summary>
    /// Controller for managing platform-related actions.
    /// </summary>
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformsController"/> class.
        /// </summary>
        public PlatformsController(){ }

        /// <summary>
        /// Tests the inbound connection to the API.
        /// </summary>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation.</returns>
        [HttpPost]
        public ActionResult TestInboundConnection() 
        {
            Console.WriteLine("--> Inbound POST # Command Service");

            return Ok("Inbound test of from Platforms Controller");
        }
    }
    
}