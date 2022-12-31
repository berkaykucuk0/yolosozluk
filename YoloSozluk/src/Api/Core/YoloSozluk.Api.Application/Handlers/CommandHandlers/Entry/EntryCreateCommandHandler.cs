using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.IRepositories;
using YoloSozluk.Common.Exceptions.User;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Handlers.CommandHandlers.Entry
{
    public class EntryCreateCommandHandler : IRequestHandler<EntryCreateCommand, Guid>
    {
        private readonly IEntryRepository _entryRepo;
        private readonly IMapper _mapper;

        public EntryCreateCommandHandler(IEntryRepository entryRepo, IMapper mapper)
        {
            _entryRepo = entryRepo;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(EntryCreateCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new EntryException("Entry cannot be null!");

            var entry = _mapper.Map<Domain.Entities.Entry>(request);

            await _entryRepo.AddAsync(entry);

            return entry.Id;
        }
    }
}
