using ETicaret.Identities.DTOs;
using ETicaret.Identities.Repositories.Interface;
using ETicaret.Identities.Security.Hashing;
using ETicaret.Identities.Security.JWT;
using ETicaret.Users.Entities;
using ETicaret.Users.Repositories.Interface;
using EventBusRabbitMQ.Core;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Mvc;
using System.Text.Unicode;

namespace ETicaret.Identities.Repositories
{
    public class AuthRepository 
        //: IAuthRepository
    {
        //private readonly IUserRepository _userRepository;
        //private readonly ITokenHelper _tokenHelper;
        //private readonly EventBusRabbitMQProducer _eventBus;

        //public AuthRepository(IUserRepository userRepository, ITokenHelper tokenHelper, EventBusRabbitMQProducer eventBus)
        //{
        //    _userRepository = userRepository;
        //    _tokenHelper = tokenHelper;
        //    _eventBus = eventBus;
        //}

        //public AccessToken CreateAccessToken(User user)
        //{
        //    var accessToken =_tokenHelper.CreateToken(user);
        //    return accessToken;
        //}

        //public async Task<User> Login(UserForLoginDto userForLoginDto)
        //{
        //    var userToCheck = await _userRepository.GetUserByMail(userForLoginDto.Email);
        //    if (userToCheck == null)
        //    {
        //        return null;
        //    }
        //    if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash,userToCheck.PasswordSalt))
        //    {
        //        return null;
        //    }
        //    return userToCheck;
        //}

        //public Task<UserCreateEvent> RegisterAsync(UserForRegisterDto userForRegisterDto, string password)
        //{
        //    byte[] passwordHash, passwordSalt;
           
        //    HashingHelper.CreatePasswordHash(password,out passwordHash,out passwordSalt);
        //    UserCreateEvent eventMessage = new UserCreateEvent();
        //    {
        //        eventMessage.Email = userForRegisterDto.Email;
        //        eventMessage.FirstName = userForRegisterDto.FirstName;
        //        eventMessage.LastName = userForRegisterDto.LastName;
        //        eventMessage.PasswordHash = passwordHash;
        //        eventMessage.PasswordSalt = passwordSalt;
        //        eventMessage.Status = true;
        //    };
        //    _eventBus.Publish(EventBusConstants.UserCreateQueue,eventMessage);
        //    return ;
        //}

        //public async Task<bool> UserExists(string email)
        //{
        //    var userCheck = await _userRepository.GetUserByMail(email);
        //    if (userCheck != null)
        //    {
        //        return false;
        //    }
        //    return true; 
        //}
        
    }
}
