using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloSozluk.Common.Events
{
    public class EntryVoteDeleteEvent
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
    }
}
