using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Common;
using YoloSozluk.Common.Events;
using YoloSozluk.Common.Infrastructure;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Handlers.CommandHandlers
{
    public class EntryFavoriteCreateCommandHandler : IRequestHandler<EntryFavoriteCreateCommand, bool>
    {
        public async Task<bool> Handle(EntryFavoriteCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                QueueFactory.SendMessageToExchange(exchangeName: Constants.FavoriteExchangeName,
                                              exchangeType: Constants.ExchangeType,
                                              queueName: Constants.EntryCommentFavoriteCreateQueueName,
                                              obj: new EntryFavoriteCreateEvent()
                                              {
                                                  EntryId = request.EntryId.Value,
                                                  UserId = request.UserId.Value
                                              });

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                LoggingExtension.YoloErrorLog(ex, nameof(EntryFavoriteCreateCommandHandler), request);
                throw;
            }

        }
    }
}
