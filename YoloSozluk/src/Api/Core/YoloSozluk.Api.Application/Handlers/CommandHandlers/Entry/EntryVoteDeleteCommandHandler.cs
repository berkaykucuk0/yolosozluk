using MediatR;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Common;
using YoloSozluk.Common.Events;
using YoloSozluk.Common.Infrastructure;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Handlers.CommandHandlers.Entry
{
    public class EntryVoteDeleteCommandHandler : IRequestHandler<EntryVoteDeleteCommand, bool>
    {
        public async Task<bool> Handle(EntryVoteDeleteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: Constants.EntryVoteExchangeName,
                                                   exchangeType: Constants.ExchangeType,
                                                   queueName: Constants.EntryVoteCreateQueueName,
                                                   obj: new EntryVoteDeleteEvent()
                                                   {
                                                       EntryId = request.EntryId,
                                                       UserId = request.UserId,
                                                   });

            return await Task.FromResult(true);
        }
    }
}
