using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class GetEntriesQueryHandler : IRequestHandler<GetEntriesQuery, List<GetEntriesViewModel>>
    {
        private readonly IEntryRepository _entryRepo;
        private readonly IMapper _mapper;

        public GetEntriesQueryHandler(IEntryRepository entryRepo, IMapper mapper)
        {
            _entryRepo = entryRepo;
            _mapper = mapper;
        }

        public async Task<List<GetEntriesViewModel>> Handle(GetEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = _entryRepo.AsQueryAble();
            if (request.TodaysEntries)
            {
                query = query.Where(x => x.CreateDate >= DateTime.Now.Date)
                             .Where(x => x.CreateDate <= DateTime.Now.AddDays(1).Date);
            }

            query = query.Include(x => x.EntryComments) 
                 .OrderBy(x => x.CreateDate)
                 .Take(request.Count);

            //Projectto sql den select alırken GetEntriesViewModel içerisindeki alanları select etmek amacıyla , hepsini yapmıyor
            return await query.ProjectTo<GetEntriesViewModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
