using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestLoop
{
    public class GameEnvironment
    {
        private Game gameObject { get; set; }
        private List<InformationArea> registeredInfoArea = new List<InformationArea>();
        private List<GameArea> registeredGameArea = new List<GameArea>();

        public GameEnvironment(Game currentGame)
        {
            gameObject = currentGame;
        }

        public void AddGameArea(params GameArea[] newAreas)
        {
            foreach (GameArea newArea in newAreas)
            {
                registeredGameArea.Add(newArea);

                newArea.Initialize();
            }
        }

        public virtual void Initialize()
        {
            for (int i = registeredGameArea.Count - 1; i >= 0; i--)
            {
                registeredGameArea[i].Update();
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            EventClock.CurrentGameTime = gameTime;
            InputHandler.UpdateInputs();

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
