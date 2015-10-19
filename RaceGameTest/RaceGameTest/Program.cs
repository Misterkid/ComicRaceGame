using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using RaceGameTest.Q_Engine;
namespace RaceGameTest
{
    public static class Program //: BaseGame
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Game _game = new Game();
            _game.Run(new Form1(_game));
            //Program program = new Program();
            //BaseGame test = new BaseGame();
            //test.Run(new Form1());
            //this.Run(new Form1());
            //MyGameProgram program = new MyGameProgram();
            //program.Run(new Form1());
            //Application.Run(new Form1());
        }
    }
}
