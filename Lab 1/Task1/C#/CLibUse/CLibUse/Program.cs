using System.Runtime.InteropServices;

namespace CLibUse;

class Program
{
    [DllImport(@"D:\work\TechLab1\C#\CLibUse\SharpDll.dll")]
    static extern void helloThere();
    
    static void Main(string[] args)
    {
        helloThere();
    }
}