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
    public class EntryCommentFavoriteDeleteCommandHandler : IRequestHandler<EntryCommentFavoriteDeleteCommand, bool>
    {
        public async Task<bool> Handle(EntryCommentFavoriteDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                QueueFactory.SendMessageToExchange(exchangeName: Constants.FavoriteExchangeName,
                                                     exchangeType: Constants.ExchangeType,
                                                     queueName: Constants.EntryCommentFavoriteDeleteQueueName,
                                                     obj: new EntryCommentFavoriteDeleteEvent()
                                                     {
                                                         EntryCommentId = request.EntryCommentId,
                                                         UserId = request.UserId
                                                     });

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                LoggingExtension.YoloErrorLog(ex, nameof(EntryCommentFavoriteDeleteCommandHandler), request);
                throw;
            }
        }
    }
}
