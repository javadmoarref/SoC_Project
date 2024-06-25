using _0_Framework.Application;

namespace ServiceHost.Requirements;

public class FileUploader:IFileUploader
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileUploader(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }


    //public string Upload(IFormFile file,string path)
    //{
    //    if (file == null) return "";
    //    var directoryPath =$"{_webHostEnvironment.WebRootPath}//Photo//{path}";
    //    if (!Directory.Exists(directoryPath))
    //    {
    //        Directory.CreateDirectory(directoryPath);
    //    }

    //    var filePath = $"{directoryPath}//{file.FileName}";
    //    using (var output=System.IO.File.Create(filePath))
    //    {
    //        file.CopyToAsync(output);
    //    }

    //    return $"{path}//{file.FileName}";
    //}

    public string Upload(IFormFile file, string path)
    {
        if (file == null) return "";
        var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
        uploads = Path.Combine(uploads, path);
        if (!Directory.Exists(uploads))
        {
            Directory.CreateDirectory(uploads);
        }

        var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";
        if (file.Length > 0)
        {
            using (var fileStream=new FileStream(Path.Combine(uploads,fileName),FileMode.Create))
            { 
               file.CopyTo(fileStream);
            }
        }
        return  Path.Combine(path,fileName);
    }
}