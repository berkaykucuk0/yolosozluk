using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Common.Enums;

namespace YoloSozluk.Common.Events
{
    public class EntryVoteCreateEvent
    {
        public Guid EntryId { get; set; }
        public VoteType VoteType { get; set; }
        public Guid UserId { get; set; }
    }
}
