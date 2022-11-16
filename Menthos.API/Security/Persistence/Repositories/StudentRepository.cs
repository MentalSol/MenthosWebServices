using Menthos.API.Security.Domain.Models;
using Menthos.API.Security.Domain.Repositories;
using Menthos.API.Shared.Persistence.Contexts;
using Menthos.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Menthos.API.Security.Persistence.Repositories;

public class StudentRepository: BaseRepository, IStudentRepository
{
    public StudentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Student>> ListAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task AddAsync(Student student)
    {
        await _context.Students.AddAsync(student);
    }
    
    public async Task<Student> FindByIdAsync(int id)
    {
        return await _context.Students.FindAsync(id);
    }

    public async Task<Student> FindByUsernameAsync(string username)
    {
        return await _context.Students.SingleOrDefaultAsync(x => x.Username == username);
    }
        
    public Student FindById(int id)
    {
        return _context.Students.Find(id);
    }

    public bool ExistsByUsername(string username)
    {
        return _context.Students.Any(x => x.Username == username);
    }

    public void Update(Student student)
    {
        _context.Students.Update(student);
    }

    public void Remove(Student student)
    {
        _context.Students.Remove(student);
    }
}