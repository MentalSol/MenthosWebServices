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

namespace Menthos.API.Security.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IJwtHandler _jwtHandler;
    private readonly IMapper _mapper;

    public StudentService(IJwtHandler jwtHandler,
        IMapper mapper, IStudentRepository studentRepository, IUnitOfWork unitOfWork)
    {
        _jwtHandler = jwtHandler;
        _mapper = mapper;
        _studentRepository = studentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
    {
        var student = await _studentRepository.FindByUsernameAsync(request.Username);
        Console.WriteLine($"Request: {request.Username}, {request.Password}");
        Console.WriteLine($"Student: {student.Id}, {student.Name}, {student.LastName}, {student.Username}, {student.Codigo}, {student.email}, {student.telephone}, {student.PasswordHash}");
        
        // validate
        if (student == null || !BCryptNet.Verify(request.Password, student.PasswordHash))
        {
            Console.WriteLine("Authentication Error");
            throw new AppException("Username or password is incorrect");
        }
        
        Console.WriteLine("Authentication successful. About to generate token");
        // authentication successful
        var response = _mapper.Map<AuthenticateResponse>(student);
        Console.WriteLine($"Response: {response.Id}, {response.Name}, {response.LastName}, {response.Username}, {response.Codigo}, {response.email}, {response.telephone}");
        response.Token = _jwtHandler.GenerateToken(student);
        Console.WriteLine($"Generated token is {response.Token}");
        return response;
    }

    public async Task<IEnumerable<Student>> ListAsync()
    {
        return await _studentRepository.ListAsync();
    }

    public async Task<Student> GetByIdAsync(int id)
    {
        var student = await _studentRepository.FindByIdAsync(id);
        if (student == null) throw new KeyNotFoundException("Student not found");
        return student;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        // validate
        if (_studentRepository.ExistsByUsername(request.Username))
            throw new AppException("Username '" + request.Username + "' is already taken");
        
        //map model to new user object
        var student = _mapper.Map<Student>(request);
        
        // hash password
        student.PasswordHash = BCryptNet.HashPassword(request.Password);
        
        // save user
        try
        {
            await _studentRepository.AddAsync(student);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the student: {e.Message}");
        }
    }
    
    public async Task UpdateAsync(int id, UpdateRequest request)
    {
        var student = GetById(id);

        // Validate
        if (_studentRepository.ExistsByUsername(request.Username)) 
            throw new AppException("Username '" + request.Username + "' is already taken");

        // Hash password if it was entered
        if (!string.IsNullOrEmpty(request.Password))
            student.PasswordHash = BCryptNet.HashPassword(request.Password);

        // Copy model to user and save
        _mapper.Map(request, student);
        try
        {
            _studentRepository.Update(student);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the student: {e.Message}");
        }
    }
    
    public async Task DeleteAsync(int id)
    {
        var student = GetById(id);

        try
        {
            _studentRepository.Remove(student);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while deleting the student: {e.Message}");
        }
    }
    
    // helper methods

    private Student GetById(int id)
    {
        var student = _studentRepository.FindById(id);
        if (student == null) throw new KeyNotFoundException("Student not found");
        return student;
    }
}