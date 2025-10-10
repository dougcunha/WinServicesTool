namespace WinServicesTool.Models;

[Flags]
public enum ServiceTypeEx
{
    KernelDriver = 0x1,
    FileSystemDriver = 0x2,
    Adapter = 0x4,
    RecognizerDriver = 0x8,
    Win32OwnProcess = 0x10,
    Win32ShareProcess = 0x20,
    InteractiveProcess = 0x100,
    UserService = 0x40,
    UserServiceInstance = 0x80,
    PackageService = 0x200
}

public static class ServiceTypeHelper
{
    public static string Describe(int rawType)
    {
        var type = (ServiceTypeEx)rawType;
        var parts = new List<string>();

        foreach (ServiceTypeEx flag in Enum.GetValues(typeof(ServiceTypeEx)))
        {
            if (type.HasFlag(flag))
                parts.Add(flag.ToString());
        }

        return parts.Count > 0
            ? string.Join(" + ", parts)
            : $"Unknown (0x{rawType:X})";
    }
}
