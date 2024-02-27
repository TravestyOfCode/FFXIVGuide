using FFXIVGuide.Web.Data.Instance;

namespace FFXIVGuide.Web.Models.Guide;

public class IndexViewModel
{
    public SidebarViewModel Sidebar { get; set; } = new SidebarViewModel();

    public InstanceModel InstanceDetails { get; set; }
}
