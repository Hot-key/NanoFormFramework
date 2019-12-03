using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NanoFormFramework.Util.Graphic;

namespace NanoFormFramework.NanoForms
{
    internal class NanoDrawAbleForm : Form
    {
        public NanoDrawAbleForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            //this.TopMost = true;
            this.ShowInTaskbar = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            using (Bitmap backImage = new Bitmap(this.Width, this.Height))
            {
                using (Graphics graphics = Graphics.FromImage(backImage))
                {
                    Rectangle gradientRectangle = new Rectangle(1, 1, this.Width - 2, this.Height - 2);
                    using (Pen pen = new Pen(Color.Black, 2))
                    {
                        pen.DashStyle = DashStyle.Dash;
                        pen.DashPattern = new [] { 0.5f, 0.5f };

                        graphics.DrawRectangle(pen, gradientRectangle);

                        using (Brush b = new SolidBrush(Color.FromArgb(1,0,0,0)))
                        {
                            //graphics.FillRectangle(b, gradientRectangle);
                        }
                    }
                    PerPixelAlphaBlend.SetBitmap(backImage, Left, Top, Handle);
                }
            }

            base.OnLoad(e);
        }


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!DesignMode)
                {
                    cp.ExStyle |= 0x00080000;   
                }
                return cp;
            }
        }
    }
}
