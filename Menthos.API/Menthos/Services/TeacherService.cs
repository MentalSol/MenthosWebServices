using AutoMapper;
using Menthos.API.Menthos.Domain.Repositories;
using Menthos.API.Security.Authorization.Handlers.Interfaces;
using Menthos.API.Security.Domain.Models;
using Menthos.API.Security.Domain.Repositories;
using Menthos.API.Security.Domain.Services;
using Menthos.API.Security.Domain.Services.Communication;
using Menthos.API.Security.Exceptions;
using Menthos.API.Shared.Domain.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Menthos.API.Menthos.Services;

public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IJwtHandler _jwtHandler;
    private readonly IMapper _mapper;

    public TeacherService(IJwtHandler jwtHandler,
        IMapper mapper, ITeacherRepository teacherRepository, IUnitOfWork unitOfWork)
    {
        _jwtHandler = jwtHandler;
        _mapper = mapper;
        _teacherRepository = teacherRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
    {
        var teacher = await _teacherRepository.FindByUsernameAsync(request.Username);
        Console.WriteLine($"Request: {request.Username}, {request.Password}");
        Console.WriteLine($"Teacher: {teacher.Id}, {teacher.Name}, {teacher.LastName}, {teacher.Username}, {teacher.Codigo}, {teacher.email}, {teacher.telephone}, {teacher.PasswordHash}");
        
        // validate
        if (teacher == null || !BCryptNet.Verify(request.Password, teacher.PasswordHash))
        {
            Console.WriteLine("Authentication Error");
            throw new AppException("Username or password is incorrect");
        }
        
        Console.WriteLine("Authentication successful. About to generate token");
        // authentication successful
        var response = _mapper.Map<AuthenticateResponse>(teacher);
        Console.WriteLine($"Response: {response.Id}, {response.Name}, {response.LastName}, {response.Username}, {response.Codigo}, {response.email}, {response.telephone}");
        response.Token = _jwtHandler.GenerateToken(teacher);
        Console.WriteLine($"Generated token is {response.Token}");
        return response;
    }

    public async Task<IEnumerable<Teacher>> ListAsync()
    {
        return await _teacherRepository.ListAsync();
    }

    public async Task<Teacher> GetByIdAsync(int id)
    {
        var teacher = await _teacherRepository.FindByIdAsync(id);
        if (teacher == null) throw new KeyNotFoundException("Teacher not found");
        return teacher;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        // validate
        if (_teacherRepository.ExistsByUsername(request.Username))
            throw new AppException("Username '" + request.Username + "' is already taken");
        
        //map model to new user object
        var teacher = _mapper.Map<Teacher>(request);
        
        // hash password
        teacher.PasswordHash = BCryptNet.HashPassword(request.Password);
        
        // save user
        try
        {
            await _teacherRepository.AddAsync(teacher);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the teacher: {e.Message}");
        }
    }
    
    public async Task UpdateAsync(int id, UpdateRequest request)
    {
        var teacher = GetById(id);

        // Validate
        if (_teacherRepository.ExistsByUsername(request.Username)) 
            throw new AppException("Username '" + request.Username + "' is already taken");

        // Hash password if it was entered
        if (!string.IsNullOrEmpty(request.Password))
            teacher.PasswordHash = BCryptNet.HashPassword(request.Password);

        // Copy model to user and save
        _mapper.Map(request, teacher);
        try
        {
            _teacherRepository.Update(teacher);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the teacher: {e.Message}");
        }
    }
    
    public async Task DeleteAsync(int id)
    {
        var teacher = GetById(id);

        try
        {
            _teacherRepository.Remove(teacher);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while deleting the teacher: {e.Message}");
        }
    }
    
    // helper methods

    private Teacher GetById(int id)
    {
        var teacher = _teacherRepository.FindById(id);
        if (teacher == null) throw new KeyNotFoundException("Teacher not found");
        return teacher;
    }
}