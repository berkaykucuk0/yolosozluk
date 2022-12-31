using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloSozluk.Common.Models.Commands
{
    public class EntryFavoriteCreateCommand:IRequest<bool>
    {              
        public Guid? EntryId { get; set; }
        public Guid? UserId { get; set; }

        public EntryFavoriteCreateCommand(Guid? entryId, Guid? userId)
        {
            EntryId = entryId;
            UserId = userId;
        }
    }
}
