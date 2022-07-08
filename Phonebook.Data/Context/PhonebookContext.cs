using PhoneBook.Domain.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Data.Context
{
    public class PhonebookContext : DbContext
    {
        public PhonebookContext(DbContextOptions<PhonebookContext> dbco) : base(dbco) { }

        public DbSet<PhonebookEntity> Phonebook { get; set; }
        public DbSet<EntryEntity> Entries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            //Newly Created Entities
            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();
            AddedEntities.ForEach(E =>
            {
                E.Property("CreatedDate").CurrentValue = DateTime.Now;
                E.Property("ModifiedDate").CurrentValue = DateTime.Now;
            });

            //Modified Entities
            var EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();
            EditedEntities.ForEach(E =>
            {
                E.Property("ModifiedDate").CurrentValue = DateTime.Now;
            });
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
