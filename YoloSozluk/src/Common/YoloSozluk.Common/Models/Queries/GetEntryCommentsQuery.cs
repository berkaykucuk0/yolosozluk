using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Common.Models.Pages;
using YoloSozluk.Common.Models.ViewModels;

namespace YoloSozluk.Common.Models.Queries
{
    public class GetEntryCommentsQuery: BasePageQuery, IRequest<PagedViewModel<GetEntryCommentsViewModel>>
    {
        public GetEntryCommentsQuery(Guid entryId, Guid? userId,int page, int pageSize) : base(page, pageSize)
        {
            EntryId = entryId;
            UserId = userId;
        }

        public Guid EntryId { get; set; }
        public Guid? UserId { get; set; }
    }
}
