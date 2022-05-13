using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Api.Domain.Entities.Base;

namespace YoloSozluk.Api.Domain.Entities
{
    public class User: BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool EmailConfirmed { get; set; }
        public virtual ICollection<Entry> Entries { get; set; }
        public virtual ICollection<EntryFavourite> EntryFavourites { get; set; }
        public virtual ICollection<EntryVote> EntryVotes { get; set; }
        public virtual ICollection<EntryComment> EntryComments { get; set; }
        public virtual ICollection<EntryCommentFavourite> EntryCommentFavourites { get; set; }
        public virtual ICollection<EntryCommentVote> EntryCommentVotes { get; set; }

    }
}
