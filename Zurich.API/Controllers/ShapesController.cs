/*
 * Alberto Blanco 2024 
 * hola@albertoblanco.dev
 * 
 */


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zurich.BusinessLogic.BindingModels;
using Zurich.BusinessLogic.Services.Abstract;

namespace Zurich.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShapesController : ControllerBase
    {

        private readonly IShapeService _shapeService;

        public ShapesController(IShapeService shapeService)
        {
            _shapeService = shapeService;
        }

        // GET: api/Shapes
        [HttpGet]
        public async Task<IActionResult> GetAllObjects()
        {
            var objects = await _shapeService.ReadAllObjectsAsync();
            return Ok(objects);
        }

        // PUT: api/Shapes/move
        [HttpPut("move")]
        public async Task<IActionResult> MoveAllObjects([FromBody] MoveRequest request)
        {
            await _shapeService.MoveAllObjectsAsync(request.OffsetX, request.OffsetY);
            return NoContent();
        }

        // GET: api/Shapes/display
        [HttpGet("display")]
        public async Task<IActionResult> DisplayAllObjects()
        {            
            await _shapeService.DisplayAllObjectsAsync();
            return NoContent();
        }

    }
}
