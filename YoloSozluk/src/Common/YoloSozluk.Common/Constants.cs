using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloSozluk.Common
{
    public class Constants
    {
        public const string HostName = "localhost";
        public const string ExchangeType = "direct";

        public const string UserEmailChangedExchangeName = "UserEmailChangedExchange";
        public const string UserEmailChangedQueueName = "UserEmailChangedQueue";

        public const string FavoriteExchangeName = "FavoriteExchange";
        public const string EntryCommentFavoriteCreateQueueName = "EntryCommentFavoriteCreateQueue";
        public const string EntryFavoriteCreateQueueName = "EntryFavoriteCreateQueue";
        public const string EntryFavoriteDeleteQueueName = "EntryFavoriteDeleteQueue";

        public const string EntryVoteExchangeName = "EntryVoteExchange";
        public const string EntryVoteCreateQueueName = "EntryVoteCreateQueue";
    }
}
