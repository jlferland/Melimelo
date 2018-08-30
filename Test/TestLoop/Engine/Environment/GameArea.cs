using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoop
{
    public class GameArea
    {
        private List<GameObject> registeredObjects = new List<GameObject>();
        private List<GameObject> activeObjects = new List<GameObject>();

        public void AddGameObject(GameObject newObject)
        {
            // Init object
            newObject.Initialize();

            // Populate collections
            registeredObjects.Add(newObject);            

            if (newObject.Active)
            {
                activeObjects.Add(newObject);
            }
        }

        public virtual void Initialize()
        {

        }

        public virtual void Update()
        {
            for (int i = activeObjects.Count - 1; i >= 0; i--)
            {
                activeObjects[i].Update();
            }
        }
    }
}
