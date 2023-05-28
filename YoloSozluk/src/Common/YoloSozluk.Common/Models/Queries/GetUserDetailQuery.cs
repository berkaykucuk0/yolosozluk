using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloSozluk.Common.Models.Queries
{
    //Kullanıcıyı Id si veya UserName i ile bulabilmek için ikisi , normalde id den bulunabilir ama çeşitlilik amaçlı
    public class GetUserDetailQuery: IRequest<GetUserDetailViewModel>
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public GetUserDetailQuery(Guid userId, string userName= null)
        {
            UserId = userId;
            UserName = userName;
        }
    }
}
