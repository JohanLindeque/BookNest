using System;
using BookNest.Data;
using BookNest.Models.Entities;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Repositories;

public class CheckoutRepository : ICheckoutRepository
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
            throw new KeyNotFoundException($"Checkout with id {id} was not found.");

        _context.Checkouts.Remove(checkoutEntry);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Checkout>> GetAllAsync()
    {
        var checkoutsInDb = await _context
            .Checkouts.Include(c => c.Book)
            .Include(c => c.Member)
            .ToListAsync();

        return checkoutsInDb;
    }

    public async Task<Checkout> GetByIdAsync(int id)
    {
        var checkoutEntry = await _context
            .Checkouts.Include(c => c.Book)
            .Include(c => c.Member)
            .FirstOrDefaultAsync(checkout => checkout.Id == id);

        if (checkoutEntry == null)
            throw new KeyNotFoundException($"Checkout with id {id} was not found.");

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
            .Include(c => c.Book)
            .Include(c => c.Member)
            .ToListAsync();

        return memberCheckouts;
    }

    public async Task<IEnumerable<Checkout>> GetAllOverdueAsync()
    {
        var now = DateTime.UtcNow;

        var overdueCheckouts = await _context
            .Checkouts.Where(chk => chk.ReturnedDate == null && chk.DueDate < now)
            .Include(c => c.Book)
            .Include(c => c.Member)
            .ToListAsync();

        return overdueCheckouts;
    }
}
