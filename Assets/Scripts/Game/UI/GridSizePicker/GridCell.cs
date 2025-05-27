using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Root.View
{
    public class GridCell: MonoBehaviour
    {
        [SerializeField] private Image background;

        private int x, y;
        public void SetCoordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void SetHighlighted(bool highlighted)
        {
            background.color = highlighted ? Color.green : Color.white;
        }
    }
}