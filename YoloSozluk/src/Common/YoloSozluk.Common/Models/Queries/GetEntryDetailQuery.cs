using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Common.Models.ViewModels;

namespace YoloSozluk.Common.Models.Queries
{
    public class GetEntryDetailQuery: IRequest<GetEntryDetailViewModel>
    {
        public Guid EntryId { get; set; }
        public Guid? UserId { get; set; }

        public GetEntryDetailQuery(Guid entryId, Guid? userId)
        {
            EntryId = entryId;
            UserId = userId;
        }
    }
}
