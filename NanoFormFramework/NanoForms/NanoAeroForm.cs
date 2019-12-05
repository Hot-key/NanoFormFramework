using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NanoFormFramework.Util.Api;
using NanoFormFramework.Util.Graphic;

namespace NanoFormFramework.NanoForms
{
    public class NanoAeroForm : Form
    {
        public new Color BackColor;

        private Timer drawTimer = new Timer();
        public NanoAeroForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!DesignMode)
            {
                drawTimer.Interval = 1000 / 60;
                drawTimer.Tick += DrawForm;
                drawTimer.Start();
                TransparencyConverter.EnableBlur(this.Handle, Win32.AccentState.ACCENT_ENABLE_BLURBEHIND);
            }
            base.OnLoad(e);
        }

        private void DrawForm(object sender, EventArgs e)
        {
            using (Bitmap backImage = new Bitmap(this.Width, this.Height))
            {
                using (Graphics graphics = Graphics.FromImage(backImage))
                {
                    Rectangle rectangle = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                    using (Brush backColorBrush = new SolidBrush(this.BackColor))
                    {
                        graphics.FillRectangle(backColorBrush, rectangle);
                    }
                    // on paint 는 컨트롤 그리기 전 처리

                    //OnPaint(new PaintEventArgs(graphics,rectangle));

                    foreach (Control ctrl in this.Controls)
                    {
                        using (Bitmap bmp = new Bitmap(ctrl.Width, ctrl.Height))
                        {
                            Rectangle rect = new Rectangle(0, 0, ctrl.Width, ctrl.Height);
                            ctrl.DrawToBitmap(bmp, rect);
                            graphics.DrawImage(bmp, ctrl.Location);
                        }
                    }
                    PerPixelAlphaBlend.SetBitmap(backImage, Left, Top, Handle);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            drawTimer.Stop();
            drawTimer.Dispose();
            base.Dispose(disposing);
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
