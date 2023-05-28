using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.IRepositories;
using YoloSozluk.Common.Models.Queries;
using YoloSozluk.Common.Models.ViewModels;

namespace YoloSozluk.Api.Application.Handlers.QueryHandlers
{
    public class SearchEntryQueryHandler : IRequestHandler<SearchEntryQuery, List<SearchEntryViewModel>>
    {
        private readonly IEntryRepository _entryRepo;

        public SearchEntryQueryHandler(IEntryRepository entryRepo)
        {
            _entryRepo = entryRepo;
        }
        public async Task<List<SearchEntryViewModel>> Handle(SearchEntryQuery request, CancellationToken cancellationToken)
        {

            //Burada text uzunluğu kontrol edilebilir.
            var result = _entryRepo.Get(x => EF.Functions.Like(x.Subject, $"{request.SearchText}%")).Select(x => new SearchEntryViewModel { Id = x.Id, Subject = x.Subject });

            return await result.ToListAsync();
        }
    }
}
