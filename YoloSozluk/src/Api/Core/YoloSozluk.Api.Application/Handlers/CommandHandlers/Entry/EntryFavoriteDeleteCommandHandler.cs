using MediatR;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Common;
using YoloSozluk.Common.Events;
using YoloSozluk.Common.Infrastructure;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Handlers.CommandHandlers
{
    public class EntryFavoriteDeleteCommandHandler : IRequestHandler<EntryFavoriteDeleteCommand, bool>
    {
        public async Task<bool> Handle(EntryFavoriteDeleteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: Constants.FavoriteExchangeName,
                                                exchangeType: Constants.ExchangeType,
                                                queueName: Constants.EntryFavoriteDeleteQueueName,
                                                obj: new EntryFavoriteDeleteEvent()
                                                {
                                                    EntryId = request.EntryId,
                                                    UserId = request.UserId
                                                });

            return await Task.FromResult(true);
        }
    }
}
