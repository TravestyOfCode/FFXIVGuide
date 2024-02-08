using System.Collections.Generic;

namespace FFXIVGuide.Web.Models.Guide;

public class SidebarViewModel
{
    public Dictionary<int, string> RouletteTypeList { get; set; }

    public Dictionary<int, string> InstanceList { get; set; }
}
