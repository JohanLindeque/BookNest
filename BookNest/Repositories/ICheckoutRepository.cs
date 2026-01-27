using System;
using BookNest.Models.Entities;

namespace BookNest.Repositories;

public interface ICheckoutRepository : IRepository<Checkout>
{
    Task<IEnumerable<Checkout>> GetByUserIdAsync(string memberId);
    Task<IEnumerable<Checkout>> GetAllOverdueAsync();
}
