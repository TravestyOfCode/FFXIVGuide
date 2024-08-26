using Microsoft.AspNetCore.Mvc;


namespace FFXIVGuide.Controllers;

public class GuideController : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var model = new ViewModels.Guide.Get.IndexModel();

        return View(model);
    }
}
