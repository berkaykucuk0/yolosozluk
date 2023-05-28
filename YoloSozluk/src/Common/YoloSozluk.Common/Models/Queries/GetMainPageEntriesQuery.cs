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
    public class GetMainPageEntriesQuery: BasePageQuery , IRequest<PagedViewModel<GetEntryDetailViewModel>>
    {
        public GetMainPageEntriesQuery(Guid? userId , int page, int pageSize) : base(page,pageSize)
        {
            UserId = userId;
        }

        public Guid? UserId  { get; set; }
    }
}
