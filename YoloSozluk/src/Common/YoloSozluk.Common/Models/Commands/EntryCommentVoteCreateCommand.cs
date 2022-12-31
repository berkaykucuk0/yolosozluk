using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Common.Enums;

namespace YoloSozluk.Common.Models.Commands
{
    public class EntryCommentVoteCreateCommand : IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public VoteType VoteType { get; set; }
        public Guid UserId { get; set; }

        public EntryCommentVoteCreateCommand(Guid entryCommentId, VoteType voteType, Guid userId)
        {
            EntryCommentId = entryCommentId;
            VoteType = voteType;
            UserId = userId;
        }
    }
}
