using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UWFTool
{
    static class Program
    {
        [STAThread]
        static void Main() {
            UWFController uwfController = new UWFController();

            if (!uwfController.UWFCurrentEnabled()) {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new NotificationForm(uwfController));
            }
        }
    }
}
