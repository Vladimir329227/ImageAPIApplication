var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/image", async (HttpContext context,int id) =>
{
    var response = context.Response;
    var request = context.Request;

    response.ContentType = "text/html; charset=utf-8";

    IFormFileCollection files = request.Form.Files;
    var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
    Directory.CreateDirectory(uploadPath);

    foreach (var file in files)
    {
        string fullPath = $"{uploadPath}/{file.FileName}";

        using (var fileStream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
    }
    await response.WriteAsync("Файлы успешно загружены");
});

app.MapGet("/image", async (HttpContext context, int id) =>
{
    var response = context.Response;
    var request = context.Request;

    response.ContentType = "text/html; charset=utf-8";

    IFormFileCollection files = request.Form.Files;
    var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
    Directory.CreateDirectory(uploadPath);

    foreach (var file in files)
    {
        string fullPath = $"{uploadPath}/{file.FileName}";

        using (var fileStream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
    }
    await response.WriteAsync("Файлы успешно загружены");
});

app.MapGet("/image", async (HttpContext context) =>
{
    var response = context.Response;
    var request = context.Request;

    response.ContentType = "text/html; charset=utf-8";

    IFormFileCollection files = request.Form.Files;
    var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
    Directory.CreateDirectory(uploadPath);

    foreach (var file in files)
    {
        string fullPath = $"{uploadPath}/{file.FileName}";

        using (var fileStream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
    }
    await response.WriteAsync("Файлы успешно загружены");
});

app.MapPost("/image/new", async (HttpContext context, IConfiguration config) =>
{
    var form = await context.Request.ReadFormAsync();
    var file = form.Files.GetFile("file");

    var storagePath = config.GetValue<string>("StoragePath");

    var path = Path.Combine(Directory.GetCurrentDirectory(), storagePath + "/Pictures");
    if (!Directory.Exists(path))
    {
        Directory.CreateDirectory(path);
    }

    var fileName = Path.GetFileName(file.FileName);
    var filePath = Path.Combine(path, fileName);
    if (System.IO.File.Exists(filePath))
    {
        var extension = Path.GetExtension(fileName);
        var fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
        fileName = $"{fileNameWithoutExt}_{DateTime.UtcNow.Ticks}{extension}";
        filePath = Path.Combine(path, fileName);
    }

    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }
});

app.Run();
