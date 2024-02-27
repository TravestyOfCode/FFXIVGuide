namespace FFXIVGuide.Web.Models.Guide;

public class SidebarViewModel
{
    public Dictionary<int, string> RouletteTypeList { get; set; } = new Dictionary<int, string>();

    public Dictionary<int, string> InstanceList { get; set; } = new Dictionary<int, string>();
}
