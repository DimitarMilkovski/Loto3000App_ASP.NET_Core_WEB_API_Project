using Loto3000_App.DTOs.WinnerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.Services.Interfaces
{
    public interface IDrawService
    {
        List<WinnerDto> StartDraw();
        List<WinnerDto> GetAllWinners();
        List<WinnerDto> GetLastSessionWinners();
    }
}
