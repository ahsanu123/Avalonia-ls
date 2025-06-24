namespace Main;
using System.Runtime.InteropServices;

public static class IOCTL_PROVIDER
{
    //i should probably not use unsafe here and wrap with GC but who gives a fuck
    [DllImport("libc")]
    public static extern unsafe int ioctl(int _fd, ulong _request, void* _args);
}
[StructLayout(LayoutKind.Sequential)]
public struct Winsize
{
    public ushort row;
    public ushort col;
    public ushort xpixel;
    public ushort ypixel;
}
