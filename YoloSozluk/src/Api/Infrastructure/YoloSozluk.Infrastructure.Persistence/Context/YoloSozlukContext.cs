using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Api.Domain.Entities;
using YoloSozluk.Api.Domain.Entities.Base;

namespace YoloSozluk.Infrastructure.Persistence.Context
{
    public class YoloSozlukContext:DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";

        public YoloSozlukContext()
        {

        }

        public YoloSozlukContext(DbContextOptions options): base(options)
        {

        }
      
        public DbSet<EmailConfirmation> EmailConfirmations { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<EntryComment> EntryCommentS { get; set; }
        public DbSet<EntryCommentFavourite> EntryCommentFavourites { get; set; }
        public DbSet<EntryCommentVote> EntryCommentVotes { get; set; }
        public DbSet<EntryFavourite> EntryFavourites { get; set; }
        public DbSet<EntryVote> EntryVotes { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var cnnStr = "Server=DESKTOP-JAS67L4\\SQLEXPRESS;Database=YoloSozlukDB;Trusted_Connection=True;";
                optionsBuilder.UseSqlServer(cnnStr);
            }

            base.OnConfiguring(optionsBuilder);     
        }

        public void OnBeforeSave()
        {
            var addedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).Select(e => (BaseModel)e.Entity);
            if (addedEntities!=null && addedEntities.Count()> 0)
            {
                PrepareAddedEntities(addedEntities);
            }

            var updatedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified).Select(e => (BaseModel)e.Entity);
            if (updatedEntities != null && updatedEntities.Count() > 0)
            {
                PrepareUpdatedEntities(updatedEntities);
            }
        }

        public void PrepareAddedEntities (IEnumerable<BaseModel> addedEntities)
        {
            foreach (var item in addedEntities)
            {
                item.CreateDate = DateTime.Now;
            }
        }
        public void PrepareUpdatedEntities(IEnumerable<BaseModel> updatedEntities)
        {
            foreach (var item in updatedEntities)
            {
                item.UpdateDate = DateTime.Now;
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            OnBeforeSave();
            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        
    }
}
