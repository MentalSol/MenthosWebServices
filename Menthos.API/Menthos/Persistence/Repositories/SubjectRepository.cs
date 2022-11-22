using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Repositories;
using Menthos.API.Shared.Persistence.Contexts;
using Menthos.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Menthos.API.Menthos.Persistence.Repositories;

public class SubjectRepository : BaseRepository, ISubjectRepository
{
    public SubjectRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Subject>> ListAsync()
    {
        return await _context.Subjects.ToListAsync();
    }
    
    public async Task AddAsync(Subject subject)
    {
        await _context.Subjects.AddAsync(subject);
    }

    public async Task<Subject> FindByIdAsync(int id)
    {
        return await _context.Subjects.FindAsync(id);
    }

    public void Update(Subject subject)
    {
        _context.Subjects.Update(subject);
    }

    public void Remove(Subject subject)
    {
        _context.Subjects.Remove(subject);
    }
}