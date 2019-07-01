using System;

namespace BlackJackBusinessLogic.Interfaces
{
    public interface IMapper<BusinessLogic,DataAccess>
    {
        DataAccess ConvertItemToDataAccess(BusinessLogic item);
        BusinessLogic ConvertItemToBusinessLogic(DataAccess item);
    }
}
