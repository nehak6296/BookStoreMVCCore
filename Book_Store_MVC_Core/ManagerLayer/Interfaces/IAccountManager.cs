﻿using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interfaces
{
    public interface IAccountManager
    {
        Register RegisterUser(Register registrationModel);
        bool LoginUser(Login loginModel);

    }
}
