using T2507E_ASP.DTOs.Student;
using T2507E_ASP.Models;

namespace T2507E_ASP.Services;

public interface IStudentService
{
    Task<ApiResult<PagedResult<StudentResponse>>> GetAllAsync(
        StudentQueryParameters parameters);
    Task<ApiResult<StudentResponse>> GetByIdAsync(int id);
    Task<ApiResult<StudentResponse>> CreateAsync(CreateStudentRequest request);
    Task<ApiResult<StudentResponse>> UpdateAsync(int id, UpdateStudentRequest request);
    Task<ApiResult<StudentResponse>> DeleteAsync(int id);
}