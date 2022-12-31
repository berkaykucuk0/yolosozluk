using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.IRepositories;
using YoloSozluk.Common.Exceptions.User;
using YoloSozluk.Common.Infrastructure;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Handlers.CommandHandlers.User
{
    public class UserChangePasswordCommandHandler : IRequestHandler<UserChangePasswordCommand, bool>
    {
        private readonly IUserRepository _userRepo;

        public UserChangePasswordCommandHandler(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<bool> Handle(UserChangePasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.UserId.HasValue)
                throw new ArgumentException(nameof(request.UserId));

            var user = await _userRepo.GetByIdAsync(request.UserId.Value);

            if (user is null)
                throw new UserException("User not found!");

            string hashedPassword = Encryptor.Encrypt(request.OldPassword);
            if (user.Password != hashedPassword)
                throw new UserException("Wrong Old Password!");

            user.Password = hashedPassword;

            await _userRepo.UpdateAsync(user);

            return true;
        }
    }
}
