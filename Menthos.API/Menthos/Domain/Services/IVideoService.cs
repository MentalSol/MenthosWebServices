using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Services.Communication;

namespace Menthos.API.Menthos.Domain.Services;

public interface IVideoService
{
    Task<IEnumerable<Video>> ListAsync();
    Task<IEnumerable<Video>> ListBySubjectIdAsync(int subjectId);
    Task<IEnumerable<Video>> ListByTeacherIdAsync(int teacherId);
    Task<VideoResponse> SaveAsync(Video video);
    Task<VideoResponse> UpdateAsync(int videoId, Video video);
    Task<VideoResponse> DeleteAsync(int videoId);
}