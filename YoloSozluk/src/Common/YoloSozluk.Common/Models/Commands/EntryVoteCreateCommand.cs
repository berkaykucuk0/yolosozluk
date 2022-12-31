using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Common.Enums;

namespace YoloSozluk.Common.Models.Commands
{
    public class EntryVoteCreateCommand : IRequest<bool>
    {
        public Guid EntryId { get; set; }
        public VoteType VoteType { get; set; }
        public Guid UserId { get; set; }

        public EntryVoteCreateCommand(Guid entryId, VoteType voteType, Guid userId)
        {
            EntryId = entryId;
            VoteType = voteType;
            UserId = userId;
        }
    }
}
