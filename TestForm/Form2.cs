using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NanoFormFramework.NanoForms;

namespace TestForm
{
    public partial class Form2 : NanoAeroForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(50, 25, 25, 25);
        }
    }
}
