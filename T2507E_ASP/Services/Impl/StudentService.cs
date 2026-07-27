using T2507E_ASP.DTOs.Student;
using T2507E_ASP.Entities;
using T2507E_ASP.Models;
using T2507E_ASP.Repositories;

namespace T2507E_ASP.Services.Impl;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    public async Task<ApiResult<List<StudentResponse>>> GetAllAsync()
    {
        var students = await _studentRepository.GetAllAsync();
        return ApiResult<List<StudentResponse>>.Success(
            students.Select(MapToResponse).ToList());
    }

    public async Task<ApiResult<StudentResponse>> GetByIdAsync(int id)
    {
        var student =  await _studentRepository.GetByIdAsync(id);
        return student is null ? 
                ApiResult<StudentResponse>.Failure("NOT FOUND")
            : ApiResult<StudentResponse>.Success(MapToResponse(student));
    }

    public async Task<ApiResult<StudentResponse>> CreateAsync(CreateStudentRequest request)
    {
        var code = request.Code.Trim().ToUpperInvariant();
        var codeExists = await _studentRepository.ExistsByCodeAsync(code);
        if (codeExists)
        {
            return ApiResult<StudentResponse>.Failure("CODE_EXISTS");
        }

        var student = new Student
        {
            StudentCode = code,
            Name = request.Name,
            Email = request.Email,
            Age = request.Age
        };
        await _studentRepository.AddAsync(student);
        await _studentRepository.SaveChangesAsync();
        return ApiResult<StudentResponse>.Success(MapToResponse(student));
    }

    public async Task<ApiResult<StudentResponse>> UpdateAsync(int id, UpdateStudentRequest request)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student is null)
        {
            return ApiResult<StudentResponse>.Failure("NOT FOUND");
        }
        student.Name = request.Name;
        student.Age = request.Age;
        await _studentRepository.SaveChangesAsync();
        return ApiResult<StudentResponse>.Success(MapToResponse(student));
    }

    public async Task<ApiResult<StudentResponse>> DeleteAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student is null)
        {
            return ApiResult<StudentResponse>.Failure("NOT FOUND");
        }
        _studentRepository.Remove(student);
        await _studentRepository.SaveChangesAsync();
        return ApiResult<StudentResponse>.Success(MapToResponse(student));
    }

    public static StudentResponse MapToResponse(Student student)
    {
        return new StudentResponse
        {
            Id = student.Id,
            StudentCode = student.StudentCode,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age,
        };
    }
}