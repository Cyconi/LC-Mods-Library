using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraPlus
{
    internal class StartMsg
    {
        internal static void Msg()
        {
            CLog.L("");
            CLog.L("================== Keybinds ==================");
            CLog.L("Mouse5          | zoom");
            CLog.L("Middle Mouse    | reset FOV");
            CLog.L("Scroll (Zoom)   | adjust zoom FOV");
            CLog.L("Scroll (3rd)    | adjust the camera distance");
            CLog.L("Crtl + T        | cycle camera (1st, 3rd)");
            CLog.L("Arrow Up / Down | adjust FOV for all cameras");
            CLog.L("Will add keybinds later");
            CLog.L("================== Keybinds =================");
            CLog.L("");
        }
    }
}
