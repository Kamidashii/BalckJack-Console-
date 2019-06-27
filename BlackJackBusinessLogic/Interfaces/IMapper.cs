using System;

namespace BlackJackBusinessLogic.Interfaces
{
    public interface IMapper<BSL,DA>
    {
        DA ConvertItemToDataAccess(BSL item);
        BSL ConvertItemToBusinessLogic(DA item);
    }
}
