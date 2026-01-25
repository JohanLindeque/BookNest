using System;

namespace BookNest.Repositories;

public interface IRepository<T>
    where T : class // constraint, only type classs no ints entc.
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task DeleteAsync(int id);
    Task UpdateAsync(T entity);
}
