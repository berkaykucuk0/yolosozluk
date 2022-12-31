using MediatR;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Common;
using YoloSozluk.Common.Events;
using YoloSozluk.Common.Infrastructure;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Handlers
{
    public class EntryCommentVoteCreateCommandHandler : IRequestHandler<EntryCommentVoteCreateCommand, bool>
    {
        public async Task<bool> Handle(EntryCommentVoteCreateCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: Constants.EntryCommentVoteExchangeName,
                                                  exchangeType: Constants.ExchangeType,
                                                  queueName: Constants.EntryCommentVoteCreateQueueName,
                                                  obj: new EntryCommentVoteCreateEvent()
                                                  {
                                                      EntryCommentId = request.EntryCommentId,
                                                      UserId = request.UserId,
                                                      VoteType = request.VoteType
                                                  });

            return await Task.FromResult(true);
        }
    }
}
