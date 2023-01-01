using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloSozluk.Common.Models.Commands
{
    public class UserConfirmEmailCommand : IRequest<bool>
    {
        public Guid ConfirmationId  { get; set; }
    }
}
