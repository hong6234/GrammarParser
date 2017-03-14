using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GrammarParser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override void OnStartup(StartupEventArgs e)
        {
            SetLogger();
            PrintEnvironmentInfo();
            Log.Info("Application Start.");

            base.OnStartup(e);
        }

        private void SetLogger()
        {
            if (File.Exists(Constant.LogConfigFile))
            {
                log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(Constant.LogConfigFile));
            }
        }

        private void PrintEnvironmentInfo()
        {
            Log.Info("--------------------------------------------------------------------------------------");
            Log.Info("OS Version : " + System.Environment.OSVersion);
            Log.Info("CLR Version : " + System.Environment.Version);
            Log.Info("Processor Count : " + System.Environment.ProcessorCount);
            Log.Info("Machine Name : " + System.Environment.MachineName);
            Log.Info("User Name : " + System.Environment.UserName);
            Log.Info("Is 64 Bit Operating System ? " + System.Environment.Is64BitOperatingSystem);
            Log.Info("--------------------------------------------------------------------------------------");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Log.Info("Application Exit.");
        }
    }
}
