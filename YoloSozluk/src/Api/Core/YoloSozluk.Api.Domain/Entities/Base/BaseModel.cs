using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloSozluk.Api.Domain.Entities.Base
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ? UpdateDate { get; set; }
    }
}
