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
using NHotkey;
using NHotkey.WindowsForms;
using UWFTool.UWFController;

namespace UWFTool {
    public partial class NotificationForm : Form {
        private bool _hiddenMode;

        public NotificationForm(bool hiddenMode) {
            _hiddenMode = hiddenMode;
            InitializeComponent();
            enableButton.FlatStyle = FlatStyle.System;
            NativeMethods.SendMessage(enableButton.Handle, NativeMethods.BCM_SETSHIELD, 0, (IntPtr) 1);

            Screen screen = Screen.FromControl(this);
            Rectangle area = screen.WorkingArea;
            Location = new Point(area.Width - Size.Width, area.Height - Size.Height);

            if (hiddenMode) {
                SetHotKey();
            }
        }

        private void SetHotKey() {
            HotkeyManager.Current.AddOrReplace("DisableUWF", Keys.U | Keys.Control | Keys.Alt | Keys.Shift,
                OnDisableUWF);
        }

        private void OnDisableUWF(object sender, HotkeyEventArgs e) {
            var result = MessageBox.Show("Do you want to disable UWF and restart computer?", "UWFTool",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                ProcessStartInfo info = new ProcessStartInfo();
                info.UseShellExecute = true;
                info.WorkingDirectory = Environment.CurrentDirectory;
                info.FileName = UWFController.Program.GetProgramLocation();
                info.Arguments = "disable";
                info.Verb = "runas";
                try {
                    Process.Start(info);
                    HotkeyManager.Current.Remove("DisableUWF");
                    Application.Exit();
                } catch (Exception) {
                }
            }

            e.Handled = true;
        }


        protected override void SetVisibleCore(bool value) {
            base.SetVisibleCore(!_hiddenMode & value);
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

        private void NotificationForm_Load(object sender, EventArgs e) {
        }
    }
}