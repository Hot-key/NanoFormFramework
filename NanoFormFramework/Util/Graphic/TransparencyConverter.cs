using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using NanoFormFramework.Util.Api;

namespace NanoFormFramework.Util.Graphic
{
    internal class TransparencyConverter
    {
        internal static void EnableBlur(IntPtr handle, Win32.AccentState state = Win32.AccentState.ACCENT_ENABLE_TRANSPARENTGRADIENT)
        {
            var accent = new Win32.AccentPolicy();
            var accentStructSize = Marshal.SizeOf(accent);
            accent.AccentState = state;

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new Win32.WindowCompositionAttributeData();
            data.Attribute = Win32.WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            Win32.SetWindowCompositionAttribute(handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }
    }
}
