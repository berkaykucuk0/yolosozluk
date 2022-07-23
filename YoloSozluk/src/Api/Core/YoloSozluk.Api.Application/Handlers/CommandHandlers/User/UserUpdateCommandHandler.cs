using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.IRepositories;
using YoloSozluk.Common;
using YoloSozluk.Common.Events;
using YoloSozluk.Common.Exceptions.User;
using YoloSozluk.Common.Infrastructure;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Handlers.CommandHandlers.User
{
    //BOOL olayı değişip ortak bir base response dönülmeli
    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;
        public UserUpdateCommandHandler(IMapper mapper, IUserRepository userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }

        public async Task<bool> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            
            var user = await _userRepo.GetByIdAsync(request.Id);
            var oldEmail = user.Email;

            if (user == null)
                throw new UserException("User not found!");

            _mapper.Map(request, user);
            var response = await _userRepo.UpdateAsync(user);

            if (response > 0 && user.Email != oldEmail)
            {
                var @event = new UserEmailChangedEvent
                {
                    NewEmail = user.Email,
                    OldEmail = oldEmail
                };

                QueueFactory.SendMessageToExchange(exchangeName: Constants.UserEmailChangedExchangeName,
                                                   exchangeType: Constants.ExchangeType,
                                                   queueName: Constants.UserEmailChangedQueueName,
                                                   obj: @event);

                user.EmailConfirmed = false;
                await _userRepo.UpdateAsync(user);
            }



            return response > 0 ;

            //EMAIL CONFIRMATION WILL ADD
        }
    }
}
