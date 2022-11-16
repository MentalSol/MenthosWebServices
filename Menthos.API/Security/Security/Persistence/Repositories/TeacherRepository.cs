using Menthos.API.Security.Domain.Models;
using Menthos.API.Security.Domain.Repositories;
using Menthos.API.Security.Domain.Services;
using Menthos.API.Shared.Persistence.Contexts;
using Menthos.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Menthos.API.Security.Persistence.Repositories;

public class TeacherRepository : BaseRepository, ITeacherRepository
{
    public TeacherRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Teacher>> ListAsync()
    {
        return await _context.Teachers.ToListAsync();
    }

    public async Task AddAsync(Teacher teacher)
    {
        await _context.Teachers.AddAsync(teacher);
    }
    
    public async Task<Teacher> FindByIdAsync(int id)
    {
        return await _context.Teachers.FindAsync(id);
    }

    public async Task<Teacher> FindByUsernameAsync(string username)
    {
        return await _context.Teachers.SingleOrDefaultAsync(x => x.Username == username);
    }
        
    public Teacher FindById(int id)
    {
        return _context.Teachers.Find(id);
    }

    public bool ExistsByUsername(string username)
    {
        return _context.Teachers.Any(x => x.Username == username);
    }

    public void Update(Teacher teacher)
    {
        _context.Teachers.Update(teacher);
    }

    public void Remove(Teacher teacher)
    {
        _context.Teachers.Remove(teacher);
    }
}