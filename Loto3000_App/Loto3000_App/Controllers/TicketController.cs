using Loto3000_App.Domain;
using Loto3000_App.Domain.Enums;
using Loto3000_App.DTOs.TicketDtos;
using Loto3000_App.Services.Interfaces;
using Loto3000_App.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Loto3000_App.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }


        [HttpPost("addTicket")]
        public IActionResult AddTicket([FromBody] List<CombinationDto> combinations)
        {
            try
            {
                int userId = GetAuthorizedUserId();
                _ticketService.CreateTicket(combinations, userId);
                return StatusCode(StatusCodes.Status201Created, "Ticket created!");
            }
            catch (TicketDataException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("getPlayerTickets")]
        public ActionResult<List<TicketDto>> GetPlayerTickets()
        {
            try
            {
                int userId = GetAuthorizedUserId();
                return _ticketService.GetPlayersTickets(userId);
            }
            catch (TicketNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetPlayerOngoingTickets")]
        public ActionResult<List<TicketDto>> GetPlayerOngoingTickets()
        {
            try
            {
                int userId = GetAuthorizedUserId();
                return _ticketService.GetOngoingSesssionTickets(userId);
            }
            catch (TicketNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<TicketDto>> GetAllTickets()
        {
            try
            {
                return _ticketService.GetAllTickets();
            }
            catch (TicketNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete ("{id}")]
        public IActionResult DeleteTicket(int id) 
        {
            try
            {
                int userId = GetAuthorizedUserId();
                _ticketService.DeleteTicket(id, userId);
                return Ok($"Ticket with id: {id} was successfully deleted!");
            }
            catch (TicketNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TicketDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        [Authorize (Roles = "Admin")]
        [HttpGet("{id}")]
        public ActionResult<TicketDto> GetTicketById(int id)
        {
            try
            {
                TicketDto ticketDto = _ticketService.GetById(id);
                return Ok(ticketDto);
            }
            catch (TicketNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public ActionResult<UpdateTicketDto> UpdateTicket([FromBody] UpdateTicketDto updateTicketDto)
        {
            try
            {
                TicketDto updatedTicket = _ticketService.UpdateTicket(updateTicketDto);

                return Ok(updatedTicket);
            }
            catch (TicketNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TicketDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        



        private int GetAuthorizedUserId()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?
                .Value, out var userId))
            {
                string name = User.FindFirst(ClaimTypes.Name)?.Value;
                throw new Exception($"{name} identifier claim does not exist!");
            }
            return userId;
        }

        

    }
}
