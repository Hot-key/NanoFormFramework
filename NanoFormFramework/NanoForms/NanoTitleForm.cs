using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NanoFormFramework.NanoForms
{
    public class NanoTitleForm : Form
    {
        private Size titleSize = new Size(0,0);

        private Brush titleBrushBlack = new SolidBrush(Color.FromArgb( 27, 27, 27));
        private Brush titleBrushWhite = new SolidBrush(Color.FromArgb(235, 235, 235));
        
        private Pen formButtonsPen = new Pen(Color.FromArgb(235, 235, 235));

        private Rectangle exitButtonBounds;
        private Rectangle minButtonBounds;
        private Rectangle maxButtonBounds;

        public NanoTitleForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            titleSize = new Size(this.Width, 30);
            exitButtonBounds = new Rectangle(Width - 30, 0, 30, 30);
            maxButtonBounds = new Rectangle(Width - 60, 0, 30, 30);
            minButtonBounds = new Rectangle(Width - 90, 0, 30, 30);
            base.OnSizeChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.FillRectangle(titleBrushBlack, 0, 0, titleSize.Width, titleSize.Height);

            g.DrawLine(
                formButtonsPen,
                exitButtonBounds.X + (int)(exitButtonBounds.Width * 0.33),
                exitButtonBounds.Y + (int)(exitButtonBounds.Height * 0.33),
                exitButtonBounds.X + (int)(exitButtonBounds.Width * 0.66),
                exitButtonBounds.Y + (int)(exitButtonBounds.Height * 0.66)
            );
            g.DrawLine(
                formButtonsPen,
                exitButtonBounds.X + (int)(exitButtonBounds.Width * 0.66),
                exitButtonBounds.Y + (int)(exitButtonBounds.Height * 0.33),
                exitButtonBounds.X + (int)(exitButtonBounds.Width * 0.33),
                exitButtonBounds.Y + (int)(exitButtonBounds.Height * 0.66));

            if (WindowState == FormWindowState.Normal)
            {
                g.DrawRectangle(
                    formButtonsPen,
                    maxButtonBounds.X + (int)(maxButtonBounds.Width * 0.33),
                    maxButtonBounds.Y - 1 + (int)(maxButtonBounds.Height * 0.33),
                    (int)(maxButtonBounds.Width * 0.39),
                    (int)(maxButtonBounds.Height * 0.39)
                );
            }
            else if(WindowState == FormWindowState.Maximized)
            {
                g.DrawRectangle(
                    formButtonsPen,
                    maxButtonBounds.X + 2 + (int)(maxButtonBounds.Width * 0.33),
                    maxButtonBounds.Y - 2 + (int)(maxButtonBounds.Height * 0.36),
                    (int)(maxButtonBounds.Width * 0.31),
                    (int)(maxButtonBounds.Height * 0.31)
                );

                g.FillRectangle(
                    titleBrushBlack,
                    maxButtonBounds.X + (int)(maxButtonBounds.Width * 0.33),
                    maxButtonBounds.Y + (int)(maxButtonBounds.Height * 0.36),
                    (int)(maxButtonBounds.Width * 0.31),
                    (int)(maxButtonBounds.Height * 0.31)
                );
                g.DrawRectangle(
                    formButtonsPen,
                    maxButtonBounds.X + (int)(maxButtonBounds.Width * 0.33),
                    maxButtonBounds.Y + (int)(maxButtonBounds.Height * 0.36),
                    (int)(maxButtonBounds.Width * 0.31),
                    (int)(maxButtonBounds.Height * 0.31)
                );
            }

            int x = this.WindowState == FormWindowState.Normal ? minButtonBounds.X : maxButtonBounds.X;
            int y = this.WindowState == FormWindowState.Normal ? minButtonBounds.Y : maxButtonBounds.Y;

            g.DrawLine(
                formButtonsPen,
                x + (int)(minButtonBounds.Width * 0.33),
                y + (int)(minButtonBounds.Height * 0.66),
                x + (int)(minButtonBounds.Width * 0.66),
                y + (int)(minButtonBounds.Height * 0.66)
            );

            float sizeInPoints = this.Font.SizeInPoints / 72 * e.Graphics.DpiX;

            g.DrawString(this.Text,this.Font, titleBrushWhite, 35, (titleSize.Height - sizeInPoints) / 2);

            g.DrawIcon(this.Icon, new Rectangle(5, 5, 20, 20));

            base.OnPaint(e);
        }

        protected override void Dispose(bool disposing)
        {
            titleBrushBlack.Dispose();
            formButtonsPen.Dispose();

            base.Dispose(disposing);
        }
    }
}
