using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DBMS_FUTSAL_PROJECT
{
    public static class mdiProperties
    {
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewlong);
        [DllImport("user32.dll")]
        private static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_CLIENTEDGE = 0X200;
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_FRAMECHANGED = 0X0020;

        public static bool SetBevel(this Form form, bool show)
        {
            foreach (Control c in form.Controls)
            {
                MdiClient client = c as MdiClient;
                if (client != null)
                {
                    int WindowLong = GetWindowLong(c.Handle, GWL_EXSTYLE);
                    if (show)
                    {
                        WindowLong |= WS_EX_CLIENTEDGE;
                    }
                    else
                    {
                        WindowLong &= ~WS_EX_CLIENTEDGE;
                    }
                    SetWindowLong(c.Handle, GWL_EXSTYLE, WindowLong);

                    // กำหนดตำแหน่งให้อยู่ที่ตำแหน่งเดียวกันเสมอ
                    int fixedX = 100; // กำหนดค่า X ที่ต้องการ
                    int fixedY = 100; // กำหนดค่า Y ที่ต้องการ
                    SetWindowPos(client.Handle, IntPtr.Zero, fixedX, fixedY, 0, 0,
                        SWP_FRAMECHANGED | SWP_NOSIZE | SWP_NOZORDER);
                    return true;
                }
            }
            return false;
        }

    }
}
