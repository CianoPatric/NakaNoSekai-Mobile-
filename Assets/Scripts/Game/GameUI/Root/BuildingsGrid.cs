using Game.GameRoot;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class BuildingsGrid : MonoBehaviour, IInjectable
{
    public Vector2Int GridSize;
    public int Width;
    public int Height;
    private Building[,] grid;
    public GameObject PrefabOnCard;

    public void Inject(DIContainer container)
    {
        var enterParams = container.Resolve<GameUIEnterParams>();
        Width = enterParams.Width;
        Height = enterParams.Height;
        Debug.Log($"[UIEntryPoint] <UNK> <UNK> <UNK>: {Width} x {Height}");
    }
    public void Generate()
    {
        GridSize = new Vector2Int(Width, Height);
        grid = new Building[GridSize.x, GridSize.y];
        for (int i = 0; i < GridSize.x; i++)
        {
            for (int j = 0; j < GridSize.y; j++)
            {
                
            }
        }
    }
}
