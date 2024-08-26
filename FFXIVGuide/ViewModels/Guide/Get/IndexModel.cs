namespace FFXIVGuide.ViewModels.Guide.Get;

public class IndexModel
{
    public SidebarModel Sidebar { get; set; } = new SidebarModel();

    public GuideContentModel Content { get; set; } = new GuideContentModel();
}
