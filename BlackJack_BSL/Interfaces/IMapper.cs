﻿using System;

namespace BlackJack_BSL.Interfaces
{
    public interface IMapper<BSL,DA>
    {
        DA ConvertItemToDataAccess(BSL item);
        BSL ConvertItemToBusinessLogic(DA item);
    }
}