using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    class KeyInputManager
    {
        //key => keyboarkey [up , s , w , ..]
        //value => bool true if pressed , false if not
        static Dictionary<Keys, bool> mykeys
            = new Dictionary<Keys, bool>();

        public static void setKeyState(Keys k, bool state)
        {
            mykeys[k] = state;
        }

        public static bool getKeyState(Keys k)
        {
            if (mykeys.ContainsKey(k) == false)
                return false;

            return mykeys[k];

        }
    }
}
