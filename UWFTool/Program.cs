using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UWFTool.UWFController;

namespace UWFTool
{
    static class Program
    {
        [STAThread]
        static void Main() {
            UWFManagement uwfManagement = new UWFManagement();

            if (!uwfManagement.UWFCurrentEnabled()) {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new NotificationForm(uwfManagement));
            }
        }
    }
}
