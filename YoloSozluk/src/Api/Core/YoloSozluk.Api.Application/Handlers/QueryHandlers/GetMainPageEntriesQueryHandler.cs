using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.IRepositories;
using YoloSozluk.Common.Enums;
using YoloSozluk.Common.Extensions;
using YoloSozluk.Common.Models.Queries;
using YoloSozluk.Common.Models.ViewModels;

namespace YoloSozluk.Api.Application.Handlers.QueryHandlers
{
    public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PagedViewModel<GetEntryDetailViewModel>>
    {
        private readonly IEntryRepository _entryRepo;

        public GetMainPageEntriesQueryHandler(IEntryRepository entryRepo)
        {
            _entryRepo = entryRepo;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = _entryRepo.AsQueryAble();
            query = query.Include(x => x.EntryComments)
                         .Include(x => x.EntryFavourites)
                         .Include(x => x.EntryVotes);

            var list = query.Select(x => new GetEntryDetailViewModel
            {
                 Id = x.Id,
                 Subject = x.Subject,
                 Content = x.Content,
                 IsFavorited = request.UserId.HasValue && x.EntryFavourites.Any(y=>y.CreatedById == request.UserId),
                 FavoritedCount = x.EntryFavourites.Count,
                 CreatedDate = x.CreateDate,
                 CreatedByUserName = x.CreatedBy.UserName,
                 VoteType = request.UserId.HasValue && 
                            x.EntryVotes.Any(y=>y.CreatedById == request.UserId) ? x.EntryVotes.FirstOrDefault(y=>y.CreatedById == request.UserId).VoteType : VoteType.None
            });

            var entries = await list.GetPaged(request.Page, request.PageSize);

            return entries;
        }
    }
}
