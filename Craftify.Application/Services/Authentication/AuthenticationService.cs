using Craftify.Application.Common.Interfaces.Authentication;
using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Services.Authentication
{
    public class AuthenticationService(
        IJwtTokenGenerator _jwtTokenGenerator,
        IUserRepository _userRepository
        ) : IAuthenticationService
    {

        public AuthenticationResult Login(string email, string password)
        {
            //1. validate user does exist
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("user with given email does not exist");
            }
            //2. Validate the password is correct

            if(user.Password != password)
            {
                throw new Exception("Invalid password");
            }
            //3. Create JWT token
            string token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token
                );
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {

            //1. Validate the user doesn't exist
            if(_userRepository.GetUserByEmail(email) is not null)
            {
                throw new Exception("user with given email already exists");
            }
            //2. Create user (generat unique ID) & Presist to DB
            User user = new()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = password
            };
            _userRepository.Add(user);
            //3. Create JWT token
            string token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token
                );
        }
    }
}
