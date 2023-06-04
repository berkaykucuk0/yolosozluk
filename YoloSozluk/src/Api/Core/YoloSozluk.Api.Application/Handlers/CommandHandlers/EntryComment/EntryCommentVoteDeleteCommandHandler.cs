using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Common;
using YoloSozluk.Common.Events;
using YoloSozluk.Common.Infrastructure;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Handlers.CommandHandlers.EntryComment
{
    public class EntryCommentVoteDeleteCommandHandler : IRequestHandler<EntryCommentVoteDeleteCommand, bool>
    {
        public async Task<bool> Handle(EntryCommentVoteDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                QueueFactory.SendMessageToExchange(exchangeName: Constants.EntryVoteExchangeName,
                                                   exchangeType: Constants.ExchangeType,
                                                   queueName: Constants.EntryCommentVoteDeleteQueueName,
                                                   obj: new EntryCommentVoteDeleteEvent()
                                                   {
                                                       EntryCommentId = request.EntryCommentId,
                                                       UserId = request.UserId
                                                   });

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                LoggingExtension.YoloErrorLog(ex, nameof(EntryCommentVoteDeleteCommandHandler), request);
                throw;
            }
        }
    }
}
