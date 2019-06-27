using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackBusinessLogic.Interfaces.Models
{
    public interface IAce : ICard
    {
        int SpecialCost {get;set;}
        bool IsSpecialOn { get; set; }

        int GetSpecialCostDifference();
    }
}
