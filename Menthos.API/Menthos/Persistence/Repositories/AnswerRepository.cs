using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Repositories;
using Menthos.API.Shared.Persistence.Contexts;
using Menthos.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Menthos.API.Menthos.Persistence.Repositories;

public class AnswerRepository : BaseRepository, IAnswerRepository
{
    public AnswerRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Answer>> ListAsync()
    {
        return await _context.Answers
            .Include(p => p.Question)
            .ToListAsync();
    }

    public async Task AddAsync(Answer answer)
    {
        await _context.Answers.AddAsync(answer);
    }

    public async Task<Answer> FindByIdAsync(int answerId)
    {
        return await _context.Answers
            .Include(p => p.Question)
            .FirstOrDefaultAsync(p => p.Id == answerId);
    }

    public async Task<Answer> FindByContentAsync(string content)
    {
        return await _context.Answers
            .Include(p => p.Question)
            .FirstOrDefaultAsync(p => p.Content == content);
    }

    public async Task<IEnumerable<Answer>> FindByQuestionIdAsync(int questionId)
    {
        return await _context.Answers
            .Where(p => p.QuestionId == questionId)
            .Include(p => p.Question)
            .ToListAsync();
    }

    public void Update(Answer answer)
    {
        _context.Answers.Update(answer);
    }

    public void Remove(Answer answer)
    {
        _context.Answers.Remove(answer);
    }
}