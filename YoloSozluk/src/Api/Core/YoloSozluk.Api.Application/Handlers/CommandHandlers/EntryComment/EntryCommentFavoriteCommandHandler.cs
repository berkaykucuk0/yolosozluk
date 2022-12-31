using MediatR;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Common;
using YoloSozluk.Common.Events;
using YoloSozluk.Common.Infrastructure;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Handlers.CommandHandlers.EntryComment
{
    public class EntryCommentFavoriteCommandHandler : IRequestHandler<EntryCommentFavoriteCommand, bool>
    {


        public async Task<bool> Handle(EntryCommentFavoriteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: Constants.FavoriteExchangeName,
                                                   exchangeType: Constants.ExchangeType,
                                                   queueName: Constants.EntryCommentFavoriteCreateQueueName,
                                                   obj: new EntryCommentFavoriteCreateEvent() { 
                                                             EntryCommentId = request.EntryCommentId,
                                                             UserId = request.UserId 
                                                   });

            return await Task.FromResult(true);
        }
    }
}
