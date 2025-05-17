using Game.GameRoot;
using UnityEngine;

public class BuildingsGrid : MonoBehaviour, IInjectable
{
    public Vector2Int GridSize;
    public int Width;
    public int Height;
    private Building[,] grid;
    public GameObject PrefabY;

    public void Inject(DIContainer container)
    {
        var enterParams = container.Resolve<GameUIEnterParams>();
        Width = enterParams.Width;
        Height = enterParams.Height;
        GridSize = new Vector2Int(Width, Height);
        grid = new Building[Width, Height];
        Generate();
    }
    private void Generate()
    {
        grid = new Building[GridSize.x, GridSize.y];
        for (int i = 0; i < GridSize.x; i++)
        {
            for (int j = 0; j < GridSize.y; j++)
            {
                Instantiate(PrefabY, new Vector3(i, 0, j), Quaternion.identity);
            }
        }
    }
}
