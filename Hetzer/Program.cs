using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace Hetzer
{
    class Program
    {

        [STAThread]
        static void Main(string[] arguments)
        {
            ITwitterCredentials credentials = Login.Instance.getCredentials();
            if (credentials == null) credentials = newCredentials(); ;
            if (credentials == null) return;
            Auth.SetCredentials(credentials);

            //AchiveClean.clean();

            while (true)
            {
                Eraser.CreateEraser(50).StartErase();
                Thread.Sleep(1000 * 60 * 10);
            }

            //var application = new Application
            //{
            //    StartupUri = new Uri("MainWindow.xaml", UriKind.RelativeOrAbsolute)
            //};
            //application.Run();
        }

        private static ITwitterCredentials newCredentials()
        {
            var url = Login.Instance.startAuthFlow();
            Process.Start(url);
            ConsoleManager.Instance.InitConsole();
            var pinCode = Console.ReadLine();
            ConsoleManager.Instance.DestroyConsole();
            return Login.Instance.confirmAuthFlow(pinCode);
        }
    }
}
