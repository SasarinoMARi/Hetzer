using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

class ConsoleManager
{
    private static ConsoleManager instance;
    private bool consoleInitialized = false;

    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole();

    [DllImport("kernel32.dll")]
    private static extern bool AttachConsole(int pid);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool FreeConsole();

    ConsoleManager()
    {
    }

    ~ConsoleManager()
    {
        if (consoleInitialized) FreeConsole();
    }

    public static ConsoleManager Instance
    {
        get
        {
            if (instance == null)
                instance = new ConsoleManager();
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    internal void InitConsole()
    {
        if (!AttachConsole(-1))
            AllocConsole();
        consoleInitialized = true;
    }
}
