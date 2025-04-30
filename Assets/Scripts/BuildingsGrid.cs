using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class BuildingsGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);
    private Building[,] grid;
    public GameObject PrefabOnCard;
    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y];
    }
}
