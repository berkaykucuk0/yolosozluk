using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YoloSozluk.Infrastructure.Persistence.Context;
using YoloSozluk.Infrastructure.Persistence.EntityConfigurations.Base;

namespace YoloSozluk.Infrastructure.Persistence.EntityConfigurations.EmailConfirmation
{
    public class EmailConfirmationConfiguration : BaseEntryConfiguration<Api.Domain.Entities.EmailConfirmation>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Entities.EmailConfirmation> builder)
        {
            base.Configure(builder);

            builder.ToTable("emailconfirmation", YoloSozlukContext.DEFAULT_SCHEMA);

        }
    }
}
