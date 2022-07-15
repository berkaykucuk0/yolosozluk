using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Api.Domain.Entities;
using YoloSozluk.Common.Infrastructure;

namespace YoloSozluk.Infrastructure.Persistence.Context
{
    public class SeedData
    {
        public async Task SeedAsync(IConfiguration conf)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();
            dbContextBuilder.UseSqlServer(conf["YoloConnection"]);

            var context = new YoloSozlukContext(dbContextBuilder.Options);


            var users = GetUsers();
            var userIds = users.Select(x => x.Id);
            
            await context.Users.AddRangeAsync(users);

            var entries = GetEntries(userIds);
            var entryIds = entries.Select(x => x.Id);

            await context.Entries.AddRangeAsync(entries);

            var entryComments = GetEntryComments(entryIds, userIds);

            await context.EntryCommentS.AddRangeAsync(entryComments);

            await context.SaveChangesAsync();


        }

        private static List<User> GetUsers()
        {
            var result = new Faker<User>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.FirstName, i => i.Person.FirstName)
                .RuleFor(i => i.LastName, i => i.Person.LastName)
                .RuleFor(i => i.Email, i => i.Internet.Email())
                .RuleFor(i => i.UserName, i => i.Internet.UserName())
                .RuleFor(i => i.Password, i => Encryptor.Encrypt(i.Internet.Password()))
                .RuleFor(i => i.EmailConfirmed, i => i.PickRandom(true, false))
                .Generate(500);
            return result;
        }

        private static List<Entry> GetEntries(IEnumerable<Guid> userIds)
        {
            var result = new Faker<Entry>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Subject, i => i.Lorem.Sentence(5, 5))
                .RuleFor(i => i.Content, i => i.Lorem.Paragraph(5))
                .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))
                .Generate(150);
            return result;
        }

        private static List<EntryComment> GetEntryComments(IEnumerable<Guid> entryIds, IEnumerable<Guid> userIds)
        {
            var result = new Faker<EntryComment>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Content, i => i.Lorem.Paragraph(2))
                .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))
                .RuleFor(i => i.EntryId, i => i.PickRandom(entryIds))
                .Generate(1000);
            return result;
        }

    }
}
