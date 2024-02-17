using FFXIVGuide.Web.Data.Result;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FFXIVGuide.Web.Data.Instance.Commands;

public class SaveInstanceImage : IRequest<Result<string>>
{
    public IFormFile ImageFile { get; set; }
}

internal class SaveInstanceImageHandler : IRequestHandler<SaveInstanceImage, Result<string>>
{
    private readonly IWebHostEnvironment _env;

    private readonly ILogger<SaveInstanceImageHandler> _logger;

    public SaveInstanceImageHandler(IWebHostEnvironment env, ILogger<SaveInstanceImageHandler> logger)
    {
        _env = env;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(SaveInstanceImage request, CancellationToken cancellationToken)
    {
        // TODO: Verify header bytes are an image, ensure size is within parameters,
        // scan for virus, create internal name that is not filename.
        // For now, we are trusting the image file being uploaded.
        try
        {
            var path = Path.Combine(_env.WebRootPath, "images", request.ImageFile.FileName);

            var file = File.Create(path);

            await request.ImageFile.CopyToAsync(file, cancellationToken);

            return Result.Ok(path);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<string>();
        }
    }
}
