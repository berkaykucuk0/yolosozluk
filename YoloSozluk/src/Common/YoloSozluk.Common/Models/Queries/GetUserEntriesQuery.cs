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
    public class GetUserEntriesQuery:  BasePageQuery , IRequest<PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        public GetUserEntriesQuery(Guid? userId,string userName = null ,int page = 1, int pageSize= 10) : base(page, pageSize)
        {
            UserId = userId;
            UserName = userName;
        }

        public Guid? UserId { get; set; }
        public string UserName { get; set; }
    }
}
