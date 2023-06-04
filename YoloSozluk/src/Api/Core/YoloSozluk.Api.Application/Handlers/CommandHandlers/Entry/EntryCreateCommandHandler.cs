using AutoMapper;
using MediatR;
using Serilog;
using Serilog.Context;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.IRepositories;
using YoloSozluk.Common;
using YoloSozluk.Common.Exceptions.User;
using YoloSozluk.Common.Models.Commands;


namespace YoloSozluk.Api.Application.Handlers.CommandHandlers.Entry
{
    public class EntryCreateCommandHandler : IRequestHandler<EntryCreateCommand, Guid>
    {
        private readonly IEntryRepository _entryRepo;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EntryCreateCommandHandler(IEntryRepository entryRepo, IMapper mapper, ILogger logger)
        {
            _entryRepo = entryRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> Handle(EntryCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new EntryException("Entry cannot be null!");

                var entry = _mapper.Map<Domain.Entities.Entry>(request);

                await _entryRepo.AddAsync(entry);

                return entry.Id;
            }
            catch (Exception ex)
            {
                LoggingExtension.YoloErrorLog(ex, nameof(EntryCreateCommandHandler), request);
                throw;
            }
        }
    }
}
