using Menthos.API.Menthos.Domain.Repositories;
using Menthos.API.Shared.Domain.Repositories;
using Menthos.API.Shared.Persistence.Contexts;

namespace Menthos.API.Shared.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}