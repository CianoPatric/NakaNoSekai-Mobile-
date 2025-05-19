using Game.GameRoot;
using Game.GameUI.Root;
using UnityEngine;

public class BuildingsGrid : MonoBehaviour, IInjectable
{
    public Vector2Int GridSize;
    public int Width;
    public int Height;
    public Build[,] grid;
    public GameObject PrefabY;

    public void Inject(DIContainer container)
    {
        var enterParams = container.Resolve<GameUIEnterParams>();
        Width = enterParams.Width;
        Height = enterParams.Height;
        GridSize = new Vector2Int(Width, Height);
        Generate();
    }
    public static BuildingsGrid Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Generate()
    {
        grid = new Build[GridSize.x, GridSize.y];
        for (int i = 0; i < GridSize.x; i++)
        {
            for (int j = 0; j < GridSize.y; j++)
            {
                Instantiate(PrefabY, new Vector3(i, 0, j), Quaternion.identity);
            }
        }
    }
}
