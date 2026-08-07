using T2507E_ASP.DTOs.Files;

namespace T2507E_ASP.Storages.Impl;

public class LocalStorageProvider : IFileStorageProvider
{
    private readonly IWebHostEnvironment _env;
    public LocalStorageProvider(IWebHostEnvironment env)
    {
        _env = env;
    }
    public string Name => "Local";
    public async Task<FileUploadResponse> UploadAsync(IFormFile file, string folder)
    {
        var extension = Path.GetExtension(file.FileName);
        var fileId = Guid.NewGuid().ToString();
        var storedFileName = fileId + extension;
        var rootPath = Path.Combine(_env.ContentRootPath, "wwwroot");
        var path = Path.Combine(rootPath, "uploads", folder);
        Directory.CreateDirectory(path); // tạo thư mục để upload file
        var physicalPath = Path.Combine(path, storedFileName);
        await using var stream = 
                new FileStream(physicalPath, FileMode.Create);
        await file.CopyToAsync(stream);
        return new FileUploadResponse
        {
            FileId = fileId,
            StoredFileName = storedFileName,
            OriginalFileName = file.FileName,
            ContentType = file.ContentType,
            Size = file.Length,
            Url = $"/uploads/{folder}/{storedFileName}",
            Provider = Name
        };
    }

    public Task<Stream> DownloadAsync(string storedFileName, string folder)
    {
        var rootPath = _env.WebRootPath;
        var physicalPath = Path.Combine(rootPath,"uploads",
                            folder, storedFileName);
        Stream stream = new FileStream(physicalPath, 
                                    FileMode.Open, FileAccess.Read);
        return Task.FromResult(stream);
    }

    public Task<bool> DeleteAsync(string storedFileName, string folder)
    {
        var rootPath = _env.WebRootPath;
        var physicalPath = Path.Combine(rootPath, "uploads", 
                            folder, storedFileName);
        if (File.Exists(physicalPath))
        {
            File.Delete(physicalPath);
        }
        return Task.FromResult(true);
    }
}