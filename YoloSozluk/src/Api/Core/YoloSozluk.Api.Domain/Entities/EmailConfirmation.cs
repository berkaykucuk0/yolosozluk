using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Api.Domain.Entities.Base;

namespace YoloSozluk.Api.Domain.Entities
{
    public class EmailConfirmation: BaseModel
    {
        public string OldEmailAdress { get; set; }
        public string NewEmailAdress { get; set; }
    }
}
