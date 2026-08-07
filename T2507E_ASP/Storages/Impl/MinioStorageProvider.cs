using T2507E_ASP.DTOs.Files;

namespace T2507E_ASP.Storages.Impl;

public class MinioStorageProvider : IFileStorageProvider
{
    public string Name { get; }
    public Task<FileUploadResponse> UploadAsync(IFormFile file, string folder)
    {
        throw new NotImplementedException();
    }

    public Task<Stream> DownloadAsync(string storedFileName, string folder)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string storedFileName, string folder)
    {
        throw new NotImplementedException();
    }
}