using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TiledSharp;

namespace TestLoop
{
    public class Map
    {
        public TmxMap map;

        public Map(string assetName)
        {
            Load(assetName);
        }

        private void Load(string assetName)
        {
            XDocument xDoc = ContentUtility.GetXmlAsset(assetName);
            map = new TmxMap(xDoc, assetName.Substring(0, assetName.LastIndexOf("\\")));
        }
    }

}
