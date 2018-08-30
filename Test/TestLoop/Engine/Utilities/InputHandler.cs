using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TestLoop
{
    public class InputHandler
    {
        private static KeyboardState kstate = Keyboard.GetState();
        private static GamePadState c1state = GamePad.GetState(PlayerIndex.One);

        public static void UpdateInputs()
        {
            kstate = Keyboard.GetState();
            c1state = GamePad.GetState(PlayerIndex.One);
        }

        public static KeyboardState CurrentKState
        {
            get { return kstate; }
        }

        public static GamePadState CurrentC1State
        {
            get { return c1state; }
        }
    }
}
