using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloSozluk.Common.Models.Commands
{
    public class EntryCommentFavoriteDeleteCommand : IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }

        public EntryCommentFavoriteDeleteCommand(Guid entryCommentId, Guid userId)
        {
            EntryCommentId = entryCommentId;
            UserId = userId;
        }
    }
}
