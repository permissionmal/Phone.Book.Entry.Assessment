using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Repositories;
using Phonebook.Data.Context;

namespace Phonebook.Data.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private readonly PhonebookContext _context;
        private readonly DbSet<EntryEntity> _table;
        public EntryRepository(PhonebookContext context)
        {
            this._context = context;
            _table = this._context.Set<EntryEntity>();
        }
        public async Task<List<EntryEntity>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<List<EntryEntity>> GetAllAsync(Expression<Func<EntryEntity, bool>> expression)
        {
            return (await Task.Run(() => _table.Where(expression))).ToList();
        }

        public async Task<EntryEntity> GetByIdAsync(string id)
        {
            return await _table.FindAsync(id);
        }
        public async Task<EntryEntity> GetByIdAsync(Guid id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<EntryEntity> AddAsync(EntryEntity item)
        {
            var entity = await _table.AddAsync(item);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }


        public async Task AddAsync(List<EntryEntity> items)
        {
            _table.AddRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
