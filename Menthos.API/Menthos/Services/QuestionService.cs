using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Repositories;
using Menthos.API.Menthos.Domain.Services;
using Menthos.API.Menthos.Domain.Services.Communication;
using Menthos.API.Shared.Domain.Repositories;

namespace Menthos.API.Menthos.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public QuestionService(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
    {
        _questionRepository = questionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Question>> ListAsync()
    {
        return await _questionRepository.ListAsync();
    }

    

    public async Task<QuestionResponse> SaveAsync(Question question)
    {
        // Validate Content

        var existingQuestionWithContent = await _questionRepository.FindByContentAsync(question.Content);

        if (existingQuestionWithContent != null)
            return new QuestionResponse("Question content already exists.");

        try
        {
            // Add Question
            await _questionRepository.AddAsync(question);

            // Complete Transaction
            await _unitOfWork.CompleteAsync();

            // Return response
            return new QuestionResponse(question);

        }
        catch (Exception e)
        {
            //Error Handling
            return new QuestionResponse($"An error occurred while saving the question: {e.Message}");
        }
        
    }

    public async Task<QuestionResponse> UpdateAsync(int questionId, Question question)
    {
        var existingQuestion = await _questionRepository.FindByIdAsync(questionId);
        
        // Validation Question

        if (existingQuestion == null)
            return new QuestionResponse("Question not found.");
        
        // Validation Content

        var existingQuestionWithContent = await _questionRepository.FindByContentAsync(question.Content);

        if (existingQuestionWithContent != null && existingQuestionWithContent.Content != existingQuestion.Content)
            return new QuestionResponse("Question content already exists.");
        
        // Modify Fields
        existingQuestion.Content = question.Content;

        try
        {
            _questionRepository.Update(existingQuestion);
            await _unitOfWork.CompleteAsync();

            return new QuestionResponse(existingQuestion);
        }
        catch (Exception e)
        {
            // Error Handling
            return new QuestionResponse($"An error occurred while updating the question: {e.Message}");
        }
    }

    public async Task<QuestionResponse> DeleteAsync(int questionId)
    {
        var existingQuestion = await _questionRepository.FindByIdAsync(questionId);
        
        // Validate Question

        if (existingQuestion == null)
            return new QuestionResponse("Question not found");

        try
        {
            _questionRepository.Remove(existingQuestion);
            await _unitOfWork.CompleteAsync();

            return new QuestionResponse(existingQuestion);
        }
        catch (Exception e)
        {
            // Error Handling
            return new QuestionResponse($"An error occurred while deleting the question: {e.Message}");
        }
    }
}