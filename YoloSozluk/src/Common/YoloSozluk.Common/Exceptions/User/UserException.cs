using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.Dtos;
using YoloSozluk.Common.Exceptions.Base;

namespace YoloSozluk.Common.Exceptions.User
{
	public class UserException : ExceptionBase
	{
		public UserException(string message) : base(message)
		{
		}

		public UserException(IEnumerable<ErrorBaseDto> errors, string message) : base(errors, message)
		{
		}
	}
}
