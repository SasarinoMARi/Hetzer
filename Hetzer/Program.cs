using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Tweetinvi;
using Tweetinvi.Parameters;

namespace Hetzer
{
    class Program
    {
        const string ct = "";
        const string cs = "";
        const string ut = "";
        const string us = "";

        [STAThread]
        static void Main(string[] arguments)
        {
            Auth.SetUserCredentials(ct, cs, ut, us);
            while(true)
            {
                Thread.Sleep(1000 * 60 * 10);
                Eraser.CreateEraser().StartErase();
            }

            //var application = new Application
            //{
            //    StartupUri = new Uri("MainWindow.xaml", UriKind.RelativeOrAbsolute)
            //};
            //application.Run();
        }
    }
}
