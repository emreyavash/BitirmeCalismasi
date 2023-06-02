using ETicaret.Identities.DTOs;
using ETicaret.Identities.Repositories.Interface;
using ETicaret.Identities.Security.Hashing;
using ETicaret.Identities.Security.JWT;
using ETicaret.Users.Entities;
using ETicaret.Users.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text.Unicode;

namespace ETicaret.Identities.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;

        public AuthRepository(IUserRepository userRepository, ITokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }

        public AccessToken CreateAccessToken(User user)
        {
            var accessToken =_tokenHelper.CreateToken(user);
            return accessToken;
        }

        public async Task<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await _userRepository.GetUserByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return null;
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash,userToCheck.PasswordSalt))
            {
                return null;
            }
            return userToCheck;
        }

        public async Task<User> RegisterAsync(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
           
            HashingHelper.CreatePasswordHash(password,out passwordHash,out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            await _userRepository.AddUser(user);
            return user;
        }

        public async Task<bool> UserExists(string email)
        {
            var userCheck = await _userRepository.GetUserByMail(email);
            if (userCheck != null)
            {
                return false;
            }
            return true; 
        }
        
    }
}
