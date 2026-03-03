using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotBranchAPI.Business.Utils.Exceptions
{
    public class EdgeNotFoundException : Exception
    {
        public EdgeNotFoundException() { }

        public EdgeNotFoundException(string message) : base(message) { }

        public EdgeNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
