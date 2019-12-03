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
using NanoFormFramework.Util.Api;

namespace TestForm
{
    public partial class Form1 : NanoRoundedForm
    {
        private bool isFormMove = false;
        private Point basePoint;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                basePoint = e.Location;
                isFormMove = true;
            }
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isFormMove)
            {
                this.Location = new Point(e.X + (this.Location.X - basePoint.X), e.Y + (this.Location.Y - basePoint.Y));
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isFormMove = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form2 fm = new Form2();
            fm.Show();
        }
    }
}
