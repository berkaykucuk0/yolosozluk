using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Api.Domain.Entities.Base;

namespace YoloSozluk.Api.Domain.Entities
{
    public class EntryComment:BaseModel
    {
        public string Content { get; set; }
        public Guid CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
        public Guid EntryId { get; set; }
        public virtual Entry Entry { get; set; }
        public virtual ICollection<EntryCommentVote> EntryCommentVotes { get; set; }
        public virtual ICollection<EntryCommentFavourite> EntryCommentFavourites { get; set; }
    }
}
