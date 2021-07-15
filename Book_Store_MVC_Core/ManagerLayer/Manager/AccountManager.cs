using ManagerLayer.Interfaces;
using ModelsLayer;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Manager
{
    public class AccountManager : IAccountManager
    {
        private readonly IAccountRepo accountRepo;

        public AccountManager(IAccountRepo accountRepo)
        {
            this.accountRepo = accountRepo;
        }

        public bool LoginUser(Login loginModel)
        {
            return this.accountRepo.LoginUser(loginModel);
        }

        public Register RegisterUser(Register registrationModel)
        {
            return this.accountRepo.RegisterUser(registrationModel);
        }

    }

}
