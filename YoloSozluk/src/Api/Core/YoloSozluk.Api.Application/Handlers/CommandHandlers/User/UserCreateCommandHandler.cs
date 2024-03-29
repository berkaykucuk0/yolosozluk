﻿using AutoMapper;
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
            
            try
            {
                var existUser = await _userRepo.GetSingleAsync(x => x.Email == request.Email);

                if (existUser != null)
                    throw new UserException("User already exist with this email!");

                request.Password = Encryptor.Encrypt(request.Password);

                var user = _mapper.Map<Domain.Entities.User>(request);
                var response = await _userRepo.AddAsync(user);

                if (response > 0)
                {
                    var @event = new UserEmailChangedEvent
                    {
                        NewEmail = user.Email,
                        OldEmail = null
                    };

                    QueueFactory.SendMessageToExchange(exchangeName: Constants.UserEmailChangedExchangeName,
                                                       exchangeType: Constants.ExchangeType,
                                                       queueName: Constants.UserEmailChangedQueueName,
                                                       obj: @event);
                }

                return true;
            }
            catch (Exception ex)
            {
                LoggingExtension.YoloErrorLog(ex, nameof(UserCreateCommandHandler), request);
                throw;
            }
        }
    }
}
