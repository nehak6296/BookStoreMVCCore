using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{    
     public interface IAccountRepo
    {
        bool LoginUser(Login loginModel);
        Register RegisterUser(Register registrationModel);
    }
}
