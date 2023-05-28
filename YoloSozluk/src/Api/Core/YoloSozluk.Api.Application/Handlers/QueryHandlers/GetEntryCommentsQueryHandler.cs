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
    public class GetEntryCommentsQueryHandler : IRequestHandler<GetEntryCommentsQuery, PagedViewModel<GetEntryCommentsViewModel>>
    {
        private readonly IEntryCommentRepository _entryCommentRepo;

        public GetEntryCommentsQueryHandler(IEntryCommentRepository entryCommentRepo)
        {
            _entryCommentRepo = entryCommentRepo;
        }
        public async Task<PagedViewModel<GetEntryCommentsViewModel>> Handle(GetEntryCommentsQuery request, CancellationToken cancellationToken)
        {
            var query = _entryCommentRepo.AsQueryAble();
            query = query.Include(x => x.EntryCommentFavourites)
                         .Include(x => x.CreatedBy)
                         .Include(x => x.EntryCommentVotes)
                         .Where(x=>x.EntryId == request.EntryId);

            var list = query.Select(x => new GetEntryCommentsViewModel
            {
                Id = x.Id,
                Content = x.Content,
                IsFavorited = request.UserId.HasValue && x.EntryCommentFavourites.Any(y => y.CreatedById == request.UserId),
                FavoritedCount = x.EntryCommentFavourites.Count,
                CreatedDate = x.CreateDate,
                CreatedByUserName = x.CreatedBy.UserName,
                VoteType = request.UserId.HasValue &&
                            x.EntryCommentVotes.Any(y => y.CreatedById == request.UserId) ? x.EntryCommentVotes.FirstOrDefault(y => y.CreatedById == request.UserId).VoteType : VoteType.None
            });

            var comments = await list.GetPaged(request.Page, request.PageSize);

            return comments;
        }
    }
}
