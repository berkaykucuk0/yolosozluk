using MediatR;
using System;

namespace YoloSozluk.Common.Models.Commands
{
    public class EntryCommentVoteDeleteCommand : IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }

        public EntryCommentVoteDeleteCommand(Guid entryCommentId, Guid userId)
        {
            EntryCommentId = entryCommentId;
            UserId = userId;
        }
    }
}
