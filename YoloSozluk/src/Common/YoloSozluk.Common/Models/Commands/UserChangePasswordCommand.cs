using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloSozluk.Common.Models.Commands
{
    public class UserChangePasswordCommand: IRequest<bool>
    {
        public Guid? UserId  { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordRepeat { get; set; }

        public UserChangePasswordCommand(Guid? userId, string oldPassword, string newPassword, string newPasswordRepeat )
        {
            UserId = userId;
            OldPassword = oldPassword;
            NewPassword = newPassword;
            NewPasswordRepeat = newPasswordRepeat;
        }
    }
}
