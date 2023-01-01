using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.Dtos;
using YoloSozluk.Common.Exceptions.Base;

namespace YoloSozluk.Common.Exceptions.User
{
    public class UserMailConfirmationException:ExceptionBase
    {
		public UserMailConfirmationException(string message) : base(message)
		{
		}

		public UserMailConfirmationException(IEnumerable<ErrorBaseDto> errors, string message) : base(errors, message)
		{
		}
	}
}
