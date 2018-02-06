using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UWFTool
{
    public partial class NotificationForm : Form
    {
        public NotificationForm()
        {
            InitializeComponent();
            Screen screen = Screen.FromControl(this);
            Rectangle area = screen.WorkingArea;
            Location = new Point(area.Width - Size.Width, area.Height - Size.Height);
        }

        private void NotificationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
