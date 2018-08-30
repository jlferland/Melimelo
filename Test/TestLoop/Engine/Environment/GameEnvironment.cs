﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestLoop
{
    public class GameEnvironment
    {
        private Game gameObject;
        private List<InformationArea> registeredInfoArea = new List<InformationArea>();
        private List<GameArea> registeredGameArea = new List<GameArea>();

        public GameEnvironment(Game currentGame)
        {
            gameObject = currentGame;
        }

        public void AddGameArea(GameArea newArea)
        {
            registeredGameArea.Add(newArea);

            newArea.Initialize();
        }

        public virtual void Initialize()
        {
            for (int i = registeredGameArea.Count - 1; i >= 0; i--)
            {
                registeredGameArea[i].Update();
            }
        }

        public virtual void Update()
        {
            for (int i = registeredGameArea.Count - 1; i >= 0; i--)
            {
                registeredGameArea[i].Update();
            }
        }

        public virtual void ClearUnmanagedContent()
        {
            GraphicsUtility.ClearUnmanagedContent();
        }
    }
}
