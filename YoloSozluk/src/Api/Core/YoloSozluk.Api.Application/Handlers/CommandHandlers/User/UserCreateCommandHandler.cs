using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.IRepositories;
using YoloSozluk.Common.Exceptions.User;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Handlers.CommandHandlers.User
{
    //BOOL olayı değişip ortak bir base response dönülmeli
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;
        public UserCreateCommandHandler(IMapper mapper, IUserRepository userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }

        public async Task<bool> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var existUser = _userRepo.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (existUser != null)
                throw new UserException("User already exist with this email!");

            var user = _mapper.Map<Domain.Entities.User>(request);
            var response = await _userRepo.AddAsync(user);

            return true;

            //EMAIL CONFIRMATION WILL ADD
        }
    }
}
