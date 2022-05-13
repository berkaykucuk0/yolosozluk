using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YoloSozluk.Infrastructure.Persistence.Context;
using YoloSozluk.Infrastructure.Persistence.EntityConfigurations.Base;

namespace YoloSozluk.Infrastructure.Persistence.EntityConfigurations.EntryComment
{
    public class EntryCommentVoteFavouriteConfiguration : BaseEntryConfiguration<Api.Domain.Entities.EntryCommentVote>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Entities.EntryCommentVote> builder)
        {
            base.Configure(builder);

            builder.ToTable("entrycommentvote", YoloSozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.CreatedBy)
                  .WithMany(x => x.EntryCommentVotes)
                  .HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.EntryComment)
                   .WithMany(x => x.EntryCommentVotes)
                   .HasForeignKey(x => x.EntryCommentId);
        }
    }
}
