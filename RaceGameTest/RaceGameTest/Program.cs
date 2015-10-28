using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using RaceGameTest.Q_Engine;

using System.Runtime.InteropServices;
namespace RaceGameTest
{
    public static class Program
    {
#if __DEBUG_MODE
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
#endif
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if __DEBUG_MODE
            AllocConsole();
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Game _game = new Game();
            //_game.Run(new Form1(_game));
            _game.Run(new Menu(_game));
        }
    }
}
