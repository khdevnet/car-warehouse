using CW.Domain;
using CW.WebApi.Services.Authentication;
using System;

namespace CW.WebApi.Abstractions.Services.Authentication
{
   public interface IAuthenticationService
    {
        (string, User) Authenticate(LoginUserRequestModel user);
    }
}
