using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Infrastructure.Persistence.Context;
using YoloSozluk.Infrastructure.Persistence.EntityConfigurations.Base;

namespace YoloSozluk.Infrastructure.Persistence.EntityConfigurations.Entry
{
    public class EntryVoteConfiguration : BaseEntryConfiguration<Api.Domain.Entities.EntryVote>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Entities.EntryVote> builder)
        {
            base.Configure(builder);

            builder.ToTable("entryvote", YoloSozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.CreatedBy)
                   .WithMany(x => x.EntryVotes)
                   .HasForeignKey(x => x.CreatedById)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Entry)
                   .WithMany(x => x.EntryVotes)
                   .HasForeignKey(x => x.EntryId);
        }
    }
}
