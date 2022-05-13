using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YoloSozluk.Api.Domain.Entities.Base;

namespace YoloSozluk.Infrastructure.Persistence.EntityConfigurations.Base
{
    public abstract class BaseEntryConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseModel
    {
        public virtual  void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CreateDate).ValueGeneratedOnAdd();
        }
    }
}
