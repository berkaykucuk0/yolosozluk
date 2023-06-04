using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.IRepositories;
using YoloSozluk.Common;
using YoloSozluk.Common.Exceptions.User;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Handlers.CommandHandlers.Entry
{
    public class EntryCommentCreateCommandHandler : IRequestHandler<EntryCommentCreateCommand, Guid>
    {
        private readonly IEntryCommentRepository _commentRepo;
        private readonly IMapper _mapper;

        public EntryCommentCreateCommandHandler(IEntryCommentRepository commentRepo, IMapper mapper)
        {
            _commentRepo = commentRepo;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(EntryCommentCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    throw new EntryException("Request cannot be null!");

                var comment = _mapper.Map<Domain.Entities.EntryComment>(request);
                await _commentRepo.AddAsync(comment);

                return comment.Id;
            }
            catch (Exception ex)
            {
                LoggingExtension.YoloErrorLog(ex, nameof(EntryCommentCreateCommandHandler), request);
                throw;
            }
        }
    }
}
