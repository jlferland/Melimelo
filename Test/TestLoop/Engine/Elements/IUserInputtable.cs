﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace TestLoop
{
    public interface IUserInputtable
    {
        void ProcessUserInput(KeyboardState kstate, GamePadState gstate);
    }
}
