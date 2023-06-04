using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.IRepositories;
using YoloSozluk.Common;
using YoloSozluk.Common.Exceptions.User;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Handlers.CommandHandlers.User
{
    public class UserConfirmEmailCommandHandler : IRequestHandler<UserConfirmEmailCommand, bool>
    {
        private readonly IUserRepository _userRepo;
        private readonly IEmailConfirmationRepository _confRepo;

        public UserConfirmEmailCommandHandler(IUserRepository userRepo, IEmailConfirmationRepository confRepo)
        {
            _userRepo = userRepo;
            _confRepo = confRepo;
        }

        public async Task<bool> Handle(UserConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var confirmation = await _confRepo.GetByIdAsync(request.ConfirmationId);

                if (confirmation is null)
                    throw new UserMailConfirmationException("Confirmation not found!");

                var user = await _userRepo.GetSingleAsync(x => x.Email == confirmation.NewEmailAdress);

                if (user is null)
                    throw new UserException("User not found!");

                if (user.EmailConfirmed)
                    throw new UserMailConfirmationException("Email address is already confirmed!");

                user.EmailConfirmed = true;

                await _userRepo.UpdateAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                LoggingExtension.YoloErrorLog(ex, nameof(UserConfirmEmailCommandHandler), request);
                throw;
            }
        }
    }
}
