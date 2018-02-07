using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UWFTool.UWFController;

namespace UWFTool
{
    public partial class NotificationForm : Form {
        private UWFManagement _uwfManagement;
        public NotificationForm(UWFManagement uwfManagement) {
            _uwfManagement = uwfManagement;
            InitializeComponent();
            enableButton.FlatStyle = FlatStyle.System;
            NativeMethods.SendMessage(enableButton.Handle, NativeMethods.BCM_SETSHIELD, 0, (IntPtr)1);
            
            Screen screen = Screen.FromControl(this);
            Rectangle area = screen.WorkingArea;
            Location = new Point(area.Width - Size.Width, area.Height - Size.Height);
        }

        private void enableButton_Click(object sender, EventArgs e) {
            Visible = false;
            ProcessStartInfo info = new ProcessStartInfo();
            info.UseShellExecute = true;
            info.WorkingDirectory = Environment.CurrentDirectory;
            info.FileName = UWFController.Program.GetProgramLocation();
            info.Arguments = "enable";
            info.Verb = "runas";
            try {
                Process.Start(info);
            } catch (Exception) {
                Visible = true;
                return;
            }
            Application.Exit();
        }

        private void NotificationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
