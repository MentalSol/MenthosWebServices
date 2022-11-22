using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Services.Communication;

namespace Menthos.API.Menthos.Domain.Services;

public interface ICommentService
{
    Task<IEnumerable<Comment>> ListAsync();
    Task<IEnumerable<Comment>> ListByVideoIdAsync(int videoId);
    Task<IEnumerable<Comment>> ListByStudentIdAsync(int studentId);
    Task<CommentResponse> SaveAsync(Comment comment);
    Task<CommentResponse> UpdateAsync(int commentId, Comment comment);
    Task<CommentResponse> DeleteAsync(int commentId);
}