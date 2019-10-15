using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube_V11.Entities
{
    public class LayersList
    {
        private List<List<Node>> layers;

        public LayersList()
        {
            layers = new List<List<Node>>();
        }

        public void AddLayer(List<Node> layer)
        {
            layers.Add(layer);
        }

        public List<Node> GetLayer(int id)
        {
            return layers[id];
        }

        public int GetLayersCount()
        {
            return layers.Count();
        }

        public void ClearLayers()
        {
            layers.Clear();
        }
    }
}
