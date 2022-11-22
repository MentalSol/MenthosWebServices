using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Repositories;
using Menthos.API.Shared.Persistence.Contexts;
using Menthos.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Menthos.API.Menthos.Persistence.Repositories;

public class CommentRepository : BaseRepository, ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Comment>> ListAsync()
    {
        return await _context.Comments
            .Include(p => p.Student)
            .ToListAsync();
    }
    
    public async Task AddAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
    }
    
    public async Task<Comment> FindByIdAsync(int commentId)
    {
        return await _context.Comments
            .Include(p => p.Student)
            .FirstOrDefaultAsync(p => p.Id == commentId);
    }
    
    public async Task<Comment> FindByMessageAsync(string messageC)
    {
        return await _context.Comments
            .Include(p => p.Student)
            .FirstOrDefaultAsync(p => p.MessageC == messageC);
    }
    
    public async Task<IEnumerable<Comment>> FindByVideoIdAsync(int videoId)
    {
        return await _context.Comments
            .Where(p => p.VideoId == videoId)
            .Include(p => p.Student)
            .ToListAsync();
    }

    public async Task<IEnumerable<Comment>> FindByStudentIdAsync(int studentId)
    {
        return await _context.Comments
            .Where(p => p.StudentId == studentId)
            .Include(p => p.Student)
            .ToListAsync();
    }
    
    public void Update(Comment comment)
    {
        _context.Comments.Update(comment);
    }

    public void Remove(Comment comment)
    {
        _context.Comments.Remove(comment);
    }
}