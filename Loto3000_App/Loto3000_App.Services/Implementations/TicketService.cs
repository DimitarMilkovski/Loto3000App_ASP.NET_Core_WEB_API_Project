using Loto3000_App.DataAcess.Interfaces;
using Loto3000_App.Domain;
using Loto3000_App.DTOs.TicketDtos;
using Loto3000_App.Mappers;
using Loto3000_App.Services.Interfaces;
using Loto3000_App.Shared;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISessionRepository _sessionRepository;

        public TicketService(ITicketRepository ticketRepository, IUserRepository userRepository, ISessionRepository sessionRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
        }


        public void CreateTicket(List<CombinationDto> combinations, int userId)
        {

            if (combinations.Count == 0)
            {
                throw new TicketDataException("There are no combinations added in the ticket!");
            }
            if(combinations.Count >10) 
            {
                throw new TicketDataException("Ticket cannot have more than 10 combinations");
            }
            bool validateCombinations = ValidateCombinations(combinations);
            if(!validateCombinations) 
            {
                throw new TicketDataException("The combinations must have 7 distinct numbers from 1 - 37!");
            }

            User userDb = _userRepository.GetById(userId);
            if(userDb == null)
            {
                throw new TicketDataException($"User with id{userId} was not found!");

            }

            Session onGoingSessionDb = _sessionRepository.GetOngoingSession();
            if(onGoingSessionDb == null)
            {
                throw new TicketDataException("There is no ongoing session at the moment!");
            }

            Ticket newTicket = new Ticket
            {
                User = userDb,
                UserId = userId,
                Session = onGoingSessionDb,
                SessionId = onGoingSessionDb.Id,
            };

            List<Combination> ticketsCombinations = new List<Combination>();
            foreach (CombinationDto combinationDto in combinations)
            {
                Combination newCombination = new Combination
                {
                    Ticket = newTicket,
                    Number1 = combinationDto.Number1,
                    Number2 = combinationDto.Number2,
                    Number3 = combinationDto.Number3,
                    Number4 = combinationDto.Number4,
                    Number5 = combinationDto.Number5,
                    Number6 = combinationDto.Number6,
                    Number7 = combinationDto.Number7,

                };
                ticketsCombinations.Add(newCombination);
            }
            newTicket.Combinations = ticketsCombinations;
            _ticketRepository.Add(newTicket);
        }


        public void DeleteTicket(int id, int userId)
        {
            Ticket ticketDb = _ticketRepository.GetById(id);
            User userDb = _userRepository.GetById(userId);


            if (ticketDb == null)
            {
                throw new TicketNotFoundException($"Ticket with id {id} does not exist!");
            }
            if (userDb == null)
            {
                throw new TicketDataException($"User with id{userId} was not found!");

            }
            if(ticketDb.UserId != userId)
            {
                throw new TicketDataException($"The ticket belongs to another player and cannot be deleted!");
            }


            _ticketRepository.Delete(ticketDb);
        }

        public List<TicketDto> GetAllTickets()
        {
            List<Ticket> allTicketsDb = _ticketRepository.GetAll();
            if (allTicketsDb.IsNullOrEmpty())
            {
                throw new TicketNotFoundException("There are no tickets yet!");
            }
            return allTicketsDb.Select(x=>x.ToTicketDto()).ToList();
        }

        public TicketDto GetById(int id)
        {
            Ticket TicketDb = _ticketRepository.GetById(id);
            if(TicketDb == null)
            {
                throw new TicketNotFoundException($"There is no ticket with id:{id}");
            }
            return TicketDb.ToTicketDto();
        }

        public List<TicketDto> GetOngoingSesssionTickets(int userId)
        {
            Session ongoingSession = _sessionRepository.GetOngoingSession();
            if(ongoingSession == null)
            {
                throw new TicketNotFoundException("There is no ongoing session!");
            }
            List<Ticket> playerOngoingTickets = _ticketRepository.GetByUserAndSessionId(userId, ongoingSession.Id).ToList();
            if (playerOngoingTickets.IsNullOrEmpty())
            {
                throw new TicketNotFoundException("There is no tickets for this session!");
            }

            return playerOngoingTickets.Select(x => x.ToTicketDto()).ToList();
        }

        public List<TicketDto> GetPlayersTickets(int userId)
        {
            List<Ticket> allPLayerTickets = _ticketRepository.GetByUserId(userId);
            if (allPLayerTickets.IsNullOrEmpty())
            {
                throw new TicketNotFoundException("There are no tickets yet!");
            }
            return allPLayerTickets.Select(x=>x.ToTicketDto()).ToList() ;
        }

        public TicketDto UpdateTicket(UpdateTicketDto updateTicketDto)
        {
            Ticket ticketDb = _ticketRepository.GetById(updateTicketDto.Id);
            if (ticketDb == null)
            {
                throw new TicketNotFoundException($"Ticket with id: {updateTicketDto.Id} was not found!");
            }

            User userDb = _userRepository.GetById(updateTicketDto.UserId);
            if (userDb == null)
            {
                throw new TicketDataException($"User with id: {updateTicketDto.Id} does not exist!");
            }

            Session ongoingSession = _sessionRepository.GetOngoingSession();
            if (ongoingSession.Id !=updateTicketDto.SessionId )
            {
                throw new TicketDataException("The ticket cannot be updated to expired session!");
            }

            ticketDb.User = userDb;
            ticketDb.UserId = userDb.Id;
            ticketDb.Session = ongoingSession;
            ticketDb.SessionId = ongoingSession.Id;

            List<Combination> updatedCombinations = updateTicketDto.Combinations.Select(x=>x.ToCombination(ticketDb)).ToList();

            ticketDb.Combinations = updatedCombinations;

            _ticketRepository.Update(ticketDb);
            TicketDto ticketDtoResult = ticketDb.ToTicketDto();
            return ticketDtoResult;

        }

        //Method that validates the combination numbers (integers from 1-37 and Distinct) return true or false
        private bool ValidateCombinations(List<CombinationDto> combinations)
        {
            foreach (CombinationDto combination in combinations)
            {

                PropertyInfo[] properties = typeof(CombinationDto).GetProperties();
                List<int> combinationNumbers = new List<int>();

                foreach (var property in properties)
                {
                    var value = property.GetValue(combination);

                    if (value == null)
                    {
                        return false;
                    }

                    if (int.TryParse(value.ToString(), out int combinationNumber))
                    {
                        if (combinationNumber <= 0 || combinationNumber > 37)
                        {
                            return false;
                        }
                        combinationNumbers.Add(combinationNumber);
                    }
                    else
                    {
                        return false;
                    }

                }
                if(combinationNumbers.Distinct().Count() != combinationNumbers.Count())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
