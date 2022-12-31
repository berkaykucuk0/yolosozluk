using MediatR;
using System;

namespace YoloSozluk.Common.Models.Commands
{
    public class EntryVoteDeleteCommand : IRequest<bool>
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }

        public EntryVoteDeleteCommand(Guid entryId,  Guid userId)
        {
            EntryId = entryId;
            UserId = userId;
        }
    }
}
