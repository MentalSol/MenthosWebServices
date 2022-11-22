using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Repositories;
using Menthos.API.Menthos.Domain.Services;
using Menthos.API.Menthos.Domain.Services.Communication;
using Menthos.API.Shared.Domain.Repositories;

namespace Menthos.API.Menthos.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<Comment>> ListAsync()
    {
        return await _commentRepository.ListAsync();
    }
    
    public async Task<IEnumerable<Comment>> ListByStudentIdAsync(int studentId)
    {
        return await _commentRepository.FindByStudentIdAsync(studentId);
    }
    
    public async Task<IEnumerable<Comment>> ListByVideoIdAsync(int videoId)
    {
        return await _commentRepository.FindByVideoIdAsync(videoId);
    }
    
    public async Task<CommentResponse> SaveAsync(Comment comment)
    {
        // Validate Message

        var existingCommentWithMessage = await _commentRepository.FindByMessageAsync(comment.MessageC);

        if (existingCommentWithMessage != null)
            return new CommentResponse("Comment message already exists.");

        try
        {
            // Add Question
            await _commentRepository.AddAsync(comment);

            // Complete Transaction
            await _unitOfWork.CompleteAsync();

            // Return response
            return new CommentResponse(comment);

        }
        catch (Exception e)
        {
            //Error Handling
            return new CommentResponse($"An error occurred while saving the comment: {e.Message}");
        }
        
    }

    public async Task<CommentResponse> UpdateAsync(int commentId, Comment comment)
    {
        var existingComment = await _commentRepository.FindByIdAsync(commentId);
        
        // Validation Question

        if (existingComment == null)
            return new CommentResponse("Comment not found.");
        
        // Validation Message

        var existingCommentWithMessage = await _commentRepository.FindByMessageAsync(comment.MessageC);

        if (existingCommentWithMessage != null && existingCommentWithMessage.MessageC != existingComment.MessageC)
            return new CommentResponse("Comment message already exists.");
        
        // Modify Fields
        existingComment.MessageC = comment.MessageC;

        try
        {
            _commentRepository.Update(existingComment);
            await _unitOfWork.CompleteAsync();

            return new CommentResponse(existingComment);
        }
        catch (Exception e)
        {
            // Error Handling
            return new CommentResponse($"An error occurred while updating the comment: {e.Message}");
        }
    }

    public async Task<CommentResponse> DeleteAsync(int commentId)
    {
        var existingComment = await _commentRepository.FindByIdAsync(commentId);
        
        // Validate Question

        if (existingComment == null)
            return new CommentResponse("Comment not found");

        try
        {
            _commentRepository.Remove(existingComment);
            await _unitOfWork.CompleteAsync();

            return new CommentResponse(existingComment);
        }
        catch (Exception e)
        {
            // Error Handling
            return new CommentResponse($"An error occurred while deleting the comment: {e.Message}");
        }
    }
}