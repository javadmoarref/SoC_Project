using _0_Framework.Application;

namespace ServiceHost.Requirements;

public class FileUploader:IFileUploader
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileUploader(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public string Upload(IFormFile file,string path)
    {
        if (file == null) return "";
        var directoryPath =$"{_webHostEnvironment.WebRootPath}//Images//{path}";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var filePath = $"{directoryPath}//{file.FileName}";
        using (var output=System.IO.File.Create(filePath))
        {
            file.CopyToAsync(output);
        }

        return $"{path}/{file.FileName}";
    }
}