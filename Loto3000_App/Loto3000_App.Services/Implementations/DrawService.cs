using Loto3000_App.DataAcess.Implementations;
using Loto3000_App.DataAcess.Interfaces;
using Loto3000_App.Domain;
using Loto3000_App.DTOs.WinnerDtos;
using Loto3000_App.Mappers;
using Loto3000_App.Services.Interfaces;
using Loto3000_App.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.Services.Implementations
{
    public class DrawService : IDrawService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IRepository<Combination> _combinationRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWinnerRepository _winnerRepository;

        public DrawService(ISessionRepository sessionRepository, IRepository<Combination> combinationRepository, ITicketRepository ticketRepository, IUserRepository userRepository, IWinnerRepository winnerRepository)
        {
            _combinationRepository = combinationRepository;
            _sessionRepository = sessionRepository;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _winnerRepository = winnerRepository;
        }
        public List<WinnerDto> GetAllWinners()
        {
            List<Winner> allWinners = _winnerRepository.GetAll();
            if(allWinners == null)
            {
                throw new WinnerNotFoundException("No winner was found!");
            }
            return allWinners.Select(x=>x.ToWinnerDto()).ToList();
        }

        public List<WinnerDto> GetLastSessionWinners()
        {
            Session lastExpiredSession = _sessionRepository.GetLastExpiredSession();
            if(lastExpiredSession == null)
            {
                throw new DrawDataException("Last expired session was not found!");
            }
            List<Winner> allSessionWinners = _winnerRepository.GetBySession(lastExpiredSession.Id);
            if(allSessionWinners == null)
            {
                throw new WinnerNotFoundException("No Winners were found for the last expired Session");
            }
            return allSessionWinners.Select(x => x.ToWinnerDto()).ToList();
        }

        public List<WinnerDto> StartDraw()
        {
            //first close the ongoing session
            Session ongoingSession = _sessionRepository.GetOngoingSession();
            if (ongoingSession == null) 
            {
                throw new DrawDataException("There was no ongoing Session");
            }
            if (ongoingSession.EndDate > DateTime.Now)
            {
                throw new DrawDataException("you cant end a session before its end date!");
            }
            ongoingSession.Ongoing = false;
            _sessionRepository.Update(ongoingSession);

            // draw the numbers
            List<int> drawnNumbers = DrawSessionWinningNumbers();

            //get all Tickets from the current session
            List<Ticket> allSessionTickets = _ticketRepository.GetAllSessionTickets(ongoingSession.Id);


            //compare combinations with drawn numbers
            foreach (var ticket in allSessionTickets)
            {
                foreach (var combination in ticket.Combinations)
                {
                    List<int> combinationNumbers = CombinationNumbersToList(combination);

                    int matchingNumbers = CountMatchingNumbers(drawnNumbers, combinationNumbers);

                    switch (matchingNumbers)
                    {
                        case 3:
                            {
                                _winnerRepository.Add(new Winner
                                {
                                    User = ticket.User,
                                    UserId = ticket.UserId,
                                    Session = ticket.Session,
                                    SessionId = ticket.SessionId,
                                    Prize = Domain.Enums.PrizeEnum.GiftCard50,
                                    TicketNumber = ticket.TicketNumber,
                                    WinningCombination = combination,
                                    WinningCombinationId = combination.Id
                                });
                                break;
                            }
                        case 4:
                            {
                                _winnerRepository.Add(new Winner
                                {
                                    User = ticket.User,
                                    UserId = ticket.UserId,
                                    Session = ticket.Session,
                                    SessionId = ticket.SessionId,
                                    Prize = Domain.Enums.PrizeEnum.GiftCard100,
                                    TicketNumber = ticket.TicketNumber,
                                    WinningCombination = combination,
                                    WinningCombinationId = combination.Id

                                });
                                break;
                            }
                        case 5:
                            {
                                _winnerRepository.Add(new Winner
                                {
                                    User = ticket.User,
                                    UserId = ticket.UserId,
                                    Session = ticket.Session,
                                    SessionId = ticket.SessionId,
                                    Prize = Domain.Enums.PrizeEnum.TV,
                                    TicketNumber = ticket.TicketNumber,
                                    WinningCombination = combination,
                                    WinningCombinationId = combination.Id
                                });
                                break;
                            }
                        case 6:
                            {
                                _winnerRepository.Add(new Winner
                                {
                                    User = ticket.User,
                                    UserId = ticket.UserId,
                                    Session = ticket.Session,
                                    SessionId = ticket.SessionId,
                                    Prize = Domain.Enums.PrizeEnum.Vacation,
                                    TicketNumber = ticket.TicketNumber,
                                    WinningCombination = combination,
                                    WinningCombinationId = combination.Id
                                });
                                break;
                            }
                        case 7:
                            {
                                _winnerRepository.Add(new Winner
                                {
                                    User = ticket.User,
                                    UserId = ticket.UserId,
                                    Session = ticket.Session,
                                    SessionId = ticket.SessionId,
                                    Prize = Domain.Enums.PrizeEnum.Car,
                                    TicketNumber = ticket.TicketNumber,
                                    WinningCombination = combination,
                                    WinningCombinationId = combination.Id
                                });
                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }
                }
            }

            //add the winners to the session winnerslist
            List<Winner> sessionWinners = _winnerRepository.GetBySession(ongoingSession.Id);
            //ongoingSession.WinnerList = sessionWinners;
            //_sessionRepository.Update(ongoingSession);

            //create new session
            Session newSession = new Session
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                Ongoing = true
            };
            _sessionRepository.Add(newSession);

            return sessionWinners.Select(x=>x.ToWinnerDto()).ToList();
        }


        private List<int> DrawSessionWinningNumbers()
        {
            Random rnd = new Random();
            int minValue = 1;
            int maxValue = 37;
            int numberOfDrawnNumbers = 8;

            List<int> WinningNumbers = new List<int>();

            while (WinningNumbers.Count < numberOfDrawnNumbers)
            {
                int randomNumber = rnd.Next(minValue,maxValue+1);
                if(!WinningNumbers.Contains(randomNumber))
                {
                    WinningNumbers.Add(randomNumber);
                }
            }

            return WinningNumbers;
        }

        private List<int> CombinationNumbersToList(Combination combination)
        {
            return new List<int>
            {
                combination.Number1,
                combination.Number2,
                combination.Number3,
                combination.Number4,
                combination.Number5,
                combination.Number6,
                combination.Number7
            };
        }
        private int CountMatchingNumbers (List<int> list1, List<int> list2)
        {
            int count = 0;
            foreach (int n in list1)
            {
                if (list2.Contains(n))
                {
                    count++;
                }
            }
            return count;
        }

    }
}
