using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CW.WebApi.Services.Authentication
{
    public class LoginUserRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
