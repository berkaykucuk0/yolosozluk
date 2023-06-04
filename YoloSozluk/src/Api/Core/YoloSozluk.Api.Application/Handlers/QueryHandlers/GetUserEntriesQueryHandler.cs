using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.IRepositories;
using YoloSozluk.Common;
using YoloSozluk.Common.Exceptions.User;
using YoloSozluk.Common.Extensions;
using YoloSozluk.Common.Models.Queries;
using YoloSozluk.Common.Models.ViewModels;

namespace YoloSozluk.Api.Application.Handlers.QueryHandlers
{
    public class GetUserEntriesQueryHandler : IRequestHandler<GetUserEntriesQuery, PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        private readonly IEntryRepository _entryRepo;

        public GetUserEntriesQueryHandler(IEntryRepository entryRepo)
        {
            _entryRepo = entryRepo;
        }
        public async Task<PagedViewModel<GetUserEntriesDetailViewModel>> Handle(GetUserEntriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _entryRepo.AsQueryAble();

                if (request.UserId != null && request.UserId.HasValue && request.UserId != Guid.Empty)
                {
                    query = query.Where(x => x.CreatedById == request.UserId);
                }
                else if (!string.IsNullOrEmpty(request.UserName))
                {
                    query = query.Where(x => x.CreatedBy.UserName == request.UserName);
                }
                else
                {
                    throw new EntryException("UserId and UserName both cannot be null!");
                }

                query = query.Include(x => x.EntryFavourites).Include(x => x.CreatedBy);

                var list = query.Select(x => new GetUserEntriesDetailViewModel
                {
                    Id = x.Id,
                    Subject = x.Subject,
                    Content = x.Content,
                    IsFavorited = false,
                    FavoritedCount = x.EntryFavourites.Count,
                    CreatedDate = x.CreateDate,
                    CreatedByUserName = x.CreatedBy.UserName,
                });

                var entries = await list.GetPaged(request.Page, request.PageSize);
                return entries;

            }
            catch (Exception ex)
            {
                LoggingExtension.YoloErrorLog(ex, nameof(GetUserEntriesQueryHandler), request);
                throw;
            }
        }
    }
}
