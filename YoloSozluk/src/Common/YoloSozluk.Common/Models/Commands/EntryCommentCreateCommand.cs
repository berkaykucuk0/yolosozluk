using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloSozluk.Common.Models.Commands
{
    public class EntryCommentCreateCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid EntryId { get; set; }
        public string Content { get; set; }

        public EntryCommentCreateCommand()
        {

        }
        public EntryCommentCreateCommand(Guid userId, Guid entryId, string content)
        {
            UserId = userId;
            EntryId = entryId;
            Content = content;
        }
    }
}
