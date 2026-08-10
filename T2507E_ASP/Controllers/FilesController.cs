using Microsoft.AspNetCore.Mvc;
using T2507E_ASP.DTOs.Files;
using T2507E_ASP.Models;
using T2507E_ASP.Services;

namespace T2507E_ASP.Controllers;
[ApiController]
[Route("api/files")]
public class FilesController : ControllerBase
{
    private readonly IFileService _fileService;
    
    public  FilesController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost("upload")]
    public async Task<ActionResult<ApiResult<FileUploadResponse>>> Upload(
        IFormFile file,[FromQuery] string folder = "common"
        )
    {
        try
        {
            var result = await _fileService.UploadAsync(file, folder);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}