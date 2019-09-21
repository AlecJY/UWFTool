using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using log4net.Config;

namespace UWFTool.UWFController {
    public class Program {
        public static void Main(String[] args) {
            XmlConfigurator.Configure(new FileInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                                                   Path.DirectorySeparatorChar + "log.config"));       
            if (args.Length == 1) {
                UWFManagement uwfManager = new UWFManagement();
                switch (args[0]) {
                    case "enable":
                        uwfManager.UWFEnable();
                        uwfManager.UWFRestartSystem();
                        break;
                    case "disable":
                        uwfManager.UWFDisable();
                        uwfManager.UWFRestartSystem();
                        break;
                }
            }
        }

        public static String GetProgramLocation() {
            return System.Reflection.Assembly.GetExecutingAssembly().Location;
        }
    }
}