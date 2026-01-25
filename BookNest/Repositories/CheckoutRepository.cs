using System;
using BookNest.Data;
using BookNest.Models.Entities;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Repositories;

public class CheckoutRepository : IRepository<Checkout>
{
    private readonly AppDbContext _context;

    public CheckoutRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Checkout entity)
    {
        await _context.Checkouts.AddAsync(entity);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var checkoutEntry = await _context.Checkouts.FirstOrDefaultAsync(checkout =>
            checkout.Id == id
        );

        if (checkoutEntry == null)
            throw new KeyNotFoundException();

        _context.Checkouts.Remove(checkoutEntry);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Checkout>> GetAllAsync()
    {
        var checkoutsInDb = await _context.Checkouts.ToListAsync();

        return checkoutsInDb;
    }

    public async Task<Checkout> GetByIdAsync(int id)
    {
        var checkoutEntry = await _context.Checkouts.FirstOrDefaultAsync(checkout =>
            checkout.Id == id
        );

        if (checkoutEntry == null)
            throw new KeyNotFoundException();

        return checkoutEntry;
    }

    public async Task UpdateAsync(Checkout entity)
    {
        _context.Checkouts.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Checkout>> GetByUserIdAsync(string memberId)
    {
        var memberCheckouts = await _context
            .Checkouts.Where(chk => chk.MemberId == memberId)
            .ToListAsync();

        return memberCheckouts;
    }
}
