using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Common;
using YoloSozluk.Common.Events;
using YoloSozluk.Common.Infrastructure;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Handlers.CommandHandlers.Entry
{
    public class EntryVoteCreateCommandHandler : IRequestHandler<EntryVoteCreateCommand, bool>
    {
        public async Task<bool> Handle(EntryVoteCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                QueueFactory.SendMessageToExchange(exchangeName: Constants.EntryVoteExchangeName,
                                                exchangeType: Constants.ExchangeType,
                                                queueName: Constants.EntryVoteCreateQueueName,
                                                obj: new EntryVoteCreateEvent()
                                                {
                                                    EntryId = request.EntryId,
                                                    UserId = request.UserId,
                                                    VoteType = request.VoteType
                                                });

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                LoggingExtension.YoloErrorLog(ex, nameof(EntryVoteCreateCommandHandler), request);
                throw;
            }
        }
    }
}
        