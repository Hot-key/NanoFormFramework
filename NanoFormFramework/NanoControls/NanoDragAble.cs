using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NanoFormFramework.NanoForms;
using NanoFormFramework.Util.Api;
using NanoFormFramework.Util.Graphic;

namespace NanoFormFramework.NanoControls
{
    public class NanoDragAble : Component
    {
        private Control targetControl;
        private Form targetForm;

        private bool isFastDrag;

        [Category("기타")]
        [Description("NanoAeroForm에서 Aero 효과를 비활성화 하여 빠른 드래그를 활성화 합니다")]
        [DefaultValue(false)]
        public bool FastDrag
        {
            get => isFastDrag;
            set => isFastDrag = value;
        }

        public Control Control
        {
            get => targetControl;
            set
            {
                targetControl = value;
                if (!DesignMode && targetControl != null)
                {
                    targetControl.MouseDown += Control_MouseDown;
                }
            }
        }

        private void Control_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (targetForm != null)
                {
                    if (isFastDrag)
                    {
                        TransparencyConverter.EnableBlur(targetForm.Handle, Win32.AccentState.ACCENT_DISABLED);
                        Win32.ReleaseCapture();
                        Win32.SendMessage(targetForm.Handle, Win32.WM_NCLBUTTONDOWN, Win32.HT_CAPTION, 0);
                        TransparencyConverter.EnableBlur(targetForm.Handle, Win32.AccentState.ACCENT_ENABLE_BLURBEHIND);
                    }
                    else
                    {
                        Win32.ReleaseCapture();
                        Win32.SendMessage(targetForm.Handle, Win32.WM_NCLBUTTONDOWN, Win32.HT_CAPTION, 0);
                    }
                }
                else
                {
                    targetForm = this.targetControl.FindForm();
                    Control_MouseDown(sender, e);
                }
            }
        }
    }
}
