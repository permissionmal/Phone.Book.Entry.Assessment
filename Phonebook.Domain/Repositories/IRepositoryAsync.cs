using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PhoneBook.Domain.Repositories
{
    public interface IRepositoryAsync<T>
    {
            Task<List<T>> GetAllAsync();
            Task<T> GetByIdAsync(string id);
            Task<T> GetByIdAsync(Guid id);
            Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
            Task<T> AddAsync(T item);
            Task AddAsync(List<T> items);
    }
}