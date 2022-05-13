using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YoloSozluk.Infrastructure.Persistence.Context;
using YoloSozluk.Infrastructure.Persistence.EntityConfigurations.Base;

namespace YoloSozluk.Infrastructure.Persistence.EntityConfigurations.User
{
    public class UserConfiguration : BaseEntryConfiguration<Api.Domain.Entities.User>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Entities.User> builder)
        {
            base.Configure(builder);

            builder.ToTable("user", YoloSozlukContext.DEFAULT_SCHEMA);

        }
    }
}
