using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SortingAssignment1
{
    public static class _
    {
        //https://stackoverflow.com/questions/33538527/display-a-image-in-a-console-application
        [DllImport("kernel32.dll", EntryPoint = "GetConsoleWindow", SetLastError = true)]
        private static extern IntPtr GetConsoleHandle();
        public static void PrintLogo()
        {

            var handler = GetConsoleHandle();

            var img = Properties.Resources.Logo3;

            //for (int i = 0 -( img.Width / 10); i < 0; i+=100)
            //{
            //    using (var graphics = Graphics.FromHwnd(handler))
            //    using (var image = Properties.Resources.Logo3)
            //        graphics.DrawImage(image, i, 0, img.Width / 10, img.Height / 10);

            //}
            using (var graphics = Graphics.FromHwnd(handler))
            using (var image = Properties.Resources.Logo3)
                graphics.DrawImage(image, 0, 0, img.Width / 10, img.Height / 10);

            Thread.Sleep(100);



        }


       

    }
}
