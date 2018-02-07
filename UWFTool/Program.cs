using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using log4net;
using log4net.Config;
using UWFTool.UWFController;

namespace UWFTool
{
    static class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger("UWFTool");
        [STAThread]
        static void Main() {
            XmlConfigurator.Configure(new FileInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "log.config"));
            UWFManagement uwfManagement = new UWFManagement();

            if (!uwfManagement.NoPermission() && !uwfManagement.UWFCurrentEnabled()) {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new NotificationForm(uwfManagement));
            }
        }
    }
}
