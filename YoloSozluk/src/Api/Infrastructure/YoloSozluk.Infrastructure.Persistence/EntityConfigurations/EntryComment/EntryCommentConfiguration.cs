using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YoloSozluk.Infrastructure.Persistence.Context;
using YoloSozluk.Infrastructure.Persistence.EntityConfigurations.Base;

namespace YoloSozluk.Infrastructure.Persistence.EntityConfigurations.EntryCommentComment
{
    public class EntryCommentConfiguration : BaseEntryConfiguration<Api.Domain.Entities.EntryComment>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Entities.EntryComment> builder)
        {
            base.Configure(builder);

            builder.ToTable("entrycomment", YoloSozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.CreatedBy)
                  .WithMany(x => x.EntryComments)
                  .HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.Entry)
                   .WithMany(x => x.EntryComments)
                   .HasForeignKey(x => x.EntryId);
        }
    }
}
