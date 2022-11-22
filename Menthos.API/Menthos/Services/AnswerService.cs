using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Repositories;
using Menthos.API.Menthos.Domain.Services;
using Menthos.API.Menthos.Domain.Services.Communication;
using Menthos.API.Shared.Domain.Repositories;

namespace Menthos.API.Menthos.Services;

public class AnswerService : IAnswerService
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IQuestionRepository _questionRepository;

    public AnswerService(IAnswerRepository answerRepository, IUnitOfWork unitOfWork,
        IQuestionRepository questionRepository)
    {
        _answerRepository = answerRepository;
        _unitOfWork = unitOfWork;
        _questionRepository = questionRepository;
    }

    public async Task<IEnumerable<Answer>> ListAsync()
    {
        return await _answerRepository.ListAsync();
    }

    public async Task<IEnumerable<Answer>> ListByQuestionIdAsync(int questionId)
    {
        return await _answerRepository.FindByQuestionIdAsync(questionId);
    }
    
    public async Task<AnswerResponse> SaveAsync(Answer answer)
    {
        //Validate QuestionId

        var existingQuestion = await _questionRepository.FindByIdAsync(answer.QuestionId);

        if (existingQuestion == null)
            return new AnswerResponse("Invalid Question");
        
        //Validate Content

        var existingAnswerWithContent = await _answerRepository.FindByContentAsync(answer.Content);

        if (existingAnswerWithContent != null)
            return new AnswerResponse("Answer content already exists.");

        try
        {
            //Add Answer
            await _answerRepository.AddAsync(answer);
            
            //Complete Transaction
            await _unitOfWork.CompleteAsync();
            
            //Return response
            return new AnswerResponse(answer);
            
        }
        catch (Exception e)
        {
            //Error Handling
            return new AnswerResponse($"An error occurred while saving the answer: {e.Message}");
        }
    }

    public async Task<AnswerResponse> UpdateAsync(int answerId, Answer answer)
    {
        var existingAnswer = await _answerRepository.FindByIdAsync(answerId);
        
        //Validate Answer

        if (existingAnswer == null)
            return new AnswerResponse("Answer not found.");
        
        //Validate QuestionId

        var existingQuestion = await _questionRepository.FindByIdAsync(answer.QuestionId);

        if (existingQuestion == null)
            return new AnswerResponse("Invalid Question");
        
        //Validate Content

        var existingAnswerWithContent = await _answerRepository.FindByContentAsync(answer.Content);

        if (existingAnswerWithContent != null && existingAnswerWithContent.Id != existingAnswer.Id)
            return new AnswerResponse("Answer content already exists.");
        
        //Modify Fields
        existingAnswer.Content = answer.Content;

        try
        {
            _answerRepository.Update(existingAnswer);
            await _unitOfWork.CompleteAsync();

            return new AnswerResponse(existingAnswer);

        }
        catch (Exception e)
        {
            //Error Handling
            return new AnswerResponse($"An error occurred while updating the answer: {e.Message}");
        }
    }

    public async Task<AnswerResponse> DeleteAsync(int answerId)
    {
        var existingAnswer = await _answerRepository.FindByIdAsync(answerId);
        
        //Validate Answer

        if (existingAnswer == null)
            return new AnswerResponse("Answer not found.");

        try
        {
            _answerRepository.Remove(existingAnswer);
            await _unitOfWork.CompleteAsync();

            return new AnswerResponse(existingAnswer);
        }
        catch (Exception e)
        {
            //Error Handling
            return new AnswerResponse($"An error occurred while deleting the answer: {e.Message}");
        }
    }
}