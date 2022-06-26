using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LavaManager
{
    public partial class PleaseWaitDialog : Form
    {
        public PleaseWaitDialog()
        {
            InitializeComponent();
        }
        public void ShowReal()
        {
            Show();
            Application.DoEvents();
        }
    }
}
