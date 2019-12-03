using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NanoFormFramework.NanoControls
{
    [DefaultEvent("Click")]
    public class NanoNavigationButton : UserControl
    {
        private bool isSelected = false;

        private Color activeColor = Color.FromArgb(117, 117, 117);
        private Color defaultColor = Color.FromArgb(66, 66, 66);
        private Color foreColor = Color.White;

        private Font font = new Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        private int textOffset = 15;

        private Image iconData; 
        private Size imageSize;

        private string text;

        #region Property 


        [
            Category("모양"),
            Description("버튼에 표시되는 문자열을 지정합니다."),
        ]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => text;
            set => text = value;
        }

        [
            Category("NanoSetting"),
            Description("버튼에 표시되는 아이콘를 지정합니다."),
        ]
        public Image Icon
        {
            get => iconData;
            set
            {
                Invalidate();
                iconData = value;
            }
        }

        [
            Category("NanoSetting"),
            Description("버튼의 활성화 여부를 설정합니다."),
        ]
        [DefaultValue(false)]
        public bool IsSelected
        {
            get => isSelected;
            set => isSelected = value;
        }


        [
            Category("NanoSetting"),
            Description("버튼의 활성화 색상을 지정합니다."),
        ]
        [DefaultValue(typeof(Color), "117, 117, 11")]
        public Color ActiveColor
        {
            get => activeColor;
            set => activeColor = value;
        }


        [
            Category("NanoSetting"),
            Description("버튼의 기본 색상을 지정합니다."),
        ]
        [DefaultValue(typeof(Color), "66, 66, 66")]
        public Color DefaultColor
        {
            get => defaultColor;
            set => defaultColor = value;
        }

        [
            Category("NanoSetting"),
            Description("버튼의 기본 폰트를 지정합니다."),
        ]
        [DefaultValue(typeof(Font), "Segoe UI Semibold, 9.75pt, style=Regular")]
        public new Font Font
        {
            get => font;
            set => font = value;
        }

        [
            Category("NanoSetting"),
            Description("버튼의 기본 색상을 지정합니다."),
        ]
        [DefaultValue(typeof(Color), "White")]
        public new Color ForeColor
        {
            get => foreColor;
            set => foreColor = value;
        }

        [
            Category("NanoSetting"),
            Description("버튼의 기본 색상을 지정합니다."),
        ]
        [DefaultValue(15)]
        public int TextOffset
        {
            get => textOffset;
            set => textOffset = value;
        }

        #endregion


        public NanoNavigationButton()
        {
        }

        private void ClickedButton()
        {
            foreach (Control parentControl in Parent.Controls)
            {
                if (parentControl is NanoNavigationButton parentButton)
                {
                    parentButton.isSelected = false;
                    parentButton.BackColor = parentButton.isSelected ? parentButton.activeColor : parentButton.defaultColor;
                }
            }

            this.isSelected = true;
            this.BackColor = isSelected ? activeColor : defaultColor;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.BackColor = isSelected ? activeColor : defaultColor;
            base.OnLoad(e);
        }

        protected override void OnClick(EventArgs e)
        {
            ClickedButton();
            base.OnClick(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (iconData != null)
            {
                double ratioX = (this.Height - 16) / (double)iconData.Width;
                double ratioY = (this.Height - 16) / (double)iconData.Height;

                double ratio = Math.Min(ratioX, ratioY);

                int newWidth = (int)(iconData.Width * ratio);
                int newHeight = (int)(iconData.Height * ratio);

                imageSize = new Size(newWidth, newHeight);
            }
            base.OnSizeChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            float sizeInPoints = this.font.SizeInPoints / 72 * e.Graphics.DpiX;

            if (iconData != null)
            {
                g.DrawImage(iconData, 8, (this.Height - imageSize.Height) / 2, imageSize.Width, imageSize.Height);
            }
            

            g.DrawString(text, font, new SolidBrush(foreColor), Math.Max(imageSize.Width, imageSize.Height) + 13 + textOffset, (Height - sizeInPoints) / 2 - 3);

            base.OnPaint(e);
        }
    }
}
