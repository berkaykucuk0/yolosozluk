using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YoloSozluk.Infrastructure.Persistence.Context;
using YoloSozluk.Infrastructure.Persistence.EntityConfigurations.Base;

namespace YoloSozluk.Infrastructure.Persistence.EntityConfigurations.Entry
{
    public class EntryConfiguration: BaseEntryConfiguration<Api.Domain.Entities.Entry>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Entities.Entry> builder)
        {
            base.Configure(builder);

            builder.ToTable("entry", YoloSozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.CreatedBy)
                   .WithMany(x => x.Entries)
                   .HasForeignKey(x => x.CreatedById)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
