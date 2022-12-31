using System;
using YoloSozluk.Common.Enums;

namespace YoloSozluk.Common.Events
{
    public class EntryCommentVoteCreateEvent
    {
        public Guid EntryCommentId { get; set; }
        public VoteType VoteType { get; set; }
        public Guid UserId { get; set; }
    }
}
