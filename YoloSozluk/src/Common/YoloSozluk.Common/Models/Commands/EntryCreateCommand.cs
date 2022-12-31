using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloSozluk.Common.Models.Commands
{
    public class EntryCreateCommand: IRequest<Guid>
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public Guid CreatedUserId { get; set; }

        public EntryCreateCommand()
        {

        }
        public EntryCreateCommand(string subject, string content, Guid createdUserId)
        {
            Subject = subject;
            Content = content;
            CreatedUserId = createdUserId;
        }
    }
}
