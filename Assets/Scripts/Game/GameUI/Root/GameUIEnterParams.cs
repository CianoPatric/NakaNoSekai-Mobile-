using Game.GameRoot;
using UnityEngine;

public class GameUIEnterParams: SceneEnterParams
{
    public int Width { get; }
    public int Height { get; }

    public GameUIEnterParams(int width, int height) : base("GameUI")
    {
        Width = width;
        Height = height;
    }
}
