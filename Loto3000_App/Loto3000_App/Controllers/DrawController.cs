using Loto3000_App.DTOs.WinnerDtos;
using Loto3000_App.Services.Interfaces;
using Loto3000_App.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loto3000_App.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DrawController : ControllerBase
    {
        private readonly IDrawService _drawService;
        public DrawController(IDrawService drawService)
        {
            _drawService = drawService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
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

        [AllowAnonymous]
        [HttpGet("lastSessionWinners")]
        public ActionResult <List<WinnerDto>> LastSessionWinners() 
        {
            try
            {
                List<WinnerDto> lastSessionWinners = _drawService.GetLastSessionWinners();
                return Ok(lastSessionWinners);
            }
            catch (DrawDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (WinnerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<WinnerDto>> AllSessionsWinners()
        {
            try
            {
                List<WinnerDto> lastSessionWinners = _drawService.GetAllWinners();
                return Ok(lastSessionWinners);
            }
            catch (DrawDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (WinnerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
