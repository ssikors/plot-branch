using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotBranchAPI.Business.Utils.Exceptions
{
    public class PlotFlowNotFoundException : Exception
    {
        public PlotFlowNotFoundException() { }

        public PlotFlowNotFoundException(string message) : base(message) { }

        public PlotFlowNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
