using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YoloSozluk.Infrastructure.Persistence.Context;
using YoloSozluk.Infrastructure.Persistence.EntityConfigurations.Base;

namespace YoloSozluk.Infrastructure.Persistence.EntityConfigurations.Entry
{
    public class EntryFavouriteConfiguration : BaseEntryConfiguration<Api.Domain.Entities.EntryFavourite>
        {
            public override void Configure(EntityTypeBuilder<Api.Domain.Entities.EntryFavourite> builder)
            {
                base.Configure(builder);

                builder.ToTable("entryfavourite", YoloSozlukContext.DEFAULT_SCHEMA);

                builder.HasOne(x => x.CreatedBy)
                       .WithMany(x => x.EntryFavourites)
                       .HasForeignKey(x => x.CreatedById);

                builder.HasOne(x => x.Entry)
                       .WithMany(x => x.EntryFavourites)
                       .HasForeignKey(x => x.EntryId);
            }
        }
}
