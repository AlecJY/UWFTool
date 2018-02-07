using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using log4net;
using log4net.Config;
using UWFTool.UWFController;

namespace UWFTool {
    static class Program {
        [STAThread]
        static void Main() {
            XmlConfigurator.Configure(new FileInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                                                   Path.DirectorySeparatorChar + "log.config"));
            UWFManagement uwfManagement = new UWFManagement();

            bool hiddenMode = uwfManagement.NoPermission() || uwfManagement.UWFCurrentEnabled();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NotificationForm(hiddenMode));
        }
    }
}