using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using YoloSozluk.Common.Models.ViewModels;

namespace YoloSozluk.Common.Models.Commands
{
    public class UserLoginCommand: IRequest<UserLoginViewModel>
    {
        public string EmailAddress { get;  set; }
        public string Password { get;  set; }

        public UserLoginCommand(string emailAddress ,string password)
        {
            Password = password;
            EmailAddress = emailAddress;
        }

        public UserLoginCommand()
        {

        }
    }
}
