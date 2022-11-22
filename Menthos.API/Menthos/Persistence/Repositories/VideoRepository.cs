using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Repositories;
using Menthos.API.Shared.Persistence.Contexts;
using Menthos.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Menthos.API.Menthos.Persistence.Repositories;

public class VideoRepository : BaseRepository, IVideoRepository
{
    public VideoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Video>> ListAsync()
    {
        return await _context.Videos
            .Include(p => p.Teacher)
            .ToListAsync();
    }

    public async Task AddAsync(Video video)
    {
        await _context.Videos.AddAsync(video);
    }

    public async Task<Video> FindByIdAsync(int videoId)
    {
        return await _context.Videos
            .Include(p => p.Teacher)
            .FirstOrDefaultAsync(p => p.Id == videoId);
    }

    public async Task<Video> FindByLinkAsync(string link)
    {
        return await _context.Videos
            .Include(p => p.Teacher)
            .FirstOrDefaultAsync(p => p.Link == link);
    }

    public async Task<IEnumerable<Video>> FindBySubjectIdAsync(int subjectId)
    {
        return await _context.Videos
            .Where(p => p.SubjectId == subjectId)
            .Include(p => p.Teacher)
            .ToListAsync();
    }

    public async Task<IEnumerable<Video>> FindByTeacherIdAsync(int teacherId)
    {
        return await _context.Videos
            .Where(p=>p.TeacherId == teacherId)
            .Include(p => p.Teacher)
            .ToListAsync();
    }
    
    public void Update(Video video)
    {
        _context.Videos.Update(video);
    }

    public void Remove(Video video)
    {
        _context.Videos.Remove(video);
    }
}