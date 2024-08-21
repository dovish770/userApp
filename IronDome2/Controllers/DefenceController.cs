using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IronDome2.Models;
using IronDome2.services;
namespace IronDome2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpPut("missiles")]
        public IActionResult Missiles(Defence defence)
        {
            DefenceService.MissileCount = defence.MissileCount;
            DefenceService.MissileTypes = defence.MissileTypes;
            return Ok();
        }
    }
}
