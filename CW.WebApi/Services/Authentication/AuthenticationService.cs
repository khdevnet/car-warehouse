using CW.Domain;
using CW.WebApi.Abstractions.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace CW.WebApi.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtTokenProvider _jwtTokenProvider;
        private List<User> appUsers = new List<User>
        {
            new User {  FullName = "Admin",  UserName = "admin", Password = "admin", UserRole = "Admin" },
            new User {  FullName = "Test User",  UserName = "user", Password = "user", UserRole = "User" }
        };

        public AuthenticationService(JwtTokenProvider jwtTokenProvider)
        {
            _jwtTokenProvider = jwtTokenProvider;
        }

        public (string, User) Authenticate(LoginUserRequestModel login)
        {
            User user = AuthenticateUser(login);
            if (user != null)
            {
                var token = (_jwtTokenProvider.Generate(user), user);
                return token;
            }

            throw new AuthenticationException();
        }

        private User AuthenticateUser(LoginUserRequestModel loginCredentials)
        {
            User user = appUsers.SingleOrDefault(x => x.UserName == loginCredentials.UserName && x.Password == loginCredentials.Password);
            return user;
        }
    }
}
