using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.IRepositories;
using YoloSozluk.Api.Domain.Entities;
using YoloSozluk.Infrastructure.Persistence.Context;

namespace YoloSozluk.Infrastructure.Persistence.Repositories
{
    public class EntryRepository : GenericRepository<Entry>, IEntryRepository
    {
        public EntryRepository(YoloSozlukContext dbContext) : base(dbContext)
        {
        }
    }
}
