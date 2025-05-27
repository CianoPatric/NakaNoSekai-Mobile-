using UnityEngine;

namespace Game.GameUI.Root
{
    public class Build: MonoBehaviour
    {
        public Vector2Int buildSize = Vector2Int.one;
        private void OnDrawGizmos()
        {
            for(int x = 0; x < buildSize.x; x++)
            {
                for(int y = 0; y < buildSize.y; y++)
                {
                    Gizmos.color = new Color(1f, 0f, 1f, 0.30f);
                    Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, 0.1f, 1));
                }
            }
        }
    }
}