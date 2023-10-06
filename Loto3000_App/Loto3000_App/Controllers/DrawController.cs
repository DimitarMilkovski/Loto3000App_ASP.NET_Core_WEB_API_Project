using Loto3000_App.DTOs.WinnerDtos;
using Loto3000_App.Services.Interfaces;
using Loto3000_App.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loto3000_App.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DrawController : ControllerBase
    {
        private readonly IDrawService _drawService;
        public DrawController(IDrawService drawService)
        {
            _drawService = drawService;
        }

        [HttpGet]
        public ActionResult<List<WinnerDto>> StartDraw()
        {
            try
            {
                List<WinnerDto> drawnWinners = _drawService.StartDraw();
                return Ok(drawnWinners);
            }
            catch (DrawDataException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
