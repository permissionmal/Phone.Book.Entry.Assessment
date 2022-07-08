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
    public class PhonebookRepository : IPhonebookRepository
    {
        private readonly PhonebookContext _context;
        private readonly DbSet<PhonebookEntity> _table;
        public PhonebookRepository(PhonebookContext context)
        {
            this._context = context;
            _table = this._context.Set<PhonebookEntity>();
        }
        public async Task<List<PhonebookEntity>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<List<PhonebookEntity>> GetAllAsync(Expression<Func<PhonebookEntity, bool>> expression)
        {
            return (await Task.Run(() => _table.Where(expression))).ToList();
        }

        public async Task<PhonebookEntity> GetByIdAsync(string id)
        {
            return await _table.FindAsync(id);
        }
        public async Task<PhonebookEntity> GetByIdAsync(Guid id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<PhonebookEntity> AddAsync(PhonebookEntity item)
        {
            var entity = await _table.AddAsync(item);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }


        public async Task AddAsync(List<PhonebookEntity> items)
        {
            _table.AddRange(items);
            await _context.SaveChangesAsync();
        }
    }
}