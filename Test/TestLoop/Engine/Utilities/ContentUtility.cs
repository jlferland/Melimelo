using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Xml.Linq;


namespace TestLoop
{
    public class ContentUtility
    {
        private static Game CurrentGame;

        public static void Initialize(Game game)
        {
            CurrentGame = game;
        }

        public static XDocument GetXmlAsset(string assetName)
        {
            return XDocument.Load(TitleContainer.OpenStream(assetName));
        }

    }
}
