using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.Shared
{
    public class DrawDataException : Exception
    {
        public DrawDataException(string message) :base(message)
        {
            
        }
    }
}
