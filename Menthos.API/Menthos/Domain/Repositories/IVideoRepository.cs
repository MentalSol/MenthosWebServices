using Menthos.API.Menthos.Domain.Models;

namespace Menthos.API.Menthos.Domain.Repositories;

public interface IVideoRepository
{
    Task<IEnumerable<Video>> ListAsync();
    Task AddAsync(Video video);
    Task<Video> FindByIdAsync(int answerId);
    Task<Video> FindByLinkAsync(string link);
    Task<IEnumerable<Video>> FindBySubjectIdAsync(int subjectId);
    Task<IEnumerable<Video>> FindByTeacherIdAsync(int teacherId);
    void Update(Video video);
    void Remove(Video video);
}