using System;

namespace BlackJack_BSL.Interfaces
{
    public interface IMapper<BSL,DA>
    {
        DA ConvertItemToDA(BSL item);
        BSL ConvertItemToBSL(DA item);
    }
}
