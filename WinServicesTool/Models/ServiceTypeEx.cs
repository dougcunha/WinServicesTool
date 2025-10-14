namespace WinServicesTool.Models;

[Flags]
public enum ServiceTypeEx
{
    KernelDriver = 0x1,
    FileSystemDriver = 0x2,
    Adapter = 0x4,
    RecognizerDriver = 0x8,
    OwnProc = 0x10,
    ShareProc = 0x20,
    Interactive = 0x100,
    User = 0x40,
    UserInst = 0x80,
    Package = 0x200
}

public static class ServiceTypeHelper
{
    public static string Describe(this ServiceTypeEx rawType)
    {
        var parts = (from flag in Enum.GetValues<ServiceTypeEx>()
            where rawType.HasFlag(flag)
            select flag.ToString()).ToList();

        return parts.Count > 0
            ? string.Join(" + ", parts)
            : $"Unknown (0x{rawType:X})";
    }
}
