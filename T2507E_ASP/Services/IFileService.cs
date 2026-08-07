using T2507E_ASP.DTOs.Files;
using T2507E_ASP.Models;

namespace T2507E_ASP.Services;

public interface IFileService
{
    Task<ApiResult<FileUploadResponse>> UploadAsync(IFormFile file, string folder);
}