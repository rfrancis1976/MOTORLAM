using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using inercya.ORMLite;
using System.Web.Mvc;
using Motorlam.Entities;
using Motorlam.Data;

namespace Motorlam.Services
{ 
    public class AuthorizationManager
    {
        public DataService DataService { get; private set; }

        public AuthorizationManager(DataService MotorlamDataService)
        {
            this.DataService = MotorlamDataService;
        }

        public User CurrentUser
        {
            get
            {
                return this.DataService.UserRepository.GetCurrentUser();
            }
        }   
    }   
}