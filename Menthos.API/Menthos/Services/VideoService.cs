using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Repositories;
using Menthos.API.Menthos.Domain.Services;
using Menthos.API.Menthos.Domain.Services.Communication;
using Menthos.API.Security.Domain.Repositories;
using Menthos.API.Shared.Domain.Repositories;

namespace Menthos.API.Menthos.Services;

public class VideoService : IVideoService
{
    private readonly IVideoRepository _videoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISubjectRepository _subjectRepository;
    private readonly ITeacherRepository _teacherRepository;

    public VideoService(IVideoRepository videoRepository, IUnitOfWork unitOfWork, ISubjectRepository subjectRepository,
        ITeacherRepository teacherRepository)
    {
        _videoRepository = videoRepository;
        _unitOfWork = unitOfWork;
        _subjectRepository = subjectRepository;
        _teacherRepository = teacherRepository;
    }

    public async Task<IEnumerable<Video>> ListAsync()
    {
        return await _videoRepository.ListAsync();
    }

    public async Task<IEnumerable<Video>> ListBySubjectIdAsync(int subjectId)
    {
        return await _videoRepository.FindBySubjectIdAsync(subjectId);
    }

    public async Task<IEnumerable<Video>> ListByTeacherIdAsync(int teacherId)
    {
        return await _videoRepository.FindByTeacherIdAsync(teacherId);
    }

    public async Task<VideoResponse> SaveAsync(Video video)
    {
        //Validate TeacherId

        var existingTeacherId = await _teacherRepository.FindByIdAsync(video.TeacherId);

        if (existingTeacherId == null)
            return new VideoResponse("Invalid Video.");
        
        //Validate Link

        var existingVideoWithLink = await _videoRepository.FindByLinkAsync(video.Link);

        if (existingVideoWithLink != null)
            return new VideoResponse("Video link already exists.");

        try
        {
            // Add Video
            await _videoRepository.AddAsync(video);
            
            //Complete Transaction
            await _unitOfWork.CompleteAsync();
            
            //Return response
            return new VideoResponse(video);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new VideoResponse($"An error occurred while saving the Video: {e.Message}");
        }
    }

    public async Task<VideoResponse> UpdateAsync(int videoId, Video video)
    {
        var existingVideo = await _videoRepository.FindByIdAsync(videoId);
        
        //Validate Video

        if (existingVideo == null)
            return new VideoResponse("Video not found.");
        
        //Validate TeacherId

        var existingTeacher = await _teacherRepository.FindByIdAsync(video.TeacherId);

        if (existingTeacher == null)
            return new VideoResponse("Invalid Teacher");
        
        //Validate Link

        var existingVideoWithLink = await _videoRepository.FindByLinkAsync(video.Link);

        if (existingVideoWithLink != null && existingVideoWithLink.Id != existingVideo.Id)
            return new VideoResponse("Video Link already exists.");
        
        //Modify Fields
        existingVideo.Link = video.Link;

        try
        {
            _videoRepository.Update(existingVideo);
            await _unitOfWork.CompleteAsync();

            return new VideoResponse(existingVideo);
        }
        catch (Exception e)
        {
            // Error Handling
            return new VideoResponse($"An error occurred while updating the video: {e.Message}");
        }
    }

    public async Task<VideoResponse> DeleteAsync(int videoId)
    {
        var existingVideo = await _videoRepository.FindByIdAsync(videoId);
        
        //Validate Video

        if (existingVideo == null)
            return new VideoResponse("Video not found.");

        try
        {
            _videoRepository.Remove(existingVideo);
            await _unitOfWork.CompleteAsync();

            return new VideoResponse(existingVideo);
        }
        catch (Exception e)
        {
            // Error Handling
            return new VideoResponse($"An error occurred while deleting the video: {e.Message}");
        }
    }
}