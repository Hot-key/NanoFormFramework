using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NanoFormFramework.Util.Graphic;

namespace NanoFormFramework.NanoForms
{
    [DefaultEvent("Load")]
    public class NanoRoundedForm : Form
    {
        private Timer drawTimer = new Timer();

        private bool isGradient;

        private Color startColor;
        private Color endColor;
        private float angle;
        private int cornerRadius;

        private Brush backColorBrush;

        [Category("Gradient")]
        [Description("Paintin form use the gradient color")]
        [DefaultValue(false)]
        public bool UseGradient
        {
            get => isGradient;
            set => isGradient = value;
        }

        [Category("Gradient")]
        [Description("Set gradient start color")]
        [DefaultValue(typeof(Color), "DarkSlateBlue")]
        public Color StartColor
        {
            get => startColor;
            set
            {
                isGradient = true;
                startColor = value;
            }
        }

        [Category("Gradient")]
        [Description("Set gradient end color")]
        [DefaultValue(typeof(Color), "MediumPurple")]
        public Color EndColor
        {
            get => endColor;
            set
            {
                isGradient = true;
                endColor = value;
            }
        }

        [Category("Gradient")]
        [Description("Set gradient angle")]
        [DefaultValue(0.0f)]
        public float Angle
        {
            get => angle;
            set
            {
                isGradient = true;
                angle = value;
            }
        }

        [Category("Appearance")]
        [Description("Set corner radius")]
        [DefaultValue(15)]
        public int CornerRadius
        {
            get => cornerRadius;
            set
            {
                cornerRadius = value;
            }
        }

        public NanoRoundedForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
        }

        protected override void OnLoad(EventArgs e)
        {
            backColorBrush = new SolidBrush(this.BackColor);
            if (!DesignMode)
            {
                drawTimer.Interval = 1000 / 60;
                drawTimer.Tick += DrawForm;
                drawTimer.Start();
            }
            base.OnLoad(e);
        }

        private void DrawForm(object pSender, EventArgs pE)
        {
            using (Bitmap backImage = new Bitmap(this.Width, this.Height))
            {
                using (Graphics graphics = Graphics.FromImage(backImage))
                {
                    Rectangle gradientRectangle = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                    Brush b;
                    if (isGradient)
                    {
                        b = new LinearGradientBrush(gradientRectangle, startColor, endColor, angle);
                    }
                    else
                    {
                        b = new SolidBrush(BackColor);
                    }

                    graphics.SmoothingMode = SmoothingMode.HighQuality;

                    RoundedRectangle.FillRoundedRectangle(graphics, b, gradientRectangle, cornerRadius);

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

                    b.Dispose();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            drawTimer.Stop();
            drawTimer.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (DesignMode)
            {
                Graphics graphics = e.Graphics;
                Rectangle gradientRectangle = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                Brush b;
                Brush b1 = new SolidBrush(Color.White);
                if (isGradient)
                {
                    b = new LinearGradientBrush(gradientRectangle, startColor, endColor, angle);
                }
                else
                {
                    b = new SolidBrush(BackColor);
                }

                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.FillRectangle(b1, gradientRectangle);
                RoundedRectangle.FillRoundedRectangle(graphics, b, gradientRectangle, cornerRadius);
                //MessageBox.Show(cornerRadius.ToString());
                b.Dispose();
            }

            base.OnPaint(e);
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
