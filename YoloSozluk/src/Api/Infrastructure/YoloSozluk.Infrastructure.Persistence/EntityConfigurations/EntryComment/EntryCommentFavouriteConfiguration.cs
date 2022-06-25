using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YoloSozluk.Infrastructure.Persistence.Context;
using YoloSozluk.Infrastructure.Persistence.EntityConfigurations.Base;

namespace YoloSozluk.Infrastructure.Persistence.EntityConfigurations.EntryCommentFavourite
{
    public class EntryCommentFavouriteFavouriteConfiguration : BaseEntryConfiguration<Api.Domain.Entities.EntryCommentFavourite>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Entities.EntryCommentFavourite> builder)
        {
            base.Configure(builder);

            builder.ToTable("entrycommentfavourite", YoloSozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.CreatedBy)
                  .WithMany(x => x.EntryCommentFavourites)
                  .HasForeignKey(x => x.CreatedById)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.EntryComment)
                   .WithMany(x => x.EntryCommentFavourites)
                   .HasForeignKey(x => x.EntryCommentId);
        }
    }
}
