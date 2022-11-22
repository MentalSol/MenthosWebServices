using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Repositories;
using Menthos.API.Menthos.Domain.Services;
using Menthos.API.Menthos.Domain.Services.Communication;
using Menthos.API.Shared.Domain.Repositories;

namespace Menthos.API.Menthos.Services;

public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SubjectService(ISubjectRepository subjectRepository, IUnitOfWork unitOfWork)
    {
        _subjectRepository = subjectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Subject>> ListAsync()
    {
        return await _subjectRepository.ListAsync();
    }

    public async Task<SubjectResponse> SaveAsync(Subject subject)
    {
        try
        {
            await _subjectRepository.AddAsync(subject);
            await _unitOfWork.CompleteAsync();
            return new SubjectResponse(subject);
        }
        catch (Exception e)
        {
            return new SubjectResponse($"An error occurred while saving the Subject: {e.Message}");
        }
    }

    public async Task<SubjectResponse> UpdateAsync(int id, Subject subject)
    {
        var existingSubject = await _subjectRepository.FindByIdAsync(id);

        if (existingSubject == null)
            return new SubjectResponse("Subject not found.");

        existingSubject.Name = subject.Name;

        try
        {
            _subjectRepository.Update(existingSubject);
            await _unitOfWork.CompleteAsync();

            return new SubjectResponse(existingSubject);
        }
        catch (Exception e)
        {
            return new SubjectResponse($"An error occurred while updating the subject: {e.Message}");
        }
    }

    public async Task<SubjectResponse> DeleteAsync(int id)
    {
        var existingSubject = await _subjectRepository.FindByIdAsync(id);

        if (existingSubject == null)
            return new SubjectResponse("Subject not found.");

        try
        {
            _subjectRepository.Remove(existingSubject);
            await _unitOfWork.CompleteAsync();
            
            return new SubjectResponse(existingSubject);
        }
        catch (Exception e)
        {
            return new SubjectResponse($"An error occurred while deleting the subject: {e.Message}");
        }
    }
}