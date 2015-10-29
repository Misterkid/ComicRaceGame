using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;
using System.Diagnostics;
/*
 * https://social.msdn.microsoft.com/Forums/vstudio/en-US/9003bda2-7edd-47e4-8731-142b03d2d433/visual-c-function-that-occurs-every-frame
 * 
 */ 
namespace RaceGameTest.Q_Engine
{
    class BaseGame
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct Message
        {
            public IntPtr hWnd;//Handle
            public int msg;//message
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public Point p;
        }
        [return: MarshalAs(UnmanagedType.Bool)]
        [SuppressUnmanagedCodeSecurity, DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool PeekMessage(out Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);

        //protected float deltaTime;
        private float prevFrame;
        private float currentFrame;
        private Stopwatch stopWatch = new Stopwatch();
        private int frameCount = 0;
        private float totalDT = 0;

        public void Run(Form form)
        {
            Application.Run(form);
        }

        void Application_Idle(object sender, EventArgs e)
        {
            Message message;
            while (!PeekMessage(out message, IntPtr.Zero, 0, 0, 0))
            {
                UpdateFrame();
                DrawFrame();
            }
        }
        public void Initialize()
        {
            stopWatch.Start();
            Application.Idle += new EventHandler(Application_Idle);
        }
        public virtual void Reset()
        {
            stopWatch.Stop();
            stopWatch.Reset();
            prevFrame = 0;
            currentFrame = 0;
        }
        public virtual void Dispose()
        {
            /*
            stopWatch.Stop();
            stopWatch.Reset();
            prevFrame = 0;
            currentFrame = 0;
            Application.Idle -= new EventHandler(Application_Idle);
             */ 
        }
        protected virtual void UpdateFrame()
        {
            currentFrame = stopWatch.ElapsedMilliseconds;
            QTime.DeltaTime = (currentFrame - prevFrame) / 1000;//Make it into seconds.
            QTime.RunTime = stopWatch.ElapsedMilliseconds / 1000;//How long is the game running in seconds?
            prevFrame = currentFrame;

            frameCount++;
            totalDT += QTime.DeltaTime;
            if (totalDT > 1.0 / 1)
            {
                QTime.FramesPerSeconds = frameCount / totalDT;
                frameCount = 0;
                totalDT -= 1.0f / 1;
                Console.WriteLine(QTime.FramesPerSeconds);
            }

        }
        protected virtual void DrawFrame()
        {
            //ToDo Draw objects here instead of drawing at form1.
        }
    }
}
