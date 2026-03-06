using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotBranchAPI.Business.Utils.Exceptions
{
    public class NodeNotFoundException : Exception
    {
        public NodeNotFoundException() { }

        public NodeNotFoundException(string message) : base(message) { }

        public NodeNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
