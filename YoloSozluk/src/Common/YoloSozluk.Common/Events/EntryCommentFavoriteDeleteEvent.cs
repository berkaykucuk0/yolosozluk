using System;

namespace YoloSozluk.Common.Events
{
    public class EntryCommentFavoriteDeleteEvent
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
    }
}
