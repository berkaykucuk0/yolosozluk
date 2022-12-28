using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.IRepositories;
using YoloSozluk.Common.Exceptions.User;
using YoloSozluk.Common.Infrastructure;
using YoloSozluk.Common.Models.Commands;
using YoloSozluk.Common.Models.ViewModels;

namespace YoloSozluk.Api.Application.Handlers.CommandHandlers
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, UserLoginViewModel>
    {

        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _conf;
        private readonly TokenGenerator _tokengenerator;
        public UserLoginCommandHandler(IUserRepository userRepo, IMapper mapper, IConfiguration conf, TokenGenerator tokengenerator)
        {
            _userRepo = userRepo;
            _mapper = mapper;   
            _conf = conf;
            _tokengenerator = tokengenerator;
        }

        public async Task<UserLoginViewModel> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetSingleAsync(x => x.Email == request.EmailAddress);

            if (user == null)
                throw new UserException("User not found!");

            var password = Encryptor.Encrypt(request.Password);

            if (user.Password != password)
                throw new UserException("Wrong email or password!");

            if (!user.EmailConfirmed)
                throw new UserException("Email address is not confirmed!");

            var result = _mapper.Map<UserLoginViewModel>(user);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
            };

            result.Token = _tokengenerator.GenerateToken(claims);
            return result;
        }
    }
}
