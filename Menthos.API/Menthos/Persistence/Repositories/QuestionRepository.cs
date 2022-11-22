using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Repositories;
using Menthos.API.Shared.Persistence.Contexts;
using Menthos.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Menthos.API.Menthos.Persistence.Repositories;

public class QuestionRepository : BaseRepository, IQuestionRepository
{
    public QuestionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Question>> ListAsync()
    {
        return await _context.Questions
            .Include(p=>p.Student)
            .ToListAsync();
    }

    public async Task AddAsync(Question question)
    {
        await _context.Questions.AddAsync(question);
    }

    public async Task<Question> FindByIdAsync(int questionId)
    {
        return await _context.Questions
            .Include(p=>p.Student)
            .FirstOrDefaultAsync(p => p.Id == questionId);
    }

    public async Task<Question> FindByContentAsync(string content)
    {
        return await _context.Questions
            .Include(p=>p.Student)
            .FirstOrDefaultAsync(p => p.Content == content);
    }

    public async Task<IEnumerable<Question>> FindByStudentIdAsync(int studentId)
    {
        return await _context.Questions
            .Where(p => p.StudentId == studentId)
            .Include(p => p.Student)
            .ToListAsync();
    }

    public async Task<IEnumerable<Question>> FindBySubjectIdAsync(int subjectId)
    {
        return await _context.Questions
            .Where(p => p.SubjectId == subjectId)
            .Include(p => p.Student)
            .ToListAsync();
    }
    
    public void Update(Question question)
    {
        _context.Questions.Update(question);
    }

    public void Remove(Question question)
    {
        _context.Questions.Remove(question);
    }
}