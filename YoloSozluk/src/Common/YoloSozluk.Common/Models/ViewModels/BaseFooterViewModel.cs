using YoloSozluk.Common.Enums;

namespace YoloSozluk.Common.Models.ViewModels
{

    //Kullanıcılar kendi entrylerinde favori falan görmesin vs. diye yapışmıştır
    public class BaseFooterRateViewModel
    {
        public VoteType VoteType { get; set; }
    }

    public class BaseFooterFavoritedViewModel
    {
        public bool IsFavorited { get; set; }
        public int FavoritedCount  { get; set; }
    }

    public class BaseFooterRateFavoritedViewModel: BaseFooterFavoritedViewModel
    {
        public VoteType VoteType { get; set; }
    }
}
