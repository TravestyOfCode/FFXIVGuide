using Microsoft.AspNetCore.Http;

namespace FFXIVGuide.Web.Utilities;

public static class ViewUtils
{
    public static bool IsHXRequest(this HttpRequest request) => request.Headers.ContainsKey("hx-request");
}

public static class ViewNames
{
    public static readonly string DataList = "DataList";
    public static readonly string Index = "Index";
    public static readonly string EditingRow = "EditingRow";
    public static readonly string EditableRow = "EditableRow";
}