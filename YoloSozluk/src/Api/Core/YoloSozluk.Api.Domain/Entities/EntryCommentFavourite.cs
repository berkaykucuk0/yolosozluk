using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Api.Domain.Entities.Base;

namespace YoloSozluk.Api.Domain.Entities
{
    public class EntryCommentFavourite : BaseModel
    {
        public Guid EntryCommentId { get; set; }
        public virtual EntryComment EntryComment { get; set; }
        public Guid CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
    }
}
