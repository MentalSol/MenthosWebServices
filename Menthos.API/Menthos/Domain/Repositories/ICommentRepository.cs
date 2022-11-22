using Menthos.API.Menthos.Domain.Models;

namespace Menthos.API.Menthos.Domain.Repositories;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> ListAsync();
    Task AddAsync(Comment comment);
    Task<Comment> FindByIdAsync(int commentId);
    Task<Comment> FindByMessageAsync(string messageC);
    Task<IEnumerable<Comment>> FindByVideoIdAsync(int videoId);
    Task<IEnumerable<Comment>> FindByStudentIdAsync(int studentId);
    void Update(Comment comment);
    void Remove(Comment comment);
}