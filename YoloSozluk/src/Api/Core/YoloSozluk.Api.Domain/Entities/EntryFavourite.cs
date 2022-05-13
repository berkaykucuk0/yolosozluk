using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Api.Domain.Entities.Base;

namespace YoloSozluk.Api.Domain.Entities
{
    public class EntryFavourite :BaseModel
    {
        public Guid EntryId { get; set; }
        public virtual Entry Entry { get; set; }
        public Guid CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
    }
}
